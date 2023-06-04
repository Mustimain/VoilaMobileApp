using System;
namespace VoilaMobileApp.Src.Models
{
    public class Payment
    {
        public Address address { get; set; }
        public string GiftCode { get; set; }
        public string CardNo { get; set; }
        public string CardOwnerName { get; set; }
        public string CardDayMonth { get; set; }
        public string CardCvv { get; set; }

    }
}

