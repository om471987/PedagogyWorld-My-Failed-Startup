using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyWorld.Domain
{
    public class School
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public virtual User User { get; set; }

    }
}
