using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Api.StaticNomenclatures
{
    public enum BooleanNom
    {
        Yes = 1,//let 0(the default) be unassigned
        No
    }

    public static class BooleanNomConvert
    {
        public static bool? ToBool(BooleanNom? nomVal)
        {
            bool? boolVal;
            switch (nomVal)
            {
                case BooleanNom.Yes:
                    boolVal = true;
                    break;
                case BooleanNom.No:
                    boolVal = false;
                    break;
                default:
                    boolVal = (bool?)null;
                    break;
            }

            return boolVal;
        }

        public static BooleanNom? FromBool(bool? boolVal)
        {
            BooleanNom? nomVal;
            switch (boolVal)
            {
                case true:
                    nomVal = BooleanNom.Yes;
                    break;
                case false:
                    nomVal = BooleanNom.No;
                    break;
                default:
                    nomVal = (BooleanNom?)null;
                    break;
            }

            return nomVal;
        }
    }
}
