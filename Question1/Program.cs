using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal class Program
    {
        static readonly string connectionString = "Data Source=SoccerStarsFC.db;Version=3;";
        static void Main()
        {
            CreateDatabase();
            InitializeDatabase();
            DisplayMenu();
        }

        // If the database file doesn't exist, then create database file.
        static void CreateDatabase()
        {
            if (!File.Exists("SoccerStarsFC.db"))
            {
                SQLiteConnection.CreateFile("SoccerStarsFC.db");
            }
        }

        // If the Player table doesn't exist in the database file,
        // then create a Player Table.
        static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Players (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Age INTEGER NOT NULL,
                                        Position TEXT NOT NULL)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Display Menu after starting up and every actions.
        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to the Soccer Player Management System!\n");
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add a Player");
                Console.WriteLine("2. Remove a Player");
                Console.WriteLine("3. Search for a Player");
                Console.WriteLine("4. Display All Players");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int option = 0;

                int.TryParse(input, out option);

                switch (option)
                {
                    case 1:
                        AddPlayer();
                        break;
                    case 2:
                        RemovePlayer();
                        break;
                    case 3:
                        SearchPlayer();
                        break;
                    case 4:
                        DisplayAllPlayers();
                        break;
                    case 5:
                        Console.WriteLine("Thank you for using the Soccer Player Management System.\n");
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }

        static void AddPlayer()
        {
            Console.WriteLine("\nEnter player details:");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Position: ");
            string position = Console.ReadLine();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Players (Name, Age, Position) VALUES (@Name, @Age, @Position)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Position", position);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Player added successfully!\n");
        }

        static void RemovePlayer()
        {
            Console.Write("\nEnter the name of the player to remove: ");
            string name = Console.ReadLine();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Players WHERE Name = @Name";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Player removed successfully!\n");
                    }
                    else
                    {
                        Console.WriteLine("Player not found.\n");
                    }
                }
            }
        }

        static void SearchPlayer()
        {
            Console.Write("Enter the name of the player to search: ");
            string name = Console.ReadLine();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM Players WHERE Name = @Name";
                using (var command = new SQLiteCommand(searchQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Name: {reader["Name"]}, Age: {reader["Age"]}, Position: {reader["Position"]}\n");
                        }
                        else
                        {
                            Console.WriteLine("Player not found.\n");
                        }
                    }
                }
            }
        }

        static void DisplayAllPlayers()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM Players";
                using (var command = new SQLiteCommand(searchQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Name: {reader["Name"]}, Age: {reader["Age"]}, Position: {reader["Position"]}");
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("There are no players.\n");
                        }
                    }
                }
            }
        }
    }
}
