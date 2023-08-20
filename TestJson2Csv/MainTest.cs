using System.Globalization;
using System.Text;

namespace TestJson2Csv
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void TestGetCsvLine()
        {
            var now = DateTime.Now;
            var obj = new JObject
            {
                { "charTest", "test value" },
                { "intTest", 123 },
                { "datetimeTest", now },
                { "floatTest", 1.2 }
            };

            var test = $"\"test value\",123,{now},1.2";
            var result = Program.GetCsvLine(obj);
            Assert.AreEqual(test,result);
        }

        [TestMethod]
        public void TestGetCsvHeader()
        {
            var now = DateTime.Now;
            var obj = new JObject
            {
                { "charTest", "test value" },
                { "intTest", 123 },
                { "datetimeTest", now }
            };

            var test = "\"charTest\",\"intTest\",\"datetimeTest\"";
            var result = Program.GetCsvHeader(obj);
            Assert.AreEqual(test,result);
        }

        [TestMethod]
        public void TestConvert()
        {
            var now = DateTime.Now;
            var now2 = now.AddDays(1);
            var now_str = now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mmK");
            var now2_str = now2.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mmK");
            var json = 
                "[{" +
                "\"charTest\": \"test value 1\"," +
                "\"intTest\": 123," +
                $"\"datetimeTest\": \"{now_str}\"" +
                "},{" +
                "\"charTest\": \"test value 2\"," +
                "\"intTest\": 456," +
                $"\"datetimeTest\": \"{now2_str}\"" +
                "}]";
            var inStream = new StringReader(json);
            var builder = new StringBuilder();
            var outStream = new StringWriter(builder);

            string test =
                "\"charTest\",\"intTest\",\"datetimeTest\"" + Environment.NewLine +
                $"\"test value 1\",123,\"{now_str}\"" + Environment.NewLine +
                $"\"test value 2\",456,\"{now2_str}\"" + Environment.NewLine;
            Program.Convert(inStream, outStream);
            var result = builder.ToString();
            Assert.AreEqual(test, result, false);
        }
    }
}