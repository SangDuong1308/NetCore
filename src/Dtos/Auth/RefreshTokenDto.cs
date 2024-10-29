using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Dtos.Auth
{
    public class RefreshTokenDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}