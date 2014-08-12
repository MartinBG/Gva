using System;
using Common.Json;
using Newtonsoft.Json.Linq;

namespace Aop.Api.WordTemplates
{
    public class DataGenerator : IDataGenerator
    {
        public JObject GenerateNote(
            JObject source,
            string outgoingNum = "...",
            string date = "...",
            string secretaryName = "[СЕКРЕТАР]",
            string secretaryPosition = "/Определен със Заповед № .................../",
            string coordinatorName = "",
            string coordinatorPosition = "",
            string madeByName = "",
            string madeByPosition = ""
            )
        {
            var json = new
            {
                outgoingNum = outgoingNum,
                date = date,
                paragraph1 = this.GetParagraph1(source.Get<JObject>("paragraph1")),
                paragraph2 = this.GetParagraph2(source.Get<JObject>("paragraph2")),
                paragraph3 = this.GetParagraph3(source.Get<JObject>("paragraph3")),
                paragraph4 = this.GetParagraph4(source.Get<JObject>("paragraph4")),
                paragraph6 = this.GetParagraph6(source.Get<JObject>("paragraph6")),
                paragraph7 = this.GetParagraph7(source.Get<JObject>("paragraph7")),
                paragraph8 = this.GetParagraph8(source.Get<JObject>("paragraph8")),
                secretary = new { name = secretaryName, position = secretaryPosition },
                coordinators = new object[]
                {
                    new { name = coordinatorName, position = coordinatorPosition }
                },
                madeBy = new
                {
                    name = madeByName,
                    position = madeByPosition
                }
            };

            return JObject.FromObject(json);
        }

        public JObject GenerateReport(
            JObject source,
            string outgoingNum = "...",
            string date = "...",
            string uniqueNum = "",
            string caseManagement = "",
            string directorName = "[ДИРЕКТОР]",
            string coordinatorName = "",
            string coordinatorPosition = "",
            string madeByName = "",
            string madeByPosition = ""
            )
        {
            var json = new
            {
                outgoingNum = outgoingNum,
                date = date,
                uniqueNum = uniqueNum,
                caseManagement = caseManagement,
                paragraph1 = this.GetParagraph1(source.Get<JObject>("paragraph1")),
                paragraph2 = this.GetParagraph2(source.Get<JObject>("paragraph2")),
                paragraph3 = this.GetParagraph3(source.Get<JObject>("paragraph3")),
                paragraph4 = this.GetParagraph4(source.Get<JObject>("paragraph4")),
                paragraph6 = this.GetParagraph6(source.Get<JObject>("paragraph6")),
                paragraph7 = this.GetParagraph7(source.Get<JObject>("paragraph7")),
                paragraph8 = this.GetParagraph8(source.Get<JObject>("paragraph8")),
                director = new { name = directorName },
                coordinators = new object[]
                {
                    new { name = coordinatorName, position = coordinatorPosition }
                },
                madeBy = new
                {
                    name = madeByName,
                    position = madeByPosition
                }
            };

            return JObject.FromObject(json);
        }

        private object GetParagraph1(JObject paragraph1)
        {
            var emplJObj = paragraph1 == null ? new JObject() : paragraph1.Get<JObject>("employer.table1");
            var procJObj = paragraph1 == null ? new JObject() : paragraph1.Get<JObject>("proc");

            return new
            {
                employer = this.GetEmployer(emplJObj),
                procedure = this.GetProcedure(procJObj)
            };
        }

        private object GetParagraph2(JObject paragraph2)
        {
            if (paragraph2 == null)
            {
                return null;
            }

            var notice = this.GetNotice(paragraph2.Get<JObject>("notice"));

            if (notice == null)
            {
                return null;
            }

            return new
            {
                notice = notice
            };
        }

        private object GetParagraph3(JObject paragraph3)
        {
            if (paragraph3 == null)
            {
                return null;
            }

            var decisionText = paragraph3.Get<string>("decision.table1.findings");
            if (string.IsNullOrEmpty(decisionText))
            {
                return null;
            }

            return new
            {
                decision = new
                {
                    findings = decisionText
                }
            };
        }

