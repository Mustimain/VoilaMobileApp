using System;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class CustomerDetail : IEntity
    {
        public Customer Customer { get; set; }
        public List<Address> Address { get; set; }

    }
}

