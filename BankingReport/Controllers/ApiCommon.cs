using BankingReport.DbContexts;
using BankingReport.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BankingReport.Controllers
{
    /// <summary>
    /// Class ApiCommon.
    /// </summary>
    public class ApiCommon : IDisposable
    {
        /// <summary>
        /// Authentications the repository.
        /// </summary>
        public ApiCommon()
        {
        }

         /// <summary>
        /// Gets access to UserManager.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ApplicationUserManager.</returns>
        public static ApplicationUserManager GetUserManager(HttpRequestMessage request)
        {
                var context = request.Properties["MS_HttpContext"] as HttpContextWrapper;
                return context.GetOwinContext().Get<ApplicationUserManager>();
        }

        /// <summary>
        /// Gets access to SignInManager.
        /// </summary>
        /// <param name="request">The <see cref="HttpRequestMessage"/> request.</param>
        /// <returns><see cref="ApplicationSignInManager"/>.</returns>
        public static ApplicationSignInManager GetSignInManager(HttpRequestMessage request)
        {
            var context = request.Properties["MS_HttpContext"] as HttpContextWrapper;
            return context.GetOwinContext().Get<ApplicationSignInManager>(); 
        }

        /// <summary>
        /// Gets the owin token.
        /// </summary>
        /// <param name="user">The <see cref="IdentityUser"/> user.</param>
        /// <returns>System.String.</returns>
        public string GetOwinToken(IdentityUser user)
        {
            var identity = new ClaimsIdentity("Password");
            identity.AddClaim(new Claim("UserId", user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            identity.AddClaim(new Claim("Time", DateTime.Now.AddSeconds(30).ToString()));
            AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            return "Bearer " + Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="identity">The <see cref="IIdentity"/>.</param>
        /// <param name="db">The <see cref="DbContext"/> database.</param>
        /// <returns><see cref="User"/>.</returns>
        public static User GetUser(IIdentity identity, DbContext db)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            var user = db.Users.FirstOrDefault(e => e.AspNetUserId == claim.Value);
            return user;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }
    }
}