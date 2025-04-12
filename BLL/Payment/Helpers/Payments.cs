using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Payments
    {
        public List<Capture> Captures { get; set; } = [];
    }
}
