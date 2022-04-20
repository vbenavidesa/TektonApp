using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TektonApp.Common;

namespace TektonApp.Domain.Entities
{
    public class ProductMaster : AuditableEntity
    {
        [Key, Required]
        public int Id { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public bool Status { get; set; }

        [Column(TypeName = "decimal(18, 2)"), Required]
        public decimal Stock { get; set; }

        [StringLength(200), Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)"), Required]
        public decimal Price { get; set; }
    }
}
