# Assignment 9: BST File System Navigator - Implementation Notes

**Name:** Shayla Rohrer

## Binary Search Tree Pattern Understanding

**How BST operations work for file system navigation:**

Answer: In a BST, each node contains a file or directory, and the tree is sorted based on a comparison function. This allows searches, insertions, and deletions to operate in O(log n) time on average, because each comparison lets you skip half the tree. Using in-order traversal, files and directories can be listed in a sorted order, and the hierarchical structure naturally models folder organization, making file management efficient and predictable. The BST structure helps keep directories and files organized so that searches, deletions, and insertions are fast and consistent

## Challenges and Solutions

**Biggest challenge faced:**

Answer: The most difficult parts were figuring out how to implement the DeleteItem function and the SearchNode function correctly.

**How you solved it:**

Answer: I had to fully understand the tree’s sorting logic to correctly handle both directories and files. At first, I could only search for directories because my SearchNode function was comparing names directly rather than using the CompareFileNodes helper method. Once I updated the search logic to use CompareFileNodes, both SearchNode and DeleteItem started working correctly. Debugging the delete function helped me understand how the BST organizes files and directories.

**Most confusing concept:**

Answer: Recursive thinking was the hardest part, especially remembering to use helper methods and how the recursion flows during insertion, search, and deletion in the tree.

## Code Quality

**What you're most proud of in your implementation:**

Answer: I am most proud that I was able to get DeleteItem and SearchNode working properly after a lot of troubleshooting and understanding the correct BST ordering rules.

**What you would improve if you had more time:**

Answer: I would improve error handling, possibly add more detailed feedback when operations fail, and maybe implement additional features like file extension searches or directory size calculations.

## Real-World Applications

**How this relates to actual file systems:**

Answer: This implementation models real file systems like Windows Explorer or macOS Finder. In those systems, folders and files are organized hierarchically, and search operations use optimized tree-like structures to quickly locate files. BSTs can also be applied in database indexing where data is sorted and efficiently searchable.

**What you learned about tree algorithms:**

Answer: I learned that recursive thinking is essential for traversing, inserting, and deleting nodes in a tree. Using a helper comparison method consistently is critical for maintaining tree structure. I also gained insight into how hierarchical data organization mirrors real-world file systems and enables efficient operations.

## Stretch Features

[If you implemented any extra credit features like file pattern matching or directory size analysis, describe them here. If not, write "None implemented"]

Answer: None implemented

## Time Spent

**Total time:** 6 hours

**Breakdown:**

- Understanding BST concepts and assignment requirements: 1 hour
- Implementing the 8 core TODO methods: 3 hour
- Testing with different file scenarios: 0.5 hour
- Debugging recursive algorithms and BST operations: 1 hour
- Writing these notes: 0.5 hour

**Most time-consuming part:** [Which aspect took the longest and why - recursive thinking, BST deletion, custom comparison logic, etc.]
Implementing the core methods because I had to rewrite some a few times after debugging and better understanding the custom comparison logic.

*** I used ChatGPT to improve readability and spellcheck my notes.