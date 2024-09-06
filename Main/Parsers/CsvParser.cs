using Main.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Parsers
{
    public class CsvParser : IParser<Product>, IParser<SupplierOffer>, IParser<StockItem>
    {
        public IEnumerable<T> Parse<T>(string filePath) where T : class, new()
        {
            var lines = File.ReadAllLines(filePath).Skip(1);
            var results = new List<T>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                T? item;

                if (typeof(T) == typeof(Product))
                {
                    item = new Product { Id = parts[0], Name = parts[1] } as T;
                }
                else if (typeof(T) == typeof(SupplierOffer))
                {
                    item = new SupplierOffer
                    {
                        ProductId = parts[0],
                        Price = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                        Quantity = int.Parse(parts[2])
                    } as T;
                }
                else if (typeof(T) == typeof(StockItem))
                {
                    item = new StockItem
                    {
                        ProductId = parts[0],
                        Price = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                        Quantity = int.Parse(parts[2])
                    } as T;
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type");
                }

                results.Add(item: item);
            }

            return results;
        }

        IEnumerable<Product> IParser<Product>.Parse(string filePath)
        {
            return Parse<Product>(filePath);
        }

        IEnumerable<SupplierOffer> IParser<SupplierOffer>.Parse(string filePath)
        {
            return Parse<SupplierOffer>(filePath);
        }

        IEnumerable<StockItem> IParser<StockItem>.Parse(string filePath)
        {
            return Parse<StockItem>(filePath);
        }




        //Для тестов
        public IEnumerable<T> ParseFromString<T>(string csvData) where T : class, new()
        {
            var lines = csvData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var results = new List<T>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                T? item;

                if (typeof(T) == typeof(Product))
                {
                    item = new Product { Id = parts[0], Name = parts[1] } as T;
                }
                else if (typeof(T) == typeof(SupplierOffer))
                {
                    item = new SupplierOffer
                    {
                        ProductId = parts[0],
                        Price = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                        Quantity = int.Parse(parts[2])
                    } as T;
                }
                else if (typeof(T) == typeof(StockItem))
                {
                    item = new StockItem
                    {
                        ProductId = parts[0],
                        Price = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                        Quantity = int.Parse(parts[2])
                    } as T;
                }
                else
                {
                    throw new InvalidOperationException("Unsupported type");
                }

                results.Add(item: item);
            }

            return results;
        }

    }
}