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
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
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

        internal async Task HidePopover()
        {
            // fade out the elements
            //await Task.WhenAll(
            //    new Task[]
            //    {
            //        DrinkDetailCell.FadeTo(0),

            //    });

            // hide our fake product cell
            DrinkDetailCell.IsVisible = false;

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            DrinkDisplay element = sender as DrinkDisplay;

            DrinkDetailCell.BindingContext = element.BindingContext;
            DrinkDetailCell.checkLabels();
            DrinkDetailCell.IsVisible = true;
            // Check internet connection 
        }

    }
}
