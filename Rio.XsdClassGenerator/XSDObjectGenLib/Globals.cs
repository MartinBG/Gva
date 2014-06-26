namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Collections;

    internal class Globals
    {
        internal const string COMPLEXTYPE_DELIMINATOR = "&&";
        internal const string ELELENT_DELIMINATOR = "$$";
        internal const string ENUM_DELIMINATOR = "%%";
        internal static ArrayList globalClrNamespaceList = new ArrayList();
        internal static Hashtable globalSchemaTypeTable = new Hashtable();
        internal static bool globalSeparateImportedNamespaces = false;
        internal const string W3C = "http://www.w3.org/XML/1998/namespace";
        internal const string XSD_NAMESPACE = "http://www.w3.org/2001/XMLSchema";
    }
}

