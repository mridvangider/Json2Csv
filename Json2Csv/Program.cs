using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Json2Csv6
{
    internal class Program
    {
        public static string GetCsvLine(JObject jsonObject)
        {
            string[] values = jsonObject.Properties()
                .Select(prop => prop.Type == JTokenType.String ? "\"" + prop.Value.ToString() + "\"" : prop.Value.ToString())
                .ToArray();
            return string.Join(',', values);
        }

        public static void Convert(StreamReader reader, StreamWriter writer)
        {
            foreach (JObject obj in JArray.Load(new JsonTextReader(reader)))
            {
                writer.WriteLine(GetCsvLine(obj));
            }
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Not enough arguments. Need Json source and Csv Destination.");
                return;
            }

            var file0 = new FileInfo(args[0]);
            var file1 = new FileInfo(args[1]);

            if (file0.Extension != ".json" && file0.Extension != ".json")
            {
                Console.WriteLine("One of the files must have .json extension");
                return;
            }

            if (file1.Extension != ".csv" && file0.Extension != ".csv")
            {
                Console.WriteLine("One of the files must have .csv extension");
                return;
            }

            string sourceFile = file0.Extension == ".json" ? file0.FullName : file1.FullName;
            string destinationFile = file0.Extension == ".csv" ? file0.FullName : file1.FullName;

            using var reader = new StreamReader(sourceFile);
            using var writer = new StreamWriter(destinationFile, false, Encoding.UTF8);
            Convert(reader, writer);
        }
    }
}