using BankingReport.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(BankingReport.Startup))]
namespace BankingReport
{
    /// <summary>
    /// Class Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the o authentication server options.
        /// </summary>
        /// <value>The o authentication server options.</value>
        public static OAuthAuthorizationServerOptions OAuthServerOptions { get; private set; }

        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> application.</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        /// <summary>
        /// Configures the o authentication.
        /// </summary>
        /// <param name="app">The <see cref="IAppBuilder"/> application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/api/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                            TimeSpan.FromMinutes(1), (manager, user) => user.GenerateUserIdentityAsync(manager)),
                },
                CookieName = "BankingReportAuth"
            });
            
            OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}