        private object GetParagraph4(JObject paragraph4)
        {
            if (paragraph4 == null)
            {
                return null;
            }

            var techniqueText = paragraph4.Get<string>("technique.table1.findings");
            if (string.IsNullOrEmpty(techniqueText))
            {
                return null;
            }

            return new
            {
                technique = new
                {
                    findings = techniqueText
                }
            };
        }

        private object GetParagraph6(JObject paragraph6)
        {
            if (paragraph6 == null)
            {
                return null;
            }

            var table1Text = paragraph6.Get<string>("deadline.table1.findings");
            var table2Text = paragraph6.Get<string>("deadline.table2.findings");

            if (string.IsNullOrEmpty(table1Text) && string.IsNullOrEmpty(table2Text))
            {
                return null;
            }

            return new
            {
                deadline = new
                {
                    table1 = string.IsNullOrEmpty(table1Text) ? null : new { findings = table1Text },
                    table2 = string.IsNullOrEmpty(table2Text) ? null : new { findings = table2Text }
                }
            };
        }

        private object GetParagraph7(JObject paragraph7)
        {
            if (paragraph7 == null)
            {
                return null;
            }

            return new
            {
                additionalInfo = new
                {
                    text = paragraph7.Get<string>("additionalInfo")
                }
            };
        }

        private object GetParagraph8(JObject paragraph8)
        {
            if (paragraph8 == null)
            {
                return null;
            }

            return new
            {
                additionalInfo = new
                {
                    text = paragraph8.Get<string>("additionalInfo")
                }
            };
        }

        private object GetEmployer(JObject employer)
        {
            if (employer == null)
            {
                employer = new JObject();
            }

            return new
            {
                name = employer.Get<string>("row1"),
                type = new
                {
                    tick1 = employer.Get<bool?>("row2.tick1"),
                    tick2 = employer.Get<bool?>("row2.tick2"),
                    tick3 = employer.Get<bool?>("row2.tick3"),
                    tick4 = employer.Get<bool?>("row2.tick4"),
                },
                lotNumber = employer.Get<string>("row3"),
                incNumAndRegDate = employer.Get<string>("row4")
            };
        }

        private object GetProcedure(JObject procedure)
        {
            if (procedure == null)
            {
                procedure = new JObject();
            }

