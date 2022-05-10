using GildedRose.Console.Models;

namespace GildedRose.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            Store _store = new Store();

            _store.UpdateQuality();

            System.Console.ReadKey();

        }

    }

}
