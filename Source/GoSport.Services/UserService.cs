using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoSport.Data.Models;
using GoSport.Data.Common.Repository;

namespace GoSport.Services
{
    public class UserService : IUserService
    {
        private IDeletableEntityRepository<User> userService;

        public UserService(IDeletableEntityRepository<User> userService)
        {
            this.userService = userService;
        }

        public IQueryable<User> GetAll()
        {
            return this.userService.All();
        }

        public User GetUserById(string id)
        {
            return this.userService.All().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetUsersWithConcreteInterests(IEnumerable<SportCategory> categories)
        {
            throw new NotImplementedException();
        }
    }
}
