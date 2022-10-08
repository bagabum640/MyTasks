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
        Random random = new Random();

        public void TakeCard(Deck deck)
        {
            _cards.Add(deck.IssueCard(random.Next(0, deck.Cards.Count)));
            Console.WriteLine("\nВы берете карту из колоды.\n");
        }

        public void TakeSomeCard(Deck deck)
        {
            Console.Write("\nСколько карт вы хотите взять: ");
            uint cards = Convert.ToUInt32(Console.ReadLine());

            if (cards <= deck.Cards.Count())
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

        public List<Card> Cards = new List<Card>();

        public void Fill()
        {
            foreach (var suit in _suits)
            {
                for (int i = _minCardNumber; i <= _maxCardNumber; i++)
                {
                    Cards.Add(new Card(i, suit));  
                }
            }
        }

        public Card IssueCard(int index)
        {
            Card card = Cards[index];
            Cards.RemoveAt(index);
            return card;
        }

        public void Show()
        {
            foreach (var card in Cards)
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
