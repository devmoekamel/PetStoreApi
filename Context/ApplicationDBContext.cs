using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetStore.Model;

namespace PetStore.Context
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }


        public DbSet<Pet> pets { get; set; }
        public DbSet<Order> orders { get; set; }
  
    }
}
