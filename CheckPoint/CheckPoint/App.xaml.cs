using CheckPoint.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CheckPoint
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjczMTYwQDMyMzAyZTMyMmUzMEw0MUNnRmFnVmQvazYza00xRGVOSVZvUEM5b3gwU3IvbkZyU0l5TFVFTGM9");

            InitializeComponent();

            MainPage = new NavigationPage(new CheckPoint.Views.TabbedPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
