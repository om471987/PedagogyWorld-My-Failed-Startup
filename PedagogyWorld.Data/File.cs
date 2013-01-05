using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class File
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public Guid Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public string Path { get; set; }

      public virtual ICollection<FileType> FileTypes { get; set; }

      public virtual ICollection<Unit> Units { get; set; }

      public virtual ICollection<TeachingDate> TeachingDates { get; set; }
   }
}
