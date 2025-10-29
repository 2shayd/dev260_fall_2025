using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

namespace StackLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Uncomment the class name and Main method when ready to use this version.
    /// </summary>
    // class Program  // Uncomment this line when ready to use
    class StudentSkeleton
    {

        // TODO: Step 1 - Declare two stacks for action history and undo functionality
        private static Stack<string> actionHistory = new Stack<string>();
        private static Stack<string> undoHistory = new Stack<string>();

        // TODO: Step 2 - Add a counter for total operations
        private static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
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
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push      │ 2. Pop       │ 3. Peek/Top    │");
            Console.WriteLine("│ 4. Display   │ 5. Clear     │ 6. Undo        │");
            Console.WriteLine("│ 7. Redo      │ 8. Stats     │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"Current stack size: {actionHistory.Count} | Total operations: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO: Step 4 - Implement HandlePush method
        static void HandlePush()
        {
            Console.Write("Enter action to add to history: ");
            string? action = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(action))
            {
                actionHistory.Push(action.Trim());
                undoHistory.Clear(); //clear redo stack
                totalOperations++;
                Console.WriteLine($"Pushed '{action}' to history\n");
            }
            else
            {
                Console.WriteLine("❌ Action cannot be empty. Push operation cancelled.\n");
            }
        }

        // TODO: Step 5 - Implement HandlePop method
        static void HandlePop()
        {
            if (actionHistory.Count > 0)
            {
                string poppedAction = actionHistory.Pop();
                undoHistory.Push(poppedAction); // for redo
                totalOperations++;
                Console.WriteLine($"Popped '{poppedAction}' from history\n");
                if (actionHistory.Count > 0)
                {
                    Console.WriteLine($"New top item: {actionHistory}\n");
                }
                else
                {
                    Console.WriteLine("History is now empty.\n");
                }
            }
            else
            {
                Console.WriteLine("❌ Cannot pop from empty history.\n");
            }
        }

        // TODO: Step 6 - Implement HandlePeek method
        static void HandlePeek()
        {
            if (actionHistory.Count > 0)
            {
                string topAction = actionHistory.Peek();
                Console.WriteLine($"Top action: '{topAction}'\n");
            }
            else
            {
                Console.WriteLine("History is empty.");
            }
        }

        static void HandleDisplay()
        {
            Console.WriteLine("Current History Stack (TTB - top to bottom): ");
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("\nHistory is empty\n");
            }
            else
            {
                int position = actionHistory.Count;
                foreach (var action in actionHistory)
                {
                    string marker = position == 1 ? " Top" : "    ";
                    Console.WriteLine($" {position:D2}. {action} {marker}");
                    position--;
                }
            }
            Console.WriteLine();
        }

        static void HandleClear()
        {
            if (actionHistory.Count > 0)
            {
                int clearedCount = actionHistory.Count;
                actionHistory.Clear();
                undoHistory.Clear();
                totalOperations++;
                Console.WriteLine($"Cleared {clearedCount} actions from history\n");
            }
            else
            {
                Console.WriteLine("History is already empty.");
            }
        }

        static void HandleUndo()
        {
            if (undoHistory.Count > 0)
            {
                string actionToRestore = undoHistory.Pop();
                actionHistory.Push(actionToRestore);
                totalOperations++;
                Console.WriteLine($"Undid action: '{actionToRestore}'\n");
            }
            else
            {
                Console.WriteLine("Nothing to undo\n");
            }
        }

        static void HandleRedo()
        {
            if (actionHistory.Count > 0)
            {
                string actionToRemove = actionHistory.Pop();
                undoHistory.Push(actionToRemove);
                totalOperations++;
                Console.WriteLine($"Redid action '{actionToRemove}'\n");
            }
            else
            {
                Console.WriteLine("Nothing to redo\n");
            }
        }

        static void ShowStatistics()
        {
            Console.WriteLine("Current Session Statistics: ");
            Console.WriteLine($"- Current stack size: {actionHistory.Count}");
            Console.WriteLine($"- Undo stack size: {undoHistory.Count}");
            Console.WriteLine($"- Total operations performed: {totalOperations}");
            Console.WriteLine($"- Stack isEmpty?: {(actionHistory.Count == 0 ? "Yes" : "No")}");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine($"- Current top action: '{actionHistory.Peek()}'");
            }
            else
            {
                Console.WriteLine($"- Current top action: None");
            }
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("Final Session Summary: ");
            Console.WriteLine($"- Total operations performed: {totalOperations}");
            Console.WriteLine($"Final stack size: {actionHistory.Count}");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine("- Remaining actions in stack:");
                int position = actionHistory.Count;
                foreach (string action in actionHistory)
                {
                    Console.WriteLine($"\t{position:D2}. {action}");
                    position--;
                }
            }
            else
            {
                Console.WriteLine("Stack was cleared before exit.");
            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
