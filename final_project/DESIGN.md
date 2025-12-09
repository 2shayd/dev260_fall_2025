# Project Design & Rationale

This document describes the design decisions for the final project: a console-based Tarot Reading application. It covers the core entities, the data structures used, string handling, performance tradeoffs, and a brief note on runtime behavior.

---

## Data Model & Entities

**Core entities:**

- **Card**
	- `Name`: `string` (e.g., "The Fool") — canonical identifier for a card.
	- `Meaning`: `string` (upright meaning displayed when drawn upright).
	- `ReversedMeaning`: `string` (meaning when drawn reversed).
	- Notes: `Name` values are expected to be unique. The loader normalizes names into stable lookup keys to avoid casing/format mismatches.

- **Deck**
	- Primary storage: `List<Card>` — preserves deck order and supports index-based random draws.
	- Lookup: `Dictionary<string, Card>` keyed by a normalized `Name` (constructed with `StringComparer.OrdinalIgnoreCase`) for O(1) name-based access.
	- Purpose: `List` is used for iteration and selection; `Dictionary` is used for fast search and update operations.

- **Reading**
	- Fields: `DateTime Timestamp`, `List<ReadingCard> DrawnCards`, and `string SpreadType` (e.g., "1-card", "3-card").
	- `ReadingCard`: holds a reference to the `Card`, `bool IsReversed`, and `int Position` (1-based).
	- History: recent readings are kept in an in-memory `Queue<Reading>` (bounded to the last three entries). This queue is volatile (in-memory only) and not persisted by default.

**Identifiers (keys) and rationale:**

- `Card.Name` is the chosen key because tarot card names are unique and human-readable. To ensure stable lookups the app normalizes keys (trim/collapse whitespace, remove leading "the", convert common numeric forms to word forms, and use invariant casing). `Dictionary` lookups use `StringComparer.OrdinalIgnoreCase` for culture-agnostic, case-insensitive matching.

**CSV & loading notes:**

- The app uses `CsvHelper` to read `Data/cards.csv` into the `List<Card>` at startup. The loader validates the file exists and contains data; on malformed CSV/headers the app prints an error and exits early rather than attempting automatic repair.
- During load the code builds the normalized-key `Dictionary<string, Card>` to enable O(1) lookups for search and updates.


---

## Data Structures — Choices & Justification

### Structure #1

**Chosen Data Structure:** `List<Card>` (for loading cards properly from csv file)

**Purpose / Role in App:** Used to model an ordered deck and support random draw.

**Why it fits:** The deck is small (~78 items). `List<T>` supports indexing (O(1)), efficient iteration (O(n)), and memory use is negligible for this scale.

**Alternatives considered:** `Queue<Card>` or `Stack<Card>` — these fit specific draw semantics but make random-access awkward. `LinkedList<Card>` provides O(1) removals, but has worse cache locality and more complexity for our use.

### Structure #2

**Chosen Data Structure:** `Dictionary<string, Card>` with `StringComparer.OrdinalIgnoreCase` and input normalization (for name lookups)

**Purpose / Role in App:** Fast lookup when the app needs to find a card by name (for detail view, tests, or debugging). Also convenient for validating uniqueness when loading `cards.csv`.

**Why it fits:** Provides O(1) average-case lookups. With ~78 items the overhead is trivial and the convenience is worth it.

**Alternatives considered:** Linear search on `List<Card>` (O(n)) — acceptable at this scale but less convenient when performing many lookups; `SortedDictionary` — not necessary because we don't require ordered-key operations.

### Structure #3

**Chosen Data Structure:** `Queue<Reading>` (bounded history of recent spreads)

**Purpose / Role in App:** Stores the most recent readings (spreads) in FIFO order so users can review recent sessions. The app enqueues a new `Reading` after each spread and dequeues the oldest entry when the history exceeds the configured bound (3 items by default).

