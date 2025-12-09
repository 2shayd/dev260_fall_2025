using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

public class TarotReading
{
    private List<TarotCard> _cards;
    public class TarotCard
    {
        public string? Name { get; set; }
        public string? Meaning { get; set; }
        public string? ReversedMeaning { get; set; }
    }
    
    private static Queue<TarotReading> readingHistory = new Queue<TarotReading>();
    private readonly Random _random = new Random();

    public List<TarotCard> DrawnCards { get; set; }
    public DateTime Timestamp { get; set; }
    public string SpreadType { get; set; }
    public TarotReading(string spreadType, List<TarotCard> drawnCards)
    {
        Timestamp = DateTime.Now;
        SpreadType = spreadType;
        DrawnCards = drawnCards;
        _cards = new List<TarotCard>();
    }

/// <summary>
/// OPTION 1
/// Load tarot cards from CSV file
/// </summary>
/// <param name="filePath"></param>
    public void LoadCardsFromCsv(string filePath)
    {
        // Check if file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File '{filePath}' not found.");
            return;
        }

        // Check if file is empty
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("Error: CSV file is empty.");
            return;
        }

        // Read CSV file
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // put cards into list structure
            _cards = csv.GetRecords<TarotCard>().ToList();
        }
    }

/// <summary>
/// OPTION 2
/// List cards based on user choice
/// </summary>
/// <param name="listChoice"></param>
    public void ListCards(string listChoice)
    {
        if (_cards == null || _cards.Count == 0)
        {
            Console.WriteLine("No cards available to list.");
            return;
        }
        int pointer = 1;

        if (listChoice == "major") // list major arcana cards
        {
            var majorArcana = new List<TarotCard>();

            for (int i = 0; i < 22; i++)
            {
                majorArcana.Add(_cards[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n====Major Arcana Cards====\n");
            Console.ResetColor();

            foreach (var card in majorArcana)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write($"{pointer}. ");
                Console.ResetColor();
                Console.Write($"{card.Name}\n");
                pointer++;
            }
        }
        else if (listChoice == "minor") // list minor arcana cards
        {
            var minorArcana = new List<TarotCard>();
            
            for (int i = 22; i < _cards.Count; i++)
            {
                minorArcana.Add(_cards[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n====Minor Arcana Cards====\n");
            Console.ResetColor();

            foreach (var card in minorArcana)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{pointer}. ");
                Console.ResetColor();
                Console.Write($"{card.Name}\n");
                pointer++;
            }
        }
        else if (listChoice == "all") // list all cards
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n====All Tarot Cards====\n");
            Console.ResetColor();

            foreach (var card in _cards)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{pointer}. ");
                Console.ResetColor();
                Console.Write($"{card.Name}\n");
                pointer++;
            }
        }
        else
        {
            Console.WriteLine("Invalid list choice.");
        }
    }

/// <summary>
/// OPTION 3
/// Perform a card reading with chosen spread type
/// </summary>
/// <param name="spreadType"></param>
/// <param name="numberOfCards"></param>
    public void CardSpread(string spreadType, int numberOfCards)
    {
        if (_cards == null || _cards.Count == 0)
        {
            Console.WriteLine("No cards available for a spread.");
            return;
        }

        // draw unique cards by copying the deck and removing drawn cards
        var available = new List<TarotCard>(_cards);
        var drawnCards = new List<TarotCard>();

        for (int i = 0; i < numberOfCards; i++)
        {
            int idx = _random.Next(available.Count);
            var card = available[idx];
            available.RemoveAt(idx);

            bool isReversed = _random.Next(2) == 0; //50% chance of being reversed

            drawnCards.Add(new TarotCard
            {
                Name = card.Name,
                Meaning = isReversed ? card.ReversedMeaning : card.Meaning,
                ReversedMeaning = card.ReversedMeaning
            });
        }

        var reading = new TarotReading(spreadType, drawnCards);

        //save reading to history using helper
        SaveReading(reading);

        //display results
        var drawnCardsNames = string.Join(", ", drawnCards.Select(card => card.Name));
        Console.WriteLine($"Your {spreadType} reading card(s) are: {drawnCardsNames}\n");
        foreach (var card in drawnCards)
        {
            if (card.Meaning == card.ReversedMeaning)
            {
                Console.WriteLine($"{card.Name} (Reversed)");
            }
            else
            {
                Console.WriteLine($"{card.Name}");
            }
            Console.WriteLine($"Meaning: {card.Meaning}\n");
        }
        
    }

/// <summary>
/// OPTION 4
/// Search for a card by name and display its meanings
/// </summary>
/// <param name="name"></param>
    public void SearchCardByName(string name)
    {
        string normalizedName = NormalizeString(name); //use helper to normalize input
        var card = _cards.FirstOrDefault(c => c.Name != null && NormalizeString(c.Name).Equals(normalizedName, StringComparison.OrdinalIgnoreCase));
        if (card != null)
        {
            DisplayOneCard(card);
        }
        else
        {
            Console.WriteLine($"Card '{name}' not found.\n");
        }
    }

/// <summary>
/// OPTION 5
/// View last 3 readings from history
/// </summary>
    public void ViewHistory()
    {
        if (readingHistory.Count == 0)
        {
            Console.WriteLine("\nNo reading history available.\n");
            return;
        }

        string historyOutput = "";
        int pointer = 1;
        Console.WriteLine("\nReading History:\n");

        for (int i = 0; i < readingHistory.Count; i++)
        {
            var reading = readingHistory.ElementAt(i);

            string DrawnCardsNames = string.Join(", ", reading.DrawnCards.Select(card => card.Name));

            historyOutput = $"Spread: {reading.SpreadType}\n   Card(s): {DrawnCardsNames}\n   Timestamp: {reading.Timestamp}\n";

            Console.WriteLine($"{pointer}. {historyOutput}");
            pointer++;
        }
    }

/// <summary>
/// OPTION 6
/// Update the meanings of a card
/// </summary>
/// <param name="name"></param>
/// <param name="newMeaning"></param>
/// <param name="newReversedMeaning"></param>
    public void UpdateCardMeaning(string name)
    {
        string normalizedName = NormalizeString(name);
        var card = _cards.FirstOrDefault(c => c.Name != null && NormalizeString(c.Name).Equals(normalizedName, StringComparison.OrdinalIgnoreCase));

        if (card != null)
        {
            //display current meanings for chosen card
            Console.WriteLine($"\nYou are updating: {card.Name}");
            Console.WriteLine("===Current Meanings===");
            DisplayOneCard(card);
            
            string successMessage = "";
            string newMeaning = "";
            string newReversedMeaning = "";
            bool cardUpdated = false;

            bool validChoice = false;
            while (!validChoice)
            {
                //get which meaning user wants to update or cancel
                Console.WriteLine("Please select an option (1-3):\n");
                Console.WriteLine(" 1) Update upright meaning");
                Console.WriteLine(" 2) Update reversed meaning");
                Console.WriteLine(" 3) Update both meanings");
                Console.WriteLine(" 4) Cancel (return to menu)\n");
                Console.Write("Enter choice (1-4): ");

                string updateChoice = Console.ReadLine() ?? "";

                switch (updateChoice)
                {
                    case "1": //update upright meaning
                        Console.Write("Enter new upright meaning: ");
                        newMeaning = Console.ReadLine() ?? "";
                        
                        if (!string.IsNullOrWhiteSpace(newMeaning))
                        {
                            card.Meaning = newMeaning;
                            successMessage = $"\nUpright meaning for '{card.Name}' has been updated successfully.\n";
                            cardUpdated = true;
                            validChoice = true;
                        }
                        else
                        {
                            Console.WriteLine("\nUpright meaning cannot be empty. Please try again.\n");
                        }
                        break;

                    case "2": //update reversed meaning
                        Console.Write("Enter new reversed meaning: ");
                        newReversedMeaning = Console.ReadLine() ?? "";
                        
                        if (!string.IsNullOrWhiteSpace(newReversedMeaning))
                        {
                            card.ReversedMeaning = newReversedMeaning;
                            successMessage = $"\nReversed meaning for '{card.Name}' has been updated successfully.\n";
                            cardUpdated = true;
                            validChoice = true;
                        }
                        else
                        {
                            Console.WriteLine("\nReversed meaning cannot be empty. Please try again.\n");
                        }
                        break;
                    case "3": //update both meanings
                        Console.Write("Enter new upright meaning: ");
                        newMeaning = Console.ReadLine() ?? "";
                        Console.Write("Enter new reversed meaning: ");
                        newReversedMeaning = Console.ReadLine() ?? "";

                        if (!string.IsNullOrWhiteSpace(newMeaning) && !string.IsNullOrWhiteSpace(newReversedMeaning))
                        {
                            card.Meaning = newMeaning;
                            card.ReversedMeaning = newReversedMeaning;
                            successMessage = $"\nBoth meanings for '{card.Name}' have been updated successfully.\n";
                            cardUpdated = true;
                            validChoice = true;
                        }
                        else
                        {
                            Console.WriteLine("\nMeanings cannot be empty. Please try again.\n");
                        }
                        break;

                    case "4": //cancel
                        successMessage = "\nUpdate cancelled. Returning to menu.\n";
                        cardUpdated = false;
                        validChoice = true;
                        break;

                    default: //invalid choice
                        Console.WriteLine("\nInvalid choice. Please enter 1, 2, 3, or 4.\n");
                        break;
                }
            }
            
            Console.WriteLine(successMessage);

            if (cardUpdated)
            {
                Console.WriteLine("===Updated Card===");
                DisplayOneCard(card);
            }
        }
        else
        {
            Console.WriteLine($"\nCard '{name}' not found.\n");
            return;
        }
    }


//helper methods

/// <summary>
/// Normalize input string for searching card by name
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
    public string NormalizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        //lowercase and trim
        input = input.ToLower().Trim();

        //collapse extra spaces
        input = Regex.Replace(input, @"\s+", " ");

        //remove leading "the" e.g. "The Fool" -> "Fool"
        input = Regex.Replace(input, @"\b(the)\b\s*", "").Trim();

        //replace numbers with words e.g. "3" -> "three" "1" -> "ace"
        Dictionary<string, string> numbers = new Dictionary<string, string>()
        {
            {"1", "ace"},
            {"2", "two"},
            {"3", "three"},
            {"4", "four"},
            {"5", "five"},
            {"6", "six"},
            {"7", "seven"},
            {"8", "eight"},
            {"9", "nine"},
            {"10", "ten"},
            {"11", "page"},
            {"12", "knight"},
            {"13", "queen"},
            {"14", "king"}

        };

        foreach (var pair in numbers)
        {
            input = Regex.Replace(input, $@"\b{pair.Key}\b", pair.Value);
        }

        return input;    
    }


/// <summary>
/// Save reading to history using queue, limit to last 3 readings
/// </summary>
/// <param name="reading"></param>
     static void SaveReading(TarotReading reading)
    {
        //save reading to history
        readingHistory.Enqueue(reading);

        //limit to 3 readings
        if (readingHistory.Count > 3)
        {
            readingHistory.Dequeue(); //remove oldest reading
        }
    }


/// <summary>
/// Display details of one card helper
/// </summary>
/// <param name="card"></param>
    static void DisplayOneCard(TarotCard card)
    {
        Console.WriteLine($"\nName: {card.Name}");
        Console.WriteLine($"Meaning: {card.Meaning}");
        Console.WriteLine($"Reversed Meaning: {card.ReversedMeaning}\n");
    }

}