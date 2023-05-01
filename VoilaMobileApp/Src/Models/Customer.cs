using System;
namespace VoilaMobileApp.Src.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool EmailVerification { get; set; }
        public bool PhoneVerification { get; set; }

    }
}

