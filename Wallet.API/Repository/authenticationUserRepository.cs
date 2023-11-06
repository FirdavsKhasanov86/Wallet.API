using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;
using Microsoft.EntityFrameworkCore;

namespace Wallet.API.Repository
{
    public class authenticationUserRepository : IAuthenticationUser
    {
        private readonly WalletContext _context;
        private readonly IComputerHMAC _computerHMAC;

        public authenticationUserRepository(WalletContext context, IComputerHMAC computerHMAC)
        {
            _context = context;
            _computerHMAC = computerHMAC;
        }
        public async Task<string> AuthenticationUserAsync([FromBody] UserRequest userRequest)
        {
            if (userRequest != null)
            {
                var requestId = userRequest.Id;
                var requestPhone = userRequest.Phone;

                IDigest AlgorithmToUse = new Sha1Digest();
                var hashResult = _computerHMAC.ComputerHMAC(requestPhone, requestId, AlgorithmToUse);

                var SecretKeyPlusAccount = await _context.Accounts.Where(a => a.Key == hashResult).FirstOrDefaultAsync();
                if (SecretKeyPlusAccount == null)
                {
                    return "Unauthorized";
                }

                return "Authorized";
            }

            return "BadRequest";
        }
    }
}
