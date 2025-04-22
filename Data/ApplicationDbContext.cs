using Microsoft.EntityFrameworkCore;
using MOBILE_PHONE.Models;

namespace MOBILE_PHONE.Data
{
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options )
            : base(options) {  }

        public DbSet<Users> Users { get; set; }
        
    }
}
