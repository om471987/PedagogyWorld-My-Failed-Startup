//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PedagogyWorld
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.UnitUserProfiles = new HashSet<UnitUserProfile>();
            this.SchoolUserProfiles = new HashSet<SchoolUserProfile>();
        }
    
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<UnitUserProfile> UnitUserProfiles { get; set; }
        public virtual ICollection<SchoolUserProfile> SchoolUserProfiles { get; set; }
    }
}