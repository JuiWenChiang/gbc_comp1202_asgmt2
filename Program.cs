using System;
using System.IO;
using System.Runtime.CompilerServices;

/* 
    The program should be completed in a single file and only the .cs file must be submitted.
*/
namespace gbc_comp1202_asgmt2
{
    /* Game Class Developer - Anastasiia */
    // Game class represents a video game item in the shop inventory
    public class Game
    {
        // Private fields
        private string itemNumber;
        private string itemName;
        private decimal price;
        private int userRating;
        private int quantity;

        // Default constructor
        public Game()
        {
            itemNumber = "";
            itemName = "";
            price = 0;
            userRating = 1;
            quantity = 0;
        }

        // Constructor with all parameters
        public Game(string itemNumber, string itemName, decimal price, int userRating, int quantity)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            this.userRating = userRating;
            this.quantity = quantity;
        }

        // Getter and Setter for ItemNumber
        public string GetItemNumber()
        {
            return itemNumber;
        }

        public void SetItemNumber(string value)
        {
            // Simple validation - check if it's 4 digits
            if (value.Length == 4)
            {
                itemNumber = value;
            }
            else
            {
                Console.WriteLine("Item number must be exactly 4 digits!");
            }
        }

        // Getter and Setter for ItemName
        public string GetItemName()
        {
            return itemName;
        }

        public void SetItemName(string value)
        {
            if (value == "")
            {
                Console.WriteLine("Item name cannot be empty!");
            }
            else
            {
                itemName = value;
            }
        }

        // Getter and Setter for Price
        public decimal GetPrice()
        {
            return price;
        }

        public void SetPrice(decimal value)
        {
            if (value >= 0)
            {
                price = value;
            }
            else
            {
                Console.WriteLine("Price cannot be negative!");
            }
        }

        // Getter and Setter for UserRating
        public int GetUserRating()
        {
            return userRating;
        }

        public void SetUserRating(int value)
        {
            if (value >= 1 && value <= 5)
            {
                userRating = value;
            }
            else
            {
                Console.WriteLine("Rating must be between 1 and 5!");
            }
        }

        // Getter and Setter for Quantity
        public int GetQuantity()
        {
            return quantity;
        }

        public void SetQuantity(int value)
        {
            if (value >= 0)
            {
                quantity = value;
            }
            else
            {
                Console.WriteLine("Quantity cannot be negative!");
            }
        }

        // ToString method - displays game information nicely
        public override string ToString()
        {
            return "Item #: " + itemNumber + " | " + itemName + " | $" + price + " | Rating: " + userRating + "/5 | Stock: " + quantity;
        }

        // Convert game to file format (CSV)
        public string ToFileString()
        {
            return itemNumber + "," + itemName + "," + price + "," + userRating + "," + quantity;
        }

        // Create a game from a file line
        public static Game FromFileString(string fileLine)
        {
            string[] parts = fileLine.Split(',');

            if (parts.Length == 5)
            {
                string itemNum = parts[0];
                string itemName = parts[1];
                decimal price = decimal.Parse(parts[2]);
                int rating = int.Parse(parts[3]);
                int qty = int.Parse(parts[4]);

                return new Game(itemNum, itemName, price, rating, qty);
            }
            else
            {
                Console.WriteLine("Error reading file line!");
                return new Game();
            }
        }

        // Generate a new 4-digit item number
        public static string GenerateItemNumber()
        {
            Random random = new Random();
            int number = random.Next(1000, 9999);
            return number.ToString();
        }

