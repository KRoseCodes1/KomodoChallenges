using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoCafeRepo
{
    public class MenuRepo
    {
        private List<Menu> _listOfMenuItems = new List<Menu>();

        // Create
        public void AddNewMenuItem (Menu newItem)
        {
            _listOfMenuItems.Add(newItem);
        }

        // Read
        public List<Menu> ViewMenu()
        {
            return _listOfMenuItems;
        }

        // Delete
        public bool RemoveFromMenu (int menuNum)
        {
            Menu itemToRemove = GetItemByMealNumber(menuNum);
            if (itemToRemove == null)
            {
                return false;
            }

            int initialCount = _listOfMenuItems.Count;
            _listOfMenuItems.Remove(itemToRemove);

            if (initialCount > _listOfMenuItems.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helper methods:
        public Menu GetItemByMealNumber(int num)
        {
            foreach (Menu item in _listOfMenuItems)
            {
                if (item.MealNumber == num)
                {
                    return item;
                }
            }
            return null; 
        }
    }
}