using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Address
    {
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AdminArea2 { get; set; }
        public string? AdminArea1 { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryCode { get; set; }
    }
}
