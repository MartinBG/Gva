using System;
using Common.Json;
using Newtonsoft.Json.Linq;

namespace Aop.Api.WordTemplates
{
    public class DataGenerator : IDataGenerator
    {
        public JObject GenerateNote(
            JObject source,
            string outgoingNum = "......................................",
            string date = ".........................................",
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
                paragraph4 = this.GetParagraph4(source.Get<JObject>("paragraph4")),
                paragraph5 = this.GetParagraph5(source.Get<JObject>("paragraph5")),
                paragraph6 = this.GetParagraph6(source.Get<JObject>("paragraph6")),
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
            string outgoingNum = "......................................",
            string date = ".........................................",
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
                paragraph4 = this.GetParagraph4(source.Get<JObject>("paragraph4")),
                paragraph5 = this.GetParagraph5(source.Get<JObject>("paragraph5")),
                paragraph6 = this.GetParagraph6(source.Get<JObject>("paragraph6")),
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

        private object GetParagraph4(JObject paragraph4)
        {
            if (paragraph4 == null)
            {
                return null;
            }

            var techniqueText = paragraph4.Get<string>("technique.table1.row13");
            if (string.IsNullOrEmpty(techniqueText))
            {
                return null;
            }

            return new
            {
                text = techniqueText
            };
        }

        private object GetParagraph5(JObject paragraph5)
        {
            if (paragraph5 == null)
            {
                return null;
            }

            var table1Text = paragraph5.Get<string>("deadline.table1.row12");
            var table2Text = paragraph5.Get<string>("deadline.table2.row6");

            if (string.IsNullOrEmpty(table1Text) && string.IsNullOrEmpty(table2Text))
            {
                return null;
            }

            return new
            {
                deadline = new
                {
                    table1 = string.IsNullOrEmpty(table1Text) ? null : new { text = table1Text },
                    table2 = string.IsNullOrEmpty(table2Text) ? null : new { text = table2Text }
                }
            };
        }

        private object GetParagraph6(JObject paragraph6)
        {
            if (paragraph6 == null)
            {
                return null;
            }

            return new
            {
                additionalInfo = new
                {
                    text = paragraph6.Get<string>("additionalInfo")
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
                financedBy = procedure.Get<string>("table1.row8"),
                row1 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row1.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row1.tick2")
                },
                row2 = new
                {
                    tick1 = procedure.Get<bool?>("table2.row2.tick1"),
                    tick2 = procedure.Get<bool?>("table2.row2.tick2")
                },
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
                    tick2 = procedure.Get<bool?>("table2.row6.tick2"),
                    tick3 = procedure.Get<bool?>("table2.row6.tick3"),
                    tick4 = procedure.Get<bool?>("table2.row6.tick4"),
                    tick5 = procedure.Get<bool?>("table2.row6.tick5"),
                    tick6 = procedure.Get<bool?>("table2.row6.tick6"),
                    tick7 = procedure.Get<bool?>("table2.row6.tick7")
                },
                row7 = procedure.Get<string>("table2.row7")
            };
        }

        private object GetNotice(JObject notice)
        {
            var conclusionText = notice.Get<string>("table1.row4");
            var orderSubjText = notice.Get<string>("table2.row16");
            var depositsAndGuaranteeText = notice.Get<string>("table3.row9");
            var clausesText = notice.Get<string>("table4.row3");
            var economicalOpText = notice.Get<string>("table5.row5");
            var economicalAndFinancialOppText = notice.Get<string>("table6.row9");
            var technicalOppText = notice.Get<string>("table7.row8");
            var procedureText = notice.Get<string>("table8.row10");
            var administrativeInfoText = notice.Get<string>("table9.row17");
            var appealProcText = notice.Get<string>("table10.row4");
            var appendixAText = notice.Get<string>("table11.row3");
            var appendixBText = notice.Get<string>("table12.row3");

            if (string.IsNullOrEmpty(conclusionText) &&
                string.IsNullOrEmpty(orderSubjText) &&
                string.IsNullOrEmpty(depositsAndGuaranteeText) &&
                string.IsNullOrEmpty(economicalOpText) &&
                string.IsNullOrEmpty(economicalAndFinancialOppText) &&
                string.IsNullOrEmpty(technicalOppText) &&
                string.IsNullOrEmpty(procedureText) &&
                string.IsNullOrEmpty(administrativeInfoText) &&
                string.IsNullOrEmpty(appealProcText) &&
                string.IsNullOrEmpty(appendixAText) &&
                string.IsNullOrEmpty(appendixBText))
            {
                return null;
            }

            return new
            {
                conclusion = string.IsNullOrEmpty(conclusionText) ? null : new { text = conclusionText },
                orderSubj = string.IsNullOrEmpty(orderSubjText) ? null : new { text = orderSubjText },
                depositsAndGuarantee = string.IsNullOrEmpty(depositsAndGuaranteeText) ? null : new { text = depositsAndGuaranteeText },
                clauses = string.IsNullOrEmpty(clausesText) ? null : new { text = clausesText },
                economicalOp = string.IsNullOrEmpty(economicalOpText) ? null : new { text = economicalOpText },
                economicalAndFinancialOpp = string.IsNullOrEmpty(economicalAndFinancialOppText) ? null : new { text = economicalAndFinancialOppText },
                technicalOpp = string.IsNullOrEmpty(technicalOppText) ? null : new { text = technicalOppText },
                procedure = string.IsNullOrEmpty(procedureText) ? null : new { text = procedureText },
                administrativeInfo = string.IsNullOrEmpty(administrativeInfoText) ? null : new { text = administrativeInfoText },
                appealProc = string.IsNullOrEmpty(appealProcText) ? null : new { text = appealProcText },
                appendixA = string.IsNullOrEmpty(appendixAText) ? null : new { text = appendixAText },
                appendixB = string.IsNullOrEmpty(appendixBText) ? null : new { text = appendixBText }
            };
        }
    }
}
