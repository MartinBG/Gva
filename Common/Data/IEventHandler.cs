namespace Common.Data
{
    public interface IEventHandler
    {
        void Handle(IEvent e);
    }
}
