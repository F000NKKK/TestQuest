using Main.Models;
using Main.Services;

public class CalculationServiceTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectResult_WhenDataIsPresent()
    {
        // Arrange
        var calculationService = new CalculationService();
        var productId = "P001";
        var supplierOffers = new List<SupplierOffer>
    {
        new SupplierOffer { ProductId = "P001", Price = 9.50m, Quantity = 200 },
        new SupplierOffer { ProductId = "P001", Price = 10.00m, Quantity = 150 }
    };
        var stockItems = new List<StockItem>
    {
        new StockItem { ProductId = "P001", Price = 10.00m, Quantity = 100 },
        new StockItem { ProductId = "P001", Price = 11.00m, Quantity = 50 }
    };

        // Act
        var result = calculationService.Calculate(productId, supplierOffers, stockItems);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(9.50m, result.CostPrice); // Минимальная цена
        Assert.Equal(500, result.TotalQuantity); // Общая сумма количества
        Assert.True(result.HasData);
    }


    [Fact]
    public void Calculate_ShouldReturnNoData_WhenNoSupplierOffersOrStockItems()
    {
        // Arrange
        var calculationService = new CalculationService();
        var productId = "P001";
        var supplierOffers = new List<SupplierOffer>();
        var stockItems = new List<StockItem>();

        // Act
        var result = calculationService.Calculate(productId, supplierOffers, stockItems);

        // Assert
        Assert.Null(result); // Ожидается null
    }


    [Theory]
    [InlineData("P001", 0, 0, 9.50, 150, true)] // Позиции на складе, цены у поставщиков = 0
    [InlineData("P001", 10.00, 100, 0, 0, true)] // Только предложения от поставщиков
    [InlineData("P002", 0, 0, 0, 0, false)] // Нет данных
    [InlineData("P001", 10.00, 100, 9.50, 150, true)] // Комбинация предложений и позиций
    public void Calculate_ShouldHandleVariousInputs(string productId, decimal supplierPrice, int supplierQuantity, decimal stockPrice, int stockQuantity, bool hasData)
    {
        // Arrange
        var calculationService = new CalculationService();
        var supplierOffers = new List<SupplierOffer>
    {
        new SupplierOffer { ProductId = productId, Price = supplierPrice, Quantity = supplierQuantity }
    };
        var stockItems = new List<StockItem>
    {
        new StockItem { ProductId = productId, Price = stockPrice, Quantity = stockQuantity }
    };

        // Act
        var result = calculationService.Calculate(productId, supplierOffers, stockItems);

        // Assert
        if (hasData)
        {
            Assert.NotNull(result);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(Math.Min(supplierPrice == 0 ? decimal.MaxValue : supplierPrice, stockPrice == 0 ? decimal.MaxValue : stockPrice), result.CostPrice);
            Assert.Equal(supplierQuantity + stockQuantity, result.TotalQuantity);
            Assert.True(result.HasData);
        }
        else
        {
            Assert.Null(result);
        }
    }
}
