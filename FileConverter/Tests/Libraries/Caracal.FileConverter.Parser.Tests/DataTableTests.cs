using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

using static Xunit.Assert;

namespace Caracal.FileConverter.Parser.Tests {
    public class DataTableTests {
        private DataTable table;

        public DataTableTests() {
            table = new DataTable();
            AddMockData();
        }

        [Fact]
        public void GerRowColumnByName() {
            Equal("Row 2 - Col 2", table.Rows[1]["Header 2"]);
        }
        
        [Fact]
        public void OrderByCol() {
            table.Rows[1]["Header 2"] = "Row 2 - ACol 2";

            var row = table.Rows
                           .Select(r => r["Header 2"])
                           .OrderBy(r => r, new PartialStringComparer())
                           .First();

            Equal("Row 2 - ACol 2", row);
        }

        class PartialStringComparer : IComparer<string> {
            public int Compare(string x, string y) {
                var a = x.Substring(x.IndexOf('-') + 1);
                var b = y.Substring(y.IndexOf('-') + 1);

                return a.CompareTo(b);
            }
        }
        
        private void AddMockData() {
            AddHeaders();
            AddRows();

            void AddHeaders() {
                for (int i = 0; i < 4; i++)
                    table.Headers.Add($"Header {i}");
            }

            void AddRows(){
                for (int i = 1; i < 9; i++)
                    AddRow(i);

                void AddRow(int rowNumber){
                    var row = table.CreateRow();

                    for (int colNumber = 0; colNumber < 4; colNumber++)
                        row.Add($"Row {rowNumber} - Col {colNumber}");                    
                }
            }
        }        
    }
}