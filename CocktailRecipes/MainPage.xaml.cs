using CocktailRecipes.Controls;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CocktailRecipes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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

        }

    }
}
