using System;
namespace VoilaMobileApp.Src.Models
{
    public class BasketProductModel : BindableBase
    {
        public Product Product { get; set; }
        private int _productCount;
        public int ProductCount
        {
            get
            {
                return _productCount;
            }
            set
            {
                _productCount = value; RaisePropertyChanged();
            }
        }

    }
}

