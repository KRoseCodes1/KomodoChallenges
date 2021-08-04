using KomodoBadges_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoBadges_Console
{
    public class ProgramUI
    {
        BadgeRepo _repo = new BadgeRepo();
        public void Run()
        {
            Seed();
            Menu();
        }
        public void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Welcome to Komodo Badge Services\n" +
                    "Please choose from the following options:\n" +
                    "--------------------------------------------------\n" +
                    "1. View All Badges\n" +
                    "2. Edit Door Access for a Badge\n" +
                    "3. Add a New Badge\n" +
                    "4. Remove a Badge\n" +
                    "5. Exit the Program");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                        ViewBadges();
                        Console.Clear();
                        break;
                    case "2":
                        EditBadge();
                        Console.Clear();
                        break;
                    case "3":
                        AddBadge();
                        Console.Clear();
                        break;
                    case "4":
                        RemoveBadge();
                        Console.Clear();
                        break;
                    case "5":
                        keepRunning = false;
                        Console.WriteLine("Goodbye! Press enter to exit.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
        }
        public void ViewBadges()
        {
            Console.Clear();
            Console.WriteLine("**********                CURRENT BADGES                **********\n");
            Dictionary<int, List<String>> badges = _repo.ViewAllBadges();
            foreach (KeyValuePair<int, List<string>> kvp in badges)
            {
                Console.Write($"Badge ID: {kvp.Key}       ||      Door Access: ");
                foreach(string door in kvp.Value)
                {
                    Console.Write($"{door}   ");
                }
                Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------\n");
            }
            Console.ReadLine();
        }
        public void EditBadge()
        {
            bool keepRun = true;
            while (keepRun)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?\n" +
                    "1. Add door to badge\n" +
                    "2. Remove door from badge\n" +
                    "3. Exit to main menu");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "1":
                        AddDoorToBadge();
                        break;
                    case "2":
                        RemoveDoorFromBadge();
                        break;
                    case "3":
                        keepRun = false;
                        Console.WriteLine("Press enter to return to the main menu...");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please choose from the available options.");
                        Console.ReadLine();
                        break;
                }
            }
        }
        public void AddBadge()
        {
            Console.Clear();
            Badge newBadge = new Badge();
            Console.WriteLine("Please enter a name for the new badge:");
            newBadge.BadgeName = Console.ReadLine();

            int id;
            bool notValid = true;
            while (notValid)
            {
                Console.WriteLine("Please enter the Badge ID:");
                bool validID = Int32.TryParse(Console.ReadLine(), out id);
                if (validID)
                {
                    notValid = false;
                    newBadge.BadgeID = id;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please enter a valid ID number.");
                }
            }
            Console.WriteLine("Please enter a door you would like this badge to have access to:");
            string input = Console.ReadLine();
            List<String> doors = new List<String>();
            doors.Add(input);

            bool moreDoors = true;
            while (moreDoors)
            {
                Console.WriteLine("Would you like to add another door? (y/n)");
                string addDoor = Console.ReadLine().ToLower();
                switch (addDoor)
                {
                    case "y":
                        Console.WriteLine("Please enter the door:");
                        string door = Console.ReadLine();
                        doors.Add(door);
                        break;
                    case "n":
                        moreDoors = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please type y or n.");
                        break;
                }
            }
            newBadge.DoorAccess = doors;

            _repo.AddNewBadge(newBadge);
            Console.WriteLine("Success! Press enter to return to the previous menu.");
            Console.ReadLine();
        }
        public void RemoveBadge()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the badge you would like to remove:");
            int id;
            bool validID = Int32.TryParse(Console.ReadLine(), out id);
            if (validID)
            {
                bool removedSuccesfully = _repo.RemoveBadge(id);
                if (removedSuccesfully)
                {
                    Console.WriteLine("Removed Succesfully!");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Input was in correct format but a badge with that ID could not be found. Please try again.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid ID number.");
            }
            Console.WriteLine("Press enter to return to main menu.");
            Console.ReadLine();
        }
        public void AddDoorToBadge()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the badge you would like to edit:");
            int id;
            bool validID = Int32.TryParse(Console.ReadLine(), out id);
            if (validID)
            {
                Dictionary<int, List<string>> dict = _repo.ViewAllBadges();
                Console.WriteLine("Here are the current doors associated with that id:\n");
                foreach (string door in dict[id])
                {
                    Console.Write($"{door}     ");
                }
                Console.WriteLine("\n Enter the door you would like to add:");
                string doorChoice = Console.ReadLine();
                dict[id].Add(doorChoice);
                _repo.UpdateExistingBadge(id, dict[id]);
                Console.WriteLine("Updated Successfully! Press enter to continue.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not a number. Please enter a valid ID.");
                Console.ReadLine();
            }
        }
        public void RemoveDoorFromBadge()
        {
            Console.Clear();
            Console.WriteLine("Please enter the ID of the badge you would like to edit:");
            int id;
            bool validID = Int32.TryParse(Console.ReadLine(), out id);
            if (validID)
            {
                Dictionary<int, List<string>> dict = _repo.ViewAllBadges();
                Console.WriteLine("Here are the current doors associated with that id:\n");
                foreach(string door in dict[id])
                {
                    Console.Write($"{door}     ");
                }
                Console.WriteLine("\n Which door would you like to remove?");
                string doorChoice = Console.ReadLine();

                bool wasRemoved = dict[id].Remove(doorChoice);
                _repo.UpdateExistingBadge(id, dict[id]);
                if (wasRemoved)
                {
                    Console.WriteLine("Removed successfully! Press enter to continue.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Door was not removed. Please try again.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid ID.");
                Console.ReadLine();
            }
        }

        public void Seed()
        {
            Badge one = new Badge(10015, new List<string>() { "A5", "B6", "C6" }, "Intern");
            Badge two = new Badge(10016, new List<string>() { "A5", "B6", "C6", "B5", "C4", "C3" }, "Employee");
            Badge three = new Badge(10017, new List<string>() { "A5", "B6", "C6", "B5", "C4", "C3", "A4", "A3", "B2" }, "Supervisor");
            Badge four = new Badge(10018, new List<string>() { "A5", "B6", "C6", "B5", "C4", "C3", "A4", "A3", "B2", "A1", "B1", "C1" }, "Master Key");

            _repo.AddNewBadge(one);
            _repo.AddNewBadge(two);
            _repo.AddNewBadge(three);
            _repo.AddNewBadge(four);
        }
    }
}
