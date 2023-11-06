using System.ComponentModel.DataAnnotations;

namespace Wallet.API.DTO_s
{
    public class AuthorizationRequest
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
