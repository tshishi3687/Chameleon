using System.Security.Cryptography;
using System.Text;

namespace Chameleon.Application.Securities;

public class MdpCrypte
{
    [Obsolete("Obsolete")]
    public string CryptMdp(string mdp)
    {
        byte[] mdpByte = Encoding.UTF8.GetBytes(mdp);
        byte[] hashKey = Encoding.UTF8.GetBytes(Constantes.Log);

        using DESCryptoServiceProvider crypto = new DESCryptoServiceProvider();
        crypto.Key = hashKey;
        crypto.IV = hashKey;

        ICryptoTransform iCrypto = crypto.CreateEncryptor();

        byte[] result = iCrypto.TransformFinalBlock(mdpByte, 0, mdpByte.Length);

        return Convert.ToBase64String(result);
    }

    [Obsolete("Obsolete")]
    public bool Compart(string mdpA, string mdpB)
    {
        if (mdpA == CryptMdp(mdpB))
            return true;

        return false;
    }
}