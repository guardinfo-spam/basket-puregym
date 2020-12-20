using System;
using System.Collections.Generic;

namespace BasketLib
{
    public class Basket
    {
        public List<ProductItem> ProductItems { get; set; }
        
        public List<GiftVoucher> GiftVouchers { get; set; }

        public OfferVoucher OfferVoucher { get; set; }

        public Basket()
        {
            //make sure the lists are initialised ad not null
            this.ProductItems = new List<ProductItem>();
            this.GiftVouchers = new List<GiftVoucher>();
        }
        
        public decimal CalculateValue()
        {
            if (this.ProductItems.Count == 0)
                return 0;

            decimal totalValue = 0;
            
            //naive first implementation . calculate the value of all products in the basket;
            foreach ( var product in this.ProductItems)
            {
                totalValue += product.Product.Cost * product.Quantity;
            }

            return totalValue;
        }

    }
}
