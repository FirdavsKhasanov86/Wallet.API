using Microsoft.EntityFrameworkCore;
using Wallet.API.Data;
using Wallet.API.Interface;

namespace Wallet.API.Repository
{
    public class getUserAuthorizedAccountRepository : IGetUserAuthorizedAccount
    {
        private readonly WalletContext _context;

        public getUserAuthorizedAccountRepository(WalletContext context)
        {
            _context = context;
        }
        public async Task<string> GetUserAuthorizedAccountAsync(Guid userId)
        {
            var data = await _context.Accounts.Where(p => p.UserId == userId).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.Phone.ToString();
            }

            return "This user not found";
        }
    }
}
