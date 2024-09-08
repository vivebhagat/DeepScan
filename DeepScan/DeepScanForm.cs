namespace DeepScan
{
    public partial class DeepScanForm : Form
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public DeepScanForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private async void btnDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the directory to scan for DLL files";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected directory path
                    string selectedPath = dialog.SelectedPath;
                    this.dllTreeView.Nodes.Clear();
                    this.lblDirectory.Text = selectedPath;
                    var progress = new Progress<Tuple<int, string>>(value =>
                    {
                        this.progressBar1.Value = value.Item1;
                        statusTexbox.Text = $"Dll processed: {value.Item2}";
                    });
                    var node = await Task.Run(() => DLLAnalyzer.AnalyzeDependencies(selectedPath, progress, this.tokenSource.Token), this.tokenSource.Token);  //AnalyzeDependencies(dialog.SelectedPath)
                    LoadTree(node);
                }
            }
        }

        private void LoadTree(Node rootNode)
        {
            this.dllTreeView.Nodes.Clear();
            TreeNode rootTreeNode = CreateTreeNode(rootNode);
            this.dllTreeView.Nodes.Add(rootTreeNode);
        }

        private TreeNode CreateTreeNode(Node node)
        {
            if (node == null)
                return null;

            TreeNode treeNode = new TreeNode(node.Name);

            foreach (var child in node.Children)
            {
                if (child != null)
                    treeNode.Nodes.Add(CreateTreeNode(child));
            }

            return treeNode;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            this.tokenSource.Cancel();
            MessageBox.Show("Task canceled.");
        }
    }
}
