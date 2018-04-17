using GoSport.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Data.Models
{
    public class Address : AuditInfo, IDeletableEntity
    {
        private ICollection<User> users;
        private ICollection<SportCenter> sportCenters;

        public Address()
        {
            this.users = new HashSet<User>();
            this.sportCenters = new HashSet<SportCenter>();
        }

        public int Id { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<SportCenter> SportCenters
        {
            get { return this.sportCenters; }
            set { this.sportCenters = value; }
        }
    }
}