        // Check if game is in stock
        // Returns true if quantity > 0, false if quantity is 0 or less
        public bool IsInStock()
        {
            return quantity > 0;
        }

    }

    /* File & Search Handler Class Developer - Howard */
    public class FileHandler
    {
        private const string FILENAME = "VideoGames.txt";
        Game[]? gamesList;

        public FileHandler()
        {
            try
            {
                int lineCount = 0;
                string? fileLine;
                int i = 0;
                // Get array size
                StreamReader? sr = new StreamReader(FILENAME);
                while (sr.ReadLine() != null) lineCount++;
                gamesList = new Game[lineCount];
                sr.Close();
                // Build array of game objects
                sr = new StreamReader(FILENAME);
                while ((fileLine = sr.ReadLine()) != null) gamesList[i++] = Game.FromFileString(fileLine);
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool ViewAll()
        {
            if (gamesList != null)
            {
                foreach (var game in gamesList)
                {
                    Console.WriteLine(game.ToString());
                }
            }
            return false;
        }
        public bool Append(Game game)
        {
            StreamWriter? sw = null;
            bool isSuccess = false;
            try
            {
                sw = new StreamWriter(FILENAME, true);
                sw.WriteLine(game.ToFileString());
                isSuccess = true;
            }
            finally
            {
                sw?.Close();

            }
            return isSuccess;
        }
        // Lookup by item number
        public bool Lookup(string itemNumber)
        {
            bool isSuccess = false;
            if (gamesList != null)
                foreach (var game in gamesList)
                {
                    if (game.GetItemNumber() == itemNumber)
                    {
                        Console.WriteLine(game.ToString());
                        return true;
                    }
                }
            return isSuccess;
        }

        // Overloaded Lookup by max price
        public bool Lookup(decimal maxPrice)
        {
            bool isSuccess = false;
            int count = 0;
            Game[] filteredGames = [];
            if (gamesList != null)
            {
                foreach (var game in gamesList)
                {
                    if (game.GetPrice() <= maxPrice)
                    {
                        Array.Resize(ref filteredGames, ++count);
                        filteredGames[count - 1] = game;
                    }
                }
                Console.WriteLine("\n{0} listings found under {1:C}:", filteredGames.Length, maxPrice);
                foreach (var game in filteredGames)
                {
                    Console.WriteLine(game.ToString());
                }
                return true;
            }
            return isSuccess;
        }

        public bool ViewStats()
        {
            if (gamesList != null)
            {
                int productsTotal = 0;
                decimal valueTotal = 0m;
                decimal meanPrice;
                decimal price;
                Game cheapestGame = gamesList[0];
                Game priciestGame = gamesList[0];

                foreach (var game in gamesList)
                {
                    price = game.GetPrice();
                    productsTotal += game.GetQuantity();
                    valueTotal += game.GetQuantity() * price;

                    if (price < cheapestGame.GetPrice()) cheapestGame = game;
                    else if (price >= priciestGame.GetPrice()) priciestGame = game;
                }
                meanPrice = valueTotal / productsTotal;
                Console.WriteLine("\nThe mean price of {0} stocked items valued at {1:C}: {2:C}", productsTotal, valueTotal, meanPrice);
                Console.WriteLine("\nThe price range of our products is between {0:C} ~ {1:C}", cheapestGame.GetPrice(), priciestGame.GetPrice());
                Console.WriteLine("\nOur cheapest game is:\n{0}", cheapestGame);
                Console.WriteLine("\nAnd our most expensive game is:\n{0}", priciestGame);
            }
            else
            {
                Console.WriteLine("No games! Try adding some.");
            }
            return true;
        }
    }

    /*  Menu & Main Program Developer - JuiWen */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=== Welcome to Luck 7's video game shop! ===\n");
            // Theusershouldbeabletoaccessthedifferentoptionsviaamenuthatshouldbe displayed on running the program. The menu should include options which allow the user to display the information saved in the file about the items, to add new items, search for an item, perform statistical analysis and any other options deemed necessary, including an exit option.

            // menu item
            string[] menuItem = {
                "1.Display the information",
                "2.Add new items",
                "3.Search for an item",
                "4.Statistical analysis ",
                "5.Exit"
            };

            Console.WriteLine("Please enter a number for the options.");
            string? userInput;

            for (var item = 0; menuItem.Length > item; item++)
            {
                Console.WriteLine($"{menuItem[item]}");
            }

            userInput = Console.ReadLine();

            FileHandler file = new FileHandler();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine(menuItem[0]);
                    // Example usage:
                    // file.ViewAll();
                    break;
                case "2":
                    Console.WriteLine(menuItem[1]);
                    // Example usage:
                    // file.Append(new Game("1234", "Tetris", 10.50m, 5, 1));
                    break;
                case "3":
                    Console.WriteLine(menuItem[2]);
                    // Example usage:
                    // file.Lookup("0299"); // Item num
                    // file.Lookup(59.99m); // Max Price
                    break;
                case "4":
                    Console.WriteLine(menuItem[3]);
                    // Example usage:
                    // file.ViewStats();
                    break;
                case "5":
                    Console.WriteLine(menuItem[4]);
                    Console.WriteLine("Exiting program.Have a nice day and see you again! :) ");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }

        }
    }
}