using GoSport.Data.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace GoSport.Data.Models
{
    public class SportCategory : AuditInfo, IDeletableEntity
    {
        private ICollection<SportCenter> sportCenters;
        private ICollection<User> users;

        public SportCategory()
        {
            this.sportCenters = new HashSet<SportCenter>();
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<SportCenter> SportCenters
        {
            get { return this.sportCenters; }
            set { this.sportCenters = value; }
        }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}