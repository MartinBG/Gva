using System.ComponentModel;

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
