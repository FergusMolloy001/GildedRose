using GildedRose.Console.Models;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Tests
{
    public static class HelperExtensions
    {

        /// <summary>
        /// Brie's quality increases with age so can throw of results of quality tests
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Item> ExcludeBrie(this IEnumerable<Item> source)
        {
            return source.Where(x => x.Name != "Aged Brie");
        }
        /// <summary>
        /// Sulfuras quality does not decrease and it does not have to be sold ever.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Item> ExcludeSulfuras(this IEnumerable<Item> source)
        {
            return source.Where(x => x.Name != "Sulfuras, Hand of Ragnaros");
        }
    }
}
