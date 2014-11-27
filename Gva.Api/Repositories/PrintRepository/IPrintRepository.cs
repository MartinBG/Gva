using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;

namespace Gva.Api.Repositories.PrintRepository
{
    public interface IPrintRepository
    {
        Guid SaveStreamToBlob(Stream stream, string connectionString);

        FileStream ConvertMemoryStreamToPdfFile(MemoryStream memoryStream);
    }
}
