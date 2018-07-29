using System;
using Dartball.BusinessLayer.League.Implementation;
using Dartball.BusinessLayer.League.Interface;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        public void GoToPlayerList(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Player.PlayerListPage());
        }

        public void SetupQuickPlayGame(object sender, EventArgs args) {

            //TODO: replace with proper league settings
            ILeagueService leagueService = new LeagueService();
            var league = leagueService.GetGenericLeague();

            Navigation.PushModalAsync(new NavigationPage( new Game.SetupGamePage(league.LeagueId)));
        }
    }
}