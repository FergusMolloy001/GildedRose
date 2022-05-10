using System.Collections.Generic;

namespace GildedRose.Console.Models
{
    public class Store
    {
        public List<Item> Items { get; private set; }
        public Store()
        {
            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6},
                new Item {Name = "Conjured Mana Boots", SellIn = 1, Quality = 20}
            };
        }

        private void UpdateQuality(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    item.Quality++;
                    break;

                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.SellIn == 0)
                        item.Quality = 0;
                    else if (item.SellIn < 6)
                        item.Quality += 3;
                    else if (item.SellIn < 11)
                        item.Quality += 2;
                    else
                        item.Quality++;
                    break;

                case "Sulfuras, Hand of Ragnaros":
                    return; // return to avoid check at bottom

                default:
                    if (item.SellIn <= 0)
                        item.Quality--;

                    item.Quality--;
                    break;
            }

            if (item.Name.Contains("Conjured"))
            {
                if (item.SellIn == 0)
                    item.Quality--;
                item.Quality--;
            }

            if (item.Quality > 50)
                item.Quality = 50;
            if (item.Quality < 0)
                item.Quality = 0;
        }

        private void UpdateSellIn(Item item)
        {
            if (item.SellIn == 0)
                return;

            item.SellIn--;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateQuality(item);
                UpdateSellIn(item);
            }
        }
    }
}
