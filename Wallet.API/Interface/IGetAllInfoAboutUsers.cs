using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IGetAllInfoAboutUsers
    {
        Task<IEnumerable<SendAllUsers>> GetAllInfoAboutUsersAsync();
    }
}
