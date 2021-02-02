using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GreenAura.Models
{
    public class PlantType
    {
        [Key] 
        public int PlantTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string PlantTypeName { get; set; }

        public virtual ICollection<ProductInfo> Products { get; set; }
    }
}
