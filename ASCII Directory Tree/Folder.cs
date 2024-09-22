using System.Text;

namespace ASCII_Directory_Tree;

public class Folder
{
    private string Name { get; set; }
    private string Path { get; set; }
    private bool IsRoot { get; set; }
    private int Depth { get; set; }
    private List<Folder> Children { get; set; }
    private List<string> Files { get; set; }
    private int TotalContent { get; set; }
    private List<string> Structure { get; set; }
    public string Tree
    {
        get
        {
            StringBuilder tree = new();
            foreach (var node in Structure)
            {
                tree.AppendLine(node);
            }

            return tree.ToString();
        } 
    }

    public Folder(string path)
    {
        Name = path.TrimEnd('\\').Split('\\').Last();
        Path = path;
        IsRoot = true;
        Depth = 0;
        Children = GetChildren();
        Files = GetFiles();
        TotalContent = GetTotalContent();
        Structure = GenerateStructure();
    }

    private Folder(string path, bool root, int depth)
    {
        Name = path.Split('\\').Last();
        Path = path;
        IsRoot = root;
        Depth = IsRoot ? 0 : depth + 1;
        Children = GetChildren();
        Files = GetFiles();
        TotalContent = GetTotalContent();
        Structure = GenerateStructure();
    }

    private List<Folder> GetChildren()
    {
        List<string> paths = [.. Directory.GetDirectories(Path)];

        List<Folder> children = [];

        foreach (var path in paths)
        {
            children.Add(new Folder(path, false, Depth));
        }

        return children;
    }

    private List<string> GetFiles()
    {
        List<string> files = [.. Directory.GetFiles(Path)];

        List<string> fileNames = [];

        foreach (var file in files)
        {
            fileNames.Add(file.Split('\\').Last());
        }

        return fileNames;
    }

    private int GetTotalContent()
    {
        int total = 0;
        total += Children.Count;
        total += Files.Count;

        foreach (var child in Children)
        {
            total += child.TotalContent;
        }

        return total;
    }

    private List<string> GenerateStructure()
    {
        List<string> tree = [];

        if (IsRoot)
        {
            tree.Add($"~/{Name}/");
        }

        // Mapping files
        for (int i = 0; i < Files.Count; i++)
        {
            if (i != Files.Count - 1 || Children.Count > 0)
            {
                tree.Add($"├── {Files[i]}");
            }
            else
            {
                tree.Add($"└── {Files[i]}");
            }
        }

        var emptyDirs = Children
            .Where(x => x.Children.Count == 0 && x.Files.Count == 0)
            .ToList();

        var fullDirs = Children.Except(emptyDirs).ToList();

        // Mapping empty directories
        for (int i = 0; i < emptyDirs.Count; i++)
        {
            if (i != emptyDirs.Count - 1 || fullDirs.Count > 0)
            {
                tree.Add($"├── {emptyDirs[i].Name}/");
            }
            else
            {
                tree.Add($"└── {emptyDirs[i].Name}/");
            }
        }

        // Mapping directories with content
        for (int i = 0; i < fullDirs.Count; i++)
        {
            if (i != fullDirs.Count - 1)
            {
                tree.Add($"├── {fullDirs[i].Name}/");
            }
            else
            {
                tree.Add($"└── {fullDirs[i].Name}/");
            }

            foreach (var node in fullDirs[i].Structure)
            {
                if (i != fullDirs.Count - 1)
                {
                    tree.Add($"│   {node}");
                }
                else
                {
                    tree.Add($"    {node}");
                }
            }
        }

        return tree;
    }
}
