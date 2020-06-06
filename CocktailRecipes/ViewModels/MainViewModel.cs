using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace CocktailRecipes.ViewModels
{

    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private IList<Drink> ItemList_;

        public IList<Drink> ItemList
        {
            get { return ItemList_; }
            set
            {
                ItemList_ = value;
                SetProperty(ref ItemList_, value);
            }

        }

        public ICommand ChangeList { get; }

        public string popular = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";
        public string randomSelection = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";
        public string alcoholicOnly = "http://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Alcoholic";
        public string nonAlcoholicOnly = "http://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Non_Alcoholic";
        public string OptionalAlcohol = "https://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Optional_Alcohol";

        public MainViewModel()
        {
            ItemList = new ObservableRangeCollection<Drink>();
            
            ChangeList = new Command(GetDataForChangeList);
            GetDataAsync(randomSelection);
        }

        private async void GetDataForChangeList()
        {
            string inputUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Non_Alcoholic";
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(inputUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ItemViewModel rootObject = JsonConvert.DeserializeObject<ItemViewModel>(content);
                ItemList.Clear();
                string tempUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/lookup.php?i=";

                foreach (var item in rootObject.drinks)
                {
                    var tempresponse = await httpClient.GetAsync(tempUrl + item.idDrink);
                    var tempcontent = await tempresponse.Content.ReadAsStringAsync();
                    ItemViewModel temprootObject = JsonConvert.DeserializeObject<ItemViewModel>(tempcontent);

                    ItemList.Add(temprootObject.drinks[0]);
                }
            }
            else
            {
                Debug.WriteLine("Veri çekilirken bir hata oluştu!!");
            }
        }

        private async void GetDataAsync(String restUrl)
        {
            HttpClient httpClient = new HttpClient();
            //var restUrl1 = "https://www.thecocktaildb.com/api/json/v2/9973533/randomselection.php";
            //var restUrl2 = "https://www.thecocktaildb.com/api/json/v2/9973533/popular.php";

            var response = await httpClient.GetAsync(restUrl);
            ItemList.Clear();
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
