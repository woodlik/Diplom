using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSport.Client.ViewModels.Advanced
{
    public class SearchViewModel
    {
        public int City { get; set; }

        public int Neighborhood { get; set; }

        public string SportCategories { get; set; }
    }
}