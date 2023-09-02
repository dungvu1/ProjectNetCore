using Microsoft.EntityFrameworkCore;

namespace DOAN.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {

        }
        public DbSet<CategoryEnity> CategoryEnities { get; set; }
        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<RolesEntity> RolesEntities { get; set; }
        public DbSet<AccountsEntity> AccountsEntities { get; set; }
        public DbSet<OrdersEntity> OrdersEntities { get; set;}
        public DbSet<OrderDetailsEntity> OrderDetailsEntities { get; set; }
        public DbSet<PayEntity> PayEntities { get; set; }
        public DbSet<StatusEntity> StatusEntities { get; set; }
    }
    
}
