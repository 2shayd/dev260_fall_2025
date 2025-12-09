# DEV 260: Data Structures & Algorithms in C#

**Student:** Shayla Rohrer 

**Term:** Fall 2025 - Bellevue College

**Instructor:** Zak Brinlee

---

## About This Repository

This repository contains my coursework for DEV 260: Data Structures & Algorithms in C#. Over the term I implemented and analyzed common data structures (arrays, linked lists, stacks, queues, hash sets, trees, and graphs) and learned algorithmic techniques such as searching, sorting, BFS, and Dijkstra's algorithm. The projects and labs include working implementations, sample inputs, and small programs that demonstrate these concepts in C#.

---

## Projects

### Labs


- **Lab Loops and Conditionals:** Exercises using control flow, loops, and conditional logic to build small programs and practice algorithmic thinking.

- **Lab Stacks:** Implemented stack operations and used them in simulations (browser session) to validate LIFO behavior and state management.

- **Lab Queues:** Built queue-based examples such as matchmaking and task processing to explore FIFO semantics and performance considerations.

- **Lab Hashsets:** Used `HashSet<T>` to implement fast membership tests and a basic spell-check utility, demonstrating hashing benefits.

- **Lab Trees:** Implemented and traversed binary search trees, practiced insertion/deletion, and simulated simple file-system operations.


### Assignments


- **Assignment DS Foundations:** Implemented core data structures and evaluated their time/space complexity with examples and tests.

- **Assignment Linked Lists:** Implemented singly and doubly linked lists with insertion, deletion, and traversal, and validated edge cases.

- **Assignment Stacks:** Built the stack ADT and applied it to practical problems like browser session management.

- **Assignment Queues:** Implemented queue-based systems including matchmaking and demonstrated correct FIFO behavior under load.

- **Assignment Hashsets:** Built a spell checker using hash sets and file I/O to efficiently test word membership.

- **Assignment Trees:** Constructed a file-system-like binary search tree, implemented traversals, and gathered basic filesystem statistics.

- **Assignment Graphs:** Implemented graph representations and navigation algorithms (BFS, Dijkstra's) and applied them to flight-network examples.


### Final Project


**Tarot Reading Console App**

This is a console-based tarot reading application that reads card data from `final_project/Data/cards.csv`, models tarot cards, and generates randomized readings. The project demonstrates file I/O, object-oriented design, and the use of collections (`List<T>`, `Dictionary<TKey,TValue>`) to manage card decks and readings.

📁 [View Final Project](./final_project/)


---


## Skills Demonstrated

- C# programming fundamentals

- Data structures: Lists, Dictionaries, HashSets, Queues, Stacks, Graphs

- Algorithm implementation: BFS, Dijkstra's, Binary Search

- File I/O and CSV parsing

- Object-oriented design

- Manual testing and debugging


---

## How to Run

Each project contains its own README with specific build and run instructions.

**General steps:**

```bash

cd [project-directory]

dotnet build

dotnet run

```