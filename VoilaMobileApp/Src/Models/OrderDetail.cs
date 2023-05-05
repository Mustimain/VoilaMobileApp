using System;
namespace VoilaMobileApp.Src.Models
{
    public class OrderDetail
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
        public Address address { get; set; }

    }
}

