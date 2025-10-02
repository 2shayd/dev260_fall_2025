using System.Runtime.CompilerServices;

class Program
{

    static void Main(string[] args)
    {
        SumEvenNumbers();

        Console.WriteLine("Enter a grade percentage (0-100):");
        int score = Int32.Parse(Console.ReadLine());
        GetLetterGrade(score); // Example usage, you can change the score to test different cases
    }

    static void SumEvenNumbers()
    {
        //TASK 1
        //Write a method that calculates the sum of even numbers between 1 and 100 using:
        //for loop
        int forSum = 0;

        for (int i = 0; i <= 100; i += 2)
        {
            forSum += i;
        }

        Console.WriteLine($"Sum of even numbers between 1 and 100 in for loop is: {forSum}");

        //while loop
        int x = 0;
        int whileSum = 0;
        while (x <= 100 && x % 2 == 0)
        {
            whileSum += x;
            x += 2;
        }

        //TASK 3 Modify the method to print thats a big number if the sum is greater than 2000
        //TASK 3 ternary operator
        string message = (whileSum > 2000) ? "Thats a big number from the while sum!" : "";
        Console.WriteLine(message);
        Console.WriteLine($"Sum of even numbers between 1 and 100 in while loop is: {whileSum}");

        //foreach loop
        int foreachSum = 0;
        foreach (int num in Enumerable.Range(1, 100))
        {
            if (num % 2 == 0)
            {
                foreachSum += num;
            }
        }

        //TASK 3 Modify the method to print thats a big number if the sum is greater than 2000
        //TASK 3 if/else statement
        if (foreachSum > 2000)
        {
            Console.WriteLine("Thats a big number from the for each sum!");
            Console.WriteLine($"Sum of even numbers between 1 and 100 in foreach loop is: {foreachSum}");
        }
        else
        {
            Console.WriteLine($"Sum of even numbers between 1 and 100 in foreach loop is: {foreachSum}");
        }


        string question1 = "Answer to question 1: The for loop felt the most natural to me for this task. Maybe its because it's the first type of loop I used and I feel like I've used it most frequently as its very versatile.";
        Console.WriteLine(question1);
    }
    static void GetLetterGrade(int score)
    {
        //TASK 2

        //Write a method that takes a grade percentage (0-100) and returns the corresponding letter grade using:
        //if else statement
        string ifLetterGrade = "";

        if (score < 60)
        {
            ifLetterGrade = "F";

        }
        else if (score >= 60 && score < 70)
        {
            ifLetterGrade = "D";
        }
        else if (score >= 70 && score < 80)
        {
            ifLetterGrade = "C";
        }
        else if (score >= 80 && score < 90)
        {
            ifLetterGrade = "B";
        }
        else if (score >= 90 && score <= 100)
        {
            ifLetterGrade = "A";
        }
        else
        {
            Console.WriteLine("Invalid grade percentage. Please enter a value between 0 and 100.");
        }

        Console.WriteLine($"The letter grade in if is: {ifLetterGrade}.");

        //switch statement
        string switchLetterGrade = "";
        switch (score)
        {
            case int n when n < 60:
                switchLetterGrade = "F";
                break;
            case int n when n >= 60 && n < 70:
                switchLetterGrade = "D";
                break;
            case int n when n >= 70 && n < 80:
                switchLetterGrade = "C";
                break;
            case int n when n >= 80 && n < 90:
                switchLetterGrade = "B";
                break;
            case int n when n >= 90 && n <= 100:
                switchLetterGrade = "A";
                break;
            default:
                Console.WriteLine("Invalid grade percentage. Please enter a value between 0 and 100.");
                return;
        }
        Console.WriteLine($"The letter grade in switch is: {switchLetterGrade}.");

        string question2 = "Answer to question 2: I definitely prefer the if else statement for this excercise. I think the layout is just much more readable. As for which is easier to maintain, I think they are about equal other than the if else statement being easier for me to read";
        Console.WriteLine(question2);
    }

}