using CocktailRecipes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CocktailRecipes.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkFullDetail : ContentView
    {
        public DrinkFullDetail()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await ((MainPage)this.GetParentPage()).HidePopover();
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            StartAnimation();
        }

        private async void StartAnimation()
        {

            await Task.Delay(150);
            await buttonSelected.FadeTo(0, 250);
            await Task.Delay(150);
            await buttonSelected.FadeTo(1, 250);
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