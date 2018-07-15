using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartballApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        public void GoToPlayerList(object sender, EventArgs args) 
        {          
            Navigation.PushAsync(new Views.PlayerList());
        }
    }
}
