using System;

namespace CineLogic.Business
{
    public class DBException : Exception
    {
        public DBException(Exception innerException) : base("La base de données a rencontré une erreur.", innerException)
        {
        }
    }
}