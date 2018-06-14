using System;
using static BankingReport.Models.ModelEnums;

namespace BankingReport.Models
{
    /// <summary>
    /// Class for API report
    /// </summary>
    public class ReportFilterModel
    {
        /// <summary>
        /// Gets or sets the credit card.
        /// </summary>
        /// <value>
        /// The credit card.
        /// </value>
        public Guid CreditCard { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public string SortOrder { get; set; } = "ASC";

        /// <summary>
        /// Gets or sets a value indicating whether this instance is grouped by day.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is grouped by day; otherwise, <c>false</c>.
        /// </value>
        public int PaymentStatus { get; set; }
    }
}