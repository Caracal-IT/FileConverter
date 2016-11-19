using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Caracal.FileConverter.Parser {
    public class CsvParser {
        public static DataTable Parse(string csvText) {
            var table = new DataTable();
            var lines = csvText?.Split('\n')
                                .ToList();

            if (hasContent()) {
                ParseHeader();
                ParseRows();
            }

            return table;

            bool hasContent() => csvText?.Length > 0 && lines != null && lines.Count() > 0;
            void ParseHeader() => ParseRowIntoList(lines.First(), table.Headers); 
            void ParseRows() => lines.Skip(1).Where(r => !string.IsNullOrEmpty(r)).ToList().ForEach(i => ParseRowIntoList(i, table.CreateRow()));
                
            void ParseRowIntoList(string row, IList<string> list){
                Regex.Split(row, ",")
                     .Select(h => h.Trim())
                     .ToList()
                     .ForEach(h => list.Add(h));
            }
        }
    }
}