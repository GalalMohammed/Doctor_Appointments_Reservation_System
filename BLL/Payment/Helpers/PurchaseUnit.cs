using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class PurchaseUnit
    {
        public Amount Amount { get; set; }
        public required string ReferenceId { get; set; }
        public Shipping Shipping { get; set; }
        public Payments Payments { get; set; }
    }
}
