using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aop.AppCommunicator.AppCommunicatorObjects
{
    public enum DocumentRegistrationStatus
    {
        /// <summary>
        /// В процес на обработка.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Регистриран.
        /// </summary>
        Registered = 2,

        /// <summary>
        /// Отказан.
        /// </summary>
        NotRegistered = 3
    }
}