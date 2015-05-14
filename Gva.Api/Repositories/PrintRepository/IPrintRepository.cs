using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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

        Stream ConvertWordStreamToPdfStream(Stream memoryStream);

        HttpResponseMessage ReturnResponseMessage(string url);

        int SaveNewFile(string name, Guid blobKey);

        Stream GenerateWordDocument(int lotId, string path, string templateName, int? ratingPartIndex, int? editionPartIndex);

        HttpResponseMessage GeneratePdfWithoutSave(int lotId, string path, string templateName);
    }
}
