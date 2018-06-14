using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BankingReport
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDataStore();
        }
        private static void InitializeDataStore()
        {
            var dbConfiguration = new Migrations.DbContext.Configuration();
            var migrator = new DbMigrator(dbConfiguration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }
    }
}
