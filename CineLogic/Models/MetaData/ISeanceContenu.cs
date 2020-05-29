using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineLogic.Models
{
    public interface ISeanceContenu
    {
        string ContenuTitre { get; set; }
        int SeanceID { get; set; }
        Nullable<int> indexOrdre { get; set; }
        Nullable<bool> estPrincipal { get; }

        IContenu GetContenu();
    }
}
