using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.DTO_s;
using Wallet.API.Interface;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IAuthorization _authorRepo;
        public WalletController(IAuthorization authorRepo)
        {
            _authorRepo = authorRepo;
       
        }
        [HttpPost]
        [Route("authorization")]
        public async Task<IActionResult> Authorization(AuthorizationRequest authorizationRequest)
        {
            if (authorizationRequest == null)
            {
                return NotFound();
            }
            return Ok(await _authorRepo.AuthorizationRequest(authorizationRequest));

        }
    }
}
