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

**Data structures used:**  

- `Dictionary<...>` → A Dictionary is the ideal data structure for fast card lookup by name. Since the user may search by card name (e.g., “four of cups”), the dictionary provides O(1) average-time performance for retrieving a card when the name is known or normalized. This makes it significantly more efficient than searching through a list or array, which would require O(n) linear time for each lookup.
- `List<...>` → A List is the simplest and most efficient structure for storing the full set of cards exactly as they are loaded from the CSV file. It allows fast iteration, preserves the natural deck order, and supports random drawing with O(1) index access.
- `Queue<...>` → A Queue was chosen to store the last three spreads because it naturally provides FIFO behavior, which aligns perfectly with maintaining a fixed-size history. When a new spread is added, it is enqueued at the back, and if the queue exceeds three items, the oldest spread is automatically removed using Dequeue(). Both operations run in O(1) time. This makes Queue a clean, efficient, and minimal-overhead solution for tracking a rolling history of recent spreads.

---

## Manual Testing Summary

**Test scenarios**

- **Scenario 1 — 1 Card (Daily)**
  - Steps: Choose option `1` from the main menu.
  - Expected: A single random card is drawn and its meaning is shown (regular or reversed).
  - Result: Works as expected.

- **Scenario 2 — 3 Card (Past / Present / Future)**
  - Steps: Choose option `2` from the main menu.
  - Expected: Three unique random cards are drawn (no duplicates). Each card shows either its regular or reversed meaning.
  - Result: Works as expected.

- **Scenario 3 — List All Cards**
  - Steps: Choose option `3` from the main menu, then select the option to list all cards.
  - Expected: All 78 card names are displayed in the console.
  - Result: Works as expected.

- **Scenario 4 — Search Card By Name**
  - Steps: Choose option `4` and enter a search term (for example, `sun`).
  - Expected: The matching card (e.g., "The Sun") is displayed along with both its regular and reversed meanings.
  - Result: Works as expected.

---

## Known Limitations

**Limitations and edge cases:**  
_Describe any edge cases not handled, performance caveats, or known issues._

**Your Answer:**
- CSV parsing assumes well-formed UTF-8 CSV with the expected headers (`Name,Meaning,ReversedMeaning`). Malformed rows or unexpected headers can cause load errors; the app prints an error and exits early rather than attempting to repair the data.
- The numeric-to-word conversion used for normalization covers common tarot card numeric forms (e.g., `4` → `four`, `11` → `page`) but does not handle every possible numeric shorthand or localized number formats. Extremely unusual user inputs may not normalize to a matching key.
- Reading history is kept in-memory only and is not persisted to disk. Previous readings are lost when the program exits.
- The app assumes a single-user, single-process environment. There is no concurrency control or file locks — running multiple instances that modify the same CSV (or expect persistence) can cause inconsistencies.
- Random draws are non-deterministic. If you need reproducible draws for testing, a seedable RNG would need to be added.
- Performance: the data set is small (78 cards). No performance optimizations are necessary for the current scope; the design is not targeted at extremely large datasets.


## Comparers & String Handling

**Keys comparer:**  
_Describe what string comparer you used (e.g., StringComparer.OrdinalIgnoreCase) and why._

**Your Answer:**
- The app uses case-insensitive comparisons based on `StringComparer.OrdinalIgnoreCase` when matching user input to card names. This provides fast, culture-agnostic comparisons while treating `THE SUN` and `the sun` as equal.

**Normalization:**  
_Explain how you normalize strings (trim whitespace, consistent casing, duplicate checks)._

**Your Answer:**

- Input normalization is performed at load-time (for keys) and at query-time (for user input):
  - Trim leading/trailing whitespace and collapse multiple internal spaces into a single space.
  - Convert text to normalized casing using `ToLowerInvariant()` for stable, culture-agnostic matching.
  - Strip or normalize common punctuation that does not affect card identity (simple removal of extra commas/periods in practice).
  - Convert numeric forms to words for common tarot cases (e.g., `4 of cups` → `four of cups`, `11` → `page`) using a small mapping and pattern matching so users can enter either numeric or word forms.
  - Store dictionary keys already normalized so lookups are O(1) on the normalized form.

_Note: The normalization intentionally errs on the side of permissiveness to improve UX, but extremely malformed inputs still may fail to match._

---

## Credits & AI Disclosure

**Resources:**  
_List any articles, documentation, or code snippets you referenced or adapted._

