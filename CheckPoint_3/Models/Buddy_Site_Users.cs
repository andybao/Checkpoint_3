using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CheckPoint_3.Models
{
    [MetadataType(typeof(Site_User_Metadata))]
    public partial class Site_Users
    {
        class Site_User_Metadata
        {
            [Display(Name = "First Name")]
            public string First_Name { get; set; }
            [Display(Name = "Last Name")]
            public string Last_Name { get; set; }
        }
    }
}