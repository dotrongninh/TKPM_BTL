using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    [Table("tblQuyen")]
    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column("sTenquyen")]
        [StringLength(225)]
        public string Name { get; set; }
    }
} 