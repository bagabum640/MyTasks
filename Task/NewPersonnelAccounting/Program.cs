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
            const ConsoleKey AddDosierKey = ConsoleKey.NumPad1;
            const ConsoleKey ShowDosiersKey = ConsoleKey.NumPad2;
            const ConsoleKey DeleteDosierKey = ConsoleKey.NumPad3;
            const ConsoleKey SearchDosierKey = ConsoleKey.NumPad4;
            const ConsoleKey Exit = ConsoleKey.NumPad5;

            Console.WriteLine($"Нажмите:\n{AddDosierKey} - чтобы добавить новое досье\n{ShowDosiersKey} - чтобы показать все досье" +
                $"\n{DeleteDosierKey} - чтобы удалить досье\n{SearchDosierKey} - чтобы найти досье\n" +
                $"{Exit} - чтобы выйти\n");

            while (isExit != true)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {                    
                    case AddDosierKey:
                        AddDossier(dossiers);
                        break;

                    case ShowDosiersKey:
                        ShowDossiers(dossiers);
                        break;

                    case DeleteDosierKey:
                        DeleteDossier(dossiers);
                        break;

                    case SearchDosierKey:
                        SearchDossier(dossiers);
                        break;

                    case Exit:
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
            string fullName;
            string positionInCompany;

            Console.Write("\nВведите ФИО добавляемого сотрудника: ");
            fullName = Console.ReadLine();

            if (dossiers.ContainsKey(fullName))
            {
                Console.WriteLine("Сотрудник с таким именем уже числиться в базе!");
            }
            else
            {
                Console.Write("Введите должность добавляемого сотрудника: ");
                positionInCompany = Console.ReadLine();
                dossiers.Add(fullName, positionInCompany);
            }            
        }

        static void ShowDossiers (Dictionary<string, string> dossiers)
        {
            Console.WriteLine();

            foreach (var dossier in dossiers)
            {
                Console.WriteLine($"{dossier.Key} - {dossier.Value}");
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
