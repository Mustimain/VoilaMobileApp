using System;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly FirebaseClient client;
        public CustomerService()
        {
            client = DbContext.StartFirebaseClient();

        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var allCustomer = await GetAllCustomersAsync();
            foreach (var cus in allCustomer)
            {
                if (cus.Email.ToLower() == email.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {

                var result = (await client.Child("Customers").OnceAsync<Customer>()).Select(customer => new Customer
                {
                    Id = customer.Object.Id,
                    FirstName = customer.Object.FirstName,
                    LastName = customer.Object.LastName,
                    Email = customer.Object.Email,
                    Password = customer.Object.Password,
                    PhoneNumber = customer.Object.PhoneNumber,
                    Gender = customer.Object.Gender,
                    BirthDate = customer.Object.BirthDate,
                    EmailVerification = customer.Object.EmailVerification,
                    PhoneVerification = customer.Object.PhoneVerification,
                    RegisterDate = customer.Object.RegisterDate

                }).ToList();


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {

            var customerList = await GetAllCustomersAsync();
            var user = customerList.Where(cst => cst.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (user != null)
            {
                return user;

            }
            else
            {
                return null;
            }

        }

        public async Task<bool> LoginCusomterAsync(string email, string password)
        {

            var user = await GetCustomerByEmailAsync(email);
            if (user != null && user.Password == password)
            {
                Utilts.CustomerInfo.CurrentCustomer = user;
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<bool> RegisterCustomerAsync(Customer customer)
        {
            try
            {
                await client.Child("Customers").PostAsync(JsonConvert.SerializeObject(customer));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var toUpdateCustomer = (await client
                 .Child("Customers")
                 .OnceAsync<Customer>()).Where(a => a.Object.Id == customer.Id).FirstOrDefault();

                await client
                .Child("Customers")
                .Child(toUpdateCustomer.Key)
                .PutAsync(customer);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

