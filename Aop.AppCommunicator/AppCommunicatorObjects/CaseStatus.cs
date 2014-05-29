using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aop.AppCommunicator.AppCommunicatorObjects
{
    public enum CaseStatus
    {
        //Обработва се
        Pending = 1,

        //Отхвърлено
        Rejected = 2,

        //Разглежда се
        InProcess = 3,

        //Приключило
        Completed = 4
    }
}