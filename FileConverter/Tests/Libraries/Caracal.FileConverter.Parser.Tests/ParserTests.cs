using System.Linq;
using Xunit;

using static Xunit.Assert;

namespace Caracal.FileConverter.CsvParser.Tests {
    public class ParserTests {
        [Fact]
        public void ParseNull() {
            var t = CsvParser.Parse(null);

            False(t.Headers.Any());
            False(t.Rows.Any());
        }

        [Fact]
        public void ParseEmpty() {
            var t = CsvParser.Parse(string.Empty);

            False(t.Headers.Any());
            False(t.Rows.Any());
        }

        [Fact]
        public void ParseWithOneHeaderAndNoRows() {
            var t = CsvParser.Parse("Header1");

            Equal(1, t.Headers.Count());
            Equal("Header1", t.Headers[0]);
            False(t.Rows.Any());
        }

        [Fact]
        public void ParseWithTwoHeaders() {
            var t = CsvParser.Parse(" Header1 , Header2 ");

            Equal(2, t.Headers.Count());
            Equal("Header1", t.Headers[0]);
            Equal("Header2", t.Headers[1]);
            False(t.Rows.Any());
        }

        [Fact]        
        public void ParseWithOneHeaderAndOneRow() {
            var t = CsvParser.Parse("Header1\nRow1Col1");

            Equal(1, t.Headers.Count());
            Equal("Header1", t.Headers[0]);
            Equal(1, t.Rows.Count());
            Equal("Row1Col1", t.Rows[0][0]);
        }

        
        [Fact]
        public void ParseWithMultipleHeaderAndRows() {
            var t = CsvParser.Parse("Header1 , Header2\nRow1Col1, Row1Col2 \nRow2Col1,Row2Col2");

            Equal(2, t.Headers.Count());
            Equal("Header1", t.Headers[0]);
            Equal("Header2", t.Headers[1]);

            Equal(2, t.Rows.Count());
            Equal("Row1Col1", t.Rows[0][0]);
            Equal("Row1Col2", t.Rows[0][1]);

            Equal("Row2Col1", t.Rows[1][0]);
            Equal("Row2Col2", t.Rows[1][1]);
        }        
    }
}
