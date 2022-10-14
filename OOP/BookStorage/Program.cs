using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            const string AddBookCommand = "add";
            const string DeleteBookCommand = "delete";
            const string ShowBooksCommand = "show";
            const string ShowBooksByParameterCommand = "showby";
            const string ExitCommand = "exit";

            bool isExit = false;
            string command;
            BookStorage bookStorage = new BookStorage();            
            
            Console.WriteLine($"Введите:\n{AddBookCommand} - чтобы добавить книгу\n{DeleteBookCommand} - чтобы удалить книгу\n" +
                $"{ShowBooksCommand} - чтобы показать все книги\n{ShowBooksByParameterCommand} - показать все книги по указанному параметру\n{ExitCommand} - чтобы выйти\n");

            while (isExit == false)
            {
                Console.WriteLine();
                command = Console.ReadLine();

                switch (command)
                {
                    case AddBookCommand:
                        bookStorage.Add();
                        break;

                    case DeleteBookCommand:
                        bookStorage.Delete();
                        break;

                    case ShowBooksCommand:
                        bookStorage.Show();
                        break;

                    case ShowBooksByParameterCommand:
                        bookStorage.ShowAt();
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!\n");
                        break;
                }
            }
        }
    }

    class BookStorage
    {
        private List<Book> _storage = new List<Book>();        

        public BookStorage()
        {
            Book[] books = { new Book("Fate", "V.Vasya", "1965"), new Book("Lost", "V.Vasya", "1967"), new Book("Grot", "P.Piti", "1965") };
            _storage.AddRange(books);
        }

        public void Add()
        {
            Console.Write("Введите название книги: ");
            string name = Console.ReadLine();
            Console.Write("Введите автора книги: ");
            string writter = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            string yearOfIssue = Console.ReadLine();

            _storage.Add(new Book(name, writter, yearOfIssue));
        }
        
        public void Delete ()
        {
            Console.Write("Введите название книги: ");
            string name = Console.ReadLine();

            for (int i = 0; i < _storage.Count; i++)
            {
                if (name == _storage[i].Name)
                {
                    _storage.RemoveAt(i);
                    Console.WriteLine($"Книга удалена.");
                }
                else
                {
                    Console.WriteLine("Такой книги нет.");
                }
            }
        }

        public void Show()
        {
            foreach (var book in _storage)
            {
                book.Show();
            }
        }

        public void ShowAt()
        {
            Console.Write("Введите параметр искомой книги: ");
            string parameter = Console.ReadLine();
            bool wasNotFound = true;

            foreach (var book in _storage)
            {
                if (book.SearchedBook(parameter))
                {
                    book.Show();
                    wasNotFound = false;
                }                
            }

            if (wasNotFound)
            {
                Console.WriteLine("Книг по заданному параметру не найдено.");
            }
        }
    }

    class Book
    {
        public string Name  { get; private set; }
        public string Writter { get; private set; }
        public string YearOfIssue { get; private set; }

        public Book(string name, string writter, string yearOfIssue)
        {
            Name = name;
            Writter = writter;
            YearOfIssue = yearOfIssue;
        }

        public void Show()
        {
            Console.WriteLine($"{Name} - {Writter} - {YearOfIssue}");
        }

        public bool SearchedBook(string parameter)
        {
            if (Name == parameter || Writter == parameter || YearOfIssue == parameter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

