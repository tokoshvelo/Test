using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BookManagerApp
{
    // Book class
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Author: {Author}, Year: {Year}";
        }
    }

    // BookManager class
    public class BookManager
    {
        private const string FilePath = "books.json";
        private List<Book> books;

        public BookManager()
        {
            books = LoadBooksFromFile();
        }

        // Add a new book
        public void AddBook(string title, string author, int year)
        {
           

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("All fields must contain valid information.");
            }

            if (!Regex.IsMatch(author, @"^[a-zA-Z\s.,'-]+$"))
            {
                throw new ArgumentException("Author's name cannot contain numbers or invalid characters.");
            }

            books.Add(new Book(title, author, year));
            SaveBooksToFile();
            Console.WriteLine("Book added successfully!");
        }

        // Display all books
        public void DisplayBooks()
        {
            if (!books.Any())
            {
                Console.WriteLine("The book list is empty.");
                return;
            }

            Console.WriteLine("Book List:");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        // Search for books by title
        public void SearchBookByTitle(string title)
        {
            var foundBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!foundBooks.Any())
            {
                Console.WriteLine($"No books found with the title '{title}'.");
                return;
            }

            Console.WriteLine($"Books found with the title '{title}':");
            foreach (var book in foundBooks)
            {
                Console.WriteLine(book);
            }
        }

        // Load books from the JSON file
        private List<Book> LoadBooksFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Book>();
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            catch
            {
                Console.WriteLine("Error loading books from file. Starting with an empty list.");
                return new List<Book>();
            }
        }

        // Save books to the JSON file
        private void SaveBooksToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving books to file: {ex.Message}");
            }
        }
    }

    // User Interface
    class Program
    {
        static void Main(string[] args)
        {
            BookManager bookManager = new BookManager();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. View all books");
                Console.WriteLine("3. Search for a book by title");
                Console.WriteLine("4. Exit the program");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter book author: ");
                        string author = Console.ReadLine();

                        Console.Write("Enter publication year: ");
                        if (int.TryParse(Console.ReadLine(), out int year))
                        {
                            try
                            {
                                bookManager.AddBook(title, author, year);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid year.");
                        }
                        break;

                    case "2":
                        bookManager.DisplayBooks();
                        break;

                    case "3":
                        Console.Write("Enter the title to search for: ");
                        string searchTitle = Console.ReadLine();
                        bookManager.SearchBookByTitle(searchTitle);
                        break;

                    case "4":
                        exit = true;
                        Console.WriteLine("Program exited.");
                        break;

                    default:
                        Console.WriteLine("Please enter a valid option.");
                        break;
                }
            }
        }
    }
}
