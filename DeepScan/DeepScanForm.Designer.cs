namespace DeepScan
{
    partial class DeepScanForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnDirectory = new Button();
            dllTreeView = new TreeView();
            progressBar1 = new ProgressBar();
            statusTexbox = new TextBox();
            lblDirectory = new Label();
            btnAbort = new Button();
            SuspendLayout();
            // 
            // btnDirectory
            // 
            btnDirectory.Location = new Point(12, 12);
            btnDirectory.Name = "btnDirectory";
            btnDirectory.Size = new Size(235, 23);
            btnDirectory.TabIndex = 0;
            btnDirectory.Text = "Select Directory";
            btnDirectory.UseVisualStyleBackColor = true;
            btnDirectory.Click += btnDirectory_Click;
            // 
            // dllTreeView
            // 
            dllTreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dllTreeView.Location = new Point(12, 66);
            dllTreeView.Name = "dllTreeView";
            dllTreeView.Size = new Size(776, 372);
            dllTreeView.TabIndex = 1;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 37);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(360, 23);
            progressBar1.TabIndex = 2;
            // 
            // statusTexbox
            // 
            statusTexbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusTexbox.Location = new Point(397, 37);
            statusTexbox.Name = "statusTexbox";
            statusTexbox.ReadOnly = true;
            statusTexbox.Size = new Size(391, 23);
            statusTexbox.TabIndex = 3;
            // 
            // lblDirectory
            // 
            lblDirectory.AutoSize = true;
            lblDirectory.Location = new Point(277, 13);
            lblDirectory.Name = "lblDirectory";
            lblDirectory.Size = new Size(16, 15);
            lblDirectory.TabIndex = 4;
            lblDirectory.Text = "...";
            // 
            // btnAbort
            // 
            btnAbort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAbort.Location = new Point(713, 13);
            btnAbort.Name = "btnAbort";
            btnAbort.Size = new Size(75, 23);
            btnAbort.TabIndex = 5;
            btnAbort.Text = "Abort";
            btnAbort.UseVisualStyleBackColor = true;
            btnAbort.Click += btnAbort_Click;
            // 
            // DeepScanForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAbort);
            Controls.Add(lblDirectory);
            Controls.Add(statusTexbox);
            Controls.Add(progressBar1);
            Controls.Add(dllTreeView);
            Controls.Add(btnDirectory);
            Name = "DeepScanForm";
            Text = "DeepScan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDirectory;
        private TreeView dllTreeView;
        private ProgressBar progressBar1;
        private TextBox statusTexbox;
        private Label lblDirectory;
        private Button btnAbort;
    }
}
