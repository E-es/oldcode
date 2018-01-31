using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using HR.WebApi.Data.Entities;

namespace HR.WebApi.Data.Contexts
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext():base("AuthContext")
        {
            
        }
        public System.Data.Entity.DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}