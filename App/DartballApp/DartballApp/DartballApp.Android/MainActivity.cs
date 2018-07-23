using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;


namespace DartballApp.Droid
{
    [Activity(Label = "DartballApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            MetricsManager.Register(Application, "4d2b087d950f45b393dfbfda7f2fa57e");
            //MetricsManager.Register(Application);//, $"${this.Application.PackageName}");
        }

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, "4d2b087d950f45b393dfbfda7f2fa57e");
        }
    }
}

