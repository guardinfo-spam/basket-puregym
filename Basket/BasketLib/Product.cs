namespace BasketLib
{
    public class Product
    {
        public string Name { get; set; }

        public BasketEnums.ProductCategory Category { get; set; }

        public decimal Cost { get; set; }

        public Product()
        {
            //make sure the type always defaults to this, unless set to something else
            this.Category = BasketEnums.ProductCategory.GenericType;
        }
    }
}