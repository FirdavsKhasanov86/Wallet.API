using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet.API.Data;
using Wallet.API.DTO_s;
using Wallet.API.Interface;
using Wallet.API.Models;
using Wallet.API.Models.Enums;

namespace Wallet.API.Repository
{
    public class debitBankAccountRepository : IDebitBankAccount
    {
        private readonly WalletContext _context;


        public debitBankAccountRepository(WalletContext context)
        {
            _context = context;
        }
        public async Task<string> DebitBankAccountAsync([FromBody] BankAccountRequest bankAccountRequest)
        {
            var account = await _context.Accounts.Where(x => x.UserId == bankAccountRequest.UserId).FirstOrDefaultAsync();

            if (account == null)
            {
                return "This Account not found";
            }

            _context.Transactions.Add(new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Amount = bankAccountRequest.Amount,
                InsertDate = DateTime.Now,
                TransactionType = TransactionType.Debit
            });

            try
            {
                var userAutorizedStatus = await _context.Users.Where(x => x.Id == bankAccountRequest.UserId).FirstOrDefaultAsync();
                if (userAutorizedStatus.AuthorizationStatus == AuthorizationStatus.Unauthorized)
                {
                    if (bankAccountRequest.Amount <= 10000)
                    {
                        await _context.SaveChangesAsync();
                        var balance = account.Transactions.OrderBy(p => p.InsertDate).Select(p =>
                        {
                            if (p.TransactionType == TransactionType.Credit)
                            {
                                return p.Amount * -1;
                            }
                            return p.Amount;
                        }).Sum();
                    }
                    else { return "You cannot top up your balance over 10,000 somoni.Since you did not pass authorization successfully."; }
                }
                else
                {
                    if (userAutorizedStatus.AuthorizationStatus == AuthorizationStatus.Authorized)
                    {
                        if (bankAccountRequest.Amount <= 100000)
                        {
                            await _context.SaveChangesAsync();
                            var balance = account.Transactions.OrderBy(p => p.InsertDate).Select(p =>
                            {
                                if (p.TransactionType == TransactionType.Credit)
                                {
                                    return p.Amount * -1;
                                }
                                return p.Amount;
                            }).Sum();
                        }
                        else { return "The maximum limit of replenishment of your wallet is 100 000 somoni"; }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return "Your balance has been successfully replenished";
        }
    }
}
