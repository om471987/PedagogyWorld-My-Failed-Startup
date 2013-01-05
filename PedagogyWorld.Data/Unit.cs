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
      public Guid Id { get; set; }

      [Required]
      public string Name { get; set; }

      public string Description { get; set; }

      [Required]
      public int GradeId { get; set; }

      [Required]
      public int SubjectId { get; set; }

      public virtual ICollection<Outcome> Outcomes { get; set; }

      public virtual ICollection<File> Files { get; set; }
   }
}
