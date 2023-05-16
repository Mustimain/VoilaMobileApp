using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class OrderClaim : IEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int NumberOfProduct { get; set; }
        public string ProductId { get; set; }
    }
}

