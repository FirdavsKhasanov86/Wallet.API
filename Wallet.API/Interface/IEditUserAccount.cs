using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IEditUserAccount
    {
        Task<string> EditUserAccountAsync(Guid userId, EditUsersAccount editUsersAccount);
    }
}
