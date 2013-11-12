using Common.Data;
using Common.Extensions;
using Regs.Api.Models;
using System.Linq;

namespace Regs.Api.Managers.LobManager
{
    public class LobManager : ILobManager
    {
        private IUnitOfWork unitOfWork;

        public LobManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public TextBlob AddLob(string content)
        {
            string hash = content.CalculateSHA1();

            TextBlob textBlob = unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == hash);
            if (textBlob == null)
            {
                textBlob = new TextBlob
                {
                    Hash = hash,
                    Size = content.Length,
                    TextContent = content
                };
            }

            return textBlob;
        }
    }
}
