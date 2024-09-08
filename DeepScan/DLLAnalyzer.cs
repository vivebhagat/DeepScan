using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DeepScan
{
    internal class DLLAnalyzer
    {
        public static Node AnalyzeDependencies(string directoryPath, IProgress<Tuple<int, string>> progress, CancellationToken cancellationToken)
        {
            Dictionary<string, Node> processedAssemblies = new Dictionary<string, Node>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, string[]> versionCache = new Dictionary<string, string[]>();

            string[] dllFiles = Directory.GetFiles(directoryPath, "*.dll", SearchOption.AllDirectories);

            // Create the root node
            Node root = new Node(directoryPath);
            int totalFiles = dllFiles.Length;
            int processedFiles = 0;

            // Analyze each .dll file
            foreach (string dllFile in dllFiles)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancelled.");
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Node dllNode = ProcessAssembly(dllFile, processedAssemblies, versionCache, cancellationToken);
                if (dllNode != null)
                {
                    root.Children.Add(dllNode);
                }
                processedFiles++;
                progress?.Report(new Tuple<int, string>((processedFiles * 100) / totalFiles, dllFile));
            }
            return root;
        }

        private static Node ProcessAssembly(string dllPath, Dictionary<string, Node> processedAssemblies, Dictionary<string, string[]> versionCache, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Task cancelled.");
                cancellationToken.ThrowIfCancellationRequested();
            }
            try
            {
                // Avoid processing the same assembly multiple times
                if (processedAssemblies.ContainsKey(dllPath))
                {
                    return processedAssemblies[dllPath];
                }

                // Load the assembly
                Assembly assembly = Assembly.LoadFrom(dllPath);
                string fileVersion = "-";
                string productVersion = "-";
                if (versionCache.ContainsKey(assembly.Location))
                {
                    fileVersion = versionCache[assembly.Location][0];
                    productVersion = versionCache[assembly.Location][1];
                }
                else
                {
                    fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
                    productVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
                    versionCache.Add(assembly.Location, new string[] { fileVersion, productVersion });
                }

                Node assemblyNode = new Node($"{Path.GetFileName(dllPath)} File Ver: {fileVersion} Product ver: {productVersion} (Path: {assembly.Location})");

                // Add the node to the processed assemblies dictionary
                processedAssemblies[dllPath] = assemblyNode;

                // Get referenced assemblies
                AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();

                foreach (AssemblyName referencedAssembly in referencedAssemblies)
                {
                    string referencedDllPath = Path.Combine(Path.GetDirectoryName(dllPath), referencedAssembly.Name + ".dll");

                    if (File.Exists(referencedDllPath))
                    {
                        Node childNode = ProcessAssembly(referencedDllPath, processedAssemblies, versionCache, cancellationToken);
                        assemblyNode.Children.Add(childNode);
                    }
                }

                return assemblyNode;
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                    throw;
                Console.WriteLine($"Failed to load assembly {dllPath}: {ex.Message}");
                return null;
            }
        }

    }
}
