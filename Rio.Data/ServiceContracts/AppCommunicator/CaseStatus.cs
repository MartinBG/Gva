using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Data.ServiceContracts.AppCommunicator
{
    public enum CaseStatus
    {
        //Обработва се
        //Отхвърлено
        //Разглежда се
        //Приключило
        
        Pending = 1,
        Rejected = 2,
        InProcess = 3,
        Completed = 4
    }
}
