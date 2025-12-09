# Assignment 8: Spell Checker & Vocabulary Explorer - Implementation Notes

**Name:** Shayla Rohrer

## HashSet Pattern Understanding

**How HashSet<T> operations work for spell checking:**
I used `HashSet<string>` to store the dictionary words for constant-time membership checks (average O(1) lookup). `HashSet` guarantees automatic uniqueness of entries, which simplifies deduplication of vocabulary. For case-insensitive checks I normalize words with `ToLowerInvariant()` and use a normalized representation for both dictionary loading and tokenized text. This keeps lookups fast and reliable even for large input sets.

## Challenges and Solutions

**Biggest challenge faced:**
Normalizing and tokenizing the input text so punctuation and casing don't create false negatives during dictionary lookup.

**How you solved it:**
I used a regular expression to split input on non-letter characters and applied `Trim()` and `ToLowerInvariant()` to each token before checking membership in the dictionary `HashSet`. This removed surrounding punctuation and normalized casing. For file I/O, I used `File.ReadAllLines` and streamed large files line-by-line where appropriate to avoid memory spikes.

**Most confusing concept:**
Understanding when `HashSet`'s average O(1) lookup can degrade (e.g., pathological hash collisions) and how to choose the right equality/comparer for culture-invariant string comparisons. Using `StringComparer.OrdinalIgnoreCase` simplifies case-insensitive `HashSet` behavior.

## Code Quality

**What you're most proud of in your implementation:**
The normalization pipeline: consistent normalization for both dictionary entries and input tokens reduced false positives/negatives. I also kept IO error handling simple and robust (try/catch around file operations with clear error messages).

**What you would improve if you had more time:**
- Improve tokenization to better handle contractions and hyphenated words.
- Add unit tests for normalization and tokenization.
- Add a strategy to suggest corrections for misspelled words.

## Testing Approach

**How you tested your implementation:**
I ran the spell checker against `sample_text_1.txt` and `sample_text_2.txt` using the provided `dictionary.txt` and visually inspected outputs. I also tested edge cases manually: empty files, punctuation-only inputs, and all-uppercase inputs.

**Test scenarios you used:**
- Mixed case words (e.g., "Word", "WORD")
- Punctuation attached to words (e.g., "word," "word.")
- Repeated words to test deduplication
- Words not in `dictionary.txt` to confirm they're flagged as misspelled

**Issues you discovered during testing:**
Initial tokenization left trailing apostrophes and punctuation on tokens. Fix: use a regex tokenizer and trim non-alphabetic characters before lookup.

## HashSet vs List Understanding

**When to use HashSet:**
Use `HashSet` when you need fast membership checks and automatic deduplication (e.g., dictionary lookups, building unique vocab lists).

**When to use List:**
Use `List` when ordering matters or when you need to preserve duplicates (e.g., keeping occurrences for frequency analysis) or perform indexed access.

**Performance benefits observed:**
Lookups are effectively O(1) on average with `HashSet`, which made spell-checking large texts much faster than scanning a `List` for each token (which would be O(n) per token).

## Real-World Applications

**How this relates to actual spell checkers:**
Real spell-check systems use similar strategies (fast membership structures, normalization, and tokenization), and add suggestion algorithms and language models. My implementation captures the core membership-checking aspect used in larger systems.

**What you learned about text processing:**
Normalization and robust tokenization are the hardest and most important parts of handling real-world text. Simple punctuation and casing differences account for most false negatives.

## Stretch Features

None implemented — potential extras include vocabulary suggestions, frequency analytics, and faster lookup structures with prefix tries for autocomplete.

## Time Spent

**Total time:** ~6 hours

**Breakdown:**
- Understanding HashSet concepts and assignment requirements: [0.5 hours]
- Implementing the 6 core methods: [3 hours]
- Testing different text files and scenarios: [0.5 hour]
- Debugging and fixing issues: [1 hour]
- Writing these notes: [0.5 hours]

**Most time-consuming part:**
Implementing reliable normalization and tokenization to handle punctuation and casing.

## Key Learning Outcomes

**HashSet concepts learned:**
I learned why `HashSet` provides O(1) average lookups and how `StringComparer.OrdinalIgnoreCase` can be used to make membership checks case-insensitive.

**Text processing insights:**
Tokenization and normalization dominate correctness — spend more time on tokenization rules for production use.

**Software engineering practices:**
I reinforced defensive file I/O, separation of concerns (parsing vs checking), and the value of small targeted tests.