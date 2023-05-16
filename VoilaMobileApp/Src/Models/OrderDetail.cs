using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class OrderDetail : IEntity
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
        public Address address { get; set; }

    }
}

