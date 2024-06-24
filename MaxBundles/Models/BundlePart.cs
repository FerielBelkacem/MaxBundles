using System.ComponentModel.DataAnnotations;

namespace MaxBundles.Models
{
    public class BundlePart
    {
        [Key]
        public int BundlePartId { get; set; }
        public int BundleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Bundle Bundle { get; set; }
        public Product Product { get; set; }
    }
}
