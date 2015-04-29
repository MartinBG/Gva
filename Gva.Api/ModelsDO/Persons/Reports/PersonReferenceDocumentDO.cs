using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons.Reports
{
    public class PersonReferenceDocumentDO
    {
        public PersonReferenceDocumentDO(InventoryItemDO item)
        {
            this.Name = item.Name;
            this.Type = item.Type;
            this.Number = item.Number;
            this.FromDate = item.FromDate;
            this.ToDate = item.ToDate;
            this.Publisher = item.Publisher;
            this.Valid = item.Valid;
            this.File = item.File;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Publisher { get; set; }

        public bool? Valid { get; set; }

        public FileDataDO File { get; set; }
    }
}
