using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BankingReport.Models.ModelEnums;

namespace BankingReport.Models
{
    /// <summary>
    /// Class CreditCard.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// Gets or sets the card identifier.
        /// </summary>
        /// <value>The card identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        /// <summary>
        /// Gets or sets the expire month.
        /// </summary>
        /// <value>
        /// The expire month.
        /// </value>
        public int ExpireMonth { get; set; }

        /// <summary>
        /// Gets or sets the expire year.
        /// </summary>
        /// <value>
        /// The expire year.
        /// </value>
        public int ExpireYear { get; set; }

        /// <summary>
        /// Gets or sets the card provider.
        /// </summary>
        /// <value>
        /// The card provider.
        /// </value>
        public CardProviderEnum CardProvider { get; set; }

        /// <summary>
        /// Gets or sets the CVV code.
        /// </summary>
        /// <value>The CVV code.</value>
        public int CVVCode { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>The card number.</value>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the credit card holder.
        /// </summary>
        /// <value>
        /// The name of the credit card holder.
        /// </value>
        public string HolderName { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The <see cref="User"/>.</value>
        public virtual User User { get; set; }

        /// <summary>
        /// Determines whether [is expire month valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is expire month valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExpireMonthValid()
        {
            return this.ExpireMonth > 0 && this.ExpireMonth < 13;
        }

        /// <summary>
        /// Determines whether [is expire year valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is expire year valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExpireYearValid()
        {
            return this.ExpireYear > DateTime.Now.Year;
        }

        /// <summary>
        /// Determines whether [is holder name valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is holder name valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsHolderNameValid()
        {
            return !String.IsNullOrEmpty(this.HolderName);
        }
    }
}