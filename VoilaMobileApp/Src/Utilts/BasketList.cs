using System;
using VoilaMobileApp.Src.Models;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Utilts
{
    public static class BasketList
    {
        private static List<BasketProductModel> _globalBasketList = new List<BasketProductModel>();
        public static List<BasketProductModel> GlobalBasketList
        {
            get
            {
                return _globalBasketList;
            }
            set
            {
                _globalBasketList = value;
            }
        }

    }
}

