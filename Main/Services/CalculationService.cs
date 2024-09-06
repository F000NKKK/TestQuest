using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Services
{
    public class CalculationService : ICalculationService
    {
        public CalculationResult Calculate(string productId, IEnumerable<SupplierOffer> supplierOffers, IEnumerable<StockItem> stockItems)
        {
            // Фильтрация предложений поставщиков и позиций на складе с ценой > 0
            var relevantSupplierOffers = supplierOffers
                .Where(x => x.ProductId == productId && x.Price > 0)
                .ToList();

            var relevantStockItems = stockItems
                .Where(x => x.ProductId == productId && x.Price > 0)
                .ToList();

            if (!relevantSupplierOffers.Any() && !relevantStockItems.Any())
            {
                // Если нет данных ни от поставщиков, ни на складе
                return null;
            }

            // Определение минимальной цены среди предложений поставщиков и позиций на складе
            var minSupplierPrice = relevantSupplierOffers.Any() ? relevantSupplierOffers.Min(x => x.Price) : decimal.MaxValue;
            var minStockPrice = relevantStockItems.Any() ? relevantStockItems.Min(x => x.Price) : decimal.MaxValue;

            // Выбор минимальной цены
            var costPrice = Math.Min(minSupplierPrice, minStockPrice);

            // Подсчет общего количества
            var totalQuantity = relevantSupplierOffers.Sum(x => x.Quantity) + relevantStockItems.Sum(x => x.Quantity);

            return new CalculationResult
            {
                ProductId = productId,
                CostPrice = costPrice,
                TotalQuantity = totalQuantity,
                HasData = true
            };
        }
    }

}
