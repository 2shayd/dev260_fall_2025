using System.Reflection;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    break;
                default:
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }
            player.JoinQueue();
            Console.Clear();
            Console.WriteLine("====PLAYER ADDED TO QUEUE SUCCESSFULLY!====\n");
            Console.WriteLine($"Player: {player.Username}\nQueue Joined: {mode}\nTime Joined: {player.JoinedQueue}");
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            //using helper to get the correct queue
            if (GetQueueByMode(mode).Count < 2)
            {
                return null; // Not enough players to create a match
            }
            else if (mode == GameMode.Casual) //simple FIFO matching
            {
                Player player1 = GetQueueByMode(mode).Dequeue();
                Player player2 = GetQueueByMode(mode).Dequeue();
                RemoveFromAllQueues(player1);
                RemoveFromAllQueues(player2);
                return new Match(player1, player2, mode);
            }
            else if (mode == GameMode.Ranked)
            {
                var queue = GetQueueByMode(mode).ToList(); //convert to list for easier indexing
                for (int i = 0; i < queue.Count; i++) //iterate through players
                {
                    for (int j = i + 1; j < queue.Count; j++) //check for 2 player matches within skill range
                    {
                        if (CanMatchInRanked(queue[i], queue[j])) //using helper to check player skills
                        {
                            Player player1 = queue[i];
                            Player player2 = queue[j];
                            RemoveFromAllQueues(player1);
                            RemoveFromAllQueues(player2);
                            return new Match(player1, player2, mode);
                        }
                    }
                }
                return null; // no match with appropriate skill difference found
            }
            else if (mode == GameMode.QuickPlay)
            {
                var queue = GetQueueByMode(mode).ToList(); //convert to list for easier indexing
                if (queue.Count >= 2 && queue.Count < 4)
                {
                    for (int i = 0; i < queue.Count; i++) //iterate through players
                    {
                        for (int j = i + 1; j < queue.Count; j++) //check for 2 player matches within skill range
                        {
                            if (CanMatchInRanked(queue[i], queue[j])) //using helper to check player skills
                            {
                                Player player1 = queue[i];
                                Player player2 = queue[j];
                                RemoveFromAllQueues(player1);
                                RemoveFromAllQueues(player2);
                                return new Match(player1, player2, mode);
                            }
                        }
                    }
                }
                // if no skill based match found and more than 4 players, match any 2
                else if (queue.Count > 4)
                {
                    Player player1 = queue[0];
                    Player player2 = queue[1];
                    RemoveFromAllQueues(player1);
                    RemoveFromAllQueues(player2);
                    return new Match(player1, player2, mode);
                }
                return null; // no match found
            }
            return null; // default case
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;

            Console.WriteLine("====MATCH RESULT====\n");
            Console.WriteLine($"Mode: {match.Mode}\nTime: {match.MatchTime}\n");
            Console.WriteLine($"Player 1: {match.Player1.Username} (Skill: {match.Player1.SkillRating})");
            Console.WriteLine($"Player 2: {match.Player2.Username} (Skill: {match.Player2.SkillRating})\n");
            Console.WriteLine($"Winner: {match.Winner?.Username}");
            Console.WriteLine($"Loser: {match.Loser?.Username}");
            Console.WriteLine($"Skill Difference: {match.SkillDifference}\n");
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            Console.WriteLine("====CURRENT QUEUE STATUS====\n");
            foreach (var mode in Enum.GetValues<GameMode>()) //looping through each game mode using enum from Player.cs
            {
                var queue = GetQueueByMode(mode); //using helper to get correct queue
                Console.WriteLine($"-- {mode} Queue ({queue.Count} players) --");
                if (queue.Count == 0)
                {
                    Console.WriteLine("No players in queue.\n");
                    continue; //skip to next queue if empty
                }
                int position = 1; 
                foreach (var player in queue)
                {
                    var waitTime = DateTime.Now - player.JoinedQueue; //calculating wait time
                    Console.WriteLine($"{position}. {player.Username} (Skill: {player.SkillRating}) - Waiting: {waitTime.Minutes}m {waitTime.Seconds}s"); //displaying player info
                    position++; //increment position for next player
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            Console.WriteLine($"{player.ToDetailedString()}"); //using existing method for basic info

            Console.WriteLine("\n-- Queue Status --");
            var inQueue = casualQueue.Contains(player) || rankedQueue.Contains(player) || quickPlayQueue.Contains(player); //checking if player is in any queue
            if (inQueue)
            {
                Console.WriteLine("Status: In Queue");
                //determining which queue the player is in to get estimated wait time
                var mode = casualQueue.Contains(player) ? GameMode.Casual :
                           rankedQueue.Contains(player) ? GameMode.Ranked :
                           GameMode.QuickPlay;
                var estimate = GetQueueEstimate(mode); //using existing method to get wait time estimate
                Console.WriteLine($"Estimated Wait Time: {estimate}"); //displaying wait time estimate
            }
            else
            {
                Console.WriteLine("Status: Not in Queue");
            }

            Console.WriteLine("\n-- Recent Match History --");
            //getting last 3 matches involving this player
            var recentMatches = matchHistory 
                .Where(m => m.Player1 == player || m.Player2 == player)
                .OrderByDescending(m => m.MatchTime)
                .Take(3)
                .ToList();
            if (recentMatches.Count == 0)
            {
                Console.WriteLine("No matches played yet."); //handling no match case
            }
            else
            { //displaying recent matches
                foreach (var match in recentMatches)
                {
                    var result = match.Winner == player ? "Won" : "Lost";
                    var opponent = match.Player1 == player ? match.Player2 : match.Player1;
                    Console.WriteLine($"{match.MatchTime}: {result} against {opponent.Username} (Mode: {match.Mode}, Skill Diff: {match.SkillDifference})");
                }
            }
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            int playerCount;
            switch (mode)
            {
                case GameMode.Casual:
                    playerCount = casualQueue.Count();
                    break;
                case GameMode.Ranked:
                    playerCount = rankedQueue.Count();
                    break;
                case GameMode.QuickPlay:
                    playerCount = quickPlayQueue.Count();
                    break;
                default:
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }
            switch (playerCount)
            {
                case >= 2:
                    return "No wait";
                case 1:
                    return "Short wait";
                default:
                    return "Long wait";
            }
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}