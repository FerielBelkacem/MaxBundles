namespace MaxBundles.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ProductType ProductType { get; set; }
        public int? InventoryCount { get; set; }

        public ICollection<BundlePart> BundleParts { get; set; }
    }

    public enum ProductType
    {
        Bundle,
        Part
    }
}
