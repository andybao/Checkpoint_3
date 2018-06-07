using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckPoint_3.Models
{
    public class MessageAndUser
    {
        public List<Msgs> messages { get; set; }
        public List<Site_Users> users { get; set; }
        public Msgs message { get; set; }
        public Site_Users user { get; set; }
        
    }
}