using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class CreateOrderRequest
    {
        public required string Intent { get; set; }
        public List<PurchaseUnit> PurchaseUnits { get; set; } = [];
    }
}
