using Org.BouncyCastle.Crypto;

namespace Wallet.API.Interface
{
    public interface IComputerHMAC
    {
        string ComputerHMAC(string Text, string Key, IDigest Algorithm);
    }
}
