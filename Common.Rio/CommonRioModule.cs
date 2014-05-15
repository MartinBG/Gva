using Autofac;

namespace Common.Rio
{
    public class CommonRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<Common.Rio.RioObjectExtractor.RioObjectExtractor>().As<Common.Rio.RioObjectExtractor.IRioObjectExtractor>();
        }
    }
}
