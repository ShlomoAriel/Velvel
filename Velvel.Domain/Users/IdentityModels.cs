using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Data;
using Velvel.Domain.Managers;
using Velvel.Domain.Projects;
using Velvel.Domain.Tables.ChangesAndAdditions;
using Velvel.Domain.Tables.Defects;
using Velvel.Domain.Tables.Floorings;
using Velvel.Domain.Tables.Plumbings;
using Velvel.Domain.Users.Customers;
using Velvel.Domain.Users.Managers;

namespace Velvel.Domain.Users
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            //var userId = UserId;
            //if (userId != null) userIdentity.AddClaim(new Claim("UserId", userId.Value.ToString()));

            return userIdentity;
        }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Manager Manager { get; set; }

        public int ?ManagerId { get; set; }
        public int ?CustomerId { get; set; }
        //public int? UserId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Manager>().ToTable("Managers");
        //    modelBuilder.Entity<Customer>().ToTable("Customers");
        //    modelBuilder.Entity<Manager>().
        //                    HasMany(c => c.Projects).
        //                    WithMany(p => p.Managers).
        //                    Map(m =>
        //                    {
        //                        m.MapLeftKey("ManagerId");
        //                        m.MapRightKey("ProjectId");
        //                        m.ToTable("ManagerProjects");
        //                    });
        //    modelBuilder.Entity<Customer>().
        //                    HasMany(c => c.Projects).
        //                    WithMany(p => p.Customers).
        //                    Map(m =>
        //                    {
        //                        m.MapLeftKey("CustomerId");
        //                        m.MapRightKey("ProjectId");
        //                        m.ToTable("CustomerProjects");
        //                    });
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<Changes> Changes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Grout> Grouts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentGroup> CommentGroups { get; set; }
        public DbSet<Model> Models { get; set; }
        //public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Flooring> Floorings { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Status> States { get; set; }
        //public DbSet<File> Files { get; set; }
        public DbSet<Plumbing> Plumbings { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<MeasurementUnit> UnitType { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            //Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}