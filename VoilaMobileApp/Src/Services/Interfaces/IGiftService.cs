using System;
using VoilaMobileApp.Src.Models;

namespace VoilaMobileApp.Src.Services.Interfaces
{
    public interface IGiftService
    {
        Task<bool> AddGiftCardAsync(GiftCard giftCard);
        Task<bool> UpdateGiftCardAsync(GiftCard giftCard);
        Task<bool> DeleteGiftCardAsync(string giftId);
        Task<List<GiftCard>> GetAllGiftCardsAsync();
        Task<List<GiftCard>> GetAllMyGiftCards();
        Task<bool> CheckGiftCode(string giftCode);



    }
}

