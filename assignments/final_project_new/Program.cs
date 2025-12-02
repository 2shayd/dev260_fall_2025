using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CsvHelper;
using System.Globalization;

public class Program
{
        public static void Main(string[] args)
    {
        try
        {
            //initialize tarot reading
            var tarotReading = new TarotReading("", new List<TarotReading.TarotCard>());

            //load tarot cards from csv file
            Console.WriteLine("Loading tarot cards from CSV...");
            tarotReading.LoadCardsFromCsv("Data/cards.csv");

            //start navigation menu
            var navigation = new TarotReadingNav(tarotReading);
            navigation.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}