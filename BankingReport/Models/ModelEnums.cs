using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingReport.Models
{
    /// <summary>
    /// Class of enums
    /// </summary>
    public class ModelEnums
    {
        /// <summary>
        /// Types of credit cards providers
        /// </summary>
        public enum CardProviderEnum
        {
            Visa,
            Mastercard
        }

        public enum PaymentStatusEnum
        {
            Canceled_Reversal,
            Completed,
            Denied,
            Failed,
            Pending,
            Refunded,
            Reversed,
            Processed,
            Voided
        }
    }
}