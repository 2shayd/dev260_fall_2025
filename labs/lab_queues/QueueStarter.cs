using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Queue<T> Essential Operations:
- new Queue<SupportTicket>()        // Create empty queue
- queue.Enqueue(item)               // Add item to back (FIFO)
- queue.Dequeue()                   // Remove and return front item
- queue.Peek()                      // Look at front item (don't remove)
- queue.Clear()                     // Remove all items
- queue.Count                       // Get number of items

Safety Rules:
- ALWAYS check queue.Count > 0 before Dequeue() or Peek()
- Empty queue Dequeue() throws InvalidOperationException
- Empty queue Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (queue.Count > 0) { ... }
- FIFO order: First item enqueued is first item dequeued
- Enumeration: foreach gives front-to-back order

Helpful Icons:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display
- ℹ️ Information
- 📊 Stats
- 🎫 Ticket
- 🔄 Process
*/

namespace QueueLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Complete the TODO steps to build a complete IT Support Desk Queue system.
    /// </summary>
    class Program
    {
        private static Queue<SupportTicket> ticketQueue = new Queue<SupportTicket>();
        private static int ticketCounter = 1;      // For generating unique ticket IDs
        private static int totalOperations = 0;    // Track total queue operations
        private static DateTime sessionStart = DateTime.Now;   //track session duration

        // Pre-defined ticket options for easy selection during demos
        private static readonly string[] CommonIssues = {
            "Login issues - cannot access email",
            "Password reset request",
            "Software installation help",
            "Printer not working",
            "Internet connection problems",
            "Computer running slowly",
            "Email not sending/receiving",
            "VPN connection issues",
            "Application crashes on startup",
            "File recovery assistance",
            "Monitor display problems",
            "Keyboard/mouse not responding",
            "Video conference setup help",
            "File sharing permission issues",
            "Security software alert"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("🎫 IT Support Desk Queue Management");
            Console.WriteLine("===================================");
            Console.WriteLine("Building a ticket queue system with FIFO processing\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "submit":
                        HandleSubmitTicket();
                        break;
                    case "2":
                    case "process":
                        HandleProcessTicket();
                        break;
                    case "3":
                    case "peek":
                    case "next":
                        HandlePeekNext();
                        break;
                    case "4":
                    case "display":
                    case "queue":
                        HandleDisplayQueue();
                        break;
                    case "5":
                    case "urgent":
                        HandleUrgentTicket();
                        break;
                    case "6":
                    case "search":
                        HandleSearchTicket();
                        break;
                    case "7":
                    case "stats":
                        HandleQueueStatistics();
                        break;
                    case "8":
                    case "clear":
                        HandleClearQueue();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            string nextTicket = ticketQueue.Count > 0 ? ticketQueue.Peek().TicketId : "None";

            Console.WriteLine("┌─ Support Desk Queue Operations ─────────────────┐");
            Console.WriteLine("│ 1. Submit      │ 2. Process    │ 3. Peek/Next  │");
            Console.WriteLine("│ 4. Display     │ 5. Urgent     │ 6. Search      │");
            Console.WriteLine("│ 7. Stats       │ 8. Clear      │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"Queue: {ticketQueue.Count} tickets | Next: {nextTicket} | Total ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        static void HandleSubmitTicket()
        {
            Console.WriteLine("\n📝 Submit New Support Ticket");
            Console.WriteLine("Choose from common issues or enter custom:");

            // Math.Min() for safe array access - prevents index out of bounds errors
            // Display quick selection options
            for (int i = 0; i < Math.Min(5, CommonIssues.Length); i++)
            {
                Console.WriteLine($"  {i + 1}. {CommonIssues[i]}");
            }
            Console.WriteLine("  6. Enter custom issue");
            Console.WriteLine("  0. Cancel");
            
            Console.Write("\nSelect option (0-6): ");
            string? choice = Console.ReadLine();
            
            if (choice == "0")
            {
                Console.WriteLine("❌ Ticket submission cancelled.\n");
                return;
            }
            
            string description = "";
            // int.TryParse() for safe number conversion - better than catching exceptions
            if (int.TryParse(choice, out int index) && index >= 1 && index <= 5)
            {
                description = CommonIssues[index - 1];
            }
            else if (choice == "6")
            {
                Console.Write("Enter issue description: ");
                description = Console.ReadLine()?.Trim() ?? "";
            }

            // Input validation with multiple options - professional apps handle user choice
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("❌ Description cannot be empty. Ticket submission cancelled.\n");
                return;
            }

            string ticketId = $"T{ticketCounter:D3}";
            var ticket = new SupportTicket(ticketId, description, "Normal", "User");
            ticketQueue.Enqueue(ticket);
            ticketCounter++;
            totalOperations++;
            Console.WriteLine($"✅ Ticket {ticketId} submitted: \"{description}\" | Position in queue: {ticketQueue.Count}\n");
        }

        static void HandleProcessTicket()
        {
            if (ticketQueue.Count > 0)
            {
                SupportTicket ticket = ticketQueue.Dequeue();
                totalOperations++;
                Console.WriteLine("\n🔄 Process Next Support Ticket");
                Console.WriteLine($"{ticket.ToDetailedString()}");
                if (ticketQueue.Count > 0)
                {
                    Console.WriteLine($"👀 Next ticket in queue: {ticketQueue.Peek().TicketId} - {ticketQueue.Peek().Description}\n");
                }
                else
                {
                    Console.WriteLine("✅ All tickets have been processed. Queue is now empty.\n");
                }
            }
        }

        static void HandlePeekNext()
        {
            Console.WriteLine("\n👀 View Next Ticket");
            if (ticketQueue.Count > 0)
            {
                SupportTicket nextTicket = ticketQueue.Peek();
                Console.WriteLine("Next ticket to be processed:");
                Console.WriteLine(nextTicket.ToDetailedString());
                Console.WriteLine($"Position: 1 of {ticketQueue.Count}\n");
            }
            else
            {
                Console.WriteLine("❌ Queue is empty. No tickets to view.\n");
            }
        }

        static void HandleDisplayQueue()
        {
            Console.WriteLine("\n📋 Current Support Queue (FIFO Order):");
            if (ticketQueue.Count > 0)
            {
                Console.WriteLine($"Total tickets in queue: {ticketQueue.Count}");
                int position = 1;
                foreach (var ticket in ticketQueue)
                {
                    string nextMarker = position == 1 ? " ← Next" : "";
                    Console.WriteLine($"{position:D2}. {ticket}{nextMarker}");
                    position++;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("❌ Queue is empty - no tickets waiting.\n");
            }
        }

        static void HandleClearQueue()
        {
            Console.WriteLine("\n🗑️ Clear All Tickets");
            if (ticketQueue.Count > 0)
            {
                int ticketCount = ticketQueue.Count;
                Console.Write($"This will remove {ticketCount} tickets. Are you sure? (y/N): ");
                string? responseConfirmation = Console.ReadLine()?.ToLower();

                if (responseConfirmation == "y" || responseConfirmation == "yes")
                {
                    ticketQueue.Clear();
                    totalOperations++;
                    Console.WriteLine($"✅ Cleared {ticketCount} tickets from the queue.\n");
                }
                else
                {
                    Console.WriteLine("❌ Clear operation cancelled. No elements in the queue.\n");
                }
            }
        }

        static void HandleUrgentTicket()
        {
            Console.WriteLine("\n🚨 Submit Urgent Ticket");
            Console.WriteLine("Urgent tickets are typically processed first!");
            Console.Write("Enter urgent issue description: ");
            string? description = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("❌ Description cannot be empty. Urgent ticket submission cancelled.\n");
                return;
            }

            string ticketId = $"U{ticketCounter:D3}";
            var urgentTicket = new SupportTicket(ticketId, description, "Urgent", "User");
            // For basic implementation, we enqueue normally.
            ticketQueue.Enqueue(urgentTicket);
            ticketCounter++;
            totalOperations++;
            Console.WriteLine($"✅ Urgent Ticket {ticketId} submitted: \"{description}\"");
            Console.WriteLine($"Position in queue: {ticketQueue.Count}\n");
            Console.WriteLine("⚠️ Note: In a real system, urgent tickets would jump to the front of the queue.\n");
        }

        static void HandleSearchTicket()
        {
            Console.WriteLine("\n🔍 Search Support Tickets");

            if (ticketQueue.Count > 0)
            {
                Console.WriteLine("Enter ticket ID or description keyword:");
                string? searchTerm = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    Console.WriteLine("❌ Search term cannot be empty. Search cancelled.\n");
                    return;
                }

                bool found = false;
                int position = 1;
                Console.WriteLine("\nSearch results:");
                foreach (var ticket in ticketQueue)
                {
                    if (ticket.TicketId.ToLower().Contains(searchTerm.ToLower()) ||
                        ticket.Description.ToLower().Contains(searchTerm.ToLower()))
                    {
                        Console.WriteLine($"\t{position:D2}. {ticket}");
                        found = true;
                    }
                    position++;
                }

                if (!found)
                {
                    Console.WriteLine($"❌ No tickets found matching '{searchTerm}'.\n");
                }
                
                Console.WriteLine();
            }
        }

        static void HandleQueueStatistics()
        {
            Console.WriteLine("\n📊 Queue Statistics");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Current Queue Status:");
            Console.WriteLine($"- Tickets in queue: {ticketQueue.Count}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Session duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Next ticket ID: T{ticketCounter:D3}");
            
            if (ticketQueue.Count > 0)
            {
                var oldestTicket = ticketQueue.Peek();
                Console.WriteLine($"- Longest waiting: {oldestTicket.TicketId} ({oldestTicket.GetFormattedWaitTime()})");
                
                // Count by priority
                int normal = 0, high = 0, urgent = 0;
                foreach (var ticket in ticketQueue)
                {
                    switch (ticket.Priority.ToLower())
                    {
                        case "normal": normal++; break;
                        case "high": high++; break;
                        case "urgent": urgent++; break;
                    }
                }
                Console.WriteLine($"- By priority: 🟢 Normal({normal}) 🟡 High({high}) 🔴 Urgent({urgent})");
            }
            else
            {
                Console.WriteLine("- Queue is empty");
            }
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n📋 Final Session Summary");
            Console.WriteLine("========================");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Session Statistics:");
            Console.WriteLine($"- Duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Tickets remaining: {ticketQueue.Count}");
            
            if (ticketQueue.Count > 0)
            {
                Console.WriteLine($"- Unprocessed tickets:");
                int position = 1;
                foreach (var ticket in ticketQueue)
                {
                    Console.WriteLine($"  {position:D2}. {ticket}");
                    position++;
                }
                Console.WriteLine("\n⚠️ Remember to process remaining tickets!");
            }
            else
            {
                Console.WriteLine("✨ All tickets processed - excellent work!");
            }
            
            Console.WriteLine("\nThank you for using the Support Desk Queue System!");
            Console.WriteLine("You've learned FIFO queue operations and real-world ticket management! 🎫\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}