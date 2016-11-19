using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Caracal.FileConverter.Parser {
    public class CustomerAddressParser {
        private StreamReader reader;
        private StreamWriter writer;
        private IOrderedEnumerable<Address> addresses;

        private CustomerAddressParser(Stream input, Stream output) {
            reader = new StreamReader(input);
            writer = new StreamWriter(output) { NewLine = "\n" };
        }

        public static void Parse(Stream input, Stream output) => (new CustomerAddressParser(input, output)).Parse();

        public void Parse() {
            CreateAddressesFromInputStream();
            WriteAddressesToOutputStream();
        }

        private void CreateAddressesFromInputStream() {
            var table = CsvParser.Parse(reader.ReadToEnd());

            addresses = table.Rows
                             .GroupBy(r => r["Address"])
                             .Select(r => new Address(r.Key))
                             .OrderBy(r => r.Street)
                             .ThenBy(r => r.Number);
        }

        private void WriteAddressesToOutputStream() {
            addresses.ToList().ForEach(a => writer.WriteLine(a));            
            writer.Flush();
        }
               
        private struct Address {
            private string addressString;

            public Address(string addressString) {
                this.addressString = addressString;
                var m = Regex.Match(addressString, @"^(?<number>\d+)\s+(?<street>.+)$");

                Number = m.Groups["number"].Value;
                Street = m.Groups["street"].Value;
            }

            public string Number { get; set; }
            public string Street { get; set; }

            public override string ToString() {
                return addressString;
            }
        }
    }
}