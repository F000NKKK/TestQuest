using Main.Models;
using Main.Parsers;
using Main.Services;

class Program
{
    static void Main(string[] args)
    {
        var productsFilePath = args[0];
        var supplierOffersFilePath = args[1];
        var stockItemsFilePath = args[2];
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
            writer.WriteLine("ProductId,CostPrice,TotalQuantity,HasData");
            foreach (var result in results)
            {
                writer.WriteLine($"{result.ProductId},{result.CostPrice},{result.TotalQuantity},{result.HasData}");
            }
        }
    }
}
