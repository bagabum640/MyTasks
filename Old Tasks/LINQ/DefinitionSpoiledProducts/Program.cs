using System;
using System.Collections.Generic;
using System.Linq;

namespace DefinitionSpoiledProducts
{
    class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            storage.Work();
        }
    }

    class Storage
    {
        private List<CanStew> _cansStew = new List<CanStew>();

        public Storage()
        {
            _cansStew.Add(new CanStew("Вкусный кот", 1985, 10));
            _cansStew.Add(new CanStew("Вкусный кот", 1995, 10));
            _cansStew.Add(new CanStew("Вкусный кот", 2020, 10));
            _cansStew.Add(new CanStew("Вкусный кот", 2015, 10));
            _cansStew.Add(new CanStew("Дохлый крыс", 1985, 12));
            _cansStew.Add(new CanStew("Дохлый крыс", 2015, 12));
            _cansStew.Add(new CanStew("Дохлый крыс", 2017, 12));
            _cansStew.Add(new CanStew("Дохлый крыс", 2010, 12));
            _cansStew.Add(new CanStew("Вкусный кот", 2015, 10));
            _cansStew.Add(new CanStew("Вкусный кот", 2014, 10));
            _cansStew.Add(new CanStew("Вкусный кот", 2019, 10));
        }

        public void Work()
        {
            int thisYear = 2023;

            Console.WriteLine("Тушенка на складе:\n");
            ShowProducts(_cansStew);
            StopShow();
            Console.WriteLine($"Испорченная тушенка на {thisYear} год:\n");
            ShowProducts(_cansStew.Where(canStew => canStew.ManufactureYear+canStew.ExpirationDate < thisYear).ToList());
            StopShow();
        }

        private void ShowProducts(List<CanStew> cansStew)
        {
            foreach (var canStew in cansStew)
            {
                canStew.ShowInfo();
            }
        }

        private void StopShow()
        {
            Console.ReadKey();
            Console.Clear();
        }
    }

    class CanStew
    {
        public CanStew(string name, int manufactureYear, int expirationDate)
        {
            Name = name;
            ManufactureYear = manufactureYear;
            ExpirationDate = expirationDate;
        }

        public string Name { get; private set; }
        public int ManufactureYear { get; private set; }
        public int ExpirationDate { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Name}. Год производства: {ManufactureYear}. Срок годности: {ExpirationDate}.");
        }
    }
}