**Your Answer:**
- `CsvHelper` documentation (NuGet package and API usage examples)
- Microsoft .NET documentation for `Dictionary`, `List`, and `Queue` collection patterns
- StackOverflow answers and examples for CSV parsing, string normalization, and random unique selection
- Public domain tarot card meanings and community-compiled card lists used as reference when assembling `Data/cards.csv` (meanings were adapted and summarized to avoid verbatim copying of copyrighted material)

- **AI usage (if any):**  
   _Describe what you asked AI tools, what code they influenced, and how you verified correctness._

  **Your Answer:**
  - I used ChatGPT to help generate and format the initial `Data/cards.csv` content (summarized meanings and reversed meanings). Prompts asked the model to produce concise, original summaries for each card rather than large copyrighted passages.
  - AI output was reviewed and edited manually to ensure accuracy, appropriateness, and that wording did not reproduce copyrighted text verbatim. Verification steps included spot-checking meanings, confirming unique card names, and running the app to ensure CSV parsing worked as intended.
  ***

## Challenges and Solutions

**Biggest challenge faced:**  
_Describe the most difficult part of the project - was it choosing the right data structures, implementing search functionality, handling edge cases, designing the user interface, or understanding a specific algorithm?_

**Your Answer:**
The biggest challenge was designing a user-friendly search/normalization system so users could find cards quickly using flexible input (different casing, whitespace, numeric vs. word forms).

**How you solved it:**  
_Explain your solution approach and what helped you figure it out - research, consulting documentation, debugging with breakpoints, testing with simple examples, refactoring your design, etc._

**Your Answer:**
I implemented a small normalization pipeline (trim/collapse spaces, ToLowerInvariant, numeric-to-word mapping, simple punctuation handling) and normalized keys on load. I used unit testing and manual testing with representative inputs to iterate on edge cases. Reading `CsvHelper` docs and community examples helped safely parse and validate the CSV.

**Most confusing concept:**  
_What was hardest to understand about data structures, algorithm complexity, key comparers, normalization, or organizing your code architecture?_

**Your Answer:**
The trickiest part conceptually was balancing normalization flexibility with predictable behavior — too permissive normalization can create accidental collisions (different cards mapping to the same key), while too strict matching hurts UX. I settled on an explicit normalization that preserves unique identity for each card while supporting common user entry patterns.
## Code Quality

**What you're most proud of in your implementation:**  
_Highlight the best aspect of your code - maybe your data structure choices, clean architecture, efficient algorithms, intuitive user interface, thorough error handling, or elegant solution to a complex problem._

**Your Answer:**
- Clear separation of concerns: CSV parsing, normalization/lookup, and user interaction are separated to make testing and future changes easier.
- Use of appropriate data structures: `Dictionary` for fast card lookup, `List` for deck storage and random access, `Queue` for fixed-size history.
- Thoughtful user-facing behavior: permissive input normalization, helpful error messages, and graceful handling of missing or malformed data.

**What you would improve if you had more time:**  
_Identify areas for potential improvement - perhaps adding more features, optimizing performance, improving error handling, adding data persistence, refactoring for better maintainability, or enhancing the user experience._

**Your Answer:**
- Persist reading history to disk (JSON or small DB) so users can review readings across sessions.
- Add unit tests for normalization and CSV loading to prevent regressions.
- Improve localization support (number formatting, translated card names/meanings) and make normalization rules configurable.
- Add a simple UI for editing/saving custom card meanings from within the app and persist them.

## Real-World Applications

**How this relates to real-world systems:**  
_Describe how your implementation connects to actual software systems - e.g., inventory management, customer databases, e-commerce platforms, social networks, task managers, or other applications in the industry._

**Your Answer:**
This project is a compact example of common software patterns used across many systems:
- Fast keyed lookup with `Dictionary` is analogous to caches and indexes used in search engines, product lookups, or configuration stores.
- Normalization and tolerant input handling are important for user-facing search features (e.g., e-commerce search, contact lookup, command parsing).
- The in-memory history queue models short-lived user session state seen in chat apps, undo stacks, or recent-activity lists.

**What you learned about data structures and algorithms:**  
_What insights did you gain about choosing appropriate data structures, performance tradeoffs, Big-O complexity in practice, the importance of good key design, or how data structures enable specific features?_

**Your Answer:**
- Choose the right structure for the operation: `Dictionary` for O(1) lookups by key, `List` for ordered storage and O(1) random access, and `Queue` for fixed-size FIFO history.
- Normalization and key design are as important as algorithmic complexity — a poorly chosen key can turn an O(1) lookup into a wrong result or collision.
- For small, fixed datasets (like 78 cards), simplicity and clarity of code matters more than micro-optimizations; algorithmic choices matter more as data scales.