            return new
            {
                type = new
                {
                    tick1 = procedure.Get<bool?>("table1.row1.tick1"),
                    tick2 = procedure.Get<bool?>("table1.row1.tick2"),
                    tick3 = procedure.Get<bool?>("table1.row1.tick3"),
                    tick4 = procedure.Get<bool?>("table1.row1.tick4"),
                    tick5 = procedure.Get<bool?>("table1.row1.tick5"),
                    tick6 = procedure.Get<bool?>("table1.row1.tick6")
                },
                order = new
                {
                    tick1 = procedure.Get<bool?>("table1.row2.tick1"),
                    tick2 = procedure.Get<bool?>("table1.row2.tick2"),
                    tick3 = procedure.Get<bool?>("table1.row2.tick3")
                },
                specialPositionsSep = new
                {
                    tick1 = procedure.Get<bool?>("table1.row3.tick1"),
                    tick2 = procedure.Get<bool?>("table1.row3.tick2"),
                },
                orderSubject = procedure.Get<string>("table1.row4"),
                orderPrice = procedure.Get<string>("table1.row5"),
                endDate = procedure.Get<DateTime?>("table1.row6"),
                ratingCriteriа = new
                {
                    tick1 = procedure.Get<bool?>("table1.row7.tick1"),
                    tick2 = procedure.Get<bool?>("table1.row7.tick2"),
                },
                financedBy = new
                {
                    tick1 = procedure.Get<bool?>("table1.row8.tick1"),
                    tick2 = procedure.Get<bool?>("table1.row8.tick2"),
                    tick3 = procedure.Get<bool?>("table1.row8.tick3")
                },
                row1 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row1.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row1.tick2")
                },
                row2 = procedure.Get<string>("table2.row2"),
                row3 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row3.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row3.tick2")
                },
                row4 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row4.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row4.tick2")
                },
                row5 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row5.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row5.tick2")
                },
                row6 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row6.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row6.tick2")
                },
                row7 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row7.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row7.tick2")
                },
                findings = procedure.Get<string>("table2.findings")
            };
        }

        private object GetNotice(JObject notice)
        {
            string conclusionText = notice.Get<string>("table1.findings"),
                orderSubjText = notice.Get<string>("table2.findings"),
                quantityVolumeText = notice.Get<string>("table3.findings"),
                deadlineText = notice.Get<string>("table4.findings"),
                depositsAndGuaranteeText = notice.Get<string>("table5.findings"),
                procurementConditionsText = notice.Get<string>("table6.findings"),
                economicalOpText = notice.Get<string>("table7.findings"),
                economicalAndFinancialOppText = notice.Get<string>("table8.findings"),
                technicalOppText = notice.Get<string>("table9.findings"),
                othersText = notice.Get<string>("table10.findings"),
                procedureText = notice.Get<string>("table11.findings"),
                criteriaAssignmentText = notice.Get<string>("table12.findings"),
                administrativeInfoText = notice.Get<string>("table13.findings"),
                appealProcText = notice.Get<string>("table14.findings"),
                appendixBText = notice.Get<string>("table15.findings");

            if (string.IsNullOrEmpty(conclusionText) &&
                string.IsNullOrEmpty(orderSubjText) &&
                string.IsNullOrEmpty(quantityVolumeText) &&
                string.IsNullOrEmpty(deadlineText) &&
                string.IsNullOrEmpty(depositsAndGuaranteeText) &&
                string.IsNullOrEmpty(procurementConditionsText) &&
                string.IsNullOrEmpty(economicalOpText) &&
                string.IsNullOrEmpty(economicalAndFinancialOppText) &&
                string.IsNullOrEmpty(technicalOppText) &&
                string.IsNullOrEmpty(othersText) &&
                string.IsNullOrEmpty(procedureText) &&
                string.IsNullOrEmpty(criteriaAssignmentText) &&
                string.IsNullOrEmpty(administrativeInfoText) &&
                string.IsNullOrEmpty(appealProcText) &&
                string.IsNullOrEmpty(appendixBText))
            {
                return null;
            }

            return new
            {
                conclusion = string.IsNullOrEmpty(conclusionText) ? null : new { findings = conclusionText },
                orderSubj = string.IsNullOrEmpty(orderSubjText) ? null : new { findings = orderSubjText },
                quantityVolume = string.IsNullOrEmpty(quantityVolumeText) ? null : new { findings = quantityVolumeText },
                deadline = string.IsNullOrEmpty(deadlineText) ? null : new { findings = deadlineText },
                depositsAndGuarantee = string.IsNullOrEmpty(depositsAndGuaranteeText) ? null : new { findings = depositsAndGuaranteeText },
                procurementConditions = string.IsNullOrEmpty(procurementConditionsText) ? null : new { findings = procurementConditionsText },
                economicalOp = string.IsNullOrEmpty(economicalOpText) ? null : new { findings = economicalOpText },
                economicalAndFinancialOpp = string.IsNullOrEmpty(economicalAndFinancialOppText) ? null : new { findings = economicalAndFinancialOppText },
                technicalOpp = string.IsNullOrEmpty(technicalOppText) ? null : new { findings = technicalOppText },
                others = string.IsNullOrEmpty(othersText) ? null : new { findings = othersText },
                procedure = string.IsNullOrEmpty(procedureText) ? null : new { findings = procedureText },
                criteriaAssignment = string.IsNullOrEmpty(criteriaAssignmentText) ? null : new { findings = procedureText },
                administrativeInfo = string.IsNullOrEmpty(administrativeInfoText) ? null : new { findings = administrativeInfoText },
                appealProc = string.IsNullOrEmpty(appealProcText) ? null : new { findings = appealProcText },
                appendixB = string.IsNullOrEmpty(appendixBText) ? null : new { findings = appendixBText }
            };
        }
    }
}
