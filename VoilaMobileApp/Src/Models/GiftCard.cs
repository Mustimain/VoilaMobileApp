using System;
using VoilaMobileApp.Src.CustomControls;
using VoilaMobileApp.Src.Models.Enums;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class GiftCard : IEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public double GiftAmount { get; set; }
        public GiftStatusType IsUsed { get; set; }
        public DateTime LastUseDate { get; set; }
        public string GiftCode { get; set; }

    }
}

