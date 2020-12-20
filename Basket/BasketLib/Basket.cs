using System;
using System.Collections.Generic;

namespace BasketLib
{
    public class Basket
    {
        public List<ProductItem> ProductItems { get; set; }
        
        public List<GiftVoucher> GiftVouchers { get; set; }

        public OfferVoucher OfferVoucher { get; set; }
    }
}
