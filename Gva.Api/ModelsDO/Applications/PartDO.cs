namespace Gva.Api.ModelsDO.Applications
{
    public class PartDO<T> where T : class, new()
    {
        public PartDO(T part, DocCaseDO caseDO = null)
            : this()
        {
            this.Part = part;
            this.Case = caseDO;
        }

        public PartDO()
        {
            this.Part = new T();
        }

        public T Part { get; set; }

        public DocCaseDO Case { get; set; }
    }
}
