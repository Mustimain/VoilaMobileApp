using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class Address : IEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressTitle { get; set; }
        public string OpenAddress { get; set; }
    }
}

