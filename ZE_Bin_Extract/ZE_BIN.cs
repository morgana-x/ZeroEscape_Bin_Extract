namespace ZE_BIN
{
    public class ZE_BIN
    {
        private static string ComposeExport(List<int> ids, List<string> texts)
        {
            string output = "";
            for (int i = 0; i < ids.Count; i++)
                output += $"{ids[i]}:{texts[i].Replace("\n", "\\n")}\n";
            return output;
        }
        private static Tuple<List<int>, List<string>> ComposeImport(string input)
        {
            List<int> ids = new();
            List<string> texts = new();
            string[] split = input.Split("\n");
            for (int i = 0; i < split.Length; i++)
            {
                int splitIndex = split[i].IndexOf(':') + 1;
                if (splitIndex <= 0)
                {
                    Console.WriteLine($"Couldn't find ':' at index {i}! (Ignore)");
                    continue;
                }
                ids.Add(int.Parse(split[i].Substring(0, splitIndex - 1)));
                texts.Add(split[i].Substring(splitIndex, split[i].Length - splitIndex).Replace("\\n", "\n"));
            }
            return Tuple.Create(ids, texts);
        }
        private static void ExtractBin(Stream s, string outPath)
        {
            List<int> ids = new List<int>();
            List<string> texts = new List<string>();
            while (s.Position < s.Length)
            {
                ids.Add(BIN_Util.Read_Int32(s));
                texts.Add(BIN_Util.Read_Bin_String(s));
            }
            File.WriteAllText(outPath, ComposeExport(ids, texts));
        }
        public static void ExtractBin(string filePath)
        {
            Stream s = File.OpenRead(filePath);
            ExtractBin(s, filePath.Replace(".bin", ".txt"));
            s.Dispose();
            s.Close();
        }
        public static void RepackBin(string filePath, string outPath = "")
        {
            if (outPath == "")
                outPath = filePath.Replace(".txt", ".bin");
            string text = File.ReadAllText(filePath);
            var a = ComposeImport(text);
            Stream s = new FileStream(outPath, FileMode.Create, FileAccess.Write);
            for (int i = 0; i < a.Item1.Count; i++)
            {
                s.Write(BitConverter.GetBytes(a.Item1[i]));
                BIN_Util.Write_Bin_String(s, a.Item2[i]);
            }
            s.Dispose();
            s.Close();
        }
    }
}