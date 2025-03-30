using System.Security.Cryptography;
using System.Text;

namespace Chameleon.Application.Securities;

public class MdpCrypte
{
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
}