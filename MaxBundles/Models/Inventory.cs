using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaxBundles.Models
{
    public class Inventory
    {
        [Key]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
