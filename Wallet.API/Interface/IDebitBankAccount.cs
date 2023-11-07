using Microsoft.AspNetCore.Mvc;
using Wallet.API.DTO_s;

namespace Wallet.API.Interface
{
    public interface IDebitBankAccount
    {
        Task<string> DebitBankAccountAsync([FromBody] BankAccountRequest bankAccountRequest);
    }
}
