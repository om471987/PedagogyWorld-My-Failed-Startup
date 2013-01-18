using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class School
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public virtual int Id { get; set; }

      [Required]
      public virtual string Name { get; set; }

      [Required]
      public virtual int DistrictId { get; set; }

      public virtual ICollection<UserProfile> UserProfiles { get; set; }
   }
}
