using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace GoSport.Client.Areas.Administration.ViewModels
{
    public class AdminAddressViewModel : IMapFrom<Address>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Neighbour { get; set; }

        public string City { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Address, AdminAddressViewModel>()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.City));
            configuration.CreateMap<Address, AdminAddressViewModel>()
                .ForMember(x => x.Neighbour, opt => opt.MapFrom(x => x.Neighborhood));
        }
    }
}