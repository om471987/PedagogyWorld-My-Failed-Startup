using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class UserProfile
   {
   [Key]
   [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
   public int UserId { get; set;}

   [Required]
   public string UserName { get; set; }

   public string FirstName { get; set; }

   public string LastName { get; set; }

   [Required]
   public string Email { get; set; }

   public virtual ICollection<School> Schools { get; set; }
   }
}