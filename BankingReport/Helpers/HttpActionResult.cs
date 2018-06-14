using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankingReport.Helpers
{
    /// <summary>
    /// Class HttpActionResult.
    /// </summary>
    /// <seealso cref="System.Web.Http.IHttpActionResult" />
    public class HttpActionResult : IHttpActionResult
    {
        /// <summary>
        /// The message
        /// </summary>
        private readonly string _message;

        /// <summary>
        /// The status code
        /// </summary>
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpActionResult"/> class.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> status code.</param>
        /// <param name="message">The message.</param>
        public HttpActionResult(HttpStatusCode statusCode, string message)
        {
            _statusCode = statusCode;
            _message = message;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> token to monitor for cancellation requests.</param>
        /// <returns>A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.</returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_message)
            };
            return Task.FromResult(response);
        }
    }
}