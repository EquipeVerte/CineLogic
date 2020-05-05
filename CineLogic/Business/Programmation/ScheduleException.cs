using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineLogic.Business.Programmation
{
    public class ScheduleException : Exception
    {
        public ScheduleException(string message) : base(message)
        {
        }
    }
}