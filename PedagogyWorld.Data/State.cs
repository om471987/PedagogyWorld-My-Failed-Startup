using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class State
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public string ShortForm { get; set; }

      public virtual ICollection<District> Districts { get; set; }
   }
}
