using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KomodoCafeRepo;
using System;

namespace KomodoCafe_Tests
{
    [TestClass]
    public class CafeRepoTests
    {
        private MenuRepo _repo;
        private Menu item;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepo();
            item = new Menu();
        }
            [TestMethod]
        public void CreateNewMenuItem_ShouldReturnNotNull()
        {
            // Arrange
            item.MealNumber = 5;
           
            // Act
            _repo.AddNewMenuItem(item);
            Menu contentFromRepository = _repo.GetItemByMealNumber(5);

            // Assert
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
