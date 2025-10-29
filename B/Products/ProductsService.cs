namespace B.Products
{
    public class ProductsService: IProduct
    {
        public IEnumerable<Product> GetAll()
        {
            return new List<Product>
            {
                new Product{Id = 1, Name = "Test", Category = "categoria1"},
                new Product{Id = 2, Name = "Test", Category = "categoria2"},
            };
        }
    }
}
