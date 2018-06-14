using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Runtime.Remoting.Messaging;

namespace BankingReport.DbContexts
{
    /// <summary>
    /// Class BankingDbConfiguration.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbConfiguration" />
    public class BankingDbConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankingDbConfiguration"/> class.
        /// </summary>
        public BankingDbConfiguration()
        {
            this.SetExecutionStrategy("System.Data.SqlClient", () => SuspendExecutionStrategy
              ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
              : new SqlAzureExecutionStrategy());
        }
        /// <summary>
        /// Gets or sets a value indicating whether [suspend execution strategy].
        /// </summary>
        /// <value><c>true</c> if [suspend execution strategy]; otherwise, <c>false</c>.</value>
        public static bool SuspendExecutionStrategy
        {
            get { return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false; }
            set { CallContext.LogicalSetData("SuspendExecutionStrategy", value); }
        }
    }
}