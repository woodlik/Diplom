using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSport.Client.ViewModels
{
    public class AddressViewModel : IMapFrom<Address>
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }
    }
}