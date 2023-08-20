using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Json2Csv6
{
    public class Program
    {
        public static string GetCsvLine(JObject jsonObject)
        {
            string[] values = jsonObject.Properties()
                .Where(prop => prop.Value.Type != JTokenType.Object && prop.Value.Type != JTokenType.Array)
                .Select(prop =>
                {
                    if (prop.Value.Type == JTokenType.String )
                        return "\"" + prop.Value.ToString() + "\"";

                    else if (prop.Value.Type == JTokenType.Float )
                        return prop.Value.ToString().Replace(',', '.');

                    else
                        return prop.Value.ToString();
                })
                .ToArray();
            return string.Join(',', values);
        }

        public static string GetCsvHeader(JObject jsonObject)
        {
            return string.Join(',', jsonObject.Properties().Select(prop => "\"" + prop.Name + "\""));
        }

        public static void Convert(TextReader reader, TextWriter writer)
        {
            var arr = JArray.Load(new JsonTextReader(reader)).Cast<JObject>().ToList();
            var header = GetCsvHeader(arr[0]);
            writer.WriteLine(header);
            arr.ForEach(obj => writer.WriteLine(GetCsvLine(obj)));
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