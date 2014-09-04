using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models
{
    public class GvaConstants
    {
       public static readonly int IsReadyApplication = 6;
       public static readonly int IsReceivedApplication = 7;
       public static readonly int IsDoneApplication = 8;
    }
}
