using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Capture
    {
        public required string Id { get; set; }
        public required string Status { get; set; }
        public Amount Amount { get; set; }
        public SellerProtection SellerProtection { get; set; }
        public bool FinalCapture { get; set; }
        public string? DisbursementMode { get; set; }
        public SellerReceivableBreakdown SellerReceivableBreakdown { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<Link> Links { get; set; } = [];
    }
}
