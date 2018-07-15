using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class EditPlayer : ContentPage
    {
        ViewModels.EditPlayer EditPlayerViewModel;


        public EditPlayer(Guid? playerId)
        {
            EditPlayerViewModel = new ViewModels.EditPlayer();
            EditPlayerViewModel.FillPlayer(playerId);

            BindingContext = EditPlayerViewModel;

            InitializeComponent();
        }


        public void Save(object sender, EventArgs args) {
            

           
        }

        public void Cancel(object sender, EventArgs args) {
            Navigation.PushAsync(new PlayerList());
        }


    }
}
