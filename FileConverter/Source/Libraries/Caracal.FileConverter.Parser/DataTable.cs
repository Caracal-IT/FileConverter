using System.Collections.Generic;

namespace Caracal.FileConverter.Parser {

    public class DataTable {
        public IList<string> Headers { get; set; }
        public IList<Row> Rows { get; private set; }

        public DataTable() {
            Headers = new List<string>();
            Rows = new List<Row>();
        } 
        
        public Row CreateRow() {
            var r = new Row(Headers);
            Rows.Add(r);

            return r;
        }          
    }

    public class Row : List<string> {
        private IList<string> headers;

        public Row(IList<string> headers) {
            this.headers = headers;
        }

        public string this[string key] {
            get { return this[headers.IndexOf(key)]; }
            set { this[headers.IndexOf(key)] = value; }
        }
    }
}