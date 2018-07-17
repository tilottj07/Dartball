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
                new MainMenuItem() { Title = "Players", TargetType = typeof(PlayerListPage) },
                new MainMenuItem() { Title = "Teams", TargetType = typeof(TeamListPage) }
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
                switch(item.Title) {
                    case "Home":
                        Detail = new NavigationPage(new MainPage());
                        break;
                    case "Players":
                        Detail = new NavigationPage(new PlayerListPage());
                        break;
                    case "Teams":
                        Detail = new NavigationPage(new TeamListPage());
                        break;
                }
                          
                MenuListView.SelectedItem = null;
                IsPresented = false;
            }
        }

    }
}
