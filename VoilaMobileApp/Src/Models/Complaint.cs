using System;
namespace VoilaMobileApp.Src.Models
{
    public class Complaint
    {
        public string Id { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

