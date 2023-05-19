using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface IAddressService
    {
        Task<bool> AddAddressAsync(Address address);
        Task<bool> UpdateAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(string addressId);
        Task<List<Address>> GetAllAddressAsync();
        Task<Address> GetAddressByIdAsync(string Id);
    }
}

