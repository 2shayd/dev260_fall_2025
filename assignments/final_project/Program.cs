using System;
using Systen.Collections.Generic;
using System.Linq;
using System.IO;

public class Program
{

    public class TarotCard
    {
        public string Name { get; set; }
        public string Meaning { get; set; }
        public int ReversedMeaning { get; set; }

        public TarotCard(string name, string meaning, int reversed)
        {
            Name = name;
            Meaning = meaning;
            ReversedMeaning = reversed;
        }

        public override string ToString()
        {
            return $"{Name} of {Suit} (Number: {Number})";
        }
    }
    public static void Main(string[] args)
    {
        string filePath = "Data/cards.csv";
        List<TarotCard> deck = new List<TarotCard>();

        try
        {
            // Read all lines from the file
            lines = File.ReadAllLines(filePath).ToList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. Please check the file path.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return;
        }

        // Process each line
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }
}