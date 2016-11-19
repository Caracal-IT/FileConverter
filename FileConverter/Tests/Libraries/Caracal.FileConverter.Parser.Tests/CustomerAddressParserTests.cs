using Xunit;

using static Xunit.Assert;
using static System.Text.Encoding;
using System.IO;
using Caracal.FileConverter.Parser.Tests.TestDoubles;

namespace Caracal.FileConverter.Parser.Tests {
    public class CustomerAddressParserTests {
        [Fact]
        public void ParseAddress() {
            string[] result = null;

            using (var output = new MemoryStream()) {
                using (var input = MockCustomerData.Stream) {
                    CustomerAddressParser.Parse(input, output);
                }

                result = UTF8.GetString(output.ToArray()).Split('\n');
            }

            Equal(5, result.Length);
            Equal("65 Ambling Way", result[0]);
            Equal("8 Crimson Rd", result[1]);
            Equal("102 Long Lane", result[2]);
            Equal("94 Roland St", result[3]);
            Equal(string.Empty, result[4]);
        }
    }
}