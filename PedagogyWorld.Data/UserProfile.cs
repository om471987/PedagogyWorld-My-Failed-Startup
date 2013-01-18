using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedagogyWorld.Data
{
   public class UserProfile
   {
   [Key]
   [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
   public virtual int UserId { get; set; }

   [Required]
   public virtual string UserName { get; set; }

   public virtual string FirstName { get; set; }

   public virtual string LastName { get; set; }

   [Required]
   public virtual string Email { get; set; }

   public virtual ICollection<School> Schools { get; set; }
   }
}