using System.Xml.Serialization;

namespace Counter_Lekhkun_Andrii.Models
{
    public class Counter
    {
        public string DataFilename { get; set; }
        public string Count { get; set; }
        public string StartCount { get; set; }
        public string Name { get; set; }
        public string Color {  get; set; }

        public Counter()
        {
            //DataFilename = $"{Path.GetRandomFileName()}.counters.txt";
            DataFilename = $"${Path.GetRandomFileName()}.counters.xml";
            Count = StartCount = string.Empty;
            Name = string.Empty;
            Color = string.Empty;
        }

        public void Save()
        {
            //File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, DataFilename), Name + "\n" + Count + "\n" + StartCount + "\n" + Color);

            string path = Path.Combine(FileSystem.AppDataDirectory, DataFilename);

            var serializer = new XmlSerializer(typeof(Counter));
            using var writer = new StreamWriter(path);
            serializer.Serialize(writer, this);
        }

        public void Delete()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, DataFilename);

            if (File.Exists(path))
                File.Delete(path);
        }

        public static Counter Load(string filename)
        {
            filename = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            //return
            //    new()
            //    {
            //        DataFilename = Path.GetFileName(filename),
            //        Name = File.ReadAllText(filename).Split('\n')[0],
            //        Count = File.ReadAllText(filename).Split('\n')[1],
            //        StartCount = File.ReadAllText(filename).Split('\n')[2],
            //        Color = File.ReadAllText(filename).Split('\n')[3]
            //    };

            var serializer = new XmlSerializer(typeof(Counter));
            using var reader = new StreamReader(filename);
            return (Counter)serializer.Deserialize(reader);
        }

        public static IEnumerable<Counter> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory
                //.EnumerateFiles(appDataPath, "*.counters.txt")
                .EnumerateFiles(appDataPath, "*.counters.xml")
                .Select(filename => Load(Path.GetFileName(filename)))
                .OrderByDescending(counter => counter.Name);
        }
    }
}
