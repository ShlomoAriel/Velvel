using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using System.Web;
using Velvel.Domain;
using Velvel.Domain.Attributes.Common;
using Velvel.Domain.Attributes.Rooms;
using Velvel.Domain.Users;
using Velvel.Domain.Tables.Floorings;
using Velvel.Domain.Tables.Plumbings;

namespace IdentitySample.Models
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db) {
            
            IList<Status> States = new List<Status>();

            States.Add(new Status() { Name = "נפתח"});
            States.Add(new Status() { Name = "אושר"});
            States.Add(new Status() { Name = "סוכם"});
            States.Add(new Status() { Name = "נסגר"});
            States.Add(new Status() { Name = "למחיקה" });
            foreach (Status state in States)
            {
                db.States.Add(state);
            }

            IList<MeasurementUnit> UnitTypes = new List<MeasurementUnit>();

            UnitTypes.Add(new MeasurementUnit() { Name = "יחידות" });
            UnitTypes.Add(new MeasurementUnit() { Name = "מטר" });
            UnitTypes.Add(new MeasurementUnit() { Name = "קילוגרם" });
            UnitTypes.Add(new MeasurementUnit() { Name = "ליטר" });
            foreach (MeasurementUnit measurement in UnitTypes)
            {
                db.UnitType.Add(measurement);
            }

            IList<Room> Rooms = new List<Room>();

            Rooms.Add(new Room() { Name = "מטבח" });
            Rooms.Add(new Room() { Name = "סלון" });
            Rooms.Add(new Room() { Name = "חדר שינה" });
            Rooms.Add(new Room() { Name = "שירותים" });
            foreach (Room room in Rooms)
            {
                db.Rooms.Add(room);
            }

            IList<Grout> Grouts = new List<Grout>();

            Grouts.Add(new Grout() { Name = "סטנדרט" });
            foreach (Grout grout in Grouts)
            {
                db.Grouts.Add(grout);
            }

            IList<Model> Models = new List<Model>();

            Models.Add(new Model() { Name = "סטנדרט" });
            foreach (Model model in Models)
            {
                db.Models.Add(model);
            }

            //IList<Measurement> Measurements = new List<Measurement>();

            //Measurements.Add(new Measurement() { Name = "סטנדרט" });
            //foreach (Measurement measurement in Measurements)
            //{
            //    db.Measurements.Add(measurement);
            //}

            IList<Accessory> Accessories = new List<Accessory>();

            Accessories.Add(new Accessory() { Name = "סטנדרט" });
            foreach (Accessory accessory in Accessories)
            {
                db.Accessories.Add(accessory);
            }


            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            string name = "admin@example.com";
            string password = "Admin@123456";
            string roleName = "Admin";
            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null) {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }
            var user = userManager.FindByName(name);
            if (user == null) {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name)) {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
           


            roleName = "Manager";
            role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            roleName = "Customer";
            role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //name = "micha@gmail.com";
            //password = "Micha@123456";
            //roleName = "Manager";
            //user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}
            //rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}


            //name = "gil@gmail.com";
            //password = "Gil@123456";
            //roleName = "Manager";
            //user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}
            //rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}



            //roleName = "Customer";
            //role = roleManager.FindByName(roleName);
            //if (role == null)
            //{
            //    role = new IdentityRole(roleName);
            //    var roleresult = roleManager.Create(role);
            //}



            
            //name = "roni@gmail.com";
            //password = "Roni@123456";
            //roleName = "Customer";
            //user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}
            //rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}


            //name = "shlomo@gmail.com";
            //password = "Shlomo@123456";
            //roleName = "Customer";
            //user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}
            //rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}

        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}