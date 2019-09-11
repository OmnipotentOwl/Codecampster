using System;
using Android.App;
using Android.Runtime;
using Codecamp.Mobile;

namespace Codecamp.Mobile.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }


        public override void OnCreate()
        {
            base.OnCreate();
            Shiny.AndroidShinyHost.Init(this, new Startup());
        }
    }
}