using GoSport.Data.Common.Models;
using GoSport.Data.Common.Repository;
using GoSport.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Data.DataSeed
{
    public class DataSeeder
    {
        private ApplicationDbContext db;
        private UserManager<User> userManager;
        
        public DataSeeder(ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = new UserManager<User>(new UserStore<User>(db));
        }

        public void SeedRoles()
        {
            db.Roles.Add(new IdentityRole { Name = "Admin" });
        }

        public  void SeedCategories()
        {
            this.db.SportCategories.Add(new SportCategory() { Name = "Аэробика" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Тренажерный зал" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Йога" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Йогалатес" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Фитнесс" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Пилатес" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Шейпинг" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Танцы" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Бодифлекс" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Гимнастика для беременных" });
            this.db.SportCategories.Add(new SportCategory() { Name = "КроссФит" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Фенкциональный тренинг" });
            this.db.SportCategories.Add(new SportCategory() { Name = "Интервальный тренинг" });
        }

        public void SeedAddresses()
        {
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро Фрунзенская" });
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро пл.Ленина" });
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро Каменная горка" });
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро Уручье" });
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро Восток" });
            this.db.Addresses.Add(new Address() { City = "Минск", Neighborhood = "ст.метро Молодежная" });
        }
        
    }
}
