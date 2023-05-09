using System;
using VoilaMobileApp.Src.Models.Enums;

namespace VoilaMobileApp.Src.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}

