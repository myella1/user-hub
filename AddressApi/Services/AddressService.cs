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
        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            _logger.LogInformation($"Retrieving all addresses");
            await Task.Delay(5000);
            return _addresses.AsEnumerable();
        }

        public async Task<Address?> GetAddressAsync(int addressId)
        {
            using (_logger.BeginScope(new Dictionary<string, object> { ["AddressId"] = addressId }))
            {
                _logger.LogInformation("Retrieving Address info.");

                await Task.Delay(1000);
                var address = _addresses.FirstOrDefault(x => x.AddressId == addressId);

                if (address == null)
                {
                    _logger.LogWarning("Address not found.");
                }

                return address;
            }
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            var maxIdValue = _addresses.Select(x => x.AddressId).AsEnumerable().Distinct().Max();
            address.AddressId = maxIdValue + 1;

            using (_logger.BeginScope(new Dictionary<string, object> { ["AddressId"] = address.AddressId }))
            {
                await Task.Delay(1000);
                _addresses.Add(address);

                _logger.LogInformation("Address created successfully.");
            }

            return address;
        }
    }
}
