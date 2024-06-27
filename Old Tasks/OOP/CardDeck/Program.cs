using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
    class Program
    {
        static void Main(string[] args)
        {
            const string TakeCommand = "take";
            const string TakeSomeCommand = "takesome";
            const string StopCommand = "stop";

            bool isStop = false;
            string command;
            Deck deck = new Deck();
            Player player = new Player();

            deck.Fill();
            Console.WriteLine($"Введите:\n{TakeCommand} - чтобы взять карту\n{TakeSomeCommand} - чтобы взять несколько карт\n{StopCommand} - чтобы вскрыть руку\n");

            while (isStop == false)
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case TakeCommand:
                        player.TakeCard(deck);
                        break;

                    case TakeSomeCommand:
                        player.TakeSomeCard(deck);
                        break;

                    case StopCommand:
                        isStop = true;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }
            }

            player.ShowHand();
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();
        private Random _random = new Random();

        public void TakeCard(Deck deck)
        {
            if (deck.CheckSize() > 0)
            {
                _cards.Add(deck.IssueCard(_random.Next(0, deck.CheckSize())));
                Console.WriteLine("\nВы берете карту из колоды.\n");
            }
            else
            {
                Console.WriteLine("В колоде недостаточно карт!");
            }
        }

        public void TakeSomeCard(Deck deck)
        {
            Console.Write("\nСколько карт вы хотите взять: ");
            
            if (uint.TryParse(Console.ReadLine(), out uint cards))
            {
                if (cards <= deck.CheckSize())
                {
                    for (int i = 0; i < cards; i++)
                    {
                        TakeCard(deck);
                    }
                }
                else
                {
                    Console.WriteLine("В колоде недостаточно карт!");
                }
            }
            else
            {
                Console.WriteLine("Неподящие данные!");
            }    
        }

        public void ShowHand()
        {
            foreach (var card in _cards)
            {
                card.ShowFace();
            }
        }
    }

    class Deck
    {        
        private int _maxCardNumber = 10;
        private int _minCardNumber = 6;                
        private string[] _suits = { "Червей","Бубей","Крестов","Пик" };
        private List<Card> _cards = new List<Card>();

        public int CheckSize()
        {
            return _cards.Count();
        }

        public void Fill()
        {
            foreach (var suit in _suits)
            {
                for (int i = _minCardNumber; i <= _maxCardNumber; i++)
                {
                    _cards.Add(new Card(i, suit));  
                }
            }
        }

        public Card IssueCard(int index)
        {
            Card card = _cards[index];
            _cards.RemoveAt(index);
            return card;
        }

        public void Show()
        {
            foreach (var card in _cards)
            {
                card.ShowFace();
            }
        }
    }

    class Card
    {
        public int Number { get; private set; }
        public string Suit { get; private set; }
        
        public Card(int number, string suit)
        {
            Number = number;
            Suit = suit;
        }

        public void ShowFace()
        {
            Console.WriteLine($"{Number} - {Suit}");
        }
    }
}
