using Main.Models;
using Main.Parsers;
using Main.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        string projectDirectory = Directory.GetParent(path: AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        string dataDirectory = Path.Combine(projectDirectory, "Data");

        string productsFilePath = Path.Combine(dataDirectory, "products.csv");
        string stockItemsFilePath = Path.Combine(dataDirectory, "stockItems.csv");
        string supplierOffersFilePath = Path.Combine(dataDirectory, "supplierOffers.csv");

        var outputFilePath = "result.csv";

        var csvParser = new CsvParser();

        var products = csvParser.Parse<Product>(productsFilePath).ToList();
        var supplierOffers = csvParser.Parse<SupplierOffer>(supplierOffersFilePath).ToList();
        var stockItems = csvParser.Parse<StockItem>(stockItemsFilePath).ToList();

        var calculationService = new CalculationService();

        var results = products.Select(product =>
        {
            return calculationService.Calculate(product.Id, supplierOffers, stockItems);
        }).ToList();

        using (var writer = new StreamWriter(outputFilePath))
        {
            writer.WriteLine("ProductId;CostPrice;TotalQuantity;HasData");
            foreach (var result in results)
            {
                writer.WriteLine($"{result.ProductId};{result.CostPrice};{result.TotalQuantity};{result.HasData}");
            }
        }
    }
}