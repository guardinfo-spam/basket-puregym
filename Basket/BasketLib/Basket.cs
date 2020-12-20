using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketLib
{
    public class Basket
    {
        //this would normally be a project setting and maybe passed into the basket as an input parameter so we easily change it if needed.
        public const decimal GIFT_VOUCHER_THRESHHOLD = 50M;
        
        public List<ProductItem> ProductItems { get; }
        
        public List<GiftVoucher> GiftVouchers { get; }

        public OfferVoucher OfferVoucher { get; set; }

        bool _giftVoucherApplied = false;        

        public Basket()
        {
            //make sure the lists are initialised and not null
            this.ProductItems = new List<ProductItem>();
            this.GiftVouchers = new List<GiftVoucher>();
        }
                
        public void ApplyGiftVoucher(GiftVoucher giftVoucher)
        {
            this.GiftVouchers.Add(giftVoucher);                        
        }        

        public void AddProductItem(ProductItem productItem)
        {
            if (productItem == null) return;

            //this is normally done on an ID, but since we're not going too deep into the data representation, a name check will suffice
            var foundItem = this.ProductItems.Where(p => p.Product.Name.Equals(productItem.Product.Name)).FirstOrDefault();

            if (foundItem != default) foundItem.Quantity++;
            else this.ProductItems.Add(productItem);
        }
        
        public decimal RecalculateBasketTotalValue()
        {
            var total = this.CalculateBasketProductValues();
            total = this.ApplyGiftVouchers(total);
            total = this.ApplyOfferVouchers(total);

            return total;
        }            

        public decimal CalculateBasketProductValues()
        {
            if (this.ProductItems.Count == 0)
                return 0;

            decimal basketValue = 0;

            //naive first implementation . calculate the value of all products in the basket;
            foreach (var product in this.ProductItems)
            {
                basketValue += product.Product.Cost * product.Quantity;
            }

            return basketValue;
        }

        public bool GiftVoucherApplied() => this._giftVoucherApplied;

        public decimal CalculateTotalWithoutGiftVouchers()
        {            
            decimal totalWithoutVouchers = 0M;
            foreach (var product in this.ProductItems)
            {
                if ( !product.Product.Category.Equals(BasketEnums.ProductCategory.GiftVoucher)  )
                    totalWithoutVouchers += product.Product.Cost * product.Quantity;
            }

            return totalWithoutVouchers;
        }

        //for now, this only looks at the first gift voucher so won't apply multiples. More work required for multiple gift voucher support
        public decimal ApplyGiftVouchers(decimal currentTotal)
        {
            //no change
            if (this.GiftVouchers.Count == 0) return currentTotal;

            var totalWithoutGiftVouchers = CalculateTotalWithoutGiftVouchers();

            //check threshhold 
            if (totalWithoutGiftVouchers >= GIFT_VOUCHER_THRESHHOLD) return currentTotal - this.GiftVouchers.First().Value;            

            return currentTotal;
        }

        public decimal ApplyOfferVouchers(decimal currentTotal)
        {
            //do we have an offer voucher?
            if (this.OfferVoucher == null) return currentTotal;
            
            //are we over the threshhold ?
            if (currentTotal < GIFT_VOUCHER_THRESHHOLD) return currentTotal;

            //do we have an applicable item?

            var applicableItem = this.ProductItems.Where(p => p.Product.Category.Equals(this.OfferVoucher.Category)).FirstOrDefault();
            if (applicableItem == default) return currentTotal;

            return currentTotal - this.OfferVoucher.Value;
        }

       
    }
}
