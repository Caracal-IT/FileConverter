using Xunit;

using static Xunit.Assert;
using static System.Text.Encoding;
using System.IO;
using Caracal.FileConverter.Parser.Tests.TestDoubles;

namespace Caracal.FileConverter.Parser.Tests {
    public class CustomerSurnameParserTests {

        [Fact]
        public void ParseSurnameCount() {
            string[] result = null;

            using (var output = new MemoryStream()) {
                using (var input = MockCustomerData.Stream) {
                    CustomerSurnameParser.Parse(input, output);
                }

                result = UTF8.GetString(output.ToArray()).Split('\n');
            }

            Equal(4, result.Length);
            Equal("Owen,2", result[0]);
            Equal("Brown,1", result[1]);
            Equal("Smith,1", result[2]);
            Equal(string.Empty, result[3]);
        }
    }
}