using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class ProductDetail : IEntity
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}

