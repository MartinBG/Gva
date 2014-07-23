using Aop.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.DataObjects
{
    public class AopEmployerDO
    {
        public AopEmployerDO()
        {
        }

        public AopEmployerDO(AopEmployer a)
            : this()
        {
            if (a != null)
            {
                this.AopEmployerId = a.AopEmployerId;
                this.Name = a.Name;
                this.LotNum = a.LotNum;
                this.Uic = a.Uic;
                this.AopEmployerTypeId = a.AopEmployerTypeId;
                this.Version = a.Version;
            }
        }

        public int? AopEmployerId { get; set; }
        public string Name { get; set; }
        public string LotNum { get; set; }
        public string Uic { get; set; }
        public int AopEmployerTypeId { get; set; }
        public byte[] Version { get; set; }
    }
}
