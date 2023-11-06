namespace Wallet.API.Interface
{
    public interface IGetUserAuthorizedAccount
    {
        Task<string> GetUserAuthorizedAccountAsync(Guid userId);
    }
}
