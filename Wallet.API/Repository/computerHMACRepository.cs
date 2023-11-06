using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using System.Text;
using Wallet.API.Interface;
using Org.BouncyCastle.Crypto.Macs;

namespace Wallet.API.Repository
{
    public class computerHMACRepository : IComputerHMAC
    {
        public string ComputerHMAC(string Text, string Key, IDigest Algorithm)
        {

            HMac hmac = new HMac(Algorithm);
            hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(Key)));
            byte[] Output = new byte[hmac.GetMacSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(Text);

            hmac.BlockUpdate(bytes, 0, bytes.Length);
            hmac.DoFinal(Output, 0);
            string Result = BitConverter.ToString(Output).Replace("-", "").ToLower();

            return Result;

        }
    }
}
