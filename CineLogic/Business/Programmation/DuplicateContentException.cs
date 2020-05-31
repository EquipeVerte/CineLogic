using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class DuplicateContentException : Exception
    {
        public DuplicateContentException(string contenuTitre) : base($"Il y a déjà un contenu avec le titre {contenuTitre} assigné à cette séance.")
        {
        }
    }

}