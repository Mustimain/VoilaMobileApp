using System;
namespace VoilaMobileApp.Src.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string AddressId { get; set; }
        public string GiftCode { get; set; }

    }
}

