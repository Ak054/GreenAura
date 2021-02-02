using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GreenAura.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }

        [Required]
        [StringLength(100)]
        public string StateName { get; set; }

        public virtual ICollection<Location> StateLocations { get; set; }
    }
}
