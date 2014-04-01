using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class PreDocDO
    {
        public PreDocDO()
        {
            this.DocNumbers = 1;
            this.Correspondents = new List<int>();
        }

        public int? DocId { get; set; }
        public int? ParentDocId { get; set; }
        public int? DocFormatTypeId { get; set; }
        public int? DocCasePartTypeId { get; set; }
        public int DocDirectionId { get; set; }
        public int? DocTypeGroupId { get; set; }
        public int? DocTypeId { get; set; }
        public string DocSubject { get; set; }

        public List<int> Correspondents { get; set; }

        public bool Register { get; set; }
        public int DocNumbers { get; set; }
    }
}
