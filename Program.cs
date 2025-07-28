using System;
using System.IO;

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
    class FileHandler
    {

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


            switch (userInput)
            {
                case "1":
                    Console.WriteLine(menuItem[0]);
                    break;
                case "2":
                    Console.WriteLine(menuItem[1]);
                    break;
                case "3":
                    Console.WriteLine(menuItem[2]);
                    break;
                case "4":
                    Console.WriteLine(menuItem[3]);
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