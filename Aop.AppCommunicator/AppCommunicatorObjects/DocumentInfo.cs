using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using R_0009_000001;
using R_0009_000085;
using R_0009_000072;

namespace Aop.AppCommunicator.AppCommunicatorObjects
{
    public class DocumentInfo
    {
        /// <summary>
        /// Guid  на получения документ в опашката
        /// и ако е регистриран - на документа регистриран в системата.
        /// </summary>
        public Guid DocumentGuid { get; set; }

        /// <summary>
        /// Статус на получения документ в опашката.
        /// </summary>
        public DocumentRegistrationStatus RegistrationStatus { get; set; }

        /// <summary>
        /// УРИ на регистриран документ 'ДА', отнасящ се за получения документ в опашката.
        /// </summary>
        public DocumentURI AcceptedDocumentUri { get; set; }

        /// <summary>
        /// УРИ на регистриран документ 'НЕ', отнасящ се за получения документ в опашката.
        /// </summary>
        public DocumentURI NotAcceptedDocumentUri { get; set; }

        /// <summary>
        /// Данни за получения документ.
        /// </summary>
        public AISDocument DocumentData { get; set; }

        /// <summary>
        /// Данни за регистрирания документ в системата.
        /// </summary>
        public AISDocumentRegisterDocumentData RegisteredDocumentData { get; set; }
    }
}
