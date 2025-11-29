using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using System.Globalization;

public class TarotReading
{
    private List<TarotCard> _cards;
    private readonly Random _random = new Random();

    public TarotReading()
    {
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

    public void DailyCardSpread(string spreadType)
    {
        if (_cards == null || _cards.Count == 0)
        {
            Console.WriteLine("No cards available for a spread.");
            return;
        }

        var drawnCard = _cards[_random.Next(_cards.Count)];
        Console.WriteLine($"Your {spreadType} reading card is:\n");
        Console.WriteLine($"Name: {drawnCard.Name}");
        Console.WriteLine($"Meaning: {drawnCard.Meaning}");
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
            drawnCards.Add(_cards[_random.Next(_cards.Count)]); 
        }

        var drawnCardsNames = string.Join(", ", drawnCards.Select(card => card.Name));
        Console.WriteLine($"Your {spreadType} reading cards are: {drawnCardsNames}\n");
        foreach (var card in drawnCards)
        {
            Console.WriteLine($"Name: {card.Name}");
            Console.WriteLine($"Meaning: {card.Meaning}\n");
        }
    }
}