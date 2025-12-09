# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Shayla Rohrer

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**
The three queues in the system each serve different player needs and use distinct matching strategies:

1. Casual Queue: Uses a simple FIFO (First-In-First-Out) strategy where players are matched based purely on their order of entry. This ensures quick matches without considering skill levels, perfect for casual gameplay.

2. Ranked Queue: Implements strict skill-based matchmaking where players can only be matched with others within ±2 skill levels. This creates balanced, competitive matches but might lead to longer wait times.

3. QuickPlay Queue: Uses a hybrid approach that balances match quality with queue times. It attempts skill-based matching (like Ranked) when the queue has 2-3 players, but switches to a faster FIFO approach when 4+ players are waiting. This provides a compromise between match quality and wait times.

## Challenges and Solutions

**Biggest challenge faced:**
The most significant challenge I encountered was implementing the optimal matching logic for the QuickPlay mode in the TryCreateMatch() method. I had a difficult time understanding the logic behind the QuickMatch match type. The system is supposed to prefer skill based matchmaking, but if there are 4 or more players in the queue, my program just matches based on entry order. When I ran the program, I found that after queueing ProGamer, Newbie, Skilled, and Expert (so there are 4 players queued), the matches were as follows:

Mode: QuickPlay
Time: 11/5/2025 8:11:54 PM

Player 1: ProGamer (Skill: 10)
Player 2: Newbie (Skill: 1)

Winner: ProGamer
Loser: Newbie
Skill Difference: 7

====MATCH RESULT====

Mode: QuickPlay
Time: 11/5/2025 8:11:54 PM

Player 1: Skilled (Skill: 6)
Player 2: Expert (Skill: 9)

Winner: Expert
Loser: Skilled
Skill Difference: 1

This confused me becasue it seems like the opposite of skill-based matchmaking.

**How you solved it:**
After analyzing the requirements and considering various approaches, I realized this is the intended logic. The system uses skill-based matching for queues with 2-3 players to ensure quality matches when possible, then switches to a FIFO approach with 4+ players to prevent long queue times. While this may occasionally result in skill mismatches, it effectively serves the QuickPlay mode's primary goal of reducing wait times while still attempting to create balanced matches when queue population is lower.

**Most confusing concept:**
The most challenging concept to grasp was finding the right balance in the QuickPlay matching system between match quality and queue processing speed. This highlighted the complex nature of real-world matchmaking systems where perfect solutions often must be balanced against practical constraints.

## Code Quality

**What you're most proud of in your implementation:**
I'm most proud of my implementation of the Ranked matchmaking system. The code efficiently iterates through the queue to find suitable skill-based matches while maintaining clean, readable code structure. I used helper methods like CanMatchInRanked to keep the logic modular and easy to understand. The system successfully ensures fair matches by keeping skill differences within the required difference of 2 range.

**What you would improve if you had more time:**
If I had more time, I would improve the QuickPlay matching algorithm to better balance skill-based matching with queue times. Instead of the current approach that switches to FIFO after 4 players, I would implement a dynamic threshold that gradually relaxes skill requirements as wait times increase. I would also add more detailed match statistics and perhaps a feature to track player win/loss streaks to further refine the matchmaking process.

## Testing Approach

**How you tested your implementation:**
I tested my implementation by trying different queue scenerios and running the program to see the outcomes. I particularly focused on testing the skill-based matching system by queueing players with different skill gaps and verifying that the matches were created according to the requirements.

**Test scenarios you used:**
1. Empty queue handling - Verified that the system properly handles attempts to create matches with insufficient players
2. Casual mode FIFO - Tested multiple different players and verified they were matched in order of queue entry
3. Ranked mode skill matching - Tested players with skills 1, 3, 6, and 9 to test the ±2 skill difference rule
4. QuickPlay threshold testing - Tested multiple players with different skill levels to test the transition from skill-based to FIFO matching
5. Edge cases - Tested queue behavior with players at extreme skill levels (1 and 10)

**Issues you discovered during testing:**
The main issue I discovered was with the QuickPlay mode matching logic, as detailed in my "Biggest challenge faced" section. The system was matching players purely based on queue order when there were 4+ players, even when better skill-based matches were possible. While this technically meets the requirements of processing matches more quickly with larger queues, it could lead to unbalanced matches.

## Game Mode Understanding

**Casual Mode matching strategy:**
For Casual mode, I implemented a straightforward FIFO approach using C#'s Queue<Player> data structure. When creating a match, the system simply dequeues the first two players in the casual queue. This ensures that players are matched in the exact order they joined, providing quick matches without any skill considerations.

**Ranked Mode matching strategy:**
In Ranked mode, I implemented skill-based matching by converting the queue to a list for easier comparison and using nested loops to find valid pairs. The system checks each potential pair of players to ensure their skill difference is within ±2 levels. I created a helper method `CanMatchInRanked` to keep the skill comparison logic clean and reusable. Players are only matched if they meet this strict skill requirement, ensuring competitive balance.

**QuickPlay Mode matching strategy:**
For QuickPlay, I implemented a hybrid approach that changes based on queue size:
- With 2-3 players: Uses the same skill-based matching as Ranked mode (±2 levels)
- With 4+ players: Switches to a FIFO approach for faster processing
This creates a balance between match quality and queue times, though as noted in my challenges section, there might be room for improvement in how the transition between these approaches is handled.

## Real-World Applications

**How this relates to actual game matchmaking:**
This implementation mirrors real-world games in several ways. Like Overwatch's Quick Play vs. Competitive modes, this program provides different matchmaking strategies for different player needs. The Ranked mode's strict skill matching is similar to League of Legends' ranked matchmaking, where the system prioritizes fair matches over queue times. The QuickPlay mode's adaptive approach resembles how many modern games dynamically adjust their matching criteria based on queue population and wait times.

**What you learned about game industry patterns:**
Through this project, I gained several insights about game industry matchmaking patterns:
1. The importance of balancing match quality with queue times
2. How different queue types serve different player preferences (casual vs. competitive)
3. Why games use skill-based matchmaking for competitive modes
4. The complexity of handling edge cases (high/low skill players, off-peak hours)
5. How queue population affects matchmaking strategies

## Stretch Features

None implemented

## Time Spent

**Total time:** 4.5 hours

**Breakdown:**

- Understanding the assignment and queue concepts: .5 hour
- Implementing the 6 core methods: 2 hours
- Testing different game modes and scenarios: 0.5 hours
- Debugging and fixing issues: 1 hours
- Writing these notes: 0.5 hours

**Most time-consuming part:** Implementing the core methods took the longest because I needed to carefully consider the different matching strategies for each queue type, especially the QuickPlay mode where I spent significant time trying to understand and implement the balance between skill-based matching and queue processing speed.

## Key Learning Outcomes

**Queue concepts learned:**
- How to effectively use different queue processing strategies (FIFO vs. priority-based)
- The importance of queue state management and player tracking
- How to handle multiple concurrent queues with different rules
- The relationship between queue size and matching strategies
- Techniques for removing players from queues efficiently

**Algorithm design insights:**
- How to balance competing requirements (match quality vs. speed)
- How to implement different matching criteria (FIFO vs. skill-based)
- Techniques for comparing player attributes efficiently
- The value of helper methods in simplifying complex logic

**Software engineering practices:**
- Importance of clear error handling and validation
- How to maintain clean code structure with multiple queue types
- The value of clear console output for user feedback
- How to organize code for maintainability and readability
