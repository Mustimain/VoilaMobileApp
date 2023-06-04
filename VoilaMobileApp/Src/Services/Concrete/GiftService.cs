using System;
using System.Net;
using Android.Locations;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using VoilaMobileApp.Src.Database.DbContext;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Services.Interfaces;

namespace VoilaMobileApp.Src.Services.Concrete
{
    public class GiftService : IGiftService
    {
        private readonly FirebaseClient client;

        public GiftService()
        {
            client = DbContext.StartFirebaseClient();

        }

        public async Task<bool> AddGiftCardAsync(GiftCard giftCard)
        {
            try
            {
                await client.Child("GiftCards").PostAsync(JsonConvert.SerializeObject(giftCard));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckGiftCode(string giftCode)
        {
            var allGiftCards = await GetAllGiftCardsAsync();
            foreach (var gift in allGiftCards)
            {
                if (gift.GiftCode.ToLower() == giftCode.ToLower())
                {
                    return true;
                }

            }
            return false;
        }

        public async Task<bool> DeleteGiftCardAsync(string giftId)
        {
            try
            {
                var toDeleteGift = (await client.Child("GiftCards")
                .OnceAsync<Customer>()).Where(a => a.Object.Id == giftId).FirstOrDefault();

                await client
                    .Child("GiftCards")
                    .Child(toDeleteGift.Key)
                    .DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<GiftCard>> GetAllGiftCardsAsync()
        {
            try
            {
                var result = (await client.Child("GiftCards")
                    .OnceAsync<GiftCard>()).Select(gft => new GiftCard
                    {
                        Id = gft.Object.Id,
                        CustomerId = gft.Object.CustomerId,
                        GiftAmount = gft.Object.GiftAmount,
                        GiftCode = gft.Object.GiftCode,
                        IsUsed = gft.Object.IsUsed,
                        LastUseDate = gft.Object.LastUseDate


                    }).ToList();


                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GiftCard>> GetAllMyGiftCards()
        {
            var allGiftCards = await GetAllGiftCardsAsync();
            var result = allGiftCards.Where(gft => gft.CustomerId == Utilts.CustomerInfo.CurrentCustomer.Id).ToList();
            return result;
        }

        public async Task<bool> UpdateGiftCardAsync(GiftCard giftCard)
        {
            try
            {
                var toUpdateGift = (await client
                 .Child("GiftCards")
                 .OnceAsync<Customer>()).Where(a => a.Object.Id == giftCard.Id).FirstOrDefault();

                await client
                .Child("GiftCards")
                .Child(toUpdateGift.Key)
                .PutAsync(toUpdateGift);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

