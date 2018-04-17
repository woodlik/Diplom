using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoSport.Client.ViewModels.SportCenters
{
    public class SportCenterViewModel : IMapFrom<SportCenter>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ExactAddress { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public SportCenterRating Rating { get; set; }

        public ICollection<string> Images { get; set; }

        public ICollection<SportCategory> Categories { get; set; }
       
        public  ICollection<Comment> Comments { get; set; }
    }
}