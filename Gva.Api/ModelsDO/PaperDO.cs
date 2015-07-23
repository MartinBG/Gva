using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PaperDO
    {
        public PaperDO() { }

        public PaperDO(GvaPaper paper)
        {
            this.PaperId = paper.PaperId;
            this.Name = paper.Name;
            this.FromDate = paper.FromDate;
            this.ToDate = paper.ToDate;
            this.FirstNumber = paper.FirstNumber;
            this.IsActive = paper.IsActive;
        }

        public int? PaperId { get; set; }

        public string Name { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? FirstNumber { get; set; }

        public bool IsActive { get; set; }
    }
}