# Tarot Reading Console App

> This app is a tarot reading simulation.

>_Tarot reading is a practice where a person uses a special deck of picture cards to help talk about life questions, feelings, or situations. The cards are drawn at random, and each one has a meaning that the reader explains. People often use tarot to reflect on their own thoughts, understand problems better, or think about possible outcomes in their life._


---
## What I Built (Overview)

**Problem this solves:** 

Tarot reading can be considered a taboo thing in many different cultures and religions. It is an art that holds a lot of meaning for certain people. This app allows a person to experience a tarot reading without owning a tarot deck or needing to be read by another person, allowing for a personal and private experience.

---
**Core features:**  

- 1 card (daily) spread
- 3 card (past, present, future) spread
- List all cards
- Search cards by name
- Save previous 3 readings to review
- Update card meanings to reflect personal preference

## How to Run

**Requirements:**  
- .NET 9.0 SDK (target framework: `net9.0`). Verify with `dotnet --version` (needs 9.x).  
- Cross-platform: Windows, macOS, Linux — console app; tested on macOS.  
- NuGet package: `CsvHelper` v`33.1.0` (used to parse `Data/cards.csv`). This is restored automatically by `dotnet restore`.  
- Data file: `Data/cards.csv` must exist and be readable at runtime.  
- Tools: `git` (optional) and the `dotnet` CLI (required).

**Install / Build / Run:**

```bash
git clone <your-repo-url>
cd final_project_new
dotnet restore
dotnet build
dotnet run
```

**Optional (manual package install):**

```bash
dotnet add package CsvHelper --version 33.1.0
dotnet restore
```

**Sample data (tarot card deck):**  
- The app reads card data from `Data/cards.csv`. The CSV should include headers matching the `TarotReading.TarotCard` properties (`Name,Meaning,ReversedMeaning`).  
- If `Data/cards.csv` is missing or empty, the program will print an error and won't run spreads.

---

## Using the App (Quick Start)

**Typical workflow:**  

1. Select a spread option (1-card) or (3-card)
2. Read meanings of the cards drawn for the chosen spread
3. Review previous readings as needed
4. Search cards by name to see both meaning and reversed meaning

**Input tips:**  
- **Case sensitivity:** Input is NOT case sensitive. "FOUR OF CUPS", "Four Of Cups", and "four of cups" are all treated identically.
- **Automatic normalization:** For card searches, input is automatically normalized:
  - Whitespace is trimmed and collapsed (extra spaces removed).
  - Numeric card names are converted to words (e.g., "4 of cups" → "four of cups", "11" → "page").
  - This allows flexible search patterns without user worry.
- **Required fields:** The app has no required input fields for spread draws (menu choices 1, 2). Card searches (menu choice 4) require a card name, but if empty or not found, the app prints "Card '[name]' not found." and continues gracefully.
- **Error handling:**
  - **Invalid menu choice:** If you enter a number outside 1–6 or non-numeric input, the app prints "Invalid choice. Please enter a number between 1 and 6." and loops back to the menu.
  - **Missing CSV file:** If `Data/cards.csv` does not exist, the app prints an error message and prevents spreads from running (returns early).
  - **Empty CSV file:** If the CSV is empty, spreads fail gracefully with "No cards available for a spread."
  - **Card not found:** Searching for a card that doesn't exist returns "Card '[name]' not found.\n" without crashing.
  - **Null/empty input on search:** Empty string input for card search is normalized to an empty string and won't match any card, returning the "not found" message.
- **No data loss:** All operations are read-only except reading history. Previous readings are preserved in the queue until the app closes (not persisted to disk).

---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**  
_List each data structure and briefly explain what feature it powers._

**Your Answer:**

- `Dictionary<...>` → A Dictionary is the ideal data structure for fast card lookup by name. Since the user may search by card name (e.g., “four of cups”), the dictionary provides O(1) average-time performance for retrieving a card when the name is known or normalized. This makes it significantly more efficient than searching through a list or array, which would require O(n) linear time for each lookup.
- `List<...>` → A List is the simplest and most efficient structure for storing the full set of cards exactly as they are loaded from the CSV file. It allows fast iteration, preserves the natural deck order, and supports random drawing with O(1) index access.
- `Queue<...>` → A Queue was chosen to store the last three spreads because it naturally provides FIFO behavior, which aligns perfectly with maintaining a fixed-size history. When a new spread is added, it is enqueued at the back, and if the queue exceeds three items, the oldest spread is automatically removed using Dequeue(). Both operations run in O(1) time. This makes Queue a clean, efficient, and minimal-overhead solution for tracking a rolling history of recent spreads.
- _(Add others: Queue, Stack, SortedDictionary, custom BST/Graph, etc.)_

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**  
_Describe each test scenario with steps and expected results._

**Your Answer:**

**Scenario 1: [Name]**

- Steps:
- Expected result:
- Actual result:

**Scenario 2: [Name]**

- Steps:
- Expected result:
- Actual result:

**Scenario 3: [Name]**

- Steps:
- Expected result:
- Actual result:

**Scenario 4: [Name] (optional)**

- Steps:
- Expected result:
- Actual result:

**Scenario 5: [Name] (optional)**

- Steps:
- Expected result:
- Actual result:

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**

-
-

## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**

-
- **AI usage (if any):**  
chatgpt for the csv file text
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**

  ***

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**

## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**

## Submission Checklist

- [ ] Public GitHub repository link submitted
- [ ] README.md completed (this file)
- [ ] DESIGN.md completed
- [ ] Source code included and builds successfully
- [ ] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
