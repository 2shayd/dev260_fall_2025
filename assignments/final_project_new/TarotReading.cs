using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

public class TarotReading
{
    private List<TarotCard> _cards;
    private readonly Random _random = new Random();
    private static Queue<TarotReading> readingHistory = new Queue<TarotReading>();
    public DateTime Timestamp { get; set; }
    public string SpreadType { get; set; }
    public List<TarotCard> DrawnCards { get; set; }

    public TarotReading(string spreadType, List<TarotCard> drawnCards)
    {
        Timestamp = DateTime.Now;
        SpreadType = spreadType;
        DrawnCards = drawnCards;
        _cards = new List<TarotCard>();
    }

    public class TarotCard
    {
        public string? Name { get; set; }
        public string? Meaning { get; set; }
        public string? ReversedMeaning { get; set; }
    
    }

    public void LoadCardsFromCsv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File '{filePath}' not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("Error: CSV file is empty.");
            return;
        }

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            _cards = csv.GetRecords<TarotCard>().ToList();
        }
        // test output
        Console.WriteLine($"Loaded {_cards.Count} tarot cards:");
        if (_cards.Count > 0)
        {
            Console.WriteLine(_cards[0].Name);
            Console.WriteLine(_cards[0].Meaning);
            Console.WriteLine(_cards[0].ReversedMeaning);
        }
    }

    public void CardSpread(string spreadType, int numberOfCards)
    {
        if (_cards == null || _cards.Count == 0)
        {
            Console.WriteLine("No cards available for a spread.");
            return;
        }
        
        var drawnCards = new List<TarotCard>();
        for (int i = 0; i < numberOfCards; i++)
        {
            var card = _cards[_random.Next(_cards.Count)]; //randomly select card

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
        Console.WriteLine($"Your {spreadType} reading cards are: {drawnCardsNames}\n");
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

    public void SearchCardByName(string name)
    {
        name = NormalizeString(name); //use helper to normalize input
        var card = _cards.FirstOrDefault(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (card != null)
        {
            Console.WriteLine($"\nName: {card.Name}");
            Console.WriteLine($"Meaning: {card.Meaning}");
            Console.WriteLine($"Reversed Meaning: {card.ReversedMeaning}\n");
        }
        else
        {
            Console.WriteLine($"Card '{name}' not found.\n");
        }
    }

    public void ViewHistory()
    {
        if (readingHistory.Count == 0)
        {
            Console.WriteLine("\nNo reading history available.\n");
            return;
        }

        string historyOutput = "";
        int pointer = 1;
        Console.WriteLine("\nLast 3 Readings:\n");

        for (int i = 0; i < readingHistory.Count; i++)
        {
            var reading = readingHistory.ElementAt(i);

            string DrawnCardsNames = string.Join(", ", reading.DrawnCards.Select(card => card.Name));

            historyOutput = $"Spread: {reading.SpreadType}\n   Card(s): {DrawnCardsNames}\n   Timestamp: {reading.Timestamp}\n";

            Console.WriteLine($"{pointer}. {historyOutput}");
            pointer++;
        }
    }

    //helper methods

    //normalize input string for searching
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

        //replace numbers with words
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


//save reading to history helper
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


//check if card is reversed and return appropriate meaning
}