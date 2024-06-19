using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question4
{
    internal class Program
    {
        public static Random random = new Random();
        public static int[] distances = new int[5];
        public static int raceDistance = 30;
        public static int winnerHorse = -1;
        public static async Task RunRace(int horseNumber)
        {
            while (distances[horseNumber - 1] < raceDistance)
            {
                int distanceCovered = random.Next(1, 10);
                distances[horseNumber - 1] += distanceCovered;
                Console.WriteLine($"Horse {horseNumber}: Distance covered: {distances[horseNumber - 1]}");
                await Task.Delay(100);
            }
            if (winnerHorse == -1) winnerHorse = horseNumber;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Horse Racing System!");
            Console.WriteLine("Race starts now!");

            Task[] tasks = new Task[5];
            for (int j = 0; j < 5; j++)
            {
                int horseNumber = j + 1;
                tasks[j] = Task.Run(() => RunRace(horseNumber));
            }

            Task.WaitAll(tasks);

            //int winningHorse = Array.IndexOf(distances, distances.Max()) + 1;
            Console.WriteLine("\nRace finished!");
            Console.WriteLine($"Horse {winnerHorse} wins the race!\n");
        }
    }
}
