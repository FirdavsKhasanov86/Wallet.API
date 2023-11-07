namespace Wallet.API.DTO_s
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
