using Newtonsoft.Json.Linq;

namespace Aop.Api.WordTemplates
{
    public interface IDataGenerator
    {
        JObject GenerateNote(
           JObject source,
           string outgoingNum = "......................................",
           string date = ".........................................",
           string secretaryName = "[СЕКРЕТАР]",
           string secretaryPosition = "/Определен със Заповед № .................../",
           string coordinatorName = "",
           string coordinatorPosition = "",
           string madeByName = "",
           string madeByPosition = ""
           );

        JObject GenerateReport(
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
            );
    }
}
