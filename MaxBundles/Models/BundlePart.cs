namespace MaxBundles.Models
{
    public class BundlePart
    {
        public int BundlePartId { get; set; }
        public int BundleId { get; set; }
        public Bundle Bundle { get; set; }

        public int PartId { get; set; }
        public Product Part { get; set; }

        public int Quantity { get; set; }
    }
}
