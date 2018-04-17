using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GoSport.Client.Infrastructure.Filters;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoSport.Client.Areas.Administration.ViewModels
{
    public class AdminSportCenterViewModel : IMapFrom<SportCenter>, IMapTo<SportCenter>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }
        
        public string ExactAddress { get; set; }
        
        public string City { get; set; }
        
        public string Neighbour  { get; set; }
        
        public string Description { get; set; }

        [ValidPhoneNumber]
        public string PhoneNumber { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<SportCenter, AdminSportCenterViewModel>()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.Address.City));
            configuration.CreateMap<SportCenter, AdminSportCenterViewModel>()
                .ForMember(x => x.Neighbour, opt => opt.MapFrom(x => x.Address.Neighborhood));
        }
    }
}