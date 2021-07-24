using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KomodoCafeRepo;
using System;

namespace KomodoCafe_Tests
{
    [TestClass]
    public class RepoTests
    {
        private MenuRepo _repo;
        private Menu _item;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepo();
            _item = new Menu();
        }
            [TestMethod]
        public void CreateNewMenuItem_ShouldReturnNotNull()
        {
            // Arrange --Set up variables and such
            Menu item = new Menu();
            item.MealNumber = 5;
            MenuRepo repository = new MenuRepo();

            // Act --Get or run code we want to test
            repository.AddNewMenuItem(item);
            Menu contentFromRepository = repository.GetItemByMealNumber(5);

            // Assert -- check results with assert commands
            Assert.IsNotNull(contentFromRepository);
        }
        [TestMethod]
        public void RemoveMenuItem_ShouldReturnTrue()
        {
            // Arrange
            List<string> ingredients = new List<String>() { "Cheese", "Beef", "Bun" };
            Menu item = new Menu(1, "Cheeseburger", "Yummy!", ingredients, 1.50);
            MenuRepo repository = new MenuRepo();
            repository.AddNewMenuItem(item);

            // Act
            bool removedSuccessfully = repository.RemoveFromMenu(1);

            // Assert
            Assert.IsTrue(removedSuccessfully);  
        }
    }
}
