using GoSport.Client.Infrastructure.Filters;
using GoSport.Client.Infrastructure.Mapping;
using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoSport.Client.ViewModels.SportCenters
{
    public class AddSportCenterViewModel : IMapTo<SportCenter>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Exact Address")]
        public string ExactAddress { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [ValidPhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "Sport Center categories")]
        public string SportCategories { get; set; }

        public HttpPostedFileBase UplodadedImage1 { get; set; }

        public HttpPostedFileBase UplodadedImage2 { get; set; }

        public HttpPostedFileBase UplodadedImage3 { get; set; }

        public HttpPostedFileBase UplodadedImage4 { get; set; }

        public HttpPostedFileBase UplodadedImage5 { get; set; }

        public HttpPostedFileBase UplodadedImage6 { get; set; }

        public HttpPostedFileBase UplodadedImage7 { get; set; }

        public HttpPostedFileBase UplodadedImage8 { get; set; }

        public HttpPostedFileBase UplodadedImage9 { get; set; }
    }
}