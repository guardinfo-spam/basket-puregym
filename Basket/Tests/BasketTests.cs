using BasketLib;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EmptyBasket_TotalValue_Zero()
        {
            var basket = new Basket();
            Assert.AreEqual(basket.RecalculateBasketTotalValue(), 0);
        }

        [Test]
        public void Basket1_Test()
        {
            var basket = new Basket();

            var jumperProduct = new ProductItem { Product = new Product { Name = "Jumper", Cost = 54.65M }, Quantity = 1 };
            var headLightProduct = new ProductItem { Product = new Product { Name = "Head Light", Cost = 3.5M }, Quantity = 1 };

            basket.ProductItems.Add(jumperProduct);
            basket.ProductItems.Add(headLightProduct);

            Assert.AreEqual(basket.RecalculateBasketTotalValue(), 58.15M);
        }

        
        [Test]
        public void Basket2_Test()
        {
            var basket = new Basket();

            var jumperProduct = new ProductItem { Product = new Product { Name = "Jumper", Cost = 54.65M }, Quantity = 1 };
            var glovesProduct = new ProductItem { Product = new Product { Name = "Gloves", Cost = 10.50M }, Quantity = 1 };

            var giftVoucher = new GiftVoucher { Value = 5M };

            basket.ProductItems.Add(jumperProduct);
            basket.ProductItems.Add(glovesProduct);

            basket.GiftVouchers.Add(giftVoucher);
            Assert.AreEqual(basket.RecalculateBasketTotalValue(), 60.15M);
        }

        [Test]
        public void Basket3_Test()
        {
            var basket = new Basket();

            var jumperProduct = new ProductItem { Product = new Product { Name = "Jumper", Cost = 26.00M }, Quantity = 1 };
            var glovesProduct = new ProductItem { Product = new Product { Name = "Gloves", Cost = 25.00M }, Quantity = 1 };            

            basket.ProductItems.Add(jumperProduct);
            basket.ProductItems.Add(glovesProduct);

            basket.OfferVoucher = new OfferVoucher { Category = BasketEnums.ProductCategory.HeadGear, Value = 5M, Code = "YYY-YYY" };
            Assert.AreEqual(basket.RecalculateBasketTotalValue(), 51.00M);
        }

        [Test]
        public void Basket4_Test()
        {
            var basket = new Basket();

            var jumperProduct = new ProductItem { Product = new Product { Name = "Jumper", Cost = 26.00M }, Quantity = 1 };
            var glovesProduct = new ProductItem { Product = new Product { Name = "Gloves", Cost = 25.00M }, Quantity = 1 };
            var headlightProduct = new ProductItem { Product = new Product { Name = "Head Light", Category = BasketEnums.ProductCategory.HeadGear, Cost = 3.50M }, Quantity = 1 };

            basket.ProductItems.Add(jumperProduct);
            basket.ProductItems.Add(glovesProduct);
            basket.ProductItems.Add(headlightProduct);

            basket.OfferVoucher = new OfferVoucher { Category = BasketEnums.ProductCategory.HeadGear, Value = 5M, Code = "YYY-YYY" };
            Assert.AreEqual(basket.RecalculateBasketTotalValue(), 51.00M);
        }
    }
}