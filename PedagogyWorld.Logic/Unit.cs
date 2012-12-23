using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedagogyWorld.Domain
{
    public class Unit
    {
        public Unit()
        {
            FileTypes = new List<FileType>();
        }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(255), MinLength(1)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public Subjects Subject { get; set; }

        public virtual List<FileType> FileTypes { get; set; }
    }
}
