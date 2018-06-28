using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DartballApp.Models;
using DartballApp.ViewModels;
using Dartball.BusinessLayer.Player.Interface;
using Dartball.BusinessLayer.Player.Dto;

namespace DartballApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerViewModel viewModel { get; set; }

        public PlayerPage(PlayerViewModel viewModel)
        {

            this.BindingContext = this.viewModel = viewModel;
        }

        public PlayerPage()
        {
            this.viewModel = new PlayerViewModel();

            this.BindingContext = this.viewModel;
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            PlayerDto dto = new PlayerDto()
            {
                UserName = this.viewModel.Player.UserName,
                Password = this.viewModel.Player.Password,
                EmailAddress = this.viewModel.Player.EmailAddress,
                Name = this.viewModel.Player.Name,
                PlayerId = this.viewModel.Player.PlayerId,
            };

            var service = DependencyService.Get<IPlayerService>();

            if (this.viewModel.IsAddNew) service.AddNew(dto);
            else service.Update(dto);

        }


    }
}
