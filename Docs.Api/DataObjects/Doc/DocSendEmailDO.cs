using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocSendEmailDO
    {
        public DocSendEmailDO()
        {
        }

        public string EmailTo { get; set; }
        public string EmailToName { get; set; }
        public int? CorrespondentContactId { get; set; }
        public int? CorrespondentId { get; set; }
        public int? UserId { get; set; }
        public int? TypeId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Param5 { get; set; }
    }
}
