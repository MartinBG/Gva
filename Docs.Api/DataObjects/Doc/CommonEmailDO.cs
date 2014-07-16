using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class CorrespondentEmailDO
    {
        public CorrespondentEmailDO()
        {
            this.EmailTo = new List<NomDo>();
            this.PublicFiles = new List<DocFileDO>();
        }

        public List<NomDo> EmailTo { get; set; }
        public string EmailBcc { get; set; }
        public List<DocFileDO> PublicFiles { get; set; }
        public int? EmailTypeId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class CommonEmailDO
    {
        public CommonEmailDO()
        {
            this.EmailTo = new List<NomDo>();
            this.PublicFiles = new List<DocFileDO>();
        }

        public List<NomDo> EmailTo { get; set; }
        public string EmailBcc { get; set; }
        public List<DocFileDO> PublicFiles { get; set; }
        //public int? CorrespondentContactId { get; set; }
        //public int? CorrespondentId { get; set; }
        //public int? UserId { get; set; }
        public int? EmailTypeId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
