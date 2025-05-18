using System;
using System.Threading;

class MemoryGame
{
    static int rows = 3;
    static int cols = 2;
    static int wins = 0;  // Track number of wins

    static void Main()
    {
        Console.WriteLine("Welcome to the Memory Matching Game!");
        Console.WriteLine("Rules:");
        Console.WriteLine("- The grid contains hidden pairs of matching letters.");
        Console.WriteLine("- On each turn, choose two different cards to flip over.");
        Console.WriteLine("- If the cards match, they stay revealed.");
        Console.WriteLine("- If they don't match, they flip back.");
        Console.WriteLine("- Match all pairs to win the game!");
        Console.WriteLine("- Enter numbers to select cards.");
        Console.WriteLine("- Enter 'q' anytime to quit.");
        Console.WriteLine();

        bool keepPlaying = true;

        while (keepPlaying)
        {
            PlayGame();

            // Display total wins (matches)
            Console.WriteLine();
            Console.WriteLine(wins == 1 ? "You got 1 win!" : $"You got {wins} wins!");
            Console.WriteLine();

            Console.WriteLine("Press Enter to play again or 'q' to quit.");
            string input = Console.ReadLine();
            if (input.Trim().ToLower() == "q")
            {
                keepPlaying = false;
            }
            Console.Clear();
        }

        Console.WriteLine("Thanks for playing! Goodbye!");
    }

    static void PlayGame()
    {
        int asciiStart = 65;
        char[] grid = new char[rows * cols];
        for (int i = 0; i < grid.Length; i++)
            grid[i] = Convert.ToChar(asciiStart + i / 2);

        Random rand = new Random();
        Shuffle(rand, grid);

        string[] playingGrid = new string[rows * cols];
        for (int i = 0; i < playingGrid.Length; i++)
            playingGrid[i] = (i + 1).ToString();

        int matches = 0;
        bool gameWon = false;

        while (!gameWon)
        {
            PrintPlayingGrid(playingGrid);
            Console.WriteLine("Select your first card (or 'q' to quit):");
            var input1 = Console.ReadLine();
            if (input1.Trim().ToLower() == "q") Environment.Exit(0);
            int choice1 = int.Parse(input1);
            playingGrid[choice1 - 1] = grid[choice1 - 1].ToString();
            Console.Clear();

            PrintPlayingGrid(playingGrid);
            Console.WriteLine("Select your second card (or 'q' to quit):");
            var input2 = Console.ReadLine();
            if (input2.Trim().ToLower() == "q") Environment.Exit(0);
            int choice2 = int.Parse(input2);
            playingGrid[choice2 - 1] = grid[choice2 - 1].ToString();
            Console.Clear();

            PrintPlayingGrid(playingGrid);

            if (grid[choice1 - 1] == grid[choice2 - 1])
            {
                Console.WriteLine("Match!");
                matches++;

                if (matches == (rows * cols) / 2)
                {
                    gameWon = true;
                    wins++;  // Increment wins counter
                }
            }
            else
            {
                Console.WriteLine("No match...");
                playingGrid[choice2 - 1] = (choice2).ToString();
                playingGrid[choice1 - 1] = (choice1).ToString();
            }

            Thread.Sleep(1000);
            Console.Clear();
        }

        Console.WriteLine("Congratulations, you win!");
    }

    static void PrintPlayingGrid(string[] playingGrid)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write(playingGrid[cols * i + j] + " | ");
            Console.WriteLine();
        }
    }

    static void Shuffle(Random rand, char[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
