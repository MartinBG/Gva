﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Common.Blob;
using Common.Data;
using Common.Owin;
using Common.WordTemplates;
using Gva.Api.Models;
using Gva.Api.WordTemplates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SautinSoft;
using System.Linq;

namespace Gva.Api.Repositories.PrintRepository
{
    public class PrintRepository : IPrintRepository
    {
        private IUnitOfWork unitOfWork;
        private IEnumerable<IDataGenerator> dataGenerators;
        private IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator;

        public PrintRepository(
            IUnitOfWork unitOfWork,
            IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator,
            IEnumerable<IDataGenerator> dataGenerators)
        {
            this.unitOfWork = unitOfWork;
            this.dataGenerators = dataGenerators;
            this.AMLNationalRatingDataGenerator = AMLNationalRatingDataGenerator;
        }

        private static readonly object syncRoot = new object();
        private const int DEFAULT_BUFFER_SIZE = 81920;
        private static readonly byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

        public Guid SaveStreamToBlob(Stream stream, string connectionString)
        {
            Guid licenceEditionDocBlobKey;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var blobWriter = new BlobWriter(connection))
                using (var blobStream = blobWriter.OpenStream())
                {
                    stream.CopyTo(blobStream);
                    licenceEditionDocBlobKey = blobWriter.GetBlobKey();
                }
            }

            return licenceEditionDocBlobKey;
        }

        public Stream ConvertWordStreamToPdfStream(Stream stream)
        {
            var tmpDocFile = Path.GetTempFileName();
            var tmpPdfFile = Path.GetTempFileName();

            lock (syncRoot)
            {
                try
                {
                    using (var tmpFileStream = File.OpenWrite(tmpDocFile))
                    {
                        this.CopyStream(stream, tmpFileStream);
                    }

                    UseOffice useOffice = new UseOffice();
                    useOffice.Serial = ConfigurationManager.AppSettings["Gva.Api:UseOfficeSerialNumber"];

                    useOffice.InitWord();
                    useOffice.ConvertFile(tmpDocFile, tmpPdfFile, UseOffice.eDirection.DOCX_to_PDF);
                    useOffice.CloseWord();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    File.Delete(tmpDocFile);
                }

            }

            return new FileStream(tmpPdfFile,
                FileMode.Open,
                FileAccess.Read,
                FileShare.None,
                DEFAULT_BUFFER_SIZE,
                FileOptions.DeleteOnClose);
        }

        private void CopyStream(Stream input, Stream output)
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public int SaveNewFile(string name, Guid blobKey)
        {
            var newFile = new GvaFile()
            {
                Filename = name,
                FileContentId = blobKey,
                MimeType = "application/pdf"
            };

            this.unitOfWork.DbContext.Set<GvaFile>().Add(newFile);

            this.unitOfWork.Save();

            return newFile.GvaFileId;
        }

        public HttpResponseMessage ReturnResponseMessage(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Redirect);
            result.Headers.Location = new Uri(url, UriKind.Relative);
            result.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true
            };

            return result;
        }

        public HttpResponseMessage GenerateDocumentWithoutSave(int lotId, string path, string templateName, bool convertToPdf)
        {
            Stream stream;
            if (convertToPdf)
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, path, templateName, null, null))
                {
                    stream = this.ConvertWordStreamToPdfStream(wordDocStream);
                }
            }
            else
            {
                stream = this.GenerateWordDocument(lotId, path, templateName, null, null);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(string.Format("application/{0}", convertToPdf ? "pdf" : "msword"));
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("inline")
                {
                    FileName = string.Format("{0}.{1}", templateName, convertToPdf ? "pdf" : "docx")
                };

            return result;
        }

        public Stream GenerateWordDocument(int lotId, string path, string templateName, int? ratingPartIndex, int? editionPartIndex)
        {
            var wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>().SingleOrDefault(t => t.Name == templateName);
            object data = null;
            if (ratingPartIndex.HasValue && editionPartIndex.HasValue)
            {
                data = this.AMLNationalRatingDataGenerator.GetData(lotId, path, ratingPartIndex.Value, editionPartIndex.Value);
            }
            else
            {
                var dataGenerator = this.dataGenerators.FirstOrDefault(dg => dg.GeneratorCode == wordTemplate.DataGeneratorCode);
                data = dataGenerator.GetData(lotId, path);
            }

            JsonSerializer jsonSerializer = JsonSerializer.Create(App.JsonSerializerSettings);
            jsonSerializer.ContractResolver = new DefaultContractResolver();

            JObject json = JObject.FromObject(data, jsonSerializer);

            var memoryStream = new MemoryStream();
            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

            new WordTemplateTransformer(memoryStream).Transform(json);
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}