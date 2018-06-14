using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankingReport.Models
{
    /// <summary>
    /// Class ApplicationUser is used for identity of user.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Generate user identity as an asynchronous operation.
        /// </summary>
        /// <param name="manager">The <see cref="ApplicationUser"/> manager.</param>
        /// <returns>
        /// <see cref="ClaimsIdentity"/>.
        /// </returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            if (FirstName != null && LastName != null)
            {
                userIdentity.AddClaim(new Claim("FirstName", FirstName));
                userIdentity.AddClaim(new Claim("LastName", LastName));
            }


            using (DbContexts.DbContext _db = new DbContexts.DbContext())
            {
                User u = _db.Users.FirstOrDefault(user => user.AspNetUserId == Id);
                if (u != null)
                {
                    userIdentity.AddClaim(new Claim("InternalUserId", u.UserId.ToString()));
                    userIdentity.AddClaim(new Claim("EmailConfirm", EmailConfirmed.ToString()));
                }
            }
            return userIdentity;
        }
    }

    /// <summary>
    /// Class ApplicationDbContext is used to interact with the database.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext{BankingReport.Models.ApplicationUser}" />
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ApplicationDbContext.</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}