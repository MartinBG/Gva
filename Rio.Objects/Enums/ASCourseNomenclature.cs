using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASCourseNomenclature
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public static readonly ASCourseNomenclature RT = new ASCourseNomenclature { Name = "RT", Code = "01" };

        public static readonly IEnumerable<ASCourseNomenclature> Values =
            new List<ASCourseNomenclature>
            {
                RT,
            };
    }
}
