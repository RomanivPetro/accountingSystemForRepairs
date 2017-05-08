namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Worker = new HashSet<Worker>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(13)]
        public string PhoneNumber { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Device { get; set; }

        [Required]
        [StringLength(500)]
        public string Problem { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReceptionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GivingDate { get; set; }

        public decimal Cost { get; set; }

        public decimal Income { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Worker> Worker { get; set; }
    }
}
