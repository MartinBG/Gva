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
       public static readonly int MinAircraftSModeCodeValue = 4530177;
       public static readonly int MaxAircraftSModeCodeValue = 4546559;
       public static readonly int MinMilitarySModeCodeValue = 4553760;
       public static readonly int MaxMilitarySModeCodeValue = 4554751;
       public static readonly int MinSquitterSModeCodeValue = 4546561;
       public static readonly int MaxSquitterSModeCodeValue = 4550655;
       public static readonly string ConcatenatingExp = "$$";
    }
}
