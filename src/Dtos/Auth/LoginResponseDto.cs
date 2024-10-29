using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dtos.Auth
{
    public class LoginResponseDto
    {
        public required string Jwt { get; set; }
        public DateTime Expiration { get; set; }
        public required string RefreshToken { get; set; }
    }
}