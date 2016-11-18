using System.Collections.Generic;

namespace Caracal.FileConverter.CsvParser {

    public class DataTable {
        public IList<string> Headers { get; set; }
        public IList<IList<string>> Rows { get; set; }

        public DataTable() {
            Headers = new List<string>();
            Rows = new List<IList<string>>();
        }
    }
}
