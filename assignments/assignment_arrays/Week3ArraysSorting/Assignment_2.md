***Assignment_2.md / README.md***

PART A DOCUMENTATION
====================

Game: Connect Four (6 x 7 grid)

Rules: First player (player x or player o) to get 4 of their tokens (represented by corresponding play identification character) in a row, column, or diagonal pattern wins. If no one gets that and the board reaches capacity, the game is a draw. User gets to chose to play again after win or draw and the board is reset.

Arrays: 2D array is used to present the board for the game to be played on. There are 6 rows and 7 columns. The board is initialized with blank "_" spaces and has labeled columns that correspond to the player's move options.


PART B DOCUMENTATION
====================

Algorithm: QuickSort
Pivot choice: Last element in the current sub-array
Sorting type: Recursive, in place

How it works:
1. Picks pivot.
2. Partitions the array so all titles less than or equal to pivot go left and greater than pivot go right.
3. Recursively sorts left and right partitions.

Pivot reasoning: For me, this was the easiest to implement. It works fine for unsorted data. I viewed the recommended documentation and worked with that.

Time complexity (Big O):
-- Best case: O(n log n)
-- Avg case: O(n log n)
-- Worst case: O(n^2)

Normalization Rules:
1. Convert letters to uppercase
2. Remove leading articles ("A","THE","AN")

2D Index:
-- Rows: first letter (A-Z)
-- Columns: second letter (A-Z)
Values start and end indices in the sorted array for all titles with that 2 letter prefix.
Lookup: If lookup is not exact, suggestions are given from the nearby index ranges

CLI Loop: Prompts user repeatedly until they choose to type 'exit'.