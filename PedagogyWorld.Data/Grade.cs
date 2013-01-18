using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class Grade
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public virtual int Id { get; set; }

      [Required]
      public virtual string Name { get; set; }

      public virtual ICollection<Unit> Units { get; set; }
   }
}
