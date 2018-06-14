using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BankingReport.Models.ModelEnums;

namespace BankingReport.Models
{
    public class Payment
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the amount of payment.
        /// </summary>
        /// <value>
        /// The amount of payment.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets or sets the transaction date.
        /// </summary>
        /// <value>
        /// The transaction date.
        /// </value>
        public DateTime TransDate { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public PaymentStatusEnum PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the credit card.
        /// </summary>
        /// <value>
        /// The credit card.
        /// </value>
        public virtual CreditCard CreditCard { get; set; }
    }
}