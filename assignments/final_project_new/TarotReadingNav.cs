using System;
using System.Collections.Generic;
using System.Linq;

public class TarotReadingNav(TarotReading tarotReading)
{
    public void Run()
    {
        bool running = true;
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";
            string spreadType;

            switch (choice)
            {
                case "1":
                    spreadType = "daily";
                    tarotReading.CardSpread(spreadType, 1);
                    break;
                case "2":
                    spreadType = "3-card";
                    tarotReading.CardSpread(spreadType, 3);
                    break;
                case "3":
                    Console.WriteLine("You selected: List all cards");
                    // Implement list all cards logic here
                    break;
                case "4":
                    Console.WriteLine("You selected: Search card by name");
                    // Implement search card by name logic here
                    break;
                case "5":
                    Console.WriteLine("Exiting the Tarot Reader. Goodbye!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
            if (running)
            {
                Console.WriteLine("\nPress Enter to return to the menu...");
                Console.ReadLine();
            }
        }
    }
    
    public static void DisplayMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════╗");
        Console.WriteLine("║      TAROT READER        ║");
        Console.WriteLine("╚══════════════════════════╝\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Please select an option:\n");
        Console.ResetColor();

        Console.WriteLine(" 1) Draw a daily card");
        Console.WriteLine(" 2) Draw a 3-card spread (Past/Present/Future)");
        Console.WriteLine(" 3) List all cards");
        Console.WriteLine(" 4) Search card by name");
        Console.WriteLine(" 5) Quit\n");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Enter choice (1-5): ");
        Console.ResetColor();
    }
}