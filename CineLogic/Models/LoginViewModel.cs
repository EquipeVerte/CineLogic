using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CineLogic.Models
{
    public class LoginViewModel
    {

        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    //public User Validate()
    //{
    //    using (CineDBEntities db = new CineDBEntities())
    //    {

    //        return null;
    //    }

    //}
}