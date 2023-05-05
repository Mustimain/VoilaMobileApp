using System;
namespace VoilaMobileApp.Src.Models
{
    public class OrderClaim
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int NumberOfProduct { get; set; }
        public string ProductId { get; set; }
    }
}

