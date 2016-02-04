using IdentitySample.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using Velvel.Domain.Users;

namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureAutoMapper();
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
            //ApplicationDbInitializer.InitializeIdentityForEF(new ApplicationDbInitializer());
        }
    }
}
