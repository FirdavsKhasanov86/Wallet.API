using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;
using Wallet.API.Models.Enums;
using Wallet.API.Models;

namespace Wallet.API.Repository
{
    public class authorizationRepository : IAuthorization
    {
        private readonly WalletContext _context;
        private readonly IComputerHMAC _computerHMAC;

        public authorizationRepository(WalletContext context, IComputerHMAC computerHMAC)
        {
            _context = context;
            _computerHMAC = computerHMAC;
        }

        public async Task<string> AuthorizationRequest(AuthorizationRequest authorizationRequest)
        {
            bool isNumerical;
            int myInt;
            if (isNumerical = int.TryParse(authorizationRequest.Phone, out myInt))
            {
                _context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = authorizationRequest.FirstName,
                    LastName = authorizationRequest.LastName,
                    AuthorizationStatus = AuthorizationStatus.Authorized,
                    InsertdDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw (ex);
                }

                var GetLastUserInfo = _context.Users.ToList().OrderByDescending(x => x.InsertdDate).Take(1);
                var findLastUserId = _context.Users.Where(x => x.Id == GetLastUserInfo.First().Id).FirstOrDefault();

                IDigest AlgorithmToUse = new Sha1Digest();
                var hashResult = _computerHMAC.ComputerHMAC(authorizationRequest.Phone, findLastUserId.Id.ToString(), AlgorithmToUse);

                _context.Accounts.Add(new Account
                {
                    Id = Guid.NewGuid(),
                    UserId = findLastUserId.Id,
                    Phone = authorizationRequest.Phone,
                    Key = hashResult,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            else
            {
                _context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = authorizationRequest.FirstName,
                    LastName = authorizationRequest.LastName,
                    AuthorizationStatus = AuthorizationStatus.Unauthorized,
                    InsertdDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

                var GetLastUserInfo = _context.Users.ToList().OrderByDescending(x => x.InsertdDate).Take(1);
                var findLastUserId = _context.Users.Where(x => x.Id == GetLastUserInfo.First().Id).FirstOrDefault();

                _context.Accounts.Add(new Account
                {
                    Id = Guid.NewGuid(),
                    UserId = findLastUserId.Id,
                    InsertDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

                return "User not authorized, because you didn't enter your phone number";

            }
            return "User successfully authorized";
        }

    }
}
