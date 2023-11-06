using Microsoft.AspNetCore.Mvc;
using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IAuthenticationUser
    {
        Task<string> AuthenticationUserAsync([FromBody] UserRequest userRequest);
    }
}
