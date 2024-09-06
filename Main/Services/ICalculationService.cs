using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Services
{
    public interface ICalculationService
    {
        CalculationResult Calculate(string productId, IEnumerable<SupplierOffer> supplierOffers, IEnumerable<StockItem> stockItems);
    }
}
