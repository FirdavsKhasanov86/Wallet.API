using System.ComponentModel.DataAnnotations;

namespace Wallet.API.DTO_s
{
    public class BankAccountRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
