//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckPoint_3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Site_Users
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Passward { get; set; }
        public int User_Role_Code { get; set; }
        public Nullable<int> New_Msg_Id { get; set; }
    
        public virtual Site_Roles Site_Roles { get; set; }
    }
}
