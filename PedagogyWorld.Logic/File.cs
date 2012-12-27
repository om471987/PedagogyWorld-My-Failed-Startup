using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Domain
{
    public class File
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(255), MinLength(1)]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public FileTypes FileType { get; set; }

        [Required]
        public User User { get; set; }
    }
}
