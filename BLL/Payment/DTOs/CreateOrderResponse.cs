using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.DTOs
{
    public sealed class CreateOrderResponse
    {
        public required string Id { get; set; }
        public required string Status { get; set; }
        public List<Link> Links { get; set; } = [];
    }
}
