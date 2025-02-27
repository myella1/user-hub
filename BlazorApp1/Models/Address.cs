namespace UserApiClient.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public required int UserId { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required AddressType AddressType { get; set; }
    }

    public enum AddressType
    {
        Billing,
        Shipping
    }
}
