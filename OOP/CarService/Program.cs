using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {
            CarService carService = new CarService();

            carService.Work();
        }
    }

    class CarService
    {
        private Car _car;
        private DetailStorage _detailStorage = new DetailStorage();
        private int _money = 0;
        private int _servicePrice = 0;
        private double _multiplierWorkCostFromPartPrice = 0.3;
        private int _refusalPenalty = 1000;
        private int _errorPenalty = 300;
        private int _debtBankruptcy = -3000;
        bool _isWork = true;

        public void Work()
        {
            const string CommandInviteClient = "Пригласить клиента";
            const string CommandReplacePart = "Заменить деталь";
            const string CommandLookAtStorage = "Заглянуть на склад";
            const string CommandRefuseClient = "Прогнать клиента";
            const string CommandFinishService = "Завершить обслуживание";
            const string CommandExit = "Выйти из автосервиса";

            string command;
            string[] commands = { CommandInviteClient, CommandReplacePart, CommandLookAtStorage, CommandRefuseClient, CommandFinishService, CommandExit };

            while (_isWork)
            {
                Console.WriteLine($"Баланс автосервиса: {_money} рублей. \tЦена текущего обслуживания: {_servicePrice} рублей.\n");

                if (_car != null) _car.ShowInfo();

                command = ChooseCommand(commands);

                switch (command)
                {
                    case CommandInviteClient:
                        InviteClient();
                        break;

                    case CommandReplacePart:
                        if (IsCarNotNull()) ReplacePart();
                        break;

                    case CommandLookAtStorage:
                        _detailStorage.ShowDetails();
                        break;

                    case CommandRefuseClient:
                        if (IsCarNotNull()) RefuseClient();
                        break;

                    case CommandFinishService:
                        if (IsCarNotNull()) FinishService();
                        break;

                    case CommandExit:
                        _isWork = false;
                        break;
                }
            }
        }

        private void ReplacePart()
        {
            string[] partNamesOfDetailStorage = _detailStorage.GetPartNames();

            if (partNamesOfDetailStorage == null)
            {
                ShowMessage("Склад пуст!");
                return;
            }

            Detail detail = _detailStorage.GetDetail(ChooseCommand(partNamesOfDetailStorage));

            if (_car.TryChangeDetail(detail))
            {
                ShowMessage($"Вы успешно заменили {detail.Name}. Цена за работу: {(int)(detail.Price * _multiplierWorkCostFromPartPrice)}. " +
                    $"Цена за деталь: {detail.Price}.");
                _servicePrice += (int)(detail.Price * (1 + _multiplierWorkCostFromPartPrice));
            }
            else
            {
                _servicePrice -= _errorPenalty;
                IsBankruptcy();
            }
        }

        private void RefuseClient()
        {
            _car = null;
            _money -= _refusalPenalty - _servicePrice;
            _servicePrice = 0;
            ShowMessage("Вы со скандалом прогоняете клиента!");
            IsBankruptcy();
        }

        private bool IsBankruptcy()
        {
            if (_money <= _debtBankruptcy)
            {
                ShowMessage("Ваш автосервис слишком убыточен! Его закрывают за долги!");
                _isWork = false;
                return true;
            }

            return false;
        }

        private bool IsCarNotNull()
        {
            if (_car != null)
            {
                return true;
            }

            ShowMessage("Сперва пригласите клиета!");
            return false;
        }

        private void InviteClient()
        {
            if (_car == null)
                _car = new Car();
            else
                ShowMessage("Сперва закончите обслуживание предыдущего клиента!");
        }

        private void FinishService()
        {
            if (_car.IsCarWorked())
            {
                _money += _servicePrice;
                _servicePrice = 0;
                _car = null;
            }
            else
            {
                ShowMessage("Вы еще не закончили работу!");
            }
        }

        private void ShowMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        private string ChooseCommand(string[] commands)
        {
            bool isWork = true;
            int numberString = 0;
            int CursorPositionX = 0;
            int CursorPositionY;
            int CorsorTopWithClient = 10;
            int CorsorTopWithoutClient = 2;

            CursorPositionY = (_car == null) ? CorsorTopWithoutClient : CorsorTopWithClient;

            while (isWork)
            {
                Console.SetCursorPosition(CursorPositionX, CursorPositionY);

                for (int i = 0; i < numberString; i++)
                {
                    Console.WriteLine(commands[i]);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(commands[numberString]);
                Console.ResetColor();

                for (int i = numberString + 1; i < commands.Length; i++)
                {
                    Console.WriteLine(commands[i]);
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        isWork = false;
                        break;

                    case ConsoleKey.DownArrow:
                        numberString = (numberString + 1 < commands.Length) ? numberString + 1 : 0;
                        break;

                    case ConsoleKey.UpArrow:
                        numberString = (numberString - 1 >= 0) ? numberString - 1 : commands.Length - 1;
                        break;                    
                }
            }

            ClearPartOfConsole(CursorPositionX, CursorPositionY, commands);
            return commands[numberString];
        }

        private void ClearPartOfConsole(int CursorPositionX, int CursorPositionY, string[] commands)
        {
            Console.SetCursorPosition(CursorPositionX, CursorPositionY);

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < commands[i].Length; j++)
                {
                    Console.Write(' ');
                }

                Console.WriteLine("");
            }

            Console.Clear();
        }
    }

    class Car
    {
        private Random _random = new Random();
        private List<Detail> _carSystem = new List<Detail>();

        public Car()
        {
            _carSystem.Add(new Detail("Колесо", 100, 30));
            _carSystem.Add(new Detail("Двигатель", 3000, 50));
            _carSystem.Add(new Detail("Фара", 350, 20));
            _carSystem.Add(new Detail("Руль", 600, 25));
            _carSystem.Add(new Detail("Вентилятор", 450, 15));
            BreakDetails();
        }

        public void ShowInfo()
        {
            Console.WriteLine("Состояние систем автомобиля:\n");

            foreach (var detail in _carSystem)
            {
                Console.Write($"{detail.Name}: ");

                if (detail.IsWorken == true)
                {
                    Console.WriteLine("Исправно.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не исправно.");
                    Console.ResetColor();
                }
            }
        }

        public bool IsCarWorked()
        {
            foreach (var detail in _carSystem)
            {
                if (detail.IsWorken == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool TryChangeDetail(Detail detail)
        {
            for (int i = 0; i < _carSystem.Count; i++)
            {
                if (detail.Name == _carSystem[i].Name)
                {
                    if (_carSystem[i].IsWorken)
                    {
                        Console.Clear();
                        Console.WriteLine("Вы попытались заменить рабочую деталь! Клиент в ярости!");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                    else
                    {
                        _carSystem[i] = detail;
                        return true;
                    }
                }
            }

            Console.WriteLine("Такой детали нет в автомобиле! Вас ждет штраф за такую ошибку...");
            return false;
        }

        private void BreakDetails()
        {
            int brokenDetails = 0;

            foreach (var detail in _carSystem)
            {
                if (_random.Next(2) == 0)
                {
                    detail.Break();
                    brokenDetails++;
                }
            }

            if (brokenDetails == 0)
                BreakDetails();
        }
    }

    class DetailStorage
    {
        private int _fullVolume = 500;
        private int _occupiedVolume = 0;
        private Random _random = new Random();
        private List<Detail> _detailsInStock = new List<Detail>();
        private Detail[] _details = { new Detail("Колесо", 100, 30), new Detail("Двигатель", 3000, 50), new Detail("Фара", 350, 20),
                              new Detail("Руль", 600, 25), new Detail("Вентилятор", 450, 15) };

        public DetailStorage()
        {
            int minDetailVolume = FindDetailWithMinVolume().Volume;
            bool isFreePlace = true;
            Detail detail;

            while (isFreePlace)
            {
                detail = _details[_random.Next(_details.Length)];

                if (_occupiedVolume + detail.Volume < _fullVolume)
                {
                    _detailsInStock.Add(detail);
                    _occupiedVolume += detail.Volume;
                }

                if (_occupiedVolume + minDetailVolume > _fullVolume)
                    isFreePlace = false;
            }

            _detailsInStock.Sort(Compare);
        }

        public void ShowDetails()
        {
            int detailNumber = 1;

            Console.WriteLine($"Место на складе: {_occupiedVolume}/{_fullVolume} м3.\n");

            foreach (var detail in _detailsInStock)
            {
                Console.Write($"{detailNumber} ");
                detail.ShowInformation();
                detailNumber++;
            }

            Console.ReadKey();
            Console.Clear();
        }

        public string[] GetPartNames()
        {
            List<string> detailNames = new List<string>();
            string detailName = "";

            if (_detailsInStock.Count == 0)
                return null;

            _detailsInStock.Sort(Compare);

            foreach (var detail in _detailsInStock)
            {
                if (detailName != detail.Name)
                {
                    detailName = detail.Name;
                    detailNames.Add(detailName);
                }
            }

            return detailNames.ToArray();
        }

        public Detail GetDetail(string detailName)
        {
            Detail detail;

            for (int i = 0; i < _detailsInStock.Count; i++)
            {
                if (detailName == _detailsInStock[i].Name)
                {
                    detail = _detailsInStock[i];
                    _detailsInStock.Remove(detail);
                    _occupiedVolume -= detail.Volume;
                    return detail;
                }
            }

            return null;
        }

        private int Compare(Detail detail1, Detail detail2)
        {
            string detailName1 = detail1.Name;
            string detailName2 = detail2.Name;

            return String.Compare(detailName1, detailName2);
        }

        private Detail FindDetailWithMinVolume()
        {
            Detail findedDetail = _details[0];

            foreach (var detail in _details)
            {
                if (detail.Volume < findedDetail.Volume)
                {
                    findedDetail = detail;
                }
            }

            return findedDetail;
        }
    }

    class Detail
    {
        public Detail(string name, int price, int volume)
        {
            Name = name;
            Price = price;
            Volume = volume;
            IsWorken = true;
        }

        public string Name { get; private set; }
        public int Volume { get; private set; }
        public int Price { get; private set; }
        public bool IsWorken { get; private set; }

        public void Break()
        {
            IsWorken = false;
        }

        public void ShowInformation()
        {
            Console.WriteLine($"{Name}: стоимость {Price} рублей, занимаемый объем {Volume} м3.");
        }
    }
}
