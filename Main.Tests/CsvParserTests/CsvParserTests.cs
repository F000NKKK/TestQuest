using Main.Models;
using Main.Parsers;
using Xunit;

namespace Main.Tests
{
    public class CsvParserTests
    {
        [Fact]
        public void ParseProduct_ShouldReturnCorrectProduct()
        {
            // Arrange
            var csvParser = new CsvParser();
            var productLine = "P001,Product A";

            // Act
            var result = csvParser.ParseFromString<Product>(productLine).First();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("P001", result.Id);
            Assert.Equal("Product A", result.Name);
        }

        [Fact]
        public void ParseStockItem_ShouldReturnCorrectStockItem()
        {
            // Arrange
            var csvParser = new CsvParser();
            var stockItemLine = "P001,10.50,100";

            // Act
            var result = csvParser.ParseFromString<StockItem>(stockItemLine).First();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("P001", result.ProductId);
            Assert.Equal(10.50m, result.Price);
            Assert.Equal(100, result.Quantity);
        }

        [Fact]
        public void ParseSupplierOffer_ShouldReturnCorrectSupplierOffer()
        {
            // Arrange
            var csvParser = new CsvParser();
            var supplierOfferLine = "P001,9.50,200";

            // Act
            var result = csvParser.ParseFromString<SupplierOffer>(supplierOfferLine).First();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("P001", result.ProductId);
            Assert.Equal(9.50m, result.Price);
            Assert.Equal(200, result.Quantity);
        }

        [Theory]
        [InlineData("P001,10.50,100", 10.50)]
        [InlineData("P001,9.75,150", 9.75)]
        [InlineData("P001,7.50,0", 7.50)]
        public void ParseStockItem_ShouldHandleDecimalPricesCorrectly(string input, decimal expectedPrice)
        {
            // Arrange
            var csvParser = new CsvParser();

            // Act
            var result = csvParser.ParseFromString<StockItem>(input).First();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPrice, result.Price);
        }
    }
}
