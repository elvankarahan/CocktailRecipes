using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace CocktailRecipes.ViewModels
{

    public class nameThumbid : ObservableObject
    {
        public IList<Drinks> DrinknameThumbiddrinks { get; set; }
    }

    public class Drinks
    {
        public string strDrink { get; set; }
        public string strDrinkThumb { get; set; }
        public string idDrink { get; set; }
    }
}
