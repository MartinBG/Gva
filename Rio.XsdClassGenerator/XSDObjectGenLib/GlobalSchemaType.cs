namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;

    internal class GlobalSchemaType
    {
        public string ClrNamespace;
        public string ClrTypeName;
        public GlobalXsdType Type;
        public string XsdNamespace;
        public string XsdNamespaceAndTypeName;
        public string XsdTypeName;

        public GlobalSchemaType(string xsdNamespace, string xsdTypeName, GlobalXsdType type, string clrNamespace, string clrTypeName)
        {
            this.XsdNamespace = xsdNamespace;
            this.XsdTypeName = xsdTypeName;
            this.Type = type;
            this.ClrNamespace = clrNamespace;
            this.ClrTypeName = clrTypeName;
            switch (this.Type)
            {
                case GlobalXsdType.ComplexType:
                    this.XsdNamespaceAndTypeName = this.XsdNamespace + "&&" + xsdTypeName;
                    break;

                case GlobalXsdType.Element:
                    this.XsdNamespaceAndTypeName = this.XsdNamespace + "$$" + xsdTypeName;
                    break;

                case GlobalXsdType.Enum:
                    this.XsdNamespaceAndTypeName = this.XsdNamespace + "%%" + xsdTypeName;
                    break;
            }
        }
    }
}

