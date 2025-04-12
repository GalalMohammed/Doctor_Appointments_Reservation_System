using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class Name
    {
        public required string GivenName { get; set; }
        public required string Surname { get; set; }
    }
}
