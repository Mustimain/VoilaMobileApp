using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<bool> RegisterCustomerAsync(Customer customer);
        Task<bool> LoginCusomterAsync(string email, string password);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> CheckEmailAsync(string email);
    }
}

