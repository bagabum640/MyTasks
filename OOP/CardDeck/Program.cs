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
            Deck deck = new Deck();
            deck.Show();
        }
    }

    class Player
    {

    }

    class Deck
    {
        static private int _size = 20;
        private int _maxCardNumber = 10;
        private int _minCardNumber = 6;
        private Card[] _cards = new Card [_size];
        private string[] _suits = { "Червей","Бубей","Крестов","Пик" };

        public Deck()
        {
            int cardIndex = 0;

            foreach (var suit in _suits)
            {
                for (int i = _minCardNumber; i <= _maxCardNumber; i++)
                {
                    _cards[cardIndex] = new Card(i, suit);
                    cardIndex++;
                }                
            }
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
