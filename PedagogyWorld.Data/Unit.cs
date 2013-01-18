using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class Unit
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public virtual Guid Id { get; set; }

      [Required]
      public virtual string Name { get; set; }

      public virtual string Description { get; set; }

      [Required]
      public virtual int GradeId { get; set; }

      [Required]
      public virtual int SubjectId { get; set; }

      public virtual ICollection<Outcome> Outcomes { get; set; }

      public virtual ICollection<File> Files { get; set; }

      public virtual ICollection<Standard> Standards { get; set; }
   }
}
