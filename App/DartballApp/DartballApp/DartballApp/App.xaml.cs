using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace DartballApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
		}

		protected override void OnStart ()
		{
            // Handle when your app starts

            Dartball.Data.DartballContext dartballContext = new Dartball.Data.DartballContext();
            dartballContext.Migrate();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
