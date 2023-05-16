using System;
using VoilaMobileApp.Src.Models.Enums;
using VoilaMobileApp.Src.Models.Interface;

namespace VoilaMobileApp.Src.Models
{
    public class Category : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}

