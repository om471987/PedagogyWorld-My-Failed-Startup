using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class District
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public virtual int Id { get; set; }

      [Required]
      public virtual string Name { get; set; }

      [Required]
      public virtual int StateId { get; set; }

      public virtual ICollection<School> Schools { get; set; }
   }
}
