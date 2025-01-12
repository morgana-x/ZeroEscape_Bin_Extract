public partial class Program
{ 
    static void Execute(string filePath)
    {
        filePath = filePath.Replace("\"", "");
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File {filePath} does not exist!");
            return;
        }
        if (filePath.EndsWith(".bin"))
        {
            ZE_BIN.ZE_BIN.ExtractBin(filePath);
            Console.WriteLine("Extracted!");
            return;
        }
        ZE_BIN.ZE_BIN.RepackBin(filePath);
        Console.WriteLine("Repacked!");
    }
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Execute(args[0]);
            return;
        }
        while (true)
        {
            Console.WriteLine("Drag and drop .bin file to extract, or .txt to repack.");
            Execute(Console.ReadLine());
        }
    }
}
