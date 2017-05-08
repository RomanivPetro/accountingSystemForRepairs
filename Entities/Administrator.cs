namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Administrator")]
    public partial class Administrator
    {
        [Key]
        [StringLength(32)]
        public string Login { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
