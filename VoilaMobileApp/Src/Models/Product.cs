using System;
namespace VoilaMobileApp.Src.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public bool IsStock { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }

    }
}

