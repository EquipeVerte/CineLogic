using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Models
{
    public interface IContenu
    {
        string Titre { get; set; }
        int RuntimeMins { get; set; }
        string typage { get; set; }
    }
}
