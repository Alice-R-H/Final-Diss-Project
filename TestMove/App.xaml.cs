using System.Configuration;
using System.Data;
using System.Windows;
using TestMove.Services;

namespace TestMove
{
    public partial class App : Application
    {
        // logic to populate the database on start-up and then clear the database on close, for development & testing purposes. 

        public App()
        {
            //register Syncfusion free registered community license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cVWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjWn9dcXVQRWBfUkd0WA==");
        }

        private DatabasePopulation databasePopulation;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            databasePopulation = new DatabasePopulation();

            // seed the database asynchronously on opening
            Task.Run(() => databasePopulation.SeedDatabaseAsync()).Wait();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            // clear the database asynchronously on closing
            Task.Run(() => databasePopulation.ClearDatabaseAsync()).Wait();

            base.OnExit(e);
        }
    }
}
