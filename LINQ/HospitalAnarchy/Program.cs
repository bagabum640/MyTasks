using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalAnarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            hospital.Work();
        }
    }

    class Hospital
    {
        private List<Patient> _patients = new List<Patient>();

        public Hospital()
        {
            _patients.Add(new Patient("Алена Ивановна", 60, "Сотрясение мозга"));
            _patients.Add(new Patient("Родион Раскольников", 27, "Нервный срыв"));
            _patients.Add(new Patient("Илья Обломов", 31, "Обширные пролежни"));
            _patients.Add(new Patient("Андрей Штольц", 31, "Нервный срыв"));
            _patients.Add(new Patient("Илья Муромец", 32, "Обширные пролежни"));
            _patients.Add(new Patient("Кощей Бессмертный", 666, "Нервный срыв"));
            _patients.Add(new Patient("Змей Горыныч", 28, "Сотрясение мозга"));
            _patients.Add(new Patient("Александр Пушкин", 37, "Пулевое ранение"));
            _patients.Add(new Patient("Кертис Джексон", 47, "Пулевое ранение"));
            _patients.Add(new Patient("Андрей Болконский", 34, "Пулевое ранение"));
        }

        public void Work()
        {
            const string CommandSortFullName = "Отсортировать по имени";
            const string CommandSortAge = "Отсортировать по возрасту";
            const string CommandFilterDesease = "Вывести больных по заболевнию";
            const string CommandExit = "Выйти";

            bool isWork = true;
            string[] commands = { CommandSortFullName, CommandSortAge , CommandFilterDesease, CommandExit };

            while (isWork)
            {
                ShowPatients(_patients);

                switch (ChooseCommand(commands))
                {
                    case CommandSortFullName:
                        SortFullName();
                        break;

                    case CommandSortAge:
                        SortAge();
                        break;

                    case CommandFilterDesease:
                        FilterDesease();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                Console.Clear();
            }
        }

        private void SortFullName()
        {            
            var sortedPatients = _patients.OrderBy(patient => patient.Name).ToList();
            OverwriteList(sortedPatients);
        }

        private void SortAge()
        {
            var sortedPatients = _patients.OrderBy(patient => patient.Age).ToList();            
            OverwriteList(sortedPatients);            
        }

        private void FilterDesease()
        {
            Console.Write("Введите диагноз: ");
            string desease = Console.ReadLine().ToLower();            
            var filteredPatient = _patients.Where(patient => patient.Desease.ToLower() == desease);
            Console.Clear();

            if (filteredPatient.Count() > 0)
            {
                ShowPatients(filteredPatient.ToList());
            }
            else
            {
                Console.WriteLine("Пациентов с таким диагнозом не найдено.");
            }

            Console.ReadKey();            
        }

        private void OverwriteList(List<Patient> patients)
        {
            _patients.Clear();
            _patients.AddRange(patients);
        }

        private void ShowPatients(List<Patient> patients)
        {      
            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }

            Console.WriteLine();
        }

        private string ChooseCommand(string[] commands)
        {
            const ConsoleKey PreviousString = ConsoleKey.UpArrow;
            const ConsoleKey NextString = ConsoleKey.DownArrow;
            const ConsoleKey SelectString = ConsoleKey.Enter;

            bool isWork = true;
            int numberString = 0;
            int cursorPositionX = Console.CursorLeft;
            int cursorPositionY = Console.CursorTop;
            ConsoleKeyInfo key;

            if (IsWrongArray(commands))
                return null;

            while (isWork)
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                WriteStrings(0, numberString, commands);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(commands[numberString]);
                Console.ResetColor();
                WriteStrings(numberString + 1, commands.Length, commands);
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case SelectString:
                        isWork = false;
                        break;

                    case NextString:
                        numberString = (numberString + 1 < commands.Length) ? numberString + 1 : 0;
                        break;

                    case PreviousString:
                        numberString = (numberString - 1 >= 0) ? numberString - 1 : commands.Length - 1;
                        break;
                }
            }

            ClearPartOfConsole(cursorPositionX, cursorPositionY, commands);
            return commands[numberString];
        }

        private bool IsWrongArray(string[] array)
        {
            if (array == null || array.Length == 0)
                return true;

            foreach (var command in array)
            {
                if (command == null)
                    return true;
            }

            return false;
        }

        private void WriteStrings(int firstString, int lastString, string[] strings)
        {
            for (int i = firstString; i < lastString; i++)
            {
                Console.WriteLine(strings[i]);
            }
        }

        private void ClearPartOfConsole(int сursorPositionX, int сursorPositionY, string[] commands)
        {
            Console.SetCursorPosition(сursorPositionX, сursorPositionY);

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < commands[i].Length; j++)
                {
                    Console.Write(' ');
                }

                Console.WriteLine("");
            }

            Console.SetCursorPosition(сursorPositionX, сursorPositionY);
        }
    }

    class Patient
    {
        public Patient(string name, int age, string desease)
        {
            Name = name;
            Age = age;
            Desease = desease;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Desease { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Пациент: {Name}, возраст: {Age} лет, диагноз: {Desease}.");
        }
    }
}
