using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Payer
    {
        public required Name Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PayerId { get; set; }
    }
}
