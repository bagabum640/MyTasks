using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains
{
    class Program
    {
        static void Main(string[] args)
        {
            ControlPanel controlPanel = new ControlPanel();

            controlPanel.CallMenu();
        }
    }

    class ControlPanel
    {
        private Route _route;
        private List<Route> _complitedRoutes = new List<Route>();

        public void CallMenu()
        {
            const string CommandCreateRoute = "Создать маршрут";
            const string CommandShowSendedTrains = "Показать отправленные поезда";
            const string CommandSellTickets = "Продать билеты по заявленному маршруту";
            const string CommandCreateTrain = "Создать поезд";
            const string CommandAddTrainCar = "Добавить вагон";
            const string CommandSeatPessengers = "Рассадить пассажиров";
            const string CommandSendTrain = "Отправить поезд";
            const string CommandDeleteRoute = "Отменить отправку поезда";
            const string CommandExit = "выйти";

            string[] commands = { CommandCreateRoute, CommandShowSendedTrains, CommandSellTickets,
                                  CommandCreateTrain, CommandAddTrainCar, CommandSeatPessengers, CommandSendTrain, CommandDeleteRoute, CommandExit };
            bool isExit = false;
            string command;

            while (isExit != true)
            {
                Console.Clear();

                if (_route != null) _route.ShowInformation();

                command = ChooseCommand(commands);

                switch (command)
                {
                    case CommandCreateRoute:
                        CreateRoute();
                        break;

                    case CommandShowSendedTrains:
                        ShowComplitedRoutes();
                        break;

                    case CommandSellTickets:
                        if (IsRouteNotNull()) _route.SellTickets();

                        break;

                    case CommandCreateTrain:
                        if (IsRouteNotNull()) _route.CreateTrain();
                        break;

                    case CommandAddTrainCar:
                        if (IsRouteNotNull()) AddTrainCar();
                        break;

                    case CommandSeatPessengers:
                        if (IsRouteNotNull()) _route.SeatPassengers();
                        break;

                    case CommandSendTrain:
                        SendTrain();
                        break;

                    case CommandDeleteRoute:
                        _route = null;
                        break;

                    case CommandExit:
                        isExit = true;
                        break;
                }
            }
        }

        private void ShowComplitedRoutes()
        {
            if (_complitedRoutes.Count > 0)
            {
                foreach (var complitedRoute in _complitedRoutes)
                {
                    complitedRoute.ShowInformation();
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нет ушедших поездов!");
                Console.ReadKey();
            }
        }

        private Route CreateRoute()
        {
            Console.Clear();
            Console.WriteLine("Введите станцию отправления: ");
            string departureStation = Console.ReadLine();
            Console.WriteLine("Введите станцию прибытия: ");
            string arrivalStation = Console.ReadLine();
            return _route = new Route(departureStation, arrivalStation);
        }

        private void SendTrain()
        {
            if (IsRouteNotNull())
            {
                if (_route.TrySendTrain())
                {
                    _complitedRoutes.Add(_route);
                    _route = null;
                }
            }
        }

        private void AddTrainCar()
        {
            const string CommandAddSmallTrainCar = "Добавить маленький вагон";
            const string CommandAddMediumTrainCar = "Добавить средний вагон";
            const string CommandAddLadgeTrainCar = "Добавить большой вагон";

            string[] commands = { CommandAddSmallTrainCar, CommandAddMediumTrainCar, CommandAddLadgeTrainCar};

            string command = ChooseCommand(commands);

            switch (command)
            {
                case CommandAddSmallTrainCar:
                    _route.AddTrainCar(10);
                    break;

                case CommandAddMediumTrainCar:
                    _route.AddTrainCar(20);
                    break;

                case CommandAddLadgeTrainCar:
                    _route.AddTrainCar(30);
                    break;
            }
        }

        private string ChooseCommand(string[] commands)
        {
            bool isLeaveMenu = false;
            int numberString = 0;
            int CursorPositionX = Console.CursorLeft;
            int CursorPositionY = Console.CursorTop;

            while (!isLeaveMenu)
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
                        isLeaveMenu = true;
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

                Console.WriteLine();
            }

            Console.SetCursorPosition(CursorPositionX, CursorPositionY);
        }

        private bool IsRouteNotNull()
        {
            if (_route == null)
            {
                Console.WriteLine("Для начала создайте маршрут!");
                Console.ReadKey();
                return false;
            }

            return true;
        }
    }

    class Route
    {
        private int _tickets = 0;
        private string _departureStation;
        private string _arrivalStation;
        private Train _train;

        public Route(string town1, string town2)
        {
            _departureStation = town1;
            _arrivalStation = town2;
        }

        public Train CreateTrain()
        {
            Console.Clear();
            Console.WriteLine("Введите название поезда: ");
            string trainName = Console.ReadLine();
            _train = new Train(trainName);
            _train.TotalQuntityPassengers = _tickets;
            return _train;
        }

        public void SellTickets()
        {
            if (_tickets == 0)
            {
                int minQuntityOfPassengers = 50;
                int maxQuantityOfPassengers = 100;
                Random random = new Random();

                _tickets = random.Next(minQuntityOfPassengers, maxQuantityOfPassengers);

                if (_train != null) 
                    _train.TotalQuntityPassengers = _tickets;
            }
            else
            {
                Console.WriteLine("Билеты уже проданы!");
            }
        }

        public void AddTrainCar(int capasity)
        {
            if (IsTrainNotNull())
                _train.AddTrainCar(capasity);
        }

        public void SeatPassengers()
        {
            if (IsTrainNotNull())
                _train.SeatPassengers();
        }

        public bool TrySendTrain()
        {
            if (IsTrainNotNull())
            {
                if (_train.TrySend())
                    return true;
            }

            return false;
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Маршрут: {_departureStation} - {_arrivalStation}. Всего продано билетов: {_tickets}.\n");

            if (_train != null) _train.ShowComposition();
        }

        private bool IsTrainNotNull()
        {
            if (_train == null)
            {
                Console.WriteLine("Сперва создайте поезд!");
                Console.ReadKey();
                return false;
            }

            return true;
        }
    }

    class Train
    {
        private int _seatedPassengers;
        private int _capasity;
        private string _name;

        private List<TrainCar> _trainCars = new List<TrainCar>();

        public Train(string name)
        {
            _name = name;
        }
        
        public int TotalQuntityPassengers { get; set; }

        public void AddTrainCar(int capasity)
        {
            TrainCar trainCar = new TrainCar(_trainCars.Count() + 1, capasity);
            _trainCars.Add(trainCar);
            _capasity += capasity;
        }

        public void SeatPassengers()
        {
            if (_capasity > TotalQuntityPassengers)
            {
                _seatedPassengers += TotalQuntityPassengers;
                _capasity -= TotalQuntityPassengers;
                TotalQuntityPassengers = 0;
            }
            else
            {
                _seatedPassengers += _capasity;
                TotalQuntityPassengers -= _capasity;
                _capasity = 0;
            }
        }

        public void ShowComposition()
        {
            Console.WriteLine($"Поезд {_name}. Общая вместимость: {_capasity + _seatedPassengers} человек. " +
                $"Содержит: {_seatedPassengers} пассажиров. Свободных мест: {_capasity}.\n");

            foreach (var trainCar in _trainCars)
            {
                trainCar.ShowInformation();
            }

            Console.WriteLine();
        }

        public bool TrySend()
        {
            if (TotalQuntityPassengers > _capasity)
            {
                Console.Clear();
                Console.WriteLine("Недостаточно вагонов для размещения всех пассажиров!");
                Console.ReadKey();
                return false;                
            }
            else if (TotalQuntityPassengers > 0)
            {
                Console.Clear();
                Console.WriteLine("Не все пассажиры рассажены!");
                Console.ReadKey();
                return false;
            }
            else if (TotalQuntityPassengers == 0 && _seatedPassengers == 0)
            {
                Console.Clear();
                Console.WriteLine("Поезд пуст! Продайте билеты и рассадите пассажиров!");
                Console.ReadKey();
                return false;
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("Поезд ушел.");
                Console.ReadKey();
                return true;
            }
        }
    }

    class TrainCar
    {
        private int _number;
        private int _capasity;
        public TrainCar(int number, int capasity)
        {
            _number = number;
            _capasity = capasity;
        }   

        public void ShowInformation()
        {
            Console.WriteLine($"Вагон № {_number} - вместимость {_capasity} человек.");
        }
    }
}
