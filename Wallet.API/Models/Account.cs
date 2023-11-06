using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
