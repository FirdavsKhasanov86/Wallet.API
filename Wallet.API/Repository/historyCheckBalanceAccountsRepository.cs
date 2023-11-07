using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;

namespace Wallet.API.Repository
{
    public class historyCheckBalanceAccountsRepository : IHistoryCheckBalanceAccounts
    {
        private readonly WalletContext _context;

        public historyCheckBalanceAccountsRepository(WalletContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TransactionResponse>> HistoryCheckBalanceAccountsAsync(Guid userId)
        {

            var query = from u in _context.Users
                        join a in _context.Accounts on u.Id equals a.UserId
                        join t in _context.Transactions on a.Id equals t.AccountId
                        where a.UserId == userId
                        select new
                        {
                            u.Id,
                            u.FirstName,
                            u.LastName,
                            a.Phone,
                            t.Amount,
                            t.InsertDate
                        };

            var TransacInfo = new List<TransactionResponse>();

            foreach (var item in query)
            {
                var model = new TransactionResponse();
                model.Id = item.Id;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.Phone = item.Phone;
                model.Amount = item.Amount;
                model.InsertDate = item.InsertDate;
                TransacInfo.Add(model);
            }

            return TransacInfo.ToList();
        }
    }
}
