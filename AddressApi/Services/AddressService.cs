using AddressApi.Models;

namespace AddressApi.Services
{
    public class AddressService : IAddressService
    {
        private readonly ILogger<AddressService> _logger;
        private static List<Address> _addresses = new() {
                                                            new Address { AddressId=1, UserId = 1, Address1="5214 Madison Ave", Address2="", City="Chicago", State="IL", PostalCode="50678", AddressType=AddressType.Billing},
                                                            new Address { AddressId=2, UserId = 1, Address1="5214 Madison Ave", Address2="", City="Chicago", State="IL", PostalCode="50678", AddressType=AddressType.Shipping},
                                                            new Address { AddressId=3, UserId = 2, Address1="100 Bourbon Ave", Address2="", City="Detroit", State="MI", PostalCode="48678", AddressType=AddressType.Billing},
                                                            new Address { AddressId=4, UserId = 2, Address1="100 Bourbon Ave", Address2="", City="Detroit", State="MI", PostalCode="48678", AddressType=AddressType.Shipping},
                                                        };
        public AddressService(ILogger<AddressService> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<Address>> GetAddresses()
        {            
            _logger.LogInformation($"Retrieving address");

            await Task.Delay(5000);
            return _addresses;
        }

        public async Task<Address?> GetAddress(int addressId)
        {
            var loggingScope = new Dictionary<string, object>
            {
                ["AddressId"] = addressId
            };
            using var _ = _logger.BeginScope(loggingScope);
            _logger.LogInformation($"Retrieving address for the addressId : {addressId}");

            await Task.Delay(5000);
            var address = _addresses.Where(x=>x.AddressId == addressId).FirstOrDefault();
            return address;
        }

        public async Task<Address> CreateAddress(Address address)
        {
            var loggingScope = new Dictionary<string, object>
            {
                ["UserId"] = address.UserId
            };
            using var _ = _logger.BeginScope(loggingScope);
            _logger.LogInformation($"Created address for the user: {address.UserId}");

            await Task.Delay(10000);
            _addresses.Add(address);
            return address;
        }
    }
}
