using eCommerce.Core.DTOs.AddressDTO;

namespace eCommerce.API.ViewModels
{
    public class CustomerUpdateViewModel
    {
        public string Name { get; set; }
        public List<AddressCreateDto>? Addresses { get; set; }
    }
}
