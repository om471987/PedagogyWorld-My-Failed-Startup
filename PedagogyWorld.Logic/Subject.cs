using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyWorld.Domain
{
    public class Subject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Subjects Name { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
