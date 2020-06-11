using CocktailRecipes.Controls;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CocktailRecipes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                InitializeComponent();
            }
            else
            {
                DisplayAlert("Alert", "No Internet Connection ", "", "Ok");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.UI);
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            ShakePage shakePage = new ShakePage();
            await Navigation.PushModalAsync(shakePage);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                DisconnectedLabel.IsVisible = false;
            }
            else
            {
                DisconnectedLabel.IsVisible = true;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        internal async Task HidePopover()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DrinkDetailCell.IsVisible = false;
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DrinkDisplay element = sender as DrinkDisplay;

            DrinkDetailCell.BindingContext = element.BindingContext;
            DrinkDetailCell.checkLabels();
            DrinkDetailCell.IsVisible = true;
        }
    }
}
