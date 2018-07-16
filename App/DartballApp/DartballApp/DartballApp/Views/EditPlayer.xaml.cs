using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class EditPlayer : ContentPage
    {
        ViewModels.EditPlayerViewModel EditPlayerViewModel;


        public EditPlayer(Guid? playerId)
        {
            EditPlayerViewModel = new ViewModels.EditPlayerViewModel();
            EditPlayerViewModel.FillPlayer(playerId);

            BindingContext = EditPlayerViewModel;

            InitializeComponent();
        }


        public void Save(object sender, EventArgs args) {
            var result = EditPlayerViewModel.SavePlayer();

            if (!result.IsSuccess) {
                StringBuilder sb = new StringBuilder();

                int index = 0;
                foreach(var item in result.ErrorMessages) {
                    if (index > 0) sb.Append(" ");
                    sb.Append(item);
                    index++;
                }

                DisplayAlert("Alert", sb.ToString(), "OK");
            }
            else {
                MessagingCenter.Send<EditPlayer>(this, "PlayerEdited");
                Navigation.PopModalAsync(animated: true);
            }
        }

        public void Cancel(object sender, EventArgs args) {
            Navigation.PopModalAsync(animated: true);
        }


    }
}
