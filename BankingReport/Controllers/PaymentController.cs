using BankingReport.DbContexts;
using BankingReport.Helpers;
using BankingReport.Models;
using BankingReport.ScheduledWorkers;
using Dapper;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace BankingReport.Controllers
{
    public class PaymentController : ApiController
    {
        /// <summary>
        /// The API common
        /// </summary>
        private ApiCommon _apiCommon = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController"/> class.
        /// </summary>
        public PaymentController()
        {
            _apiCommon = new ApiCommon();
        }

        /// <summary>
        /// Proceeds the payment random.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult ProceedPaymentRandom()
        {
            using (DbContext _db = new DbContext())
            {
                HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => new PaymentProceed().StartProcessing(cancellationToken));
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public object PaymentMonthReport(ReportFilterModel model)
        {
            using (DbContext _db = new DbContext())
            {
                var user = ApiCommon.GetUser(HttpContext.Current.User.Identity, _db);
                if (user != null && model != null)
                {
                    if (model.Month == 0 || model.Year == 0 || model.Month > 12)
                        return new HttpActionResult(HttpStatusCode.BadRequest, @"{""message"":""Please enter a valid month and year.""}");

                    using (var dapper = new Helpers.Dapper())
                    {
                        var payments = dapper.Connection.Query($"select * From Payments Where month(TransDate)=@month and year(TransDate)=@year and PaymentStatus=@status order by TransDate {model.SortOrder}", new { month = model.Month, year = model.Year, status = model.PaymentStatus });
                        return payments.Select(i => new { i.Amount, i.CreditCard_CardId, i.PaymentStatus, i.TransDate }); ;
                    }
                }
            }
            return new HttpActionResult(HttpStatusCode.Unauthorized, @"{""message"":""User doesn't exist.""}");
        }
    }
}