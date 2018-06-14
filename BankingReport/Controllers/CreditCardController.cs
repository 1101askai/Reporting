using BankingReport.DbContexts;
using BankingReport.Helpers;
using BankingReport.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BankingReport.Controllers
{
    public class CreditCardController : ApiController
    {
        /// <summary>
        /// The API common
        /// </summary>
        private ApiCommon _apiCommon = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreditCardController"/> class.
        /// </summary>
        public CreditCardController()
        {
            _apiCommon = new ApiCommon();
        }

        /// <summary>
        /// Adds the credit card.
        /// </summary>
        /// <param name="model">The <see cref="CreditCard"/> model.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> AddCreditCard(CreditCard model)
        {
            using (DbContext _db = new DbContext())
            {
                var user = ApiCommon.GetUser(HttpContext.Current.User.Identity, _db);
                if (user != null)
                {
                    if (!model.IsExpireMonthValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid month of expiration.""}");
                    if (!model.IsExpireYearValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid year of expiration.""}");
                    if (!model.IsHolderNameValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter valid holder name.""}");                    

                    CreditCard card = new CreditCard();
                    card.CardProvider = model.CardProvider;
                    card.ExpireMonth = model.ExpireMonth;
                    card.ExpireYear = model.ExpireYear;
                    card.CardNumber = model.CardNumber;
                    card.CVVCode = model.CVVCode;
                    card.HolderName = model.HolderName;
                    user.CreditCards.Add(card);
                    await _db.SaveChangesAsync();
                    return Ok();
                }
            }
            return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""User doesn't exist.""}");
        }

        /// <summary>
        /// Edits the credit card.
        /// </summary>
        /// <param name="model">The <see cref="CreditCard"/> model.</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> EditCreditCard(CreditCard model)
        {
            using (DbContext _db = new DbContext())
            {
                var user = ApiCommon.GetUser(HttpContext.Current.User.Identity, _db);
                if (user != null)
                {
                    if (!model.IsExpireMonthValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid month of expiration.""}");
                    if (!model.IsExpireYearValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid year of expiration.""}");
                    if (!model.IsHolderNameValid())
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter valid holder name.""}");

                    CreditCard card = _db.CreditCards.FirstOrDefault(e => e.CardId == model.CardId && e.User.UserId == user.UserId);
                    if (card != null)
                    {
                        card.CardProvider = model.CardProvider;
                        card.ExpireMonth = model.ExpireMonth;
                        card.ExpireYear = model.ExpireYear;
                        card.CardNumber = model.CardNumber;
                        card.CVVCode = model.CVVCode;
                        card.HolderName = model.HolderName;
                        await _db.SaveChangesAsync();
                        return Ok();
                    }
                    return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""Credit card doesn't exist.""}");
                }
            }
            return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""User doesn't exist.""}");
        }

        /// <summary>
        /// Gets the credit cards list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public object GetCreditCardsList()
        {
            using (DbContext _db = new DbContext())
            {
                var user = ApiCommon.GetUser(HttpContext.Current.User.Identity, _db);
                if (user != null)
                {
                    return user.CreditCards.Select(i => new { i.CardId, i.CardProvider, i.ExpireMonth, i.ExpireYear, i.HolderName });
                }
            }
            return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""User doesn't exist.""}");
        }
    }
}