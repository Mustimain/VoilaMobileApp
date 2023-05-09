using System;
namespace VoilaMobileApp.Src.Models.GroupLists
{
    public class CategoryGroup : List<Category>
    {
        public string GroupName { get; private set; }

        public CategoryGroup(string name, List<Category> categories) : base(categories)
        {
            this.GroupName = name;
        }
    }
}

