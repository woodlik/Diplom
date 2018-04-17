using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;

namespace GoSport.Client.ViewModels.Users
{
    public class UserProfileViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [HiddenInput]
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Detailed address")]
        public string ExactAddress { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        [Display(Name = "Facebook profile")]
        public string Facebook { get; set; }

        public string AvatarUrl { get; set; }

        [Display(Name = "Upload new picture")]
        public HttpPostedFileBase UplodadedImage { get; set; }

        [Display(Name = "About Me")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

        [Display(Name = "Favourite sports")]
        public string SportCategories { get; set; }

        [Display(Name = "Favourite sports")]
        public IEnumerable<SportCategory> FavouriteCategories { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<Message> OwnMessages { get; set; }

        public IEnumerable<Message> VisitorMessages { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserProfileViewModel>()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.Address.City));
            configuration.CreateMap<User, UserProfileViewModel>()
                .ForMember(x => x.Neighborhood, opt => opt.MapFrom(x => x.Address.Neighborhood));
        }
    }
}