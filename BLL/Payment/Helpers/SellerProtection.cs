using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class SellerProtection
    {
        public required bool Status { get; set; }
        public List<string> DisputeCategories { get; set; } = [];
    }
}
