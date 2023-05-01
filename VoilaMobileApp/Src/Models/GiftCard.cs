using System;
using VoilaMobileApp.Src.CustomControls;

namespace VoilaMobileApp.Src.Models
{
    public class GiftCard
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public double GiftAmount { get; set; }
        public bool IsUsed { get; set; }
        public DateTime LastUseDate { get; set; }
        public string GiftCode { get; set; }

    }
}

