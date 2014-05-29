using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Aop.Rio.Abbcdn
{
    [ServiceContract(Namespace = "http://abbcdn.bg/2013/AbbcdnService/v1")]
    public interface IAbbcdn
    {
        [OperationContract]
        UploadFileInfo Upload(byte[] contentBytes, string fileMimeType, string fileName, bool isInUse);

        [OperationContract]
        DownloadFileInfo Download(Guid fileKey);

        [OperationContract]
        bool FileExist(Guid fileKey);

        [OperationContract]
        bool SetIsInUse(Guid fileKey, bool isInUse);
    }

    [DataContract(Name = "AbbcdnFileInfo", Namespace = "http://abbcdn.bg/2013/AbbcdnService/v1")]
    public class UploadFileInfo
    {
        [DataMember]
        public Guid FileKey { get; set; }

        [DataMember]
        public string ContentHash { get; set; }

        [DataMember]
        public long ContentSize { get; set; }
    }

    [DataContract(Name = "AbbcdnFileInfo", Namespace = "http://abbcdn.bg/2013/AbbcdnService/v1")]
    public class DownloadFileInfo
    {
        [DataMember]
        public Guid FileKey { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FileMimeType { get; set; }

        [DataMember]
        public string ContentHash { get; set; }

        [DataMember]
        public long ContentSize { get; set; }

        [DataMember]
        public byte[] ContentBytes { get; set; }
    }
}
