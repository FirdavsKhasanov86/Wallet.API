using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IHistoryCheckBalanceAccounts
    {
        Task<IEnumerable<TransactionResponse>> HistoryCheckBalanceAccountsAsync(Guid userId);
    }
}
