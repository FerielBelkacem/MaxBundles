using System.ComponentModel.DataAnnotations;

namespace MaxBundles.Models
{
    public class Bundle
    {
        [Key]
        public int BundleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<BundlePart> BundleParts { get; set; }
    }
}
