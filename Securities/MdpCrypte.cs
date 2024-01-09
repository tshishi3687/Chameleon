using System.Security.Cryptography;
using System.Text;

namespace Chameleon.Securities;

public class MdpCrypte
{
    public string CryptMdp(string mdp)
    {
        byte[] mdpByte = Encoding.UTF8.GetBytes(mdp);
        byte[] hashKey = Encoding.UTF8.GetBytes(Constentes.Log);

        DESCryptoServiceProvider Crypto = new DESCryptoServiceProvider();
        Crypto.Key = hashKey;
        Crypto.IV = hashKey;

        ICryptoTransform Icrypto = Crypto.CreateEncryptor();

        byte[] result = Icrypto.TransformFinalBlock(mdpByte, 0, mdpByte.Length);

        return Convert.ToBase64String(result);
    }

    public bool Compart(string mdpA, string mdpB)
    {
        if (mdpA == CryptMdp(mdpB))
            return true;

        return false;
    }
}