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
        private readonly IGetAllInfoAboutUsers _getAllUsersRepo;
        public WalletController(IAuthorization authorRepo, IGetAllInfoAboutUsers getAllUsersRepo)
        {
            _authorRepo = authorRepo;
            _getAllUsersRepo = getAllUsersRepo;
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

        [HttpGet]
        [Route("GetAllInfoAboutUsers")]
        public async Task<IEnumerable<SendAllUsers>> GetAllInfoAboutUsers()
        {

            var getAllUsers = await _getAllUsersRepo.GetAllInfoAboutUsersAsync();
            return getAllUsers.ToList();

        }
    }
}
