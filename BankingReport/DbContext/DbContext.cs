using BankingReport.Models;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace BankingReport.DbContexts
{
    /// <summary>
    /// Class DbContext.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    [Localizable(false)]
    [DbConfigurationType(typeof(BankingDbConfiguration))]
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        public DbContext() : base("name=DefaultConnection")
        {
        }
        public DbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
        {
        }
    }
}