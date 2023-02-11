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
            const string CommandCreateRoute = "создать";
            const string CommandSendedTrains = "показать";
            const string CommandExit = "выйти";

            bool isExit = false;
            string command;
            ControlPanel controlPanel = new ControlPanel();

            while (isExit != true)
            {
                Console.Clear();
                Console.WriteLine($"Введите\n{CommandCreateRoute} - для создания нового маршрута\n{CommandSendedTrains} - показать ушедшие поезда\n" +
                    $"{CommandExit} - чтобы покинуть терминал\n");
                command = Console.ReadLine();

                switch (command)
                {
                    case CommandCreateRoute:
                        controlPanel.CallMenu();
                        break;

                    case CommandSendedTrains:
                        controlPanel.ShowSendedTrains();
                        break;

                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
            }            
        }
    }

    class ControlPanel
    {
        private Route _route;        
        private BillBoard _billBoard = new BillBoard();
        private List<Train> _sendedTrains = new List<Train>();

        public void CallMenu()
        {
            const string CommandSellTickets = "продать";
            const string CommandAddTrainCar = "добавить";
            const string CommandSendTrain = "отправить";
            const string CommandSeatPassengers = "рассадить";

            string command;            

            Console.Clear();            
            CreateDirection();
            _route.CreateTrain();            

            while (_route.Train.IsSend != true)
            {
                Console.Clear();
                _billBoard.ShowRouteInformation(_route);
                _billBoard.ShowTrainInformation(_route.Train);

                Console.WriteLine($"\nВведите\n{CommandSellTickets} - для продажи билетов\n{CommandAddTrainCar} - чтобы добавить вагон к поезду" +
                    $"\n{CommandSeatPassengers} - чтобы рассадить пассажиров на ищеющиеся места" +
                    $"\n{CommandSendTrain} - чтобы отправить поезд, если все пассажиры рассажены\n\n");
                command = Console.ReadLine();

                switch (command)
                {
                    case CommandSellTickets:
                        SellTickets();
                        break;

                    case CommandAddTrainCar:
                        AddTrainCar();
                        break;

                    case CommandSeatPassengers:
                        _route.SeatPassengers();
                        break;

                    case CommandSendTrain:
                        SendTrain();
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
            }
        }

        public void AddTrain(Train train)
        {
            _sendedTrains.Add(train);
        }

        public void ShowSendedTrains()
        {
            if (_sendedTrains.Count > 0)
            {                
                foreach (var sendedTrain in _sendedTrains)
                {
                    _billBoard.ShowTrainInformation(sendedTrain);                    
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нет ушедших поездов!");
                Console.ReadKey();
            }
        }

        private Route CreateDirection()
        {
            Console.WriteLine("Введите станцию отправления: ");
            string departureStation = Console.ReadLine();
            Console.WriteLine("Введите станцию прибытия: ");
            string arrivalStation = Console.ReadLine();            
            Console.Clear();
            return _route = new Route(departureStation, arrivalStation);
        }

        private void SellTickets()
        {
            if (_route.Tickets == 0 && _route.Train.Passengers == 0)
            {
                _route.SellTickets();
            }
            else
            {
                Console.WriteLine("Билеты уже проданы!");
                Console.ReadKey();
            }
        }

        private void AddTrainCar()
        {
            Console.WriteLine("Введите вместимость вагона: ");

            if (uint.TryParse(Console.ReadLine(), out uint passengers))
            {
                _route.Train.AddTrainCar(passengers);                
            }
            else
            {
                Console.WriteLine("Недопустимое значение!");
                Console.ReadKey();
            }
        }

        private void SendTrain()
        {
            if (_route.Tickets == 0)
            {
                _route.Train.Send();
                Console.WriteLine("Поезд отправлен.");
                AddTrain(_route.Train);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Недостаточно вагонов для размещения всех пассажиров! Добавьте еще вагонов перед отправлением.");
                Console.ReadKey();
            }
        }
    }

    class Route
    {        
        public string DepartureStation { get; private set; }
        public string ArrivalStation { get; private set; }
        public uint Tickets { get; private set; }
        public Train Train { get; private set; }

        public Route(string town1, string town2)
        {
            DepartureStation = town1;
            ArrivalStation = town2;
        }

        public Train CreateTrain()
        {
            Console.WriteLine("Введите название поезда: ");
            string trainName = Console.ReadLine();
            Console.Clear();
            return Train = new Train(trainName);
        }

        public void SellTickets()
        {
            int minNumberOfPassengers = 50;
            int maxNumberOfPassengers = 100;
            Random random = new Random();

            Tickets = (uint)random.Next(minNumberOfPassengers, maxNumberOfPassengers);
        }

        public void SeatPassengers()
        {
            if (Tickets <= Train.Capasity)
            {
                Train.SeatPassengers(Tickets);
                Tickets = 0;               
            }
            else
            {
                Tickets -= Train.Capasity;
                Train.SeatPassengers(Train.Capasity);                                
            }
        }
    }

    class BillBoard
    {
        public void ShowRouteInformation(Route route)
        {
            Console.WriteLine($"Маршрут {route.DepartureStation} - {route.ArrivalStation}. Нерассаженых пасажиров {route.Tickets}.");
        }

        public void ShowTrainInformation(Train train)
        {
            Console.WriteLine($"\nСкорый поезд: {train.Name}. Длина состава: {train.Length} вагонов. Рассажено пасажиров: {train.Passengers}. Остаточная вместимость поезда: {train.Capasity}\n");
            train.ShowComposition();
        }
    }

    class Train
    {
        private List<TrainCar> _trainCars = new List<TrainCar>();

        public string Name { get; private set; }
        public uint Length { get; private set; }
        public bool IsSend { get; private set; }
        public uint Passengers { get; private set; }
        public uint Capasity { get; private set; }

        public Train(string name)
        {
            Name = name;
            IsSend = false;
        }

        public void AddTrainCar(uint passengers)
        {
            Length++;
            TrainCar trainCar = new TrainCar(Length, passengers);
            _trainCars.Add(trainCar);
            Capasity += passengers;
        }

        public void SeatPassengers(uint passengers)
        {
            Capasity -= passengers;
            Passengers += passengers;
        }

        public void ShowComposition()
        {
            foreach (var trainCar in _trainCars)
            {
                trainCar.ShowInformation();
            }
        }

        public void Send()
        {
            IsSend = true;
        }                
    }

    class TrainCar
    {
        public uint Capasity { get; private set; }
        public uint Number { get; private set; }

        public TrainCar(uint number, uint passengers)
        {
            Number = number;
            Capasity = passengers;
        }

        public void ShowInformation()
        {
            Console.WriteLine($"Вагон №{Number} - вместимость {Capasity}");
        }
    }
}
