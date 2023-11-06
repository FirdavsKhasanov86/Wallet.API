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
        private readonly IEditUserAccount _editUserAccountRepo;
        private readonly IGetUserAuthorizedAccount _getUserAuthorizedAccountRepo;
        private readonly IAuthenticationUser _authUserRepo;
        public WalletController(IAuthorization authorRepo, IGetAllInfoAboutUsers getAllUsersRepo,
            IEditUserAccount editUserAccountRepo, IGetUserAuthorizedAccount getUserAuthorizedAccountRepo,
            IAuthenticationUser authUserRepo)
        {
            _authorRepo = authorRepo;
            _getAllUsersRepo = getAllUsersRepo;
            _editUserAccountRepo = editUserAccountRepo;
            _getUserAuthorizedAccountRepo = getUserAuthorizedAccountRepo;
            _authUserRepo = authUserRepo;
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

        [HttpPut]
        [Route("EditUserAccount/{userId}")]
        public async Task<IActionResult> EditUserAccount(Guid userId, EditUsersAccount editUsersAccount)
        {

            var editUsers = await _editUserAccountRepo.EditUserAccountAsync(userId, editUsersAccount);
            return Ok(editUsers);

        }

        [HttpGet]
        [Route("GetUserAuthorizedAccount/{userId}")]
        public async Task<IActionResult> GetUserAuthorizedAccount(Guid userId)
        {

            return Ok(await _getUserAuthorizedAccountRepo.GetUserAuthorizedAccountAsync(userId));
        }

        [HttpPost]
        [Route("authentication")]
        public async Task<IActionResult> AuthenticationUser([FromBody] UserRequest userRequest)
        {

            if (userRequest == null) { return NotFound(); }

            var userAuth = await _authUserRepo.AuthenticationUserAsync(userRequest);
            if (userAuth == null) { NotFound(); };
            return Ok(userAuth);

        }
    }
}
