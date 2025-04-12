using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Link
    {
        public required string Href { get; set; }
        public required string Rel { get; set; }
        public required string Method { get; set; }
    }
}
