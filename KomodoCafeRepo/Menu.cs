using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafeRepo
{
    public class Menu
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public List<string> _ingredientsList { get; set; }
        public double Price { get; set; }

        public Menu() { }
        public Menu (int mealNum, string mealName, string desc, List<string> ingredients, double cost)
        {
            MealNumber = mealNum;
            MealName = mealName;
            Description = desc;
            _ingredientsList = ingredients;
            Price = cost;
        }
    }
}