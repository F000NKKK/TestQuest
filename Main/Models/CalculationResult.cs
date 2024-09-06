using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    internal class CalculationResult
    {
        public string ProductId { get; set; }
        public decimal CostPrice { get; set; }
        public int TotalQuantity { get; set; }
        public bool HasData { get; set; } // Указывает, были ли данные для расчета
    }
}
