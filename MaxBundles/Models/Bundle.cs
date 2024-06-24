namespace MaxBundles.Models
{
    public class Bundle
    {
        public int BundleId { get; set; }
        public Product Product { get; set; }

        public ICollection<BundlePart> BundleParts { get; set; }
    }
}
