using System.ComponentModel.DataAnnotations;
using Wallet.API.Models.Enums;

namespace Wallet.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        public AuthorizationStatus AuthorizationStatus { get; set; }
   
        public DateTime InsertdDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual Account Account { get; set; }
    }
}
