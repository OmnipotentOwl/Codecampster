using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Codecamp.Mobile.Services;
using Codecamp.Mobile.Views;

namespace Codecamp.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            const string AppCenteriOS = "APPCENTER_IOS";
            const string AppCenterAndroid = "APPCENTER_ANDROID";
        }

        protected override void OnStart()
        {
#if !DEBUG
            AppCenter.Start($"ios={AppCenteriOS};" +
                $"android={AppCenterAndroid};", 
                typeof(Analytics), 
                typeof(Crashes),
                typeof(Distribute));
#endif
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
