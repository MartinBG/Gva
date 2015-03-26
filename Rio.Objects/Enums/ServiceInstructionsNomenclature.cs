using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ServiceInstructionsNomenclature : BaseNomenclature
    {
        public ServiceInstructionsNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                new BaseNomenclature("01", "МОСВ"),
                new BaseNomenclature("02", "ИАОС"),
                new BaseNomenclature("03", "БД Благоевград"),
                new BaseNomenclature("04", "БД Варна"),
                new BaseNomenclature("05", "БД Пловдив"),
                new BaseNomenclature("06", "БД Плевен"),
                new BaseNomenclature("07", "ДНП Пирин"),
                new BaseNomenclature("08", "ДНП Рила"),
                new BaseNomenclature("09", "ДНП Централен Балкан"),
                new BaseNomenclature("10", "РИОСВ Благоевград"),
                new BaseNomenclature("11", "РИОСВ Бургас"),
                new BaseNomenclature("12", "РИОСВ Варна"),
                new BaseNomenclature("13", "РИОСВ ВеликоТърново"),
                new BaseNomenclature("14", "РИОСВ Враца"),
                new BaseNomenclature("15", "РИОСВ Монтана"),
                new BaseNomenclature("16", "РИОСВ Пазарджик"),
                new BaseNomenclature("17", "РИОСВ Перник"),
                new BaseNomenclature("18", "РИОСВ Плевен"),
                new BaseNomenclature("19", "РИОСВ Пловдив"),
                new BaseNomenclature("20", "РИОСВ Русе"),
                new BaseNomenclature("21", "РИОСВ Смолян"),
                new BaseNomenclature("22", "РИОСВ София"),
                new BaseNomenclature("23", "РИОСВ Стара Загора"),
                new BaseNomenclature("24", "РИОСВ Хасково"),
                new BaseNomenclature("25", "РИОСВ Шумен")
            };
        }
    }
}
