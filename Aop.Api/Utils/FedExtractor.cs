using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.Utils
{
    public static class FedExtractor
    {
        public static List<NOMv5.nom> noms { get; set; }

        //Вид на процедурата
        public static string GetProcedureTypeShort(FEDv5.document document)
        {
            try
            {
                string documentType = document.type;
                var section = document.documentsection.First(s => s.id.Equals("S4"));
                string val = section["IV1.1"]["ProcType"];

                if (string.IsNullOrWhiteSpace(val))
                    return string.Empty;

                string nom = string.Empty;

                if (documentType == "form2_es")
                {
                    nom = "FORM_2_PROC_TYPE_ES";
                }
                else if (documentType == "form5" || documentType == "form5_es")
                {
                    nom = "FORM_5_PROC_TYPE";
                }
                else if (documentType == "form2")
                {
                    nom = "FORM_2_PROC_TYPE";
                }

                return noms.First(n => n.id == nom).item.First(i => i.key == val).value;
            }
            catch
            {
                return string.Empty;
            }
        }

        //Обект
        public static string GetObject(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("S2"))["II1.2"];
                string objectValue = section["ContractType"];

                return noms.First(n => n.id == "ORDER_TYPE").item.First(i => i.key == objectValue).value;
            }
            catch
            {
                return string.Empty;
            }
        }

        //Критерий 
        public static string GetOffersCriteriaOnly(FEDv5.document document)
        {
            try
            {
                string lowPriceCriteriaNom = string.Empty;
                string criteriaNom = string.Empty;

                switch (document.type)
                {
                    case "form2_es":
                        {
                            lowPriceCriteriaNom = "LOWEST_PRICE_CRITERIA_ES";
                            criteriaNom = "CRITERIA_CHOICE_ES_FORM2";
                            break;
                        }
                    case "form2":
                        {
                            lowPriceCriteriaNom = "LOWEST_PRICE_CRITERIA";
                            criteriaNom = "CRITERIA_CHOICE_BG";
                            break;
                        }
                    case "form5":
                        {
                            lowPriceCriteriaNom = "LOWEST_PRICE_CRITERIA";
                            criteriaNom = "CRITERIA_CHOICE_BG_FORM5";
                            break;
                        }
                    case "form5_es":
                        {
                            lowPriceCriteriaNom = "LOWEST_PRICE_CRITERIA_ES";
                            criteriaNom = "CRITERIA_CHOICE_ES2";
                            break;
                        }
                }

                var section = document.documentsection.First(s => s.id.Equals("S4"))["IV2.1"];

                if (section.award_criteria != null && section.award_criteria.low_price != null)
                {
                    string lowPriceValue = section.award_criteria.low_price.value;
                    return noms.First(n => n.id == lowPriceCriteriaNom).item.First(i => i.key == lowPriceValue).value;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        //Предмет на поръчката
        public static string GetSubject(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("S2"));
                return section["II1.1"]["Content"];
            }
            catch
            {
                return string.Empty;
            }
        }

        //Стойност на поръчката
        public static string GetPredictedValue(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("S2"));

                string priceValue = section["II2.1"]["Price"];
                string currency = string.Empty;
                string bgCurrency = noms.First(n => n.id == "CURRENCY").item.First(i => i.key == "BGN").value;

                if (!string.IsNullOrWhiteSpace(priceValue))
                {
                    currency = section["II2.1"]["PriceCurr"];

                    return string.Format("{0} {1}", priceValue, !string.IsNullOrWhiteSpace(currency) ? currency : bgCurrency);
                }
                else
                {
                    string fromPrice = section["II2.1"]["PriceRangeLo"];
                    string toPrice = section["II2.1"]["PriceRangeHi"];
                    bool hasFromPrice = !string.IsNullOrWhiteSpace(fromPrice);
                    bool hasToPrice = !string.IsNullOrWhiteSpace(toPrice);
                    currency = section["II2.1"]["PriceRangeCurr"];

                    if (hasFromPrice || hasToPrice)
                    {
                        return string.Format("от {0} до {1} {2}", hasFromPrice ? fromPrice : "-", hasToPrice ? toPrice : "-", !string.IsNullOrWhiteSpace(currency) ? currency : bgCurrency);
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        //Възложител
        public static string GetContractor(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("S1"));
                return section["I1"]["OfficialName"];
            }
            catch
            {
                return string.Empty;
            }
        }

        //Възложител - партида
        public static string GetContractorBatch(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("D"))["D1"];
                return section["Partide"];
            }
            catch
            {
                return string.Empty;
            }
        }

        //EИК
        public static string GetEIK(FEDv5.document document)
        {
            try
            {
                var section = document.documentsection.First(s => s.id.Equals("S1"))["I1"];
                return section["Number"];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
