using Common.Api.Models;
using Common.Data;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Data.Entity;
using Gva.Rio.Abbcdn;
using System.Reflection;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using System.Text;
using Common.Api.UserContext;
using Common.Utils;
using Common.Blob;
using System.ServiceModel;
using Common.Extensions;
using System.Data.SqlClient;
using System.Configuration;
using Autofac.Features.OwnedInstances;
using Common.Tests;
using Common.Api.Jobs;
using NLog;
using Common.Rio.RioObjectExtractor;
using Common.Rio.PortalBridge;
using Gva.Rio.IncomingDocProcessor;

namespace Gva.Rio.Jobs
{
    public class IncomingDocsJob : IJob
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Func<Owned<IUnitOfWork>> unitOfWorkFactory;
        private Func<Owned<IIncomingDocProcessor>> incomingDocProcessorFactory;

        public IncomingDocsJob(Func<Owned<IUnitOfWork>> unitOfWorkFactory, Func<Owned<IIncomingDocProcessor>> incomingDocProcessorFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.incomingDocProcessorFactory = incomingDocProcessorFactory;
        }

        public string Name
        {
            get { return "IncomingDocumentsJob"; }
        }

        public TimeSpan Period
        {
            get
            {
                return TimeSpan.FromSeconds(int.Parse(System.Configuration.ConfigurationManager.AppSettings["Gva.Rio:IncomingDocsJobIntervalInSeconds"]));
            }
        }

        public void Action()
        {
            try
            {
                List<int> pendingIncomingDocs = null;


                using (var unitOfWork = unitOfWorkFactory())
                {
                    pendingIncomingDocs = unitOfWork.Value.DbContext.Set<IncomingDoc>()
                        .Where(e => e.IncomingDocStatus.Alias == "Pending")
                        .Select(d => d.IncomingDocId)
                        .ToList();
                }

                using (var channelFactory = new ChannelFactory<IAbbcdn>("WSHttpBinding_IAbbcdn"))
                using (var abbcdnStorage = new AbbcdnStorage(channelFactory))
                {
                    foreach (int incomingDocId in pendingIncomingDocs)
                    {
                        using (var incomingDocProcessor = incomingDocProcessorFactory())
                        {
                            incomingDocProcessor.Value.AbbcdnStorage = abbcdnStorage;
                            incomingDocProcessor.Value.Process(incomingDocId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("General error", ex);
            }
        }
    }
}