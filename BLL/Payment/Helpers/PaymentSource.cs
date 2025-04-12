using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class PaymentSource
    {
        public required Paypal Paypal { get; set; }
    }
}
