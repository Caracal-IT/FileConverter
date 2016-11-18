using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caracal.FileConverter.CsvParser {
    public class CsvParser {
        public static DataTable Parse(string csvText) {
            var table = new DataTable();
            var lines = csvText?.Split('\n');

            if (hasContent()) {
                ParseHeader();
                ParseRows();
            }

            return table;

            bool hasContent() => csvText?.Length > 0 && lines != null && lines.Count() > 0;

            void ParseHeader() => ParseRowIntoList(lines[0], table.Headers);                

            void ParseRows() {
                for (int i = 1; i < lines.Length; i++) {
                    var row = new List<string>();
                    ParseRowIntoList(lines[i], row);
                    table.Rows.Add(row);                
                }
            }

            void ParseRowIntoList(string row, IList<string> list){
                Regex.Split(row, ",")
                     .Select(h => h.Trim())
                     .ToList()
                     .ForEach(h => list.Add(h));
            }
        }
    }

    public class DataTable {
        public IList<string> Headers { get; set; }
        public IList<IList<string>> Rows { get; set; }

        public DataTable() {
            Headers = new List<string>();
            Rows = new List<IList<string>>();
        }
    }
}
