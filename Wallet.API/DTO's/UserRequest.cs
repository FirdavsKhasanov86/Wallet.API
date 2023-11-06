using System.ComponentModel.DataAnnotations;

namespace Wallet.API.DTO_s
{
    public class UserRequest
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;
    }
}
