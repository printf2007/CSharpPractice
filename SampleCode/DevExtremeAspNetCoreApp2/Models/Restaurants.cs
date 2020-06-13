using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExtremeAspNetCoreApp2.Models
{
    public partial class Restaurants
    {
            [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Cuisine { get; set; }
    }
}
