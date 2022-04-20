using System.ComponentModel.DataAnnotations;

namespace TektonApp.Common
{
    public abstract class AuditableEntity
    {
        [StringLength(1)]
        public string State { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(40)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        [StringLength(40)]
        public string DeletedBy { get; set; }
    }
}
