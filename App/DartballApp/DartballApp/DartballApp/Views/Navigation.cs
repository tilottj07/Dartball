using System;

using Xamarin.Forms;

namespace DartballApp.Views
{
    public class Navigation : ContentPage
    {
        public Navigation()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

