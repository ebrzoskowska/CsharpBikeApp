/* **************************************************************** */
/*                        COC001 - ASSIGNMENT 2                     */
/* **************************************************************** */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Assignment2
{
    class Program
    {
        static List<Bike> Bikes = new List<Bike>();
        public static string filePath = @"inventory.txt";

        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
            Console.ReadLine();
        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Welcome to the bike world!");
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Add Bike");
            Console.WriteLine("2) Dislay Bikes Inventory");
            Console.WriteLine("3) Remove Bike");
            Console.WriteLine("4) Update Bike Data");
            Console.WriteLine("0) Exit");
            Console.Write("\r\nSelect an option: ");

            // main menu with validation
            var menuChoice = Console.ReadLine();
            int userChoice;
            while (!int.TryParse(menuChoice, out userChoice) || userChoice > 4 || userChoice < 0)
            // it will check if user choice is an int in range between 0 and 4
            {
                Console.WriteLine();
                Console.Write("Please pass a numeric value, select from options 0, 1, 2, 3, 4: ");
                menuChoice = Console.ReadLine();
                Console.Clear();
            }

            switch (userChoice)
            {
                case 1:
                    AddBike();
                    return true;
                case 2:
                    Bikes.Clear();
                    DisplayFile();
                    return true;
                case 3:
                    RemoveBike();
                    return true;
                case 4:
                    ModifyBikeData();
                    return true;
                case 0:
                    return false;
                default:
                    return true;
            }
        }

        private static void AddBike()
        {
            char createAnotherBike = 'N';
            do
            {
                // user input validation
                Console.Write("\nEnter bike make: ");
                var bikeMake = Console.ReadLine();
                while (string.IsNullOrEmpty(bikeMake)) // it will check if passed string is not empty
                {
                    Console.Write("Bike make cannot be empty. Type bike make once more please: ");
                    bikeMake = Console.ReadLine();
                }

                Console.Write("Enter bike type: ");
                var bikeType = Console.ReadLine();
                while (string.IsNullOrEmpty(bikeType)) // it will check if passed string is not empty
                {
                    Console.Write("Bike type cannot be empty. Type bike type once more please: ");
                    bikeType = Console.ReadLine();
                }

                Console.Write("Enter bike model: ");
                var bikeModel = Console.ReadLine();
                while (string.IsNullOrEmpty(bikeModel)) // it will check if passed string is not empty
                {
                    Console.Write("Bike model cannot be empty. Type bike model once more please: ");
                    bikeModel = Console.ReadLine();
                }

                int bikeYear;
                Console.Write("Enter bike year. Please pass value between 2017 and 2021: ");
                string bikeYearString = Console.ReadLine();
                while (!int.TryParse(bikeYearString, out bikeYear) || bikeYear > 2021 || bikeYear < 2017) // passing parameter by reference with 'out' keyword and it will give us back age as integer if the parsing is success.
                {
                    Console.Write("Enter bike year once more please. Please pass value between 2017 and 2021: ");
                    bikeYearString = Console.ReadLine();
                }

                Console.Write("Enter bike wheel size: ");
                var BikeWheelSize = Console.ReadLine();
                while (string.IsNullOrEmpty(BikeWheelSize)) // it will check if passed string is not empty
                {
                    Console.Write("Bike wheel size cannot be empty. Type bike wheel size once more please: ");
                    BikeWheelSize = Console.ReadLine();
                }

                Console.Write("Enter bike frame type: ");
                var BikeFrameType = Console.ReadLine();
                while (string.IsNullOrEmpty(BikeFrameType)) // it will check if passed string is not empty
                {
                    Console.Write("Bike frame type cannot be empty. Type bike frame type once more please: ");
                    BikeFrameType = Console.ReadLine();
                }

                int bikeSecurityCode;
                Console.Write("Enter bike security code. Please pass value between 644000000 and 650000000: ");
                string bikeSecurityCodeString = Console.ReadLine();
                while (!int.TryParse(bikeSecurityCodeString, out bikeSecurityCode) || bikeSecurityCode > 650000000 || bikeSecurityCode < 644000000) // passing parameter by reference with 'out' keyword and it will give us back age as integer if the parsing is success.
                {
                    Console.Write("Enter bike security code once more please. Please pass value between 644000000 and 650000000: ");
                    bikeSecurityCodeString = Console.ReadLine();
                }

                Bikes.Add(new Bike { Make = bikeMake, Type = bikeType, Model = bikeModel, Year = bikeYear, WheelSize = BikeWheelSize, FrameType = BikeFrameType, SecurityCode = bikeSecurityCode });
                File.AppendAllText(filePath, $"\n\n{bikeMake}\n{bikeType}\n{bikeModel}\n{bikeYear}\n{BikeWheelSize}\n{BikeFrameType}\n{bikeSecurityCode}");

                Console.Write("\nYour item was succesfully added.");

                //Confirm if another bike to be created
                Console.Write("\nDo you want to add another bike [Y/N]? : ");
                createAnotherBike = char.ToUpper(Console.ReadKey(false).KeyChar); //Compare always upper case input irrespective of user input casing.

            } while (createAnotherBike == 'Y');

            BackToMainMenu();
        }

        public static void DisplayFile()
        {
            // create a list which read each line from the file
            List<string> lines = File.ReadAllLines(filePath).ToList();

            int invCounter = 0;
            int invLength = lines.Count / 8; //works out the number of bikes in the inventory.txt file
            Console.WriteLine("\nThere are {0} bikes in the inventory", invLength);

            for (int i = 0; i <= invLength; i++) //for each bike
            {
                string make = lines[i + invCounter++]; //store the make
                string type = lines[i + invCounter++]; //store the type
                string model = lines[i + invCounter++]; //store the model
                int year = int.Parse(lines[i + invCounter++]); //store the year
                string wheelSize = lines[i + invCounter++]; //store the wheel size
                string frameType = lines[i + invCounter++]; //store the frame type
                int securityCode = int.Parse(lines[i + invCounter++]); //store the security code

                Bike bk = new Bike(make, type, model, year, wheelSize, frameType, securityCode); //create a new bike object with the details stored above
                Bikes.Add(bk); //add the bike to Bikes list
            }

            // read from text file and check if it is possible to and display
            Console.WriteLine("_____________________________________________________________________________________________________________________");
            Console.WriteLine("\n{0,-10}\t {1,-10}\t {2,-10}\t {3,-10}\t {4,-5}\t {5,-5}\t {6, 20}", "Security Code", "Type", "Make", "Model", "Year", "Wheel Size", "Frame Type\n");
            Console.WriteLine("_____________________________________________________________________________________________________________________\n");
            foreach (Bike bk in Bikes)
            {
                Console.WriteLine("{0,-10}\t {1,-10}\t {2,-10}\t {3,-10}\t {4,-5}\t {5,-5}\t {6, 30}", bk.SecurityCode, bk.Type, bk.Make, bk.Model, bk.Year, bk.WheelSize, bk.FrameType);
            }
            Console.ReadLine();
        }

        private static void RemoveBike()
        {
            DisplayFile();

            List<string> lines = File.ReadAllLines(filePath).ToList();

            //Console.Write("Enter the item SC you want to remove: ");
            //string itemId = Console.ReadLine();
            Console.Write("Pass Security code of bike you would like to delete: ");
            var itemId = Console.ReadLine();
            while (string.IsNullOrEmpty(itemId)) // it will check if passed string is not empty
            {
                Console.Write("Security code cannot be empty. Pass Security code of bike you would like to delete: ");
                itemId = Console.ReadLine();
            }

            int startIndex = lines.IndexOf(itemId) - 6;

            // last line range has to be removed slightly different as we want to remove blank line from te top not bottom
            var lastLine = File.ReadLines(filePath).Last();
            int lastLineIndex = lines.IndexOf(itemId) - 7;


            if (itemId == lastLine)
            {

                lines.RemoveRange(lastLineIndex, 8);
            }
            else
            {
                lines.RemoveRange(startIndex, 8);
            }

            Console.Write("Your item was succesfully deleted.");

            File.WriteAllText(filePath, String.Join("\r\n", lines));
            Console.ReadLine();
        }

        public static void ModifyBikeData()
        {

            DisplayFile(); // Print all the items in your inventory

            List<string> lines = File.ReadAllLines(filePath).ToList();

            Console.Write("Pass Security code of bike you would like to update: ");

            var itemId = Console.ReadLine();
            while (string.IsNullOrEmpty(itemId)) // it will check if passed string is not empty
            {
                Console.Write("Security code cannot be empty. Pass Security code of bike you would like to update: ");
                itemId = Console.ReadLine();
            }

            var idIndexNumber = lines.IndexOf(itemId);

            for (int row = idIndexNumber; row > idIndexNumber - 7; row--)
            {
                Console.Write("\nCurrent value: " + lines[row]);
                Console.Write("\nUpdate value: ");
                string updatedData = Console.ReadLine();
                lines[row] = updatedData;
            }

            File.WriteAllText(filePath, String.Join("\r\n", lines));
        }

        private static void BackToMainMenu()
        {
            Console.ReadLine();
            Console.Write("\r\nPress Enter to return to --MAIN MENU--");
            Console.ReadLine();
        }
    }
}
