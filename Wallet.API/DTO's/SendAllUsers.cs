using Wallet.API.Models.Enums;

namespace Wallet.API.DTO_s
{
    public class SendAllUsers
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public AuthorizationStatus AuthorizationStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime InsertdDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
