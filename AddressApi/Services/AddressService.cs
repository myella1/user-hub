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
        //public async Task<IEnumerable<Address>> GetAddress(int userId)
        public async Task<IEnumerable<Address>> GetAddress()
        {
            //var loggingScope = new Dictionary<string, object>
            //{
            //    ["UserId"] = userId
            //};
            //using var _ = _logger.BeginScope(loggingScope);
            //_logger.LogInformation($"Retrieving address for the user: {userId}");
            
            await Task.Delay(500);
            return _addresses;
                //.Where(x => x.UserId == userId).AsEnumerable();
        }

        public async Task<Address> CreateAddress(Address address)
        {
            var loggingScope = new Dictionary<string, object>
            {
                ["UserId"] = address.UserId
            };
            using var _ = _logger.BeginScope(loggingScope);
            _logger.LogInformation($"Created address for the user: {address.UserId}");

            await Task.Delay(1000);
            _addresses.Add(address);
            return address;
        }
    }
}
