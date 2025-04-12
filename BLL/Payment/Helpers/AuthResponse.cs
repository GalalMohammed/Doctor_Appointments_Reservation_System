using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLServices.Payment.Helpers
{
    public sealed class AuthResponse
    {
        public required string Scope { get; set; }
        public required string AccessToken { get; set; }
        public required string TokenType { get; set; }
        public required string AppId { get; set; }
        public int ExpiresIn { get; set; }
        public string? Nonce { get; set; }
    }
}
