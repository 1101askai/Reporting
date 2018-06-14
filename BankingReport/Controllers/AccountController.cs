using BankingReport.DbContexts;
using BankingReport.Helpers;
using BankingReport.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BankingReport.Controllers
{
    /// <summary>
    /// Class AccountController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AccountController : ApiController
    {
        /// <summary>
        /// The API common
        /// </summary>
        private ApiCommon _apiCommon = null;

        /// <summary>
        /// The database
        /// </summary>
        private readonly DbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            _apiCommon = new ApiCommon();
            _db = new DbContext();
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="userModel">The <see cref="User"/> model.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(User userModel)
        {
            if (!userModel.IsNameValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""User name can't be blank.""}");
            if (!userModel.IsPasswordValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Password should be at least 8 symbols and must contain at least one number and one symbol.""}");
            if (!userModel.IsEmailValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid email address.""}");
            if (!userModel.IsCityValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid city.""}");
            if (!userModel.IsAddressValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid address.""}");
            if (!userModel.IsPostalCodeValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid postal code.""}");
            if (!userModel.IsCountryValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid country.""}");
            if (!userModel.IsPhoneNumberValid())
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid phone number.""}");

            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Email,
                Email = userModel.Email,
                EmailConfirmed = true,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
            };
            var userManager = ApiCommon.GetUserManager(this.Request);

            var existingAccount = userManager.FindByName(user.UserName);
            if (existingAccount != null && existingAccount.Id != user.Id)
                return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Email already exists.""}");
            IdentityResult result = await userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var internalUser = new User
                {
                    AspNetUserId = user.Id,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    Email = userModel.Email,
                    CreationTime = DateTime.Now,
                    Address = userModel.Address,
                    City = userModel.City,
                    Country = userModel.Country,
                    PhoneNumber = userModel.PhoneNumber,
                    PostalCode = userModel.PostalCode
                };
                _db.Users.Add(internalUser);
                await _db.SaveChangesAsync();
                var access_token = _apiCommon.GetOwinToken(user);
                return
                    Json(
                        new
                        {
                            access_token
                        });
            }

            return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""User can't be registered.""}");
        }

        /// <summary>
        /// Logins user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="pass">The password.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> UserLogin(string email, string pass)
        {
            var userManager = ApiCommon.GetUserManager(this.Request);
            var identityUser = await userManager.FindAsync(email, pass);
            if (identityUser != null)
            {
                var access_token = _apiCommon.GetOwinToken(identityUser);

                return
                        Json(
                            new
                            {
                                access_token
                            });
            }
            return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""User doesn't exist.""}");
        }

        /// <summary>
        /// Changes the user information.
        /// </summary>
        /// <param name="model">The <see cref="User"/> model.</param>
        /// <returns>Task&lt;IHttpActionResult&gt;.</returns>
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> ChangeUserInfo(User model)
        {
            var user = ApiCommon.GetUser(HttpContext.Current.User.Identity, _db);
            if (user != null)
            {
                if (!model.IsNameValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""User name can't be blank.""}");
                if (!model.IsCityValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid city.""}");
                if (!model.IsAddressValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid address.""}");
                if (!model.IsPostalCodeValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid postal code.""}");
                if (!model.IsCountryValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid country.""}");
                if (!model.IsPhoneNumberValid())
                    return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid phone number.""}");

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.City = model.City;
                user.Address = model.Address;
                user.PostalCode = model.PostalCode;
                user.Country = model.Country;
                user.PhoneNumber = model.PhoneNumber;
                await _db.SaveChangesAsync();
                return Ok();
            }
            return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""User doesn't exist.""}");
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _apiCommon.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}