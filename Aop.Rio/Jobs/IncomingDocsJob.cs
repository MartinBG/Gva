using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Extensions;
using Common.Jobs;
using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtractor;
using Common.Tests;
using Common.Utils;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Aop.Rio.Abbcdn;
using Aop.Rio.IncomingDocProcessor;
using NLog;

namespace Aop.Rio.Jobs
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
                return TimeSpan.FromSeconds(int.Parse(System.Configuration.ConfigurationManager.AppSettings["Aop.Rio:IncomingDocsJobIntervalInSeconds"]));
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