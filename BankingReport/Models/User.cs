using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BankingReport.Models
{
    /// <summary>
    /// Class User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the ASP net user identifier.
        /// </summary>
        /// <value>
        /// The ASP net user identifier.
        /// </value>
        public string AspNetUserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [NotMapped]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>
        /// The postal code.
        /// </value>
        [Required]
        public int PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the credit cards.
        /// </summary>
        /// <value>The list of credit cards.</value>
        public virtual ICollection<CreditCard> CreditCards { get; set; }

        /// <summary>
        /// Determines whether [is name valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is name valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNameValid()
        {
            return !String.IsNullOrEmpty(this.FirstName) && !String.IsNullOrEmpty(this.LastName);
        }

        /// <summary>
        /// Determines whether [is password valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is password valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPasswordValid()
        {
            return !(String.IsNullOrEmpty(this.Password) || this.Password.Length < 8 || this.Password.Length > 20 || !Regex.IsMatch(this.Password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d\w\W]{8,}$"));
        }

        /// <summary>
        /// Determines whether [is email valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is email valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmailValid()
        {
            if (!String.IsNullOrEmpty(this.Email))
            {
                try
                {
                    var addr = new MailAddress(this.Email);
                    if (addr.Address == this.Email && !addr.User.StartsWith(".") && !addr.User.EndsWith("."))
                    {
                        if (Regex.IsMatch(this.Email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is city valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is city valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCityValid()
        {
            return !String.IsNullOrEmpty(this.City);
        }

        /// <summary>
        /// Determines whether [is address valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is address valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAddressValid()
        {
            return !String.IsNullOrEmpty(this.Address);
        }

        /// <summary>
        /// Determines whether [is postal code valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is postal code valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPostalCodeValid()
        {
            return this.PostalCode != 0;
        }

        /// <summary>
        /// Determines whether [is country valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is country valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCountryValid()
        {
            return !String.IsNullOrEmpty(this.Country);
        }

        /// <summary>
        /// Determines whether [is phone number valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is phone number valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPhoneNumberValid()
        {
            return !String.IsNullOrEmpty(this.PhoneNumber);
        }
    }
}