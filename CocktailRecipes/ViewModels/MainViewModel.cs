using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace CocktailRecipes.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IList<Drink> ItemList { get; set; }

        public string popular = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";
        public string randomSelection = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";
        public string alcoholicOnly = "http://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Alcoholic";
        public string nonAlcoholicOnly = "http://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Non_Alcoholic";

        public MainViewModel()
        {
            ItemList = new ObservableRangeCollection<Drink>();

            GetDataAsync(randomSelection);
        }

        private async void GetDataAsync(String restUrl)
        {
            HttpClient httpClient = new HttpClient();
            //var restUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/randomselection.php";
            //var restUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";

            var response = await httpClient.GetAsync(restUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ItemViewModel rootObject = JsonConvert.DeserializeObject<ItemViewModel>(content);

                foreach (var item in rootObject.drinks)
                {
                    ItemList.Add(item);
                }

            }
            else
            {
                Debug.WriteLine("Veri çekilirken bir hata oluştu!!");
            }
        }
    }
}
