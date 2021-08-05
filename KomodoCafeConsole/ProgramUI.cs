using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KomodoCafeRepo;

namespace KomodoCafeConsole
{
    class ProgramUI
    {
        MenuRepo _menuRepository = new MenuRepo();
        public void Run()
        {
            Seed();
            MainMenu();
        }
        public void MainMenu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------------------\n" +
                    "Welcome to Komodo Cafe Menu Editor! Please choose from the following options:\n" +
                    "---------------------------------------------------\n" +
                    "1. View current Menu\n" +
                    "2. Add a new meal\n" +
                    "3. Remove a meal\n" +
                    "4. Exit the Program");
                string input = Console.ReadLine();
                switch(input) 
                {
                    case "1":
                        ViewMenu();
                        break;
                    case "2":
                        AddMeal();
                        break;
                    case "3":
                        RemoveMeal();
                        break;
                    case "4":
                        keepRunning = false;
                        Console.WriteLine("Goodbye! Press any key to exit...");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid entry. Please select an option.");
                        break;
                }
            }
        }
        private void ViewMenu()
        {
            Console.Clear();
            Console.WriteLine("*********              MENU              *********\n" +
                "---------------------------------------------------------------------------------------\n" +
                "---------------------------------------------------------------------------------------");
            List<Menu> allMeals = _menuRepository.ViewMenu();
            foreach(Menu meal in allMeals)
            {
                Console.WriteLine($"#{meal.MealNumber}:   {meal.MealName}        ${meal.Price}\n" +
                    $"Description: {meal.Description}\n" +
                    $"Ingredients:"); 
                foreach(string ingredient in meal._ingredientsList)
                {
                    Console.WriteLine($"-->{ingredient}");
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
            }
            Console.ReadLine();
        }
        private void AddMeal()
        {
            Menu newMeal = new Menu();
            bool validInput = false;
            Console.Clear();
            while (!validInput)
            {
                Console.WriteLine("Please enter a Meal Number for the new meal:");
                string inputAsString = Console.ReadLine();
                bool inputToInt = int.TryParse(inputAsString, out int mealNum);
                if (!inputToInt)
                {
                    Console.WriteLine("invalid input, please try again.");
                }
                else
                {
                    validInput = true;
                    newMeal.MealNumber = mealNum;
                }
            }
            validInput = false;
            Console.Clear();
            while (!validInput)
            {
                Console.WriteLine("Please enter a name for the new meal:");
                string input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Error: No value entered. Please try again.");
                }
                else
                {
                    validInput = true;
                    newMeal.MealName = input;
                }
            }
            validInput = false;
            Console.Clear();
            while (!validInput)
            {
                Console.WriteLine("Please enter a description:");
                string description = Console.ReadLine();
                if (description == null)
                {
                    Console.WriteLine("Error: No value entered. Please try again.");
                }
                else
                {
                    validInput = true;
                    newMeal.Description = description;
                }
            }
            validInput = false;
            Console.Clear();
            while(!validInput)
            {
                Console.WriteLine("How many ingredients are in the new meal?");
                string inputAsString = Console.ReadLine();
                bool stringToInt = int.TryParse(inputAsString, out int numOfIngredients);
                List<string> ingredientsList = new List<String>();
                if (stringToInt)
                {
                    for(int count = numOfIngredients; count > 0; count --)
                    {
                        Console.WriteLine("Please enter an ingredient:");
                        ingredientsList.Add(Console.ReadLine());
                        Console.WriteLine("Ingredient Added!");
                    }
                    newMeal._ingredientsList = ingredientsList;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Error. Please enter a number.");
                }
            }
            Console.Clear();
            validInput = false;
            while(!validInput)
            {
                Console.Write("Please enter the price:\n" +
                    "$");
                string input = Console.ReadLine();
                bool inputToDouble = double.TryParse(input, out double cost);
                if (inputToDouble)
                {
                    newMeal.Price = cost;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Input should be in all number format with no letters or symbols. Please try again.");
                }
            }
            _menuRepository.AddNewMenuItem(newMeal);
            Console.WriteLine("Meal added successfully! Press enter to return to main menu.");
            Console.ReadLine();
        }
        private void RemoveMeal()
        {
            Console.Clear();
            bool validInput = false;
            while(!validInput) 
            {
                Console.WriteLine("Please enter the number of the meal you would like to remove:");
                string input = Console.ReadLine();
                bool inputToInt = int.TryParse(input, out int mealNum);
                if (inputToInt)
                {
                    _menuRepository.RemoveFromMenu(mealNum);
                    Console.WriteLine("Meal removed successfully. Press enter to return to main menu.");
                    Console.ReadLine();
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid meal number");
                }
            }
        }
        public void Seed()
        {
            List<string> mealOneIng = new List<string>(){"Bun ", "Cheese ", "Lettuce ", "Special Sauce ", "Beef "};
            List<string> mealTwoIng = new List<string>() { "Bun ", "Cheese ", "Onion ", "Ketchup ", "Beef", "Pickle ", "Mustard " };
            List<string> mealThreeIng = new List<string>() { "Bun ", "Chicken ", "Lettuce ", "Mayo ",};
            List<string> mealFourIng = new List<string>() { "Chicken ", "Choice of Sauce " };

            Menu mealOne = new Menu(1, "Big Mac", "A double cheeseburger with special sauce. (Which is really just tartar sauce.)", mealOneIng, 5.50);
            Menu mealTwo = new Menu(2, "Quarter Pounder", "A quarter pound(ish) of beef with onion, ketchup, and mustard. Unless they forget something.", mealTwoIng, 5.00);
            Menu mealThree = new Menu(3, "McChicken", "A cheap little \"chicken\" sandwich that's somehow still satisfying.", mealThreeIng, 1.00);
            Menu mealFour = new Menu(4, "Chicken Nuggets", "Ten chicken nuggies with your choice of dipping sauce. A safe choice. How could they possibly mess this one up?", mealFourIng, 3.50);

            _menuRepository.AddNewMenuItem(mealOne);
            _menuRepository.AddNewMenuItem(mealTwo);
            _menuRepository.AddNewMenuItem(mealThree);
            _menuRepository.AddNewMenuItem(mealFour);
        }
    }
}