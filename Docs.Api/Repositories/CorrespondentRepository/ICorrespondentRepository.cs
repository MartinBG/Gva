using Common.Api.Repositories;
using Common.Api.UserContext;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Repositories.CorrespondentRepository
{
    public interface ICorrespondentRepository : IRepository<Correspondent>
    {
        List<Correspondent> GetCorrespondents(
            string correspondentUin,
            string correspondentEmail,
            int limit,
            int offset,
            out int totalCount);

        Correspondent GetCorrespondent(int id);

        Correspondent CreateBgCitizen(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string bgCitizenFirstName,
            string bgCitizenLastName,
            string bgCitizenUIN,
            UserContext userContext);

        Correspondent CreateForeigner(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string foreignerFirstName,
            string foreignerLastName,
            int? foreignerCountryId,
            string foreignerSettlement,
            DateTime? foreignerBirthDate,
            UserContext userContext);

        Correspondent CreateLegalEntity(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string legalEntityName,
            string legalEntityBulstat,
            UserContext userContext);

        Correspondent CreateFLegalEntity(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string fLegalEntityName,
            int? fLegalEntityCountryId,
            string fLegalEntityRegisterName,
            string fLegalEntityRegisterNumber,
            string fLegalEntityOtherData,
            UserContext userContext);

        void DeteleCorrespondent(int id, byte[] corrVersion);
    }
}
