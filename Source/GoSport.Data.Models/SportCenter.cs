using GoSport.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoSport.Data.Models
{
    public class SportCenter : AuditInfo, IDeletableEntity
    {
        private ICollection<string> picturesUrls;
        private ICollection<SportCategory> categories;
        private ICollection<Comment> comments;

        public SportCenter()
        {
            this.picturesUrls = new HashSet<string>();
            this.comments = new HashSet<Comment>();
            this.categories = new HashSet<SportCategory>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ExactAddress { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public virtual SportCenterRating Rating { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string PicturesUrls { get; set; }

        public virtual ICollection<SportCategory> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
