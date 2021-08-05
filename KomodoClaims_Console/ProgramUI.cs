using KomodoClaims_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KomodoClaims_Repo.Claim;

namespace KomodoClaims_Console
{
    class ProgramUI
    {
        KomodoClaims_Repo.ClaimRepo _repo = new KomodoClaims_Repo.ClaimRepo();
        public void Run()
        {
            SeedContent();
            Menu();
        }

        public void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------\n" +
                    "WELCOME to the Komodo Insurance Claims Handling System!\n" +
                    "Please Choose from the following options:\n" +
                    "-----------------------------------------------------------\n" +
                    "1. View All Claims in Queue\n" +
                    "2. Take Care of Next Claim\n" +
                    "3. Enter a new Claim\n" +
                    "4. Exit the program\n");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ViewClaims();
                        break;
                    case "2":
                        NextClaim();
                        break;
                    case "3":
                        NewClaim();
                        break;
                    case "4":
                        keepRunning = false;
                        Console.WriteLine("Goodbye! (Press enter to exit.)");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid entry, please try again.");
                        break;
                }
            }
        }
        private void ViewClaims()
        {
            Console.Clear();
            Queue<Claim> listOfClaims = _repo.ViewCurrentQueue();
            Console.WriteLine("*** **                          CURRENT CLAIMS QUEUE                           *****\n" +
                "|| ClaimID || Type  ||        Description         ||     Amount   ||  DateOfAccident  ||  DateOfClaim  || IsValid ||\n" +
                "------------------------------------------------------------------------------------------------------------------");
            foreach(Claim claim in listOfClaims)
            {
                Console.WriteLine($"||  {claim.ClaimID}  ||   {claim.TypeOfClaim}  ||   {claim.Description}    ||   ${claim.ClaimAmount}.00    ||   {claim.DateOfIncident.ToString("MM/dd/yyy")}   ||   {claim.DateOfClaim.ToString("MM/dd/yyy")}  || {claim.IsValid}");
            }
            Console.WriteLine("Press enter to return to the main menu.");
            Console.ReadLine();
        }
        private void NextClaim()
        {
            Console.Clear();
            Claim claim = _repo._currentQueue.Peek();
            Console.WriteLine("HERE IS THE NEXT CLAIM IN THE QUEUE:\n" +
                "|| ClaimID || Type  ||        Description         ||     Amount   ||  DateOfAccident  ||  DateOfClaim  || IsValid ||\n" +
                "------------------------------------------------------------------------------------------------------------------\n" +
                $"||  {claim.ClaimID}  ||   {claim.TypeOfClaim}  ||   {claim.Description}    ||   ${claim.ClaimAmount}.00    ||   {claim.DateOfIncident.ToString("MM/dd/yyy")}   ||   {claim.DateOfClaim.ToString("MM/dd/yyy")}  || {claim.IsValid}\n" +
                $"Would you like to deal with this claim now? (y/n)");
            string input = Console.ReadLine();
            switch(input)
            {
                case "y":
                    _repo.RemoveClaim();
                    Console.WriteLine("Claim has been removed from queue. Press enter to return to main menu.");
                    Console.ReadLine();
                    break;
                case "n":
                    Console.WriteLine("Claim has been returned to the front of the queue. Press enter to return to main menu.");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid entry. Returning to main menu..");
                    Console.ReadLine();
                    break;
            }
        }
        private void NewClaim()
        {
            Console.Clear();
            Claim newClaim = new Claim();
            bool keepAsking = true;
            while (keepAsking) 
            {
                Console.WriteLine("Please enter the Claim ID:");
                int id;
                bool validEntry = int.TryParse(Console.ReadLine(), out id);
                if (validEntry)
                {
                    newClaim.ClaimID = id;
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("Invalid answer.  Must be a number. Please try again.");
                }
            }

            keepAsking = true;
            while(keepAsking)
            {
                Console.WriteLine("Please enter the type of claim (Car, House, Theft,  or Renter)");
                string input = Console.ReadLine().ToLower();
                switch(input) 
                {
                    case "car":
                        newClaim.TypeOfClaim = ClaimType.Car;
                        keepAsking = false;
                        break;
                    case "house":
                        newClaim.TypeOfClaim = ClaimType.House;
                        keepAsking = false;
                        break;
                    case "theft":
                        newClaim.TypeOfClaim = ClaimType.Theft;
                        keepAsking = false;
                        break;
                    case "renter":
                        newClaim.TypeOfClaim = ClaimType.Renter;
                        keepAsking = false;
                        break;
                    default:
                        Console.WriteLine("Invalid entry. Please type only one of the four options listed.");
                        break;
                }
            }
            Console.WriteLine("Please enter a short description of the claim.");
            newClaim.Description = Console.ReadLine();

            keepAsking = true;
            while (keepAsking)
            {
                Console.WriteLine("Please enter the claim amount: (ex: 400.00)");
                Console.Write("$");
                double amt;
                bool validEntry = double.TryParse(Console.ReadLine(), out amt);
                if (validEntry)
                {
                    newClaim.ClaimAmount = amt;
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("Invalid entry. Answer must be a dollar amount.");
                }
            }

            keepAsking = true;
            while(keepAsking)
            {
                Console.WriteLine("Please enter the date of the incident: (MM/DD/YYY)");
                DateTime doi;
                bool validEntry = DateTime.TryParse(Console.ReadLine(), out doi);
                if (validEntry)
                {
                    newClaim.DateOfIncident = doi;
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("Answer must be in specified format, please try again.");
                }
            }

            keepAsking = true;
            while (keepAsking)
            {
                Console.WriteLine("Please enter the date of the claim: (MM/DD/YYY)");
                DateTime doc;
                bool validEntry = DateTime.TryParse(Console.ReadLine(), out doc);
                if (validEntry)
                {
                    newClaim.DateOfClaim = doc;
                    keepAsking = false;
                }
                else
                {
                    Console.WriteLine("Answer must be in specified format, please try again.");
                }
            }
            TimeSpan ts = newClaim.DateOfClaim - newClaim.DateOfIncident;
            if(ts.TotalDays < 30)
            {
                newClaim.IsValid = true;
            }
            else
            {
                newClaim.IsValid = false;
            }
            _repo._currentQueue.Enqueue(newClaim);
            Console.Clear();
            Console.WriteLine("THE FOLLOWING CLAIM WAS ADDED SUCCESFULLY:\n" +
            "|| ClaimID || Type  ||        Description         ||     Amount   ||  DateOfAccident  ||  DateOfClaim  || IsValid ||\n" +
            "------------------------------------------------------------------------------------------------------------------\n" +
            $"||  {newClaim.ClaimID}  ||   {newClaim.TypeOfClaim}  ||   {newClaim.Description}    ||   ${newClaim.ClaimAmount}.00    ||   {newClaim.DateOfIncident.ToString("MM/dd/yyy")}   ||   {newClaim.DateOfClaim.ToString("MM/dd/yyy")}  || {newClaim.IsValid}\n" +
            "Press enter to return to the main menu.");
            Console.ReadLine();
        }
        private void SeedContent()
        {
            Claim claimOne = new Claim(1010, "Car accident on the 465.", 400.00, new DateTime(2018, 04, 25), new DateTime(2018, 04, 27), true, Claim.ClaimType.Car);
            Claim claimTwo = new Claim(1012, "House fire in kitchen.", 4000.00, new DateTime(2018, 04, 11), new DateTime(2018, 04, 12), true, Claim.ClaimType.House);
            Claim claimThree = new Claim(1013, "Stolen pancakes...Oh no!", 4.00, new DateTime(2018, 04, 27), new DateTime(2018, 06, 01), false, Claim.ClaimType.Theft);
            Claim claimFour = new Claim(1014, "Water damage from flood.", 1000.00, new DateTime(2018, 04, 30), new DateTime(2018, 05, 25), true, Claim.ClaimType.Renter);

            _repo.AddNewClaim(claimOne);
            _repo.AddNewClaim(claimTwo);
            _repo.AddNewClaim(claimThree);
            _repo.AddNewClaim(claimFour);
        }
    }
}