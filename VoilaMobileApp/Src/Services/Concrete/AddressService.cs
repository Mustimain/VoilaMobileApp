using System;
using System.Net;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly FirebaseClient client;

        public AddressService()
        {
            client = DbContext.StartFirebaseClient();

        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            try
            {
                await client.Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id).PostAsync(JsonConvert.SerializeObject(address));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAddressAsync(string addressId)
        {
            try
            {
                var toUpdateAddress = (await client.Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                .OnceAsync<Customer>()).Where(a => a.Object.Id == addressId).FirstOrDefault();

                await client
                    .Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                    .Child(toUpdateAddress.Key)
                    .DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<Address> GetAddressByIdAsync(string Id)
        {
            var allAddress = await GetAllAddressAsync();
            var result = allAddress.Where(adr => adr.Id == Id).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Address>> GetAllAddressAsync()
        {
            try
            {
                var result = (await client.Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                    .OnceAsync<Address>()).Select(adrs => new Address
                    {

                        Id = adrs.Object.Id,
                        AddressTitle = adrs.Object.AddressTitle,
                        City = adrs.Object.City,
                        CustomerId = adrs.Object.CustomerId,
                        District = adrs.Object.District,
                        OpenAddress = adrs.Object.OpenAddress

                    }).ToList();


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAddressAsync(Address address)
        {
            try
            {
                var toUpdateAddress = (await client
                 .Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                 .OnceAsync<Customer>()).Where(a => a.Object.Id == address.Id).FirstOrDefault();

                await client
                .Child("Address").Child(Utilts.CustomerInfo.CurrentCustomer.Id)
                .Child(toUpdateAddress.Key)
                .PutAsync(address);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

