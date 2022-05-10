using GildedRose.Console.Models;
using System.Linq;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        private readonly Store _store;
        public TestAssemblyTests()
        {
            _store = new Store();
        }

        /// <summary>
        /// Quality can never by less than 0.
        /// </summary>
        [Fact]
        public void TestQualityLt0()
        {
            int minIters = _store.Items.ExcludeBrie().Min(x => x.Quality) + 1;

            for (int i = 0; i < minIters; i++)
                _store.UpdateQuality();

            Assert.Equal(0, _store.Items.ExcludeBrie().Min(x => x.Quality));
        }

        /// <summary>
        /// Quality can never be more than 50, except for Sulfuras which has quality 80 always.
        /// </summary>
        [Fact]
        public void TestMaxQuality()
        {
            // since brie increases in quality, after 50 iterations it should be at max quality
            for (int i = 0; i < 52; i++)
                _store.UpdateQuality();

            Assert.Equal(50, _store.Items.ExcludeSulfuras().Max(x => x.Quality));
        }

        /// <summary>
        /// The quality of Sulfuras never degrades.
        /// </summary>
        [Fact]
        public void TestSulfurasQualNeverDecreases()
        {
            for (int i = 0; i < 52; i++)
                _store.UpdateQuality();

            Assert.Equal(80, _store.Items.Max(x => x.Quality));
        }

        /// <summary>
        /// The quality of a non-conjured item before it's sell by decreases by 1.
        /// </summary>
        [Fact]
        public void TestQualityDecreases()
        {
            int initialQual = _store.Items.First(x => x.Name == "Elixir of the Mongoose").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual - 1, _store.Items.First(x => x.Name == "Elixir of the Mongoose").Quality);
        }

        /// <summary>
        /// The quality of conjured items decreases twice as fast.
        /// </summary>
        [Fact]
        public void TestConjuredQualityDecreasesTwiceAsFast()
        {
            int initialQual = _store.Items.First(x => x.Name.Contains("Conjured")).Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual - 2, _store.Items.First(x => x.Name.Contains("Conjured")).Quality);
        }

        /// <summary>
        /// After the sell by date the quality of items degrades twice as quickly.
        /// </summary>
        [Fact]
        public void TestAfterSellInConjuredQualityDecreasesFourTimesAsFast()
        {
            Item item;
            do
            {
                item = _store.Items.First(x => x.Name == "Conjured Mana Boots");
                _store.UpdateQuality();
            }
            while (item.SellIn > 0);
            _store.UpdateQuality();

            int initialQual = _store.Items.First(x => x.Name == "Conjured Mana Boots").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual - 4, _store.Items.First(x => x.Name == "Conjured Mana Boots").Quality);
        }

        /// <summary>
        /// After the sell by date the quality of items degrades twice as quickly.
        /// </summary>
        [Fact]
        public void TestAfterSellInQualityDecreasesTwiceAsFast()
        {
            Item item;
            do
            {
                item = _store.Items.First(x => x.Name == "Elixir of the Mongoose");

                _store.UpdateQuality();
            }
            while (item.SellIn > 0);

            int initialQual = _store.Items.First(x => x.Name == "Elixir of the Mongoose").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual - 2, _store.Items.First(x => x.Name == "Elixir of the Mongoose").Quality);
        }

        /// <summary>
        /// The quality of aged brie increases with time.
        /// </summary>
        [Fact]
        public void TestAgedBrieIncreasesInValue()
        {
            int initialQual = _store.Items.First(x => x.Name == "Aged Brie").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual + 1, _store.Items.First(x => x.Name == "Aged Brie").Quality);
        }

        [Fact]
        public void TestSulfurasNeverNeedsToBeSold()
        {
            Assert.Equal(0, _store.Items.First(x => x.Name == "Sulfuras, Hand of Ragnaros").SellIn);
        }

        [Fact]
        public void TestBackStagePassQualityIncreases()
        {
            int initialQual = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual + 1, _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality);
        }

        [Fact]
        public void TestBackStagePassQualityIncreasesMoreCloseToSellBy()
        {
            Item item;
            do
            {
                item = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert");
                _store.UpdateQuality();
            } while (item.SellIn > 9);

            int initialQual = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual + 2, _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality);
        }

        [Fact]
        public void TestBackStagePassQualityIncreasesEvenMoreVeryCloseToSellBy()
        {
            Item item;
            do
            {
                item = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert");
                _store.UpdateQuality();
            } while (item.SellIn > 4);

            int initialQual = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality;

            _store.UpdateQuality();

            Assert.Equal(initialQual + 3, _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality);
        }

        [Fact]
        public void TestBackStagePassQualityIsZeroAfterSellIn()
        {
            Item item;
            do
            {
                item = _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert");
                _store.UpdateQuality();
            } while (item.SellIn > 0);

            _store.UpdateQuality();

            Assert.Equal(0, _store.Items.First(x => x.Name == "Backstage passes to a TAFKAL80ETC concert").Quality);
        }
    }
}