using System.ComponentModel.DataAnnotations;

namespace MaxBundles.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductType { get; set; }

        public ICollection<BundlePart> BundleParts { get; set; }
        public Inventory Inventory { get; set; } // Ajout de la relation avec Inventory
    }
}
