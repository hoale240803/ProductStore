using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.RestfulAPI.DataModels
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCategoryParrent { get; set; }
        public string Alias { get; set; }
    }
}
