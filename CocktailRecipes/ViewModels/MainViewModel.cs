using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CocktailRecipes.ViewModels
{

    public class MainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string randomSelection = "https://www.thecocktaildb.com/api/json/v2/9973533/randomselection.php";

        bool isRefreshing;
        public ICommand fullList { get; }
        public ICommand notFullList { get; }
        public ICommand searchCommand { get; }
        public ICommand refreshList { get; }

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
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }


        public MainViewModel()
        {
            ItemList = new ObservableRangeCollection<Drink>();

            fullList = new Command<string>((x) => GetDataAsync(x));

            notFullList = new Command<string>((x) => GetDataForChangeList(x));

            searchCommand = new Command<string>((x) => SearchBarCommand(x));

            refreshList = new Command(() =>
            {
                Refresh();
                IsRefreshing = false;
            });

            GetDataAsync(randomSelection);
        }
        private void Refresh()
        {
            GetDataAsync("https://www.thecocktaildb.com/api/json/v2/9973533/randomselection.php");

        }
        private void SearchBarCommand(string searchDrink)
        {
            GetDataAsync("https://www.thecocktaildb.com/api/json/v2/9973533/search.php?s=" + searchDrink);
        }

        private async void GetDataForChangeList(string inputUrl)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                //string inputUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/filter.php?a=Non_Alcoholic";
                HttpClient httpClient = new HttpClient();

                var response = await httpClient.GetAsync(inputUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ItemViewModel rootObject = JsonConvert.DeserializeObject<ItemViewModel>(content);

                    try
                    {
                        string tempUrl = "https://www.thecocktaildb.com/api/json/v2/9973533/lookup.php?i=";
                        ItemList.Clear();

                        foreach (var item in rootObject.drinks)
                        {
                            var tempresponse = await httpClient.GetAsync(tempUrl + item.idDrink);
                            var tempcontent = await tempresponse.Content.ReadAsStringAsync();
                            ItemViewModel temprootObject = JsonConvert.DeserializeObject<ItemViewModel>(tempcontent);

                            ItemList.Add(temprootObject.drinks[0]);
                        }
                    }
                    catch (System.NullReferenceException ex)
                    {
                        GetDataAsync(randomSelection);
                    }
                }
            }
        }

        private async void GetDataAsync(String restUrl)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                HttpClient httpClient = new HttpClient();

                var response = await httpClient.GetAsync(restUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ItemViewModel rootObject = JsonConvert.DeserializeObject<ItemViewModel>(content);
                    try
                    {
                        ItemList.Clear();
                        foreach (var item in rootObject.drinks)
                        {
                            ItemList.Add(item);
                        }
                    }
                    catch (System.NullReferenceException ex)
                    {
                        GetDataAsync(randomSelection);
                    }

                }
            }
        }

    }
}