**Why it fits:** A `Queue<T>` closely models the desired semantics for recent-history storage: constant-time enqueue/dequeue (O(1)), clear FIFO ordering, and minimal overhead for the small fixed capacity. For a console app that retains a short session history, `Queue<Reading>` is simple, efficient, and intent-revealing.

**Alternatives considered:**
`List<Reading>` with manual trimming — slightly simpler to inspect but requires manual index management to remove the oldest entry and is less semantically clear for FIFO behavior.


---

## Comparers & String Handling

**Comparer choices:**

- Use `StringComparer.OrdinalIgnoreCase` for card-name keys in `Dictionary<string, Card>` to avoid casing issues across platforms.

**Normalization rules:**

- Normalization is applied both at load time (when building lookup keys) and at query time (when matching user input). The implementation in `TarotReading` follows these steps:
	- Return empty string for null/whitespace input.
	- Convert to lower invariant (`ToLowerInvariant()`), trim leading/trailing whitespace, and collapse internal runs of whitespace to a single space.
	- Remove common leading articles (e.g., remove a leading `the`).
	- Replace common numeric forms with words for tarot-specific cases using a small mapping (examples from the code: `1` → `ace`, `2` → `two`, `3` → `three`, `4` → `four`, `5` → `five`, `6` → `six`, `7` → `seven`, `8` → `eight`, `9` → `nine`, `10` → `ten`, `11` → `page`, `12` → `knight`, `13` → `queen`, `14` → `king`).
	- The normalized keys are used to populate the `Dictionary<string, Card>` so lookups are O(1) on the normalized form.

**Bad key examples avoided:**

- Using `Meaning` or partial text as a key (not stable or unique).
- Using raw CSV line numbers as an identifier (fragile if file is reordered).

---

## Performance Considerations

**Expected data scale:** ~78 tarot cards; readings of 1 or 3 cards; deck operations are O(n) over 78 at worst.

**Performance bottlenecks identified:**

- None significant at this scale. The most expensive operations (shuffling, validating CSV) are trivial for modern hardware.

**Big-O analysis of core operations:**  

- CardSpread: O(n) average.
- SearchCardByName: O(1) average. Searches use `Dictionary<string, Card>` with `StringComparer.OrdinalIgnoreCase`.
- ListAllCards: O(n). Iterating over the `List<Card>` to display or enumerate every card.
- UpdateCardMeaning: O(1) average. Dictionary lookup O(1) plus a constant-time field update.
 - ViewHistory: O(1). History is a bounded queue of size 3; enqueue (push) and dequeue (pop) are O(1), and listing the queue is O(k) where k ≤ 3 (constant time).

---

## Design Tradeoffs & Decisions

**Key design decisions:**

- Favor simplicity and readability using built-in collections (`List<T>`, `Dictionary<TKey,TValue>`). This keeps the code concise and easy to maintain for a small dataset.
- Keep data normalized in `final_project/Data/cards.csv` and load it into memory at startup.

**Tradeoffs made:**

- Chose not to add persistence beyond CSV (no DB) because readings are ephemeral for the console app; adding a database or file-based history would increase complexity without immediate benefit.

**What you would do differently with more time:**

- Add a small persistence layer (JSON) for saving reading history and user preferences.
- Add a minimal web UI or GUI to visualize spreads and card images.
- Add unit tests for CSV parsing, shuffle randomness properties, and reading generation.

---

## Runtime Behavior (brief)

- On startup the app reads `final_project/Data/cards.csv` into a `List<Card>` and a `Dictionary<string, Card>` for lookups.
- For a reading, cards are popped into a `List<ReadingCard>`; each drawn card is randomly assigned upright/reversed orientation with a configurable probability.
- Readings are displayed to the console with position, card name, upright meaning, and reversed meaning when applicable.

## Notes

- CSV fields: `Name,Meaning,ReversedMeaning` (see `final_project/Data/cards.csv`).
- Keep card names unique and avoid renaming cards unless intentionally modifying the deck.
