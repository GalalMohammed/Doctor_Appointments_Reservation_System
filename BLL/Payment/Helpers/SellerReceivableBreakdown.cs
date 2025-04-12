using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class SellerReceivableBreakdown
    {
        public required Amount GrossAmount { get; set; }
        public required PaypalFee PayPalFee { get; set; }
        public required Amount NetAmount { get; set; }
    }
}
