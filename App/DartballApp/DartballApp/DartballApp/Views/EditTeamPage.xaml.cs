using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartballApp.Views
{
    public partial class EditTeamPage : ContentPage
    {
        ViewModels.EditTeamViewModel ViewModel;

        public EditTeamPage(Guid? teamId)
        {
            ViewModel = new ViewModels.EditTeamViewModel();
            ViewModel.FillTeam(teamId);

            BindingContext = ViewModel;

            InitializeComponent();
        }


        public void Save(object sender, EventArgs args) {
            var result = ViewModel.SaveTeam();

            if (!result.IsSuccess)
            {
                StringBuilder sb = new StringBuilder();

                int index = 0;
                foreach (var item in result.ErrorMessages)
                {
                    if (index > 0) sb.Append(" ");
                    sb.Append(item);
                    index++;
                }

                DisplayAlert("Alert", sb.ToString(), "OK");
            }
            else
            {
                MessagingCenter.Send<EditTeamPage>(this, "TeamEdited");
                Navigation.PopModalAsync(animated: true);
            }
        }

        public void Cancel(object sender, EventArgs args)
        {
            Navigation.PopModalAsync(animated: true);
        }
    }
}
