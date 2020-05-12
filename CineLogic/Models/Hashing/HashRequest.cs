using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Models.Hashing
{
    public class HashRequest
    {
        public string StringToHash { get; set; }
        public int? Iterations { get; set; }
    }
}