using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyWorld.Domain
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(255)]
        public string FirstName { get; set; }

        [MinLength(1), MaxLength(255)]
        public string LastName { get; set; }

        [Required, MaxLength(256), MinLength(3), RegularExpression(@"^([\w.-]+)@([\w-]+)((.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        public States State { get; set; }
        
        [Required, MaxLength(255), MinLength(1)]
        public string District { get; set; }

        public virtual List<School> Schools { get; set; }

        public virtual List<Subject> Subjects { get; set; }

        public virtual List<Grade> Grades { get; set; }
    }
}
