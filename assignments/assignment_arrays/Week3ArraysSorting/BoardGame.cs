using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Board Game implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// 
    /// Choose ONE game to implement:
    /// - Tic-Tac-Toe (3x3 grid)
    /// - Connect Four (6x7 grid with gravity)
    /// - Or something else creative using a 2D array! (I need to be able to understand the rules from your instructions)
    /// </summary>
    public class BoardGame
    {
        //GAME is CONNECT FOUR
        //declare 2D array as board
        private char[,] board = new char[6, 7]; // 6 rows, 7 columns

        private char currentPlayer = 'X';
        private bool gameOver = false;
        private string winner = "";

        /// <summary>
        /// Constructor - Initialize the board game
        /// TODO: Set up your chosen game
        /// </summary>
        public BoardGame()
        {

            //initialize board with blank spaces
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = '_';
                }
            }

        }

        /// <summary>
        /// Main game loop - handles the complete game session
        /// TODO: Implement the full game experience
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== CONNECT FOUR (Part A) ===");
            Console.WriteLine();

            // TODO: Display game instructions
            DisplayInstructions();

            // TODO: Implement main game loop
            bool playAgain = true;

            while (playAgain)
            {
                // TODO: Reset game state for new game
                InitializeNewGame();

                // TODO: Play one complete game
                PlayOneGame();

                // TODO: Ask if player wants to play again
                playAgain = AskPlayAgain();
            }

            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        /// <summary>
        /// Display game instructions and controls
        /// TODO: Customize for your chosen game
        /// </summary>
        private void DisplayInstructions()
        {

            //INSTRUCTIONS FOR CONNECT FOUR
            Console.WriteLine("CONNECT FOUR RULES:");
            Console.WriteLine("- Players take turns dropping tokens");
            Console.WriteLine("- Enter column number (0-6) when prompted");
            Console.WriteLine("- First to get 4 in a row wins!");

            Console.WriteLine();
        }

        /// <summary>
        /// Initialize/reset the game for a new round
        /// TODO: Reset board and game state
        /// </summary>
        private void InitializeNewGame()
        {
            // TODO: Reset game over flag
            // TODO: Clear winner

            currentPlayer = 'X';
            gameOver = false;
            winner = "";

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = '_';
                }
            }

            Console.WriteLine("New game started! Player X goes first.");
            RenderBoard();

        }

        /// <summary>
        /// Play one complete game until win/draw/quit
        /// TODO: Implement the core game loop
        /// </summary>
        private void PlayOneGame()
        {
            // TODO: Game loop structure:
            while (!gameOver)
            {
                GetPlayerMove();
                RenderBoard();
                CheckWinCondition();
                if (gameOver)
                {
                    break;
                }
            }

        }


        /// <summary>
        /// Get and validate player move input
        /// TODO: Handle user input with validation
        /// </summary>
        private void GetPlayerMove()
        {
            bool validMove = false;

            while (!validMove)
            {
                Console.WriteLine($"Player {currentPlayer}, enter your move (column 0-6): ");
                string? input = Console.ReadLine();

                //parse input to int
                if (int.TryParse(input, out int col))
                {
                    //input is between 0 and 6
                    if (col >= 0 && col < board.GetLength(1))
                    {
                        //check if col has any empty spaces
                        if (board[0, col] == '_')
                        {
                            DropToken(col, currentPlayer);
                            validMove = true;
                            break;
                        }
                        else
                        {
                            //col is full
                            Console.WriteLine("Column is full. Try a different column.");
                            validMove = false;
                        }
                    }
                    else
                    {
                        //input isnt a number between 0 and 6
                        Console.WriteLine("Invalid column. Please enter a number between 0 and 6.");
                        validMove = false;
                    }
                }
                else
                {
                    //input isnt a number
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    validMove = false;
                }
            }
        }
        private void DropToken(int col, char currentPlayer)
        {
            Console.WriteLine($"Dropping {currentPlayer}'s token in column {col}");
            //check for lowest empty space in specified col
            for (int row = board.GetLength(0) - 1; row >= 0; row--)
            {
                if (board[row, col] == '_')
                {
                    board[row, col] = currentPlayer;
                    break;
                }
            }
        }

        /// <summary>
        /// Render the current board state to console
        /// TODO: Create clear, readable board display
        /// </summary>
        private void RenderBoard()
        {
            // TODO: Display your multi-dimensional array as a visual board
            // Requirements:
            // - Clear, human-readable format
            // - Show current board state
            // - Include row/column labels for easy reference

            // rendering structure:
            // 0 1 2 3 4 5 6
            // _ _ _ _ _ _ _
            // _ _ _ _ _ _ _
            // _ _ _ _ _ _ _
            // _ _ _ _ _ _ _
            // _ _ _ _ _ _ _
            // _ _ _ _ _ _ _


            for (int col = 0; col < board.GetLength(1); col++)
            {
                Console.Write($" {col} ");
            }
            Console.WriteLine();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write($" {board[row, col]} ");
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Check if current board state has a winner or draw
        /// TODO: Implement win detection logic
        /// </summary>
        private void CheckWinCondition()
        {

            while (!gameOver)
            {
                if (CheckRow(currentPlayer))
                {
                    Console.WriteLine("Row win detected!");
                    gameOver = true;
                    break;
                }
                else if (CheckCol(currentPlayer))
                {
                    Console.WriteLine("Column win detected!");
                    gameOver = true;
                    break;
                }
                else if (CheckDiagonal(currentPlayer))
                {
                    Console.WriteLine("Diagonal win detected!");
                    gameOver = true;
                    break;
                }
                else if (CheckDraw(currentPlayer))
                {
                    Console.WriteLine("Draw detected!");
                    winner = "Draw";
                    gameOver = true;
                    break;

                }
                else
                {
                    SwitchPlayer();
                    break;
                }
            }

            if (gameOver)
            {
                if (winner == "Draw")
                {
                    Console.WriteLine("It's a draw!");
                }
                else
                {
                    winner = currentPlayer.ToString();
                    Console.WriteLine($"Player {winner} wins!");
                }
            }

        }
        /// <summary>
        /// Switch to the next player's turn
        /// TODO: Toggle between X and O
        /// </summary>
        private void SwitchPlayer()
        {
            // TODO: Switch currentPlayer between 'X' and 'O'            
            if (currentPlayer == 'X')
            {
                currentPlayer = 'O';
            }
            else
            {
                currentPlayer = 'X';
            }
            Console.WriteLine($"It's now Player {currentPlayer}'s turn.");

        }

        /// <summary>
        /// Ask player if they want to play another game
        /// TODO: Simple yes/no prompt with validation
        /// </summary>
        private bool AskPlayAgain()
        {
            Console.Write("Play again? (y/n): ");
            string? input = Console.ReadLine();
            if (input != null && (input.ToLower() == "y"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRow(char currentPlayer)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                int count = 0;
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == currentPlayer)
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            return false;
        }

        private bool CheckCol(char currentPlayer)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                int count = 0;
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    if (board[row, col] == currentPlayer)
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
            }

            return false;
        }

        private bool CheckDiagonal(char currentPlayer)
        {
            // Check for diagonals with positive slope
            for (int row = 3; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row - 1, col + 1] == currentPlayer &&
                        board[row - 2, col + 2] == currentPlayer &&
                        board[row - 3, col + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            // Check for diagonals with negative slope
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (board[row, col] == currentPlayer &&
                        board[row + 1, col + 1] == currentPlayer &&
                        board[row + 2, col + 2] == currentPlayer &&
                        board[row + 3, col + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckDraw(char currentPlayer)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[0, col] == '_')
                {
                    return false;
                }
            }
            return true;
        }

    }
        
        // TODO: Add helper methods as needed
    // Examples:
    // - IsValidMove(int row, int col)
    // - IsBoardFull()
    // - CheckRow(int row, char player)
    // - CheckColumn(int col, char player)
    // - CheckDiagonals(char player)
    // - DropToken(int column, char player) // For Connect Four

}