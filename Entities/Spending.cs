namespace Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Spending")]
    public partial class Spending
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public decimal Cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
