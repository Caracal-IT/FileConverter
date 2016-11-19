using Caracal.FileConverter.Parser;
using System;

class Program {
    static void Main(string[] args) {
        var basePath = AppContext.BaseDirectory;
        var sourcePath = $"{basePath}\\data.csv";

        ParseCustomerSurname(sourcePath, $"{basePath}\\surname.txt");
        ParseAddress(sourcePath, $"{basePath}\\address.txt");     
    }

    private static void ParseCustomerSurname(string sourcePath, string destinationPath) {
        using (var input = System.IO.File.OpenRead(sourcePath)) {
            using (var output = System.IO.File.OpenWrite(destinationPath)) {
                CustomerSurnameParser.Parse(input, output);
            }
        }
    }

    private static void ParseAddress(string sourcePath, string destinationPath) {
        using (var input = System.IO.File.OpenRead(sourcePath)) {
            using (var output = System.IO.File.OpenWrite(destinationPath)) {
                CustomerAddressParser.Parse(input, output);
            }
        }
    }
}