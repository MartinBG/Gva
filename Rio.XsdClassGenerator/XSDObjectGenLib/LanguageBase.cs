#pragma warning disable 618

namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Xml.Schema;
    using System.Configuration;

    internal abstract class LanguageBase
    {
        public static string collectionSuffix = ConfigurationSettings.AppSettings["CollectionSuffix"];
        public static string hiddenMemberPrefix = ConfigurationSettings.AppSettings["HiddenMemberPrefix"];
        public static string mixedElementFieldName = ConfigurationSettings.AppSettings["MixedElementFieldName"];
        public static bool partialClasses;
        public static string renameItemPrefix = ConfigurationSettings.AppSettings["RenameItemPrefix"];
        protected string schemaTargetNamespace = "";

        static LanguageBase()
        {
            if (collectionSuffix == null)
            {
                collectionSuffix = "Collection";
            }
            if (hiddenMemberPrefix == null)
            {
                hiddenMemberPrefix = "__";
            }
            if (renameItemPrefix == null)
            {
                renameItemPrefix = "_";
            }
            if (mixedElementFieldName == null)
            {
                mixedElementFieldName = "MixedValue";
            }
        }

        protected LanguageBase()
        {
        }

        protected virtual string CalculateNamespace(string schemaNamespace, string importedNamespace, bool any)
        {
            if ((importedNamespace != "") && (importedNamespace != schemaNamespace))
            {
                return string.Format(",Namespace" + this.AttributeAssignmentOperator + "\"{0}\"", importedNamespace);
            }
            if (any && (importedNamespace == ""))
            {
                return "";
            }
            if (schemaNamespace == "")
            {
                return "";
            }
            return (",Namespace" + this.AttributeAssignmentOperator + "Declarations.SchemaVersion");
        }

        public abstract string CheckForKeywords(string keyword);
        public virtual void ClassAttributeFieldCode(StreamWriter outStream, string dotNetDatatype, string xsdDatatype, string fieldName, string dotnetFieldName, XmlSchemaForm attributeFormDefault, bool isSchemaEnumerationType, string ns)
        {
            outStream.WriteLine();
            string str = fieldName;
            string str2 = ReplaceInvalidChars(dotnetFieldName);
            string str3 = this.CheckForKeywords(dotnetFieldName);
            string str4 = this.ConvertSystemDatatype(dotNetDatatype);
            string str5 = "";
            string str6 = "";
            if ((attributeFormDefault == XmlSchemaForm.Qualified) || (attributeFormDefault == XmlSchemaForm.Unqualified))
            {
                str6 = ",Form" + this.AttributeAssignmentOperator + "XmlSchemaForm." + attributeFormDefault.ToString();
                str5 = this.CalculateNamespace(this.schemaTargetNamespace, ns, dotNetDatatype == "System.Xml.XmlAttribute[]");
            }
            if (xsdDatatype == "anyType")
            {
                xsdDatatype = "";
            }
            else if (xsdDatatype != "")
            {
                xsdDatatype = ",DataType" + this.AttributeAssignmentOperator + "\"" + xsdDatatype + "\"";
            }
            switch (dotNetDatatype)
            {
                case "System.String":
                case "System.Byte[]":
                case "System.Object":
                case "System.Xml.XmlQualifiedName":
                    outStream.WriteLine(this.AttributeObjectTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                    break;

                case "System.Xml.XmlAttribute[]":
                    outStream.WriteLine(this.AttributeAnyTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                    break;

                case "System.DateTime":
                    outStream.WriteLine(this.AttributeDateTimeTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                    break;

                default:
                    if (str4 == "string")
                    {
                        outStream.WriteLine(this.AttributeObjectTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                    }
                    else
                    {
                        outStream.WriteLine(this.AttributeValueTypeTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                    }
                    break;
            }
        }

        public virtual void ClassComplexTypeFieldCode(StreamWriter outStream, string elementName, string dotnetTypeName, string collectionContainedType, string parentContainerClassName, decimal fieldOccurs, decimal particleOccurs, XmlSchemaForm elementFormDefault, string ns, bool isElementNullable, bool isAbstract)
        {
            outStream.WriteLine();
            string str = ReplaceInvalidChars(elementName);
            string str2 = this.CheckForKeywords(elementName);
            string str3 = this.CheckForKeywords(dotnetTypeName);
            string str4 = ReplaceInvalidChars(dotnetTypeName);
            collectionContainedType = this.CheckForKeywords(collectionContainedType);
            string str5 = "";
            string str6 = "";
            if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
            {
                str6 = ",Form" + this.AttributeAssignmentOperator + "XmlSchemaForm." + elementFormDefault.ToString();
                str5 = this.CalculateNamespace(this.schemaTargetNamespace, ns, false);
            }
            if ((particleOccurs > 1M) || (fieldOccurs > 1M))
            {
                outStream.WriteLine(this.FieldCollectionTemplate, new object[] { str4, str3, elementName, str6, str, "", str5, hiddenMemberPrefix, collectionSuffix, isElementNullable.ToString().ToLower(), collectionContainedType });
            }
            else if (isAbstract)
            {
                outStream.WriteLine(this.FieldAbstractClassTemplate, new object[] { str, str3, elementName, str6, str2, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
            }
            else
            {
                outStream.WriteLine(this.FieldClassTemplate, new object[] { str, str3, elementName, str6, str2, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
            }
        }

        public virtual void ClassElementFieldCode(StreamWriter outStream, string dotNetDatatype, string xsdDatatype, string fieldName, string dotnetFieldName, decimal fieldOccurs, decimal particleOccurs, XmlSchemaForm elementFormDefault, bool isSchemaEnumerationType, string ns, bool isElementNullable)
        {
            outStream.WriteLine();
            string str = fieldName;
            string str2 = ReplaceInvalidChars(dotnetFieldName);
            string str3 = this.CheckForKeywords(dotnetFieldName);
            string str4 = this.ConvertSystemDatatype(dotNetDatatype);
            string str5 = "";
            string str6 = "";
            if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
            {
                str6 = ",Form" + this.AttributeAssignmentOperator + "XmlSchemaForm." + elementFormDefault.ToString();
                str5 = this.CalculateNamespace(this.schemaTargetNamespace, ns, dotNetDatatype == "System.Xml.XmlElement");
                if ((dotNetDatatype == "System.Xml.XmlElement") && (str5 != ""))
                {
                    str5 = "Name" + this.AttributeAssignmentOperator + "\"" + str3 + "\"" + str5;
                }
            }
            if (xsdDatatype == "anyType")
            {
                xsdDatatype = "";
            }
            else if (xsdDatatype != "")
            {
                xsdDatatype = ",DataType" + this.AttributeAssignmentOperator + "\"" + xsdDatatype + "\"";
            }
            if ((particleOccurs > 1M) || (fieldOccurs > 1M))
            {
                if (dotNetDatatype == "System.Xml.XmlElement")
                {
                    outStream.WriteLine(this.ElementAnyMaxOccursTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
                }
                else
                {
                    outStream.WriteLine(this.FieldCollectionTemplate, new object[] { str2, str4, str, str6, str2, xsdDatatype, str5, hiddenMemberPrefix, collectionSuffix, isElementNullable.ToString().ToLower(), str4 });
                }
            }
            else
            {
                switch (dotNetDatatype)
                {
                    case "System.String":
                    case "System.Byte[]":
                    case "System.Object":
                    case "System.Xml.XmlQualifiedName":
                        outStream.WriteLine(this.ElementObjectTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
                        return;

                    case "System.Xml.XmlElement":
                        outStream.WriteLine(this.ElementAnyTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
                        return;

                    case "System.DateTime":
                        outStream.WriteLine(this.ElementDateTimeTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                        return;
                }
                if (str4 == "string")
                {
                    outStream.WriteLine(this.ElementObjectTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix, isElementNullable.ToString().ToLower() });
                }
                else
                {
                    outStream.WriteLine(this.ElementValueTypeTemplate, new object[] { str2, str4, str, str6, xsdDatatype, str3, str5, hiddenMemberPrefix });
                }
            }
        }

        public abstract void ClassHeaderCode(StreamWriter outStream, string dotnetClassName, string elementName, string complexTypeBaseClass, bool baseIsAbstract, bool isSchemaType, bool isAbstract, bool isLocalComplexType, Hashtable enumerableClasses, string ns, XmlSchemaForm elementFormDefault, string annotation, bool isElementNullable, ArrayList xmlIncludedClasses, bool globalElementAndSchemaTypeHaveSameName);
        public abstract void ClassTrailerCode(StreamWriter outStream, string dotnetClassName, ArrayList ctorList, bool defaultInitialization, bool depthFirstTraversalHooks, bool makeSchemaCompliant, string complexTypeBaseClass, bool mixed, string mixedXsdType);
        public virtual void CollectionSubclass(StreamWriter outStream, string fieldName, string datatype, bool isAbstract)
        {
            fieldName = ReplaceInvalidChars(fieldName);
            string str = this.ConvertSystemDatatype(datatype);
            string str2 = str;
            if (datatype == "System.Byte[]")
            {
                if (this is CSharpTemplate)
                {
                    str2 = "byte[0]";
                }
                else if (this is VBTemplate)
                {
                    str2 = "Object";
                }
            }
            else
            {
                str2 = str2 + "()";
            }
            string str3 = (datatype == "System.Object") ? this.HideInheritedMethodKeyword : "";
            if ((datatype == "System.String") || (str == "string"))
            {
                outStream.WriteLine(this.CollectionClassAbstractTemplate, new object[] { str, fieldName, collectionSuffix, str3, partialClasses ? this.PartialKeyword : "" });
            }
            else if (isAbstract)
            {
                outStream.WriteLine(this.CollectionClassAbstractTemplate, new object[] { str, fieldName, collectionSuffix, str3, partialClasses ? this.PartialKeyword : "" });
            }
            else
            {
                outStream.WriteLine(this.CollectionClassTemplate, new object[] { str, fieldName, collectionSuffix, str3, str2, partialClasses ? this.PartialKeyword : "" });
            }
            outStream.WriteLine();
        }

        public abstract string ConvertSystemDatatype(string systemType);
        public string FrameworkTypeMapping(string xsdType)
        {
            switch (xsdType)
            {
                case "base64Binary":
                    return "System.Byte[]";

                case "boolean":
                    return "System.Boolean";

                case "byte":
                    return "System.SByte";

                case "date":
                    return "System.DateTime";

                case "dateTime":
                    return "System.DateTime";

                case "decimal":
                    return "System.Decimal";

                case "double":
                    return "System.Double";

                case "float":
                    return "System.Single";

                case "hexBinary":
                    return "System.Byte[]";

                case "int":
                    return "System.Int32";

                case "long":
                    return "System.Int64";

                case "QName":
                    return "System.XmlQualifiedName";

                case "short":
                    return "System.Int16";

                case "time":
                    return "System.DateTime";

                case "unsignedByte":
                    return "System.Byte";

                case "unsignedInt":
                    return "System.UInt32";

                case "unsignedLong":
                    return "System.UInt64";

                case "unsignedShort":
                    return "System.UInt16";

                case "anyType":
                    return "System.Object";
            }
            return "System.String";
        }

        public bool IsValueType(string datatype)
        {
            switch (datatype)
            {
                case "System.SByte":
                case "System.Byte":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Boolean":
                case "System.Decimal":
                case "System.Char":
                case "System.DateTime":
                    return true;
            }
            return false;
        }

        public abstract void NamespaceHeaderCode(StreamWriter outStream, string ns, string schemaFile, Hashtable forwardDeclarations, string targetNamespace, Hashtable enumerations, bool depthFirstTraversalHooks, ArrayList importedReferences);
        public abstract void NamespaceTrailerCode(StreamWriter outStream, string ns);
        public static string ReplaceInvalidChars(string invalid)
        {
            if (invalid != null)
            {
                if (invalid.StartsWith("System."))
                {
                    return invalid;
                }
                bool flag = false;
                string str = "";
                if (invalid.IndexOf(".") > 0)
                {
                    foreach (string str2 in Globals.globalClrNamespaceList)
                    {
                        if (invalid.StartsWith(str2 + "."))
                        {
                            str = str2 + ".";
                            if (invalid.StartsWith(str))
                            {
                                invalid = invalid.Remove(0, str.Length);
                            }
                            flag = true;
                            break;
                        }
                    }
                }
                invalid = invalid.Replace(".", "_");
                invalid = invalid.Replace("-", "_");
                invalid = invalid.Replace("/", "_");
                invalid = invalid.Replace(":", "_");
                if (flag)
                {
                    invalid = str + invalid;
                }
            }
            return invalid;
        }

        public static string ScrubNamespace(string invalid)
        {
            if (invalid == "")
            {
                throw new XSDObjectGenException("invalid .net namespace name entered for imported schema namesapce");
            }
            invalid = invalid.Replace(".", "_");
            invalid = invalid.Replace("-", "_");
            invalid = invalid.Replace(":", "_");
            invalid = invalid.Replace("/", "_");
            invalid = invalid.Replace(@"\", "_");
            invalid = invalid.Replace("$", "_");
            return invalid;
        }

        protected abstract string AttributeAnyTemplate { get; set; }

        protected abstract string AttributeAssignmentOperator { get; }

        protected abstract string AttributeDateTimeTemplate { get; set; }

        protected abstract string AttributeObjectTemplate { get; set; }

        protected abstract string AttributeValueTypeTemplate { get; set; }

        protected abstract string ClassEnumerabilityTemplate { get; set; }

        protected abstract string CollectionClassAbstractTemplate { get; set; }

        protected abstract string CollectionClassTemplate { get; set; }

        protected abstract string ElementAnyMaxOccursTemplate { get; set; }

        protected abstract string ElementAnyTemplate { get; set; }

        protected abstract string ElementDateTimeTemplate { get; set; }

        protected abstract string ElementObjectTemplate { get; set; }

        protected abstract string ElementValueTypeTemplate { get; set; }

        protected abstract string FieldAbstractClassTemplate { get; set; }

        protected abstract string FieldClassTemplate { get; set; }

        protected abstract string FieldCollectionTemplate { get; set; }

        protected abstract string HideInheritedMethodKeyword { get; }

        protected abstract string MixedDateTimeTemplate { get; set; }

        protected abstract string MixedObjectTemplate { get; set; }

        protected abstract string MixedValueTypeTemplate { get; set; }

        protected abstract string PartialKeyword { get; }
    }
}

