using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;
using Wallet.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Wallet.API.Repository
{
    public class editUsersAccountRepository : IEditUserAccount
    {
        private readonly WalletContext _context;
        private readonly IComputerHMAC _computerHMAC;

        public editUsersAccountRepository(WalletContext context, IComputerHMAC computerHMAC)
        {
            _context = context;
            _computerHMAC = computerHMAC;
        }
        public async Task<string> EditUserAccountAsync(Guid userId, EditUsersAccount editUsersAccount)
        {
            var finUserId = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (finUserId == null) { return "This user not found"; }
            bool isNumerical;
            int myInt;
            finUserId.FirstName = editUsersAccount.FirstName;
            finUserId.LastName = editUsersAccount.LastName;
            if (isNumerical = int.TryParse(editUsersAccount.Phone, out myInt))
            {
                finUserId.AuthorizationStatus = AuthorizationStatus.Authorized;
            }
            else { finUserId.AuthorizationStatus = AuthorizationStatus.Unauthorized; }

            finUserId.InsertdDate = DateTime.Now;
            finUserId.UpdateDate = DateTime.Now;

            try
            {
                _context.Users.Update(finUserId);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            IDigest AlgorithmToUse = new Sha1Digest();
            var hashResult = _computerHMAC.ComputerHMAC(editUsersAccount.Phone, finUserId.Id.ToString(), AlgorithmToUse);

            var account = await _context.Accounts.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            account.Phone = editUsersAccount.Phone;
            account.Key = hashResult;
            account.InsertDate = DateTime.Now;
            account.UpdateDate = DateTime.Now;

            try
            {
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            if (isNumerical = int.TryParse(editUsersAccount.Phone, out myInt))
            {
                return "User successfully authorized";
            }
            else { return "User still not Unauthorized, if you want authorized please enter your phone number! "; }

        }
    }
}
