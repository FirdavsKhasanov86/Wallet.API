using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IAuthorization
    {
        Task<string> AuthorizationRequest(AuthorizationRequest authorizationRequest1);
    }
}
