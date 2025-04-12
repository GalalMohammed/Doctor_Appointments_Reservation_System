using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class CaptureOrderResponse
    {
        public required string Id { get; set; }
        public required string Status { get; set; }
        public PaymentSource paymentSource { get; set; }
        public List<PurchaseUnit> PurchaseUnits { get; set; } = [];
        public Payer payer { get; set; }
        public List<Link> Links { get; set; }
    }
}
