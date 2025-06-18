using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StarcraftDemo4
{
    class GameOptimizer
    {
        public static void PlayBaseGames(int num_games = 1)
        {
            for (int i = 1; i <= num_games; i++)
            {
                Console.WriteLine($"Playing game {i} of {num_games}...");
                
                SingleGame TestGame = new SingleGame();
                AutoPlayer Scott = new AutoPlayer();
                
                State GameState = TestGame.PlayAutoGame(Scott);
                
                Console.WriteLine($"Game {i} completed:");
                Console.WriteLine($"  Final Time: {GameState.totalTime} seconds");
                Console.WriteLine($"  Final Minerals: {GameState.minerals}");
                Console.WriteLine($"  Final Gas: {GameState.gas}");
                Console.WriteLine($"  Units: {GameState.unit_Count}/{GameState.unit_Cap}");
                Console.WriteLine($"  Total Moves: {TestGame.MovesPlayed.Count}");
                Console.WriteLine("*******************");
            }
        }
    }
}
