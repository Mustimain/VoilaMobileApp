using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class OrderDetail : IEntity
    {
        public Order Order { get; set; }
        public List<BasketProductModel> BasketProductModelList { get; set; }

    }
}

