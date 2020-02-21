using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gateway
{
        public class JwtSecurityKey
        {
            private static byte[] secretBytes = Encoding.UTF8.GetBytes("<Extra-Lang og tilfældig string her. Ingen Penis Jokes tak.>");
            public static SymmetricSecurityKey Key => new SymmetricSecurityKey(secretBytes);
        }
}