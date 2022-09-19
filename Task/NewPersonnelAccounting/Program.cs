using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPersonnelAccounting
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dossiers = new Dictionary<string, string>();
            ConsoleKeyInfo key;
            bool isExit = false;            

            Console.WriteLine("Нажмите:\n1 - чтобы добавить новое досье\n2 - чтобы показать все досье\n3 - чтобы удалить досье\n4 - чтобы найти досье\n" +
                "5 - чтобы выйти\n");

            while (isExit != true)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {                    
                    case ConsoleKey.NumPad1:
                        AddDossier(dossiers);
                        break;

                    case ConsoleKey.NumPad2:
                        ShowDossiers(dossiers);
                        break;

                    case ConsoleKey.NumPad3:
                        DeleteDossier(dossiers);
                        break;

                    case ConsoleKey.NumPad4:
                        SearchDossier(dossiers);
                        break;

                    case ConsoleKey.NumPad5:
                        isExit = true;
                        break;
                    
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }

        static void AddDossier (Dictionary<string, string> dossiers)
        {
            string fullName, positionInCompany;

            Console.Write("\nВведите ФИО добавляемого сотрудника: ");
            fullName = Console.ReadLine();
            Console.Write("Введите должность добавляемого сотрудника: ");
            positionInCompany = Console.ReadLine();
            dossiers.Add(fullName, positionInCompany);
        }

        static void ShowDossiers (Dictionary<string, string> dossiers)
        {
            Console.WriteLine();

            foreach (var item in dossiers)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
        }

        static void DeleteDossier(Dictionary<string, string> dossiers)
        {
            Console.Write("\nВведите имя удаляемого сотрудника: ");
            string fullName = Console.ReadLine();

            if (dossiers.ContainsKey(fullName))
            {
                dossiers.Remove(fullName);
            }
            else
            {
                Console.WriteLine("\nТакого сотрудника нет в базе!");
            }            
        }

        static void SearchDossier(Dictionary<string, string> dossiers)
        {
            Console.Write("\nВведите имя сотрудника,которого вы ищете: ");
            string fullName = Console.ReadLine();

            if (dossiers.ContainsKey(fullName))
            {
                Console.WriteLine($"\n{fullName} - {dossiers[fullName]}");
            }
            else
            {
                Console.WriteLine("\nТакого сотрудника нет в базе!");
            }
        }
    }
}
