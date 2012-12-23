using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Domain
{
    public class FileType
    {
        public FileType()
        {
            Files = new List<File>();
        }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(255), MinLength(1)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public virtual Unit Unit { get; set; }

        public virtual List<File> Files { get; set; }
    }
}
