using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CheckPoint_3.Models
{
    [MetadataType(typeof(MsgsMetadata))]
    public partial class Msgs
    {
        class MsgsMetadata
        {
            [Display(Name = "Sender Email")]
            public int Sender_User_Id { get; set; }

            [Display(Name = "Receiver Email")]
            public int Receiver_User_Id { get; set; }
        }
    }
}