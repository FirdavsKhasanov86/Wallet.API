﻿using Microsoft.AspNetCore.Http;
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
        private readonly IDebitBankAccount _debBankAccountRepo;
        private readonly IHistoryCheckBalanceAccounts _histCheckBalAccRepo;
        public WalletController(IAuthorization authorRepo, IGetAllInfoAboutUsers getAllUsersRepo,
            IEditUserAccount editUserAccountRepo, IGetUserAuthorizedAccount getUserAuthorizedAccountRepo,
            IAuthenticationUser authUserRepo, IDebitBankAccount debBankAccountRepo,
            IHistoryCheckBalanceAccounts histCheckBalAccRepo)
        {
            _authorRepo = authorRepo;
            _getAllUsersRepo = getAllUsersRepo;
            _editUserAccountRepo = editUserAccountRepo;
            _getUserAuthorizedAccountRepo = getUserAuthorizedAccountRepo;
            _authUserRepo = authUserRepo;
            _debBankAccountRepo = debBankAccountRepo;
            _histCheckBalAccRepo = histCheckBalAccRepo;
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

        [HttpPost]
        [Route("debit")]
        public async Task<IActionResult> DebitBankAccount([FromBody] BankAccountRequest bankAccountRequest)
        {
            if (bankAccountRequest == null) { return NotFound(); }

            return Ok(await _debBankAccountRepo.DebitBankAccountAsync(bankAccountRequest));
        }


        [HttpGet]
        [Route("HistoryCheckBalance/{userId}")]
        public async Task<IEnumerable<TransactionResponse>> HistoryCheckBalanceAccounts(Guid userId)
        {
            var getHistCheckBalAccnt = await _histCheckBalAccRepo.HistoryCheckBalanceAccountsAsync(userId);
            return getHistCheckBalAccnt;
        }
    }
}
