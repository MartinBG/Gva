using Common.Extensions;
using System;
using System.IO;
using System.ServiceModel;

namespace Rio.Data.Abbcdn
{
    public class AbbcdnStorage : IDisposable
    {
        private ChannelFactory<IAbbcdn> _factory;
        private IAbbcdn _client;

        public AbbcdnStorage(ChannelFactory<IAbbcdn> factory)
        {
            this._factory = factory;
            this._client = _factory.CreateChannel();
        }

        public UploadFileInfo UploadFile(byte[] content, string fileName)
        {
            try
            {
                string mimeType = null;
                if (!String.IsNullOrWhiteSpace(fileName))
                {
                    string extension = Path.GetExtension(fileName);
                    mimeType = MimeTypeHelper.GetFileMimeTypeByExtenstion(extension);
                }

                return _client.Upload(content, mimeType, fileName, true);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DownloadFileInfo DownloadFile(Guid fileKey)
        {
            try
            {
                return _client.Download(fileKey);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool FileExist(Guid fileKey)
        {
            return _client.FileExist(fileKey);
        }

        public bool SetIsInUse(Guid fileKey, bool isInUse)
        {
            try
            {
                return _client.SetIsInUse(fileKey, isInUse);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (_factory != null)
            {
                _factory.Close();
            }
        }
    }
}