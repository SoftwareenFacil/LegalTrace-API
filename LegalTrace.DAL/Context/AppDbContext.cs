using LegalTrace.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace LegalTrace.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientHistory> ClientHistory { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<StandardTask> StandardTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminUser = new User
            {
                Id = 1,
                Name = "admin",
                Email = "admin@admin.cl",
                Password = HashPassword("admin1234"),
                Phone = 0,
                SuperAdmin = true,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Vigency = true
            };

            modelBuilder.Entity<User>().HasData(adminUser);
        }
        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000); 
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
    }
}
