using CocktailRecipes.ViewModels;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailRecipes.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShakePage : ContentPage
    {
        public ShakePage()
        {
            InitializeComponent();
            GetDataAsync();

        }

        private async void GetDataAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                HttpClient httpClient = new HttpClient();

                string restUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/random.php";

                var response = await httpClient.GetAsync(restUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ItemViewModel rootObject = JsonConvert.DeserializeObject<ItemViewModel>(content);

                    strDrinkThumbImage.Source = rootObject.drinks[0].strDrinkThumb;
                    strDrink.Text = rootObject.drinks[0].strDrink;
                    strCategory.Text = rootObject.drinks[0].strCategory;
                    strAlcoholic.Text = rootObject.drinks[0].strAlcoholic;

                    Ingredient1.Text = rootObject.drinks[0].strIngredient1;
                    Ingredient2.Text = rootObject.drinks[0].strIngredient2;
                    Ingredient3.Text = rootObject.drinks[0].strIngredient3;
                    Ingredient4.Text = rootObject.drinks[0].strIngredient4;
                    Ingredient5.Text = rootObject.drinks[0].strIngredient5;
                    Ingredient6.Text = rootObject.drinks[0].strIngredient6;
                    Ingredient7.Text = rootObject.drinks[0].strIngredient7;

                    strMeasure1.Text = rootObject.drinks[0].strMeasure1;
                    strMeasure2.Text = rootObject.drinks[0].strMeasure2;
                    strMeasure3.Text = rootObject.drinks[0].strMeasure3;
                    strMeasure4.Text = rootObject.drinks[0].strMeasure4;
                    strMeasure5.Text = rootObject.drinks[0].strMeasure5;
                    strMeasure6.Text = rootObject.drinks[0].strMeasure6;
                    strMeasure7.Text = rootObject.drinks[0].strMeasure7;

                    strInstructions.Text = rootObject.drinks[0].strInstructions;
                }
                checkLabels();
            }
        }


        public void checkLabels()
        {
            List<Frame> allFrames = new List<Frame>();

            allFrames.Add(Frame1);
            allFrames.Add(Frame2);
            allFrames.Add(Frame3);
            allFrames.Add(Frame4);
            allFrames.Add(Frame5);
            allFrames.Add(Frame6);
            allFrames.Add(Frame7);
            allFrames.Add(Frame8);
            allFrames.Add(Frame9);
            allFrames.Add(Frame10);

            List<Label> allIngredients = new List<Label>();

            allIngredients.Add(Ingredient1);
            allIngredients.Add(Ingredient2);
            allIngredients.Add(Ingredient3);
            allIngredients.Add(Ingredient4);
            allIngredients.Add(Ingredient5);
            allIngredients.Add(Ingredient6);
            allIngredients.Add(Ingredient7);
            allIngredients.Add(Ingredient8);
            allIngredients.Add(Ingredient9);
            allIngredients.Add(Ingredient10);

            List<Label> Measures = new List<Label>();

            Measures.Add(strMeasure1);
            Measures.Add(strMeasure2);
            Measures.Add(strMeasure3);
            Measures.Add(strMeasure4);
            Measures.Add(strMeasure5);
            Measures.Add(strMeasure6);
            Measures.Add(strMeasure7);
            Measures.Add(strMeasure8);
            Measures.Add(strMeasure9);
            Measures.Add(strMeasure10);


            var LabelAndFrame = allIngredients.Zip(allFrames, (i, f) => new { ingredientZip = i, frameZip = f });
            Label toAddLabel = new Label();
            int rowIndex = 0;

            foreach (var child in IngredientsDetailGrid.Children.Reverse())
            {
                var childTypeName = child.GetType().Name;
                if (childTypeName == "Label")
                {
                    IngredientsDetailGrid.Children.Remove(child);
                }
            }


            foreach (var item in LabelAndFrame)
            {
                var labelText = item.ingredientZip.Text;
                if (!string.IsNullOrEmpty(labelText))
                {

                    IngredientsDetailGrid.Children.Add(new Label
                    {
                        Text = item.ingredientZip.Text,
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 18

                    }, 0, rowIndex);

                    IngredientsDetailGrid.Children.Add(new Label
                    {
                        Text = Measures[rowIndex].Text,
                        HorizontalTextAlignment = TextAlignment.End,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = 18

                    }, 1, rowIndex);
                    rowIndex += 1;

                }
                else
                {
                    item.frameZip.IsVisible = false;
                }


            }
        }
    }
}