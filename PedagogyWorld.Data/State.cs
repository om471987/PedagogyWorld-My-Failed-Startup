using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class State
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public virtual int Id { get; set; }

      [Required]
      public virtual string Name { get; set; }

      [Required]
      public virtual string ShortForm { get; set; }

      public virtual ICollection<District> Districts { get; set; }
   }
}
