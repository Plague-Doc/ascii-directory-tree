using ASCII_Directory_Tree;
using System.Text;

const string art = """
                _____  _____ _____ _____     _______ _____  ______ ______ 
         /\    / ____|/ ____|_   _|_   _|   |__   __|  __ \|  ____|  ____|
        /  \  | (___ | |      | |   | |        | |  | |__) | |__  | |__   
       / /\ \  \___ \| |      | |   | |        | |  |  _  /|  __| |  __|  
      / ____ \ ____) | |____ _| |_ _| |_       | |  | | \ \| |____| |____ 
     /_/    \_\_____/ \_____|_____|_____|      |_|  |_|  \_\______|______|

                                                                         
    """;

string path;

if (args.Length > 0)
{
    if (!Directory.Exists(args[0]))
    {
        Environment.Exit(0);
    }
    else if (Directory.GetDirectories(args[0]).Length + Directory.GetFiles(args[0]).Length == 0)
    {
        Environment.Exit(0);
    }

    path = args[0].Replace('/', '\\');
}
else
{
    Console.WriteLine(art);
    Console.WriteLine("Input the full path to the directory:");
    while (true)
    {
        string? input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.Clear();
            Console.WriteLine(art);
            Console.WriteLine("Input the full path to the directory:");
            continue;
        }
        else if (!Directory.Exists(input))
        {
            Console.Clear();
            Console.WriteLine(art);
            Console.WriteLine("Provided directory doesn't exist.");
            Console.WriteLine("Please try again:");
            continue;
        }
        else if (Directory.GetDirectories(input).Length + Directory.GetFiles(input).Length == 0)
        {
            Console.Clear();
            Console.WriteLine(art);
            Console.WriteLine("There's nothing inside the provided directory.");
            Console.WriteLine("Please try again:");
            continue;
        }

        path = input.Replace('/', '\\');
        break;
    }
}

Folder root = new(path);

if (args.Length > 0)
{
    if (File.Exists($"{path}\\..\\ASCII_File_Tree.txt"))
    {
        File.Delete($"{path}\\..\\ASCII_File_Tree.txt");
    }

    using FileStream fs = File.Create($"{path}\\..\\ASCII_File_Tree.txt");

    Byte[] content = new UTF8Encoding(true).GetBytes(root.Tree);
    fs.Write(content, 0, content.Length);
}
else
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"ASCII Tree of provided directory:");
        Console.WriteLine(path);
        Console.WriteLine();
        Console.WriteLine(root.Tree);
        Console.Write("\nCopy ASCII Tree to a txt file? (y/N) ");

        var input = Console.ReadKey(true).Key;

        if (input == ConsoleKey.Enter || input == ConsoleKey.N)
        {
            Console.WriteLine("N");
            break;
        }
        else if (input == ConsoleKey.Y)
        {
            Console.WriteLine("Y");

            if (File.Exists($"{path}\\..\\ASCII_File_Tree.txt"))
            {
                File.Delete($"{path}\\..\\ASCII_File_Tree.txt");
            }

            using FileStream fs = File.Create($"{path}\\..\\ASCII_File_Tree.txt");
            {
                Byte[] content = new UTF8Encoding(true).GetBytes(root.Tree);
                fs.Write(content, 0, content.Length);
            }

            List<string> pathList = [.. path.Split('\\')];

            pathList.RemoveAt(pathList.Count - 1);

            pathList.Add("ASCII_File_Tree.txt");

            string filePath = string.Join('\\', pathList);

            Console.WriteLine($"Text file has been created at '{filePath}'");
            break;
        }
    }

    Console.Write("\nPress any key to exit...");
    Console.ReadKey(true);
}