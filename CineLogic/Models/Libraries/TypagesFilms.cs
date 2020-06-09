using System.Collections.Generic;

namespace CineLogic.Models.Libraries
{
    public class TypagesFilms
    {
        public static string Standard { get => ContenuTypeLibrary.CONT_TYPE_STANDARD; }
        public static string Courtmetrage { get => ContenuTypeLibrary.CONT_TYPE_COURT; }
        //public static string Promotionnel { get => ContenuTypeLibrary.CONT_TYPE_PROMO; }

        public static Dictionary<string, string> Display
        {
            get => new Dictionary<string, string>()
            {
                [Standard] = "Standard",
                [Courtmetrage] = "Courts métrage",
                //[Promotionnel] = "Promotionnel"
            };
        }
    }
}