﻿using System.Collections.Generic;
using System.Linq;
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
        public void RowsWithRowColumnAndCount() {
            table.Rows[2]["Header 2"] = "Row 2 - Col 2";            
            var rowCounts = table.Rows
                                 .GroupBy(r => r["Header 2"])
                                 .Select(r => new { Col = r.Key, Count = r.Count() })
                                 .ToList();

            Equal("Row 2 - Col 2", rowCounts[1].Col);
            Equal(2, rowCounts[1].Count);
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