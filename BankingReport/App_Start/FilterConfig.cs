using System.Web.Mvc;

namespace BankingReport
{
    /// <summary>
    /// Class FilterConfig.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The <see cref="GlobalFilterCollection"/> filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
