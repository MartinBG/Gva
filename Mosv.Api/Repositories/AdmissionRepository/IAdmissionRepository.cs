using System.Collections.Generic;
using Mosv.Api.Models;
using System;

namespace Mosv.Api.Repositories.AdmissionRepository
{
    public interface IAdmissionRepository
    {
        IEnumerable<MosvViewAdmission> GetAdmissions(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDate,
            string applicantType,
            string applicant,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        MosvViewAdmission GetAdmission(int admissionId);
    }
}