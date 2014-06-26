namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Xml;
    using System.Xml.Schema;

    internal class ChildComplexType
    {
        public XmlSchemaComplexType ComplexType;
        public string DotnetClassName;
        public string ElementName;
        public string Namespace;
        public XmlQualifiedName Qname;

        public ChildComplexType(XmlSchemaComplexType complexType, string elementName, string dotnetClassName, string nameSpace, XmlQualifiedName qname)
        {
            this.ComplexType = complexType;
            this.ElementName = elementName;
            this.DotnetClassName = dotnetClassName;
            this.Namespace = nameSpace;
            this.Qname = qname;
        }
    }
}

