using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class School
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public int DistrictId { get; set; }

      public virtual ICollection<UserProfile> UserProfiles { get; set; }
   }
}
