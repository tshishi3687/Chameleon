using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Chameleon.Application.Securities;

public class MdpCrypte
{
    
    public readonly string SecretToken ="je kifais trop le manga Sakura Card Captor";
    // Hacher le mot de passe avec BCrypt
    public string CryptMdp(string mdp)
    {
        return BCrypt.Net.BCrypt.HashPassword(mdp);
    }

    // Vérifier si un mot de passe correspond au hash stocké
    public bool Compart(string mdpA, string mdpB)
    {
        return BCrypt.Net.BCrypt.Verify(mdpB, mdpA);
    }
    
    public string GenerateToken(List<Claim> claims)
    {
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretToken));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                Key,
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}