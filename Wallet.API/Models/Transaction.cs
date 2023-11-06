using System.ComponentModel.DataAnnotations.Schema;
using Wallet.API.Models.Enums;

namespace Wallet.API.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual Account Account { get; set; }
    }
}
