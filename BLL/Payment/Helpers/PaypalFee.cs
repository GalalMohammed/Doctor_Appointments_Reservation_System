using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class PaypalFee
    {
        public required string CurrencyCode { get; set; }
        public required string Value { get; set; }
    }
}
