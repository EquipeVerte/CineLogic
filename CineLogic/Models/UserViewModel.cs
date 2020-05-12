using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models
{
    public class UserViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NomComplet { get; set; }
        public string Type { get; set; }
    }
}