using Microsoft.EntityFrameworkCore;
using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;

namespace Wallet.API.Repository
{
    public class getAllInfoAboutUsersRepository : IGetAllInfoAboutUsers
    {
        private readonly WalletContext _context;

        public getAllInfoAboutUsersRepository(WalletContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SendAllUsers>> GetAllInfoAboutUsersAsync()
        {
            var UserList = await _context.Users.ToListAsync();
            var SendInfoUsers = new List<SendAllUsers>();

            foreach (var user in UserList)
            {
                var model = new SendAllUsers();
                model.Id = user.Id;
                model.FirstName = user.FirstName;
                model.Lastname = user.LastName;
                model.AuthorizationStatus = user.AuthorizationStatus;
                var AccountLsit = await _context.Accounts.Where(x => x.UserId == user.Id).ToListAsync();
                if (AccountLsit.Count > 0)
                {
                    foreach (var account in AccountLsit)
                    {
                        model.Phone = account.Phone;
                        decimal SumAmounts = await _context.Transactions.Where(s => s.AccountId == account.Id).Select(x => x.Amount).SumAsync();
                        model.Amount = SumAmounts;
                        var TransacList = await _context.Transactions.Where(t => t.AccountId == account.Id).ToListAsync();
                        model.InsertdDate = user.InsertdDate;
                        model.UpdatedDate = user.UpdateDate;
                        SendInfoUsers.Add(model);
                    }
                }
            }
            return SendInfoUsers.ToList();
        }
    }

}
