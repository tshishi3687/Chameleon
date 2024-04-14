using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Chameleon.Application.Securities;

public class Constantes(Context context) : IConstente
{
    public User Connected { get; set; }

    public static String RoleAd
    {
        get { return "Admin"; }
    }

    public static String RoleCl
    {
        get { return "Client"; }
    }

    public static String Log
    {
        get { return "AS%54_!t"; }
    }

    public static long Expires
    {
        get { return 1 * 24 * 3600 * 1000; }
    } // 1 jour

    public static string SecretToken
    {
        get { return "je kifais trop le manga Sakura Card Captor"; }
    }

    private Context Context = new Context();

    public string GenerateToken(List<Claim> claims)
    {
        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constantes.SecretToken));
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
    

    private static string GetReference(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return null;
        }

        var token = accessToken;
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var jti = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;


        return jti;
    }

    public void UseThisUserConnected(string accessToken)
    {
        Connected = Context.User.FirstOrDefault(p =>
            p.Email.Equals(GetReference(accessToken)))!;
    }
}