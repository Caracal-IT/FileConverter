using System.Text;
using static System.Text.Encoding;
using System.IO;

namespace Caracal.FileConverter.Parser.Tests.TestDoubles {

    public struct MockCustomerData {
        private static string CsvString {
            get {
                var customers = new StringBuilder();
                customers.Append("FirstName,LastName,Address,PhoneNumber\n");
                customers.Append("Jimmy,Smith,102 Long Lane,29384857\n");
                customers.Append("Clive,Owen,65 Ambling Way,31214788\n");
                customers.Append("James,Owen,8 Crimson Rd,32114566\n");
                customers.Append("Graham,Brown,94 Roland St,8766556");

                return customers.ToString();
            }
        }

        public static Stream Stream => new MemoryStream(UTF8.GetBytes(CsvString));
    }
}