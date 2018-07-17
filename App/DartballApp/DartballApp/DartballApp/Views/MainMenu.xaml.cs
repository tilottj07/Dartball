using System;
using System.Collections.Generic;
using DartballApp.Models;


using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class MainMenu : MasterDetailPage
    {

        public List<MainMenuItem> MainMenuItems { get; set; }

        public MainMenu()
        {
            // Set the binding context to this code behind.
            BindingContext = this;

            // Build the Menu
            MainMenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Home", TargetType = typeof(MainPage) },
                new MainMenuItem() { Title = "Players", TargetType = typeof(PlayerListPage) }
            };

            // Set the default page, this is the "home" page.
            Detail = new NavigationPage(new MainPage());

            InitializeComponent();
        }

        // When a MenuItem is selected.
        public void MainMenuItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                if (item.Title.Equals("Home"))
                {
                    Detail = new NavigationPage(new MainPage());
                }
                else if (item.Title.Equals("Players"))
                {
                    Detail = new NavigationPage(new PlayerListPage());
                }

                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }

    }
}
