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
            const string CreateRouteCommand = "создать";
            const string ExitCommand = "выйти";

            bool isExit = false;
            string command;
            ControlPanel controlPanel = new ControlPanel();

            while (isExit != true)
            {
                Console.Clear();
                Console.WriteLine($"Введите\n{CreateRouteCommand} - для создания нового маршрута\n{ExitCommand} - чтобы покинуть терминал\n");
                command = Console.ReadLine();

                if (command == CreateRouteCommand)
                {
                    controlPanel.Menu();                    
                }
                else if (command == ExitCommand)
                {
                    isExit = true;
                }
                else
                {
                    Console.WriteLine("Неверная команда!");
                }
            }            
        }
    }

    class ControlPanel
    {
        private Route _route;
        private Train _train;
        private BillBoard _billBoard = new BillBoard();

        public void Menu()
        {
            const string SellTicketsCommand = "продать";
            const string AddTrainCarCommand = "добавить";
            const string SendTrainCommand = "отправить";

            string command;
            bool isTrainSend = false;

            Console.Clear();            
            CreateDirection();
            CreateTrain();            

            while (isTrainSend != true)
            {
                Console.Clear();
                _billBoard.ShowRouteInformation(_route);
                _billBoard.ShowTrainInformation(_train);

                Console.WriteLine($"\nВведите\n{SellTicketsCommand} - для продажи билетов\n{AddTrainCarCommand} - чтобы добавить вагон к поезду" +
                    $"\n{SendTrainCommand} - чтобы отправить поезд, если все пассажиры рассажены\n\n");
                command = Console.ReadLine();

                switch (command)
                {
                    case SellTicketsCommand:
                        SellTickets();
                        break;

                    case AddTrainCarCommand:
                        AddTrainCar();
                        break;

                    case SendTrainCommand:
                        SendTrain(ref isTrainSend);
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
            }
        }

        private void CreateDirection()
        {
            Console.WriteLine("Введите станцию отправления: ");
            string departureStation = Console.ReadLine();
            Console.WriteLine("Введите станцию прибытия: ");
            string ArrivalStation = Console.ReadLine();
            _route = new Route(departureStation, ArrivalStation);
            Console.Clear();            
        }

        private void CreateTrain()
        {
            Console.WriteLine("Введите название поезда: ");
            string trainName = Console.ReadLine();            
            _train = new Train(trainName);
            Console.Clear();            
        }   

        private void SellTickets()
        {
            if (_route.Passengers == 0)
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
                _train.AddTrainCar(passengers);
            }
            else
            {
                Console.WriteLine("Недопустимое значение!");
                Console.ReadKey();
            }
        }

        private void SendTrain(ref bool isTrainSend)
        {
            if (_route.Passengers <= _train.ShowCapasity())
            {
                isTrainSend = true;
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
        public uint Passengers { get; private set; }

        public Route(string town1, string town2)
        {
            DepartureStation = town1;
            ArrivalStation = town2;
        }

        public void SellTickets()
        {
            int minNumberOfPassengers = 50;
            int maxNumberOfPassengers = 100;
            Random random = new Random();
            Passengers = (uint)random.Next(minNumberOfPassengers, maxNumberOfPassengers);
        }
    }

    class BillBoard
    {
        public void ShowRouteInformation(Route route)
        {
            Console.WriteLine($"Маршрут {route.DepartureStation} - {route.ArrivalStation}. Продано билетов {route.Passengers}.");
        }

        public void ShowTrainInformation(Train train)
        {
            Console.WriteLine($"\nСкорый поезд: {train.Name}. Длина состава: {train.Length} вагонов. Суммарная вместимость поезда: {train.ShowCapasity()}\n");
            train.ShowComposition();
        }
    }
    
    class Train
    {
        private List<TrainCar> _trainCars = new List<TrainCar>();

        public string Name { get; private set; }
        public uint Length { get; private set; }        

        public Train(string name)
        {
            Name = name;
        }

        public void AddTrainCar(uint passengers)
        {
            Length++;
            TrainCar trainCar = new TrainCar(Length, passengers);
            _trainCars.Add(trainCar);
        }

        public void ShowComposition()
        {
            foreach (var trainCar in _trainCars)
            {
                trainCar.ShowInformation();
            }
        }

        public uint ShowCapasity()
        {
            uint capasity = 0;

            foreach (var trainCar in _trainCars)
            {
                capasity += trainCar.Capasity;
            }

            return capasity;
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
            Console.WriteLine($"Вагон №{Number} - количество пассажиров {Capasity}");
        }
    }
}
