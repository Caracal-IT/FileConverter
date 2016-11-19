using System.Linq;
using System.IO;

namespace Caracal.FileConverter.Parser {
    public class CustomerSurnameParser {
        private StreamReader reader;
        private StreamWriter writer;
        private IOrderedEnumerable<Customer> customers;
        
        private CustomerSurnameParser(Stream input, Stream output) {
            reader = new StreamReader(input);
            writer = new StreamWriter(output) { NewLine = "\n" };            
        }
        
        public static void Parse(Stream input, Stream output)  => (new CustomerSurnameParser(input, output)).Parse();        

        public void Parse() {
            CreateCustomersFromInputStream();
            WriteCustomersToOutputStream();            
        }

        private void CreateCustomersFromInputStream() {
            var table = CsvParser.Parse(reader.ReadToEnd());

            customers = table.Rows
                             .GroupBy(r => r["LastName"])
                             .Select(r => new Customer { LastName = r.Key, Count = r.Count() })
                             .OrderByDescending(r => r.Count)
                             .ThenBy(r => r.LastName);
        }

        private void WriteCustomersToOutputStream() {            
            foreach (var c in customers) 
                WriteCustomer(c);                
            
            writer.Flush();
        }

        private void WriteCustomer(Customer customer) {            
            if(IsLastCustomer(customer))            
                writer.Write(customer);
            else
                writer.WriteLine(customer);
        }

        private bool IsLastCustomer(Customer customer) => customer.Equals(customers.Last());

        private struct Customer {
            public string LastName { get; set; }
            public int Count { get; set; }

            public override string ToString() {
                return $"{LastName},{Count}";
            }
        }
    }
}