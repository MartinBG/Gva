using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public enum PartOperation
    {
        [Description("Добавяне")]
        Add = 1,

        [Description("Изтриване")]
        Delete,

        [Description("Обновяване")]
        Update
    }
}
