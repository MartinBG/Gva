using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Activation.Blocks;

namespace Common.Utils
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private static readonly string ActivationBlockKey = "__NinjectControllerFactory_ActivationBlockKey__";
        private IKernel kernel;

        public NinjectControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            IActivationBlock activationBlock = (IActivationBlock)HttpContext.Current.Items[ActivationBlockKey];

            activationBlock.Dispose();

            base.ReleaseController(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(
                    404,
                    string.Format(
                        "The controller for path '{0}' was not found or does not implement IController.",
                        requestContext.HttpContext.Request.Path));
            }

            if (!typeof(IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(
                    string.Format("The controller type '{0}' must implement IController.", controllerType),
                    "controllerType");
            }

            try
            {
                if (!HttpContext.Current.Items.Contains(ActivationBlockKey))
                {
                    HttpContext.Current.Items.Add(ActivationBlockKey, this.kernel.BeginBlock());
                }

                IActivationBlock activationBlock = (IActivationBlock)HttpContext.Current.Items[ActivationBlockKey];

                return (IController)activationBlock.Get(controllerType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "An error occurred when trying to create a controller of type '{0}'. Make sure that the controller has a parameterless public constructor.",
                        controllerType),
                    ex);
            }
        }
    }
}