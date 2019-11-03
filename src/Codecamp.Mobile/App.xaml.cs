using Codecamp.Mobile.Clients.Portable.Services;
using Xamarin.Forms;

namespace Codecamp.Mobile.Clients.Portable
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
