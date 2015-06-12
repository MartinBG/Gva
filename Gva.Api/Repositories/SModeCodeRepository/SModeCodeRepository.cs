using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.SModeCodeRepository
{
    public class SModeCodeRepository : ISModeCodeRepository
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;
        private ILotRepository lotRepository;

        public SModeCodeRepository(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository,
            ILotRepository lotRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
        }

        public IEnumerable<GvaViewSModeCode> GetSModeCodes(
            int? typeId,
            string codeHex = null,
            string regMark = null,
            string identifier = null,
            int? isConnectedToRegistrationId = null,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewSModeCode>();

            if (!string.IsNullOrEmpty(codeHex))
            {
                predicate = predicate.And(o => o.CodeHex.Contains(codeHex));
            }

            if (!string.IsNullOrEmpty(regMark))
            {
                predicate = predicate.And(o => o.RegMark.ToLower().Contains(regMark.ToLower()));
            }

            if (!string.IsNullOrEmpty(identifier))
            {
                predicate = predicate.And(o => o.Identifier.ToLower().Contains(identifier.ToLower()));
            }

            if (typeId.HasValue)
            {
                predicate = predicate.And(c => c.Type.NomValueId == typeId);
            }

            if (isConnectedToRegistrationId.HasValue)
            { 
                bool getOnlyWithreg = this.nomRepository.GetNomValue(isConnectedToRegistrationId.Value).Code == "Y";
                predicate = predicate.And(c => getOnlyWithreg ? c.RegistrationPartIndex.HasValue : !c.RegistrationPartIndex.HasValue);
            }

            return this.unitOfWork.DbContext.Set<GvaViewSModeCode>()
                .Include(c => c.Type)
                .Where(predicate)
                .OrderBy(o => o.CodeHex)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public string GetNextHexCode(int typeId)
        {
            string typeAlias = this.nomRepository.GetNomValue(typeId).Alias;
            GvaViewSModeCode entryWithMaxCodeValue = this.GetSModeCodes(typeId: typeId)
                .OrderByDescending(c => Convert.ToInt32(c.CodeHex, 16))
                .FirstOrDefault();

            int nextCode = 0;
            if (entryWithMaxCodeValue != null)
            {
                int nextCodeDecimal = (Convert.ToInt32(entryWithMaxCodeValue.CodeHex, 16) + 1);
                int? minCode = null;
                switch (typeAlias)
                {
                    case "military":
                        if (nextCodeDecimal > GvaConstants.MaxMilitarySModeCodeValue)
                        {
                            throw new Exception("Max number of smode codes for militaries has been reached");
                        }
                        else if (nextCodeDecimal < GvaConstants.MinMilitarySModeCodeValue)
                        {
                            minCode = GvaConstants.MinMilitarySModeCodeValue;
                        }
                        break;
                    case "squitter":
                        if (nextCodeDecimal > GvaConstants.MaxSquitterSModeCodeValue)
                        {
                            throw new Exception("Max number of smode codes for squitters has been reached");
                        }
                        else if (nextCodeDecimal < GvaConstants.MinSquitterSModeCodeValue)
                        {
                            minCode = GvaConstants.MinSquitterSModeCodeValue;
                        }
                        break;
                    case "aircraft":
                        if (nextCodeDecimal > GvaConstants.MaxMilitarySModeCodeValue)
                        {
                            throw new Exception("Max number of smode codes for aircrafts has been reached");
                        }
                        else if (nextCodeDecimal < GvaConstants.MinAircraftSModeCodeValue)
                        {
                            minCode = GvaConstants.MinAircraftSModeCodeValue;
                        }
                        break;
                }

                nextCode =  minCode.HasValue ? minCode.Value : nextCodeDecimal;
            }
            else
            {
                switch (typeAlias)
                {
                    case "military":
                        nextCode = GvaConstants.MinMilitarySModeCodeValue;
                        break;
                    case "squitter":
                        nextCode = GvaConstants.MinSquitterSModeCodeValue;
                        break;
                    case "aircraft":
                        nextCode = GvaConstants.MinAircraftSModeCodeValue;
                        break;
                }
            }

            return string.Format("{0:x}", nextCode).ToUpper();
        }
    }
}
