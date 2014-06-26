namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Xml.Schema;

    internal class VBTemplate : LanguageBase
    {
        private string[] keywords = new string[] { 
            "AddHandler", "AddressOf", "Alias", "And", "Ansi", "As", "Assembly", "Auto", "Base", "Boolean", "ByRef", "Byte", "ByVal", "Call", "Case", "Catch", 
            "CBool", "CByte", "CChar", "CDate", "CDec", "CDbl", "Char", "CInt", "Class", "CLng", "CObj", "Const", "CShort", "CSng", "CStr", "CType", 
            "Date", "Decimal", "Declare", "Declarations", "Default", "Delegate", "Dim", "Do", "Double", "Each", "Else", "ElseIf", "End", "Enum", "Erase", "Error", 
            "Event", "Exit", "ExternalSource", "False", "Finalize", "Finally", "Float", "For", "Friend", "Function", "Get", "GetType", "Goto", "Handles", "If", "Implements", 
            "Imports", "In", "Inherits", "Integer", "Interface", "Is", "Let", "Lib", "Like", "Long", "Loop", "Me", "Mod", "Module", "MustInherit", "MustOverride", 
            "MyBase", "MyClass", "Namespace", "New", "Next", "Not", "Nothing", "NotInheritable", "NotOverridable", "Object", "On", "Option", "Optional", "Or", "Overloads", "Overridable", 
            "Overrides", "ParamArray", "Preserve", "Private", "Property", "Protected", "Public", "RaiseEvent", "ReadOnly", "ReDim", "Region", "REM", "RemoveHandler", "Resume", "Return", "Select", 
            "Set", "Shadows", "Shared", "Short", "Single", "Static", "Step", "Stop", "String", "Structure", "Sub", "SyncLock", "Then", "Throw", "To", "True", 
            "Try", "TypeOf", "Unicode", "Until", "Volatile", "When", "While", "With", "WithEvents", "WriteOnly", "Xor"
         };
        private Hashtable keywordsTable;
        private static string sAttributeAnyTemplate = "\t\t'*********************** {2} attribute ***********************\r\n\t\t<XmlAnyAttribute()> _\r\n\t\tPublic {5}() As System.Xml.XmlAttribute";
        private static string sAttributeDateTimeTemplate = "\t\t'*********************** {2} attribute ***********************\r\n\t\t<XmlAttribute(AttributeName:=\"{2}\"{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As DateTime\r\n\t\t\r\n\t\t<XmlIgnore, _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0}Specified As Boolean\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As DateTime)\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5}Utc As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{5}Utc = {7}{0}.ToUniversalTime()\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As DateTime)\r\n\t\t\t\t{7}{0} = Value.ToLocalTime()\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sAttributeObjectTemplate = "\t\t'*********************** {2} attribute ***********************\r\n\t\t<XmlAttribute(AttributeName:=\"{2}\"{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As {1}\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sAttributeValueTypeTemplate = "\t\t'*********************** {2} attribute ***********************\r\n\t\t<XmlAttribute(AttributeName:=\"{2}\"{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore, _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0}Specified As Boolean\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As {1}\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sClassEnumerabilityTemplate = "\t\t<System.Runtime.InteropServices.DispIdAttribute(-4)> _\r\n\t\tPublic Function GetEnumerator() As IEnumerator \r\n\t\t\tGetEnumerator = {0}{2}.GetEnumerator()\r\n\t\tEnd Function\r\n\r\n\t\tPublic Function Add(ByVal obj As {1}) As {1}\r\n\t\t\tAdd = {0}{2}.Add(obj)\r\n\t\tEnd Function\r\n\t\t\t\r\n\t\t<XmlIgnore()> _\r\n\t\tDefault Public ReadOnly Property Item(ByVal index As Integer) As {1}\r\n\t\t\tGet\r\n\t\t\t\tItem = {0}{2}(index)\r\n\t\t\tEnd Get\r\n\t\tEnd Property\r\n\r\n\t\t<XmlIgnore()> _\r\n\t\tPublic ReadOnly Property Count() As Integer\r\n\t\t\tGet\r\n                Count = {0}{2}.Count\r\n\t\t\tEnd Get\r\n\t\tEnd Property\r\n\r\n\t\tPublic Sub Clear()\r\n            {0}{2}.Clear()\r\n\t\tEnd Sub\r\n\r\n\t\tPublic Function Remove(ByVal index As Integer) As {1}\r\n\t\t\tDim obj As {1}\r\n\t\t\tobj = {0}{2}(index)\r\n\t\t\tRemove = obj\r\n\t\t\t{0}{2}.Remove(obj)\r\n\t\tEnd Function\r\n\r\n\t\tPublic Sub Remove(ByVal obj As Object)\r\n\t\t\t{0}{2}.Remove(obj)\r\n\t\tEnd Sub";
        private static string sCollectionClassAbstractTemplate = "\t<Serializable, _\r\n\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t{4}Public Class {1}{2}\r\n\t\tInherits ArrayList\r\n\r\n\t\tPublic Shadows Function Add(obj As {0}) As {0}\r\n\t\t\tMyBase.Add(obj)\r\n\t\t\tAdd = obj\r\n\t\tEnd Function\r\n\r\n\t\tPublic Shadows Sub Insert(index As Integer, obj As {0})\r\n\t\t\tMyBase.Insert(index, obj)\r\n\t\tEnd Sub\r\n\r\n\t\tPublic Shadows Sub Remove(obj As {0})\r\n\t\t\tMyBase.Remove(obj)\r\n\t\tEnd Sub\r\n\r\n\t\tDefault Public Shadows Property Item(ByVal index As Integer) As {0}\r\n            Get\r\n                Item = DirectCast(MyBase.Item(index), {0})\r\n            End Get\r\n            Set(ByVal Value As {0})\r\n                MyBase.Item(index) = Value\r\n            End Set\r\n        End Property\r\n\tEnd Class";
        private static string sCollectionClassTemplate = "\t<Serializable, _\r\n\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t{5}Public Class {1}{2}\r\n\t\tInherits ArrayList\r\n\r\n\t\tPublic Shadows Function Add(obj As {0}) As {0}\r\n\t\t\tMyBase.Add(obj)\r\n\t\t\tAdd = obj\r\n\t\tEnd Function\r\n\r\n\t\tPublic Shadows Function Add() As {0}\r\n            Add = Add(New {4})\r\n        End Function\r\n\r\n\t\tPublic Shadows Sub Insert(index As Integer, obj As {0})\r\n\t\t\tMyBase.Insert(index, obj)\r\n\t\tEnd Sub\r\n\r\n\t\tPublic Shadows Sub Remove(obj As {0})\r\n\t\t\tMyBase.Remove(obj)\r\n\t\tEnd Sub\r\n\r\n\t\tDefault Public Shadows Property Item(ByVal index As Integer) As {0}\r\n            Get\r\n                Item = DirectCast(MyBase.Item(index), {0})\r\n            End Get\r\n            Set(ByVal Value As {0})\r\n                MyBase.Item(index) = Value\r\n            End Set\r\n        End Property\r\n\tEnd Class";
        private static string sElementAnyMaxOccursTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlAnyElement({6})> _\r\n\t\tPublic {5}() As System.Xml.XmlElement";
        private static string sElementAnyTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlAnyElement({6})> _\r\n\t\tPublic {5} As System.Xml.XmlElement";
        private static string sElementDateTimeTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(ElementName:=\"{2}\",IsNullable:=False{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As DateTime\r\n\t\t\r\n\t\t<XmlIgnore, _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0}Specified As Boolean\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As DateTime)\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5}Utc As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{5}Utc = {7}{0}.ToUniversalTime()\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As DateTime)\r\n\t\t\t\t{7}{0} = Value.ToLocalTime()\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sElementObjectTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(ElementName:=\"{2}\",IsNullable:={8}{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As {1}\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sElementValueTypeTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(ElementName:=\"{2}\",IsNullable:=False{3}{4}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore, _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{0}Specified As Boolean\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {5} As {1}\r\n\t\t\tGet\r\n\t\t\t\t{5} = {7}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{7}{0} = Value\r\n\t\t\t\t{7}{0}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sFieldAbstractClassTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(Type:=GetType({1}),ElementName:=\"{2}\",IsNullable:={7}{3}{5}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {6}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {4} As {1}\r\n\t\t\tGet\r\n\t\t\t\t{4} = {6}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{6}{0} = Value\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sFieldClassTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(Type:=GetType({1}),ElementName:=\"{2}\",IsNullable:={7}{3}{5}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {6}{0} As {1}\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {4} As {1}\r\n\t\t\tGet\r\n\t\t\t\tIf {6}{0} Is Nothing Then {6}{0} = new {1}()\r\n\t\t\t\t{4} = {6}{0}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {1})\r\n\t\t\t\t{6}{0} = Value\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sFieldCollectionTemplate = "\t\t'*********************** {2} element ***********************\r\n\t\t<XmlElement(Type:=GetType({10}),ElementName:=\"{2}\",IsNullable:={9}{3}{5}{6}), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {7}{4}{8} As {0}{8}\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {4}{8} As {0}{8}\r\n\t\t\tGet\r\n\t\t\t\tIf {7}{4}{8} Is Nothing Then {7}{4}{8} = new {0}{8}()\r\n\t\t\t\t{4}{8} = {7}{4}{8}\r\n\t\t\tEnd Get\r\n\t\t\tSet(Value As {0}{8})\r\n\t\t\t\t{7}{4}{8} = Value\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sMixedDateTimeTemplate = "\t\t'*********************** XmlText field ***********************\r\n\t\t<XmlText(DataType:=\"{1}\"), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {2}{3} As DateTime\r\n\t\t\r\n\t\t<XmlIgnore, _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {2}{3}Specified As Boolean\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {3}() As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{3} = {2}{3}\r\n\t\t\tEnd Get\r\n\t\t\tSet(val As DateTime)\r\n\t\t\t\t{2}{3} = val\r\n\t\t\t\t{2}{3}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property\r\n\t\t\r\n\t\t<XmlIgnore> _\r\n\t\tPublic Property {3}Utc As DateTime\r\n\t\t\tGet\r\n\t\t\t\t{3}Utc = {2}{3}.ToUniversalTime()\r\n\t\t\tEnd Get\r\n\t\t\tSet(val As DateTime)\r\n\t\t\t\t{2}{3} = val.ToLocalTime()\r\n\t\t\t\t{2}{3}Specified = True\r\n\t\t\tEnd Set\r\n\t\tEnd Property";
        private static string sMixedObjectTemplate = "\t\t'*********************** XmlText field ***********************\r\n\t\t<XmlText(DataType:=\"{1}\"), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {2}{3} As {0}\r\n        \r\n\t\t<XmlIgnore()> _\r\n        Public Property {3}() As {0}\r\n            Get\r\n                {3} = {2}{3}\r\n            End Get\r\n            Set(ByVal val As {0})\r\n                {2}{3} = val\r\n            End Set\r\n        End Property";
        private static string sMixedValueTypeTemplate = "\t\t'*********************** XmlText field ***********************\r\n\t\t<XmlText(DataType:=\"{1}\"), _\r\n\t\tEditorBrowsable(EditorBrowsableState.Advanced)> _\r\n\t\tPublic {2}{3} As {0}\r\n        \r\n\t\t<XmlIgnore(), _\r\n        EditorBrowsable(EditorBrowsableState.Advanced)> _\r\n        Public {2}{3}Specified As Boolean\r\n        \r\n\t\t<XmlIgnore()> _\r\n        Public Property {3}() As {0}\r\n            Get\r\n                {3} = {2}{3}\r\n            End Get\r\n            Set(ByVal val As {0})\r\n                {2}{3} = val\r\n                {2}{3}Specified = True\r\n            End Set\r\n        End Property";

        public VBTemplate()
        {
            this.keywordsTable = new Hashtable(this.keywords.Length);
            for (int i = 0; i < this.keywords.Length; i++)
            {
                this.keywordsTable.Add(this.keywords[i].ToLower(), "");
            }
        }

        public override string CheckForKeywords(string keyword)
        {
            string invalid = keyword;
            string[] strArray = keyword.Split(new char[] { '.' });
            if (((strArray != null) && (strArray.Length >= 2)) && !keyword.StartsWith("System."))
            {
                invalid = strArray[strArray.Length - 1];
            }
            invalid = LanguageBase.ReplaceInvalidChars(invalid);
            if (this.keywordsTable.ContainsKey(invalid.ToLower()))
            {
                invalid = "[" + invalid + "]";
            }
            if (((strArray == null) || (strArray.Length < 2)) || invalid.StartsWith("System."))
            {
                return invalid;
            }
            string str2 = "";
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                str2 = str2 + strArray[i] + ".";
            }
            return (str2 + invalid);
        }

        public override void ClassHeaderCode(StreamWriter outStream, string dotnetClassName, string elementName, string complexTypeBaseClass, bool baseIsAbstract, bool isSchemaType, bool isAbstract, bool isLocalComplexType, Hashtable enumerableClasses, string ns, XmlSchemaForm elementFormDefault, string annotation, bool isElementNullable, ArrayList xmlIncludedClasses, bool globalElementAndSchemaTypeHaveSameName)
        {
            outStream.WriteLine();
            outStream.WriteLine();
            outStream.WriteLine("\t'--------------------------------------------------");
            outStream.WriteLine("\t'{0} {1}", (isSchemaType || isLocalComplexType) ? dotnetClassName : elementName, isSchemaType ? "type" : "element");
            outStream.WriteLine("\t'--------------------------------------------------");
            string str = "";
            if (isSchemaType)
            {
                if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
                {
                    str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                }
                if (globalElementAndSchemaTypeHaveSameName)
                {
                    str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                    outStream.Write("\t<XmlRoot(ElementName:=\"{0}\"{1},IsNullable:={2}),Serializable, _", elementName, str, isElementNullable.ToString());
                    outStream.Write("\n\tXmlType(TypeName:=\"{0}\"{1})", dotnetClassName, str);
                }
                else
                {
                    outStream.Write("\t<XmlType(TypeName:=\"{0}\"{1}),Serializable, _\n\tEditorBrowsable(EditorBrowsableState.{2})", dotnetClassName, str, baseIsAbstract ? "Always" : "Advanced");
                }
            }
            else if (isLocalComplexType)
            {
                if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
                {
                    str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                }
                outStream.Write("\t<XmlType(TypeName:=\"{0}\"{1}),Serializable", dotnetClassName, str);
            }
            else
            {
                str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                outStream.Write("\t<XmlRoot(ElementName:=\"{0}\"{1},IsNullable:={2}),Serializable", elementName, str, isElementNullable.ToString());
            }
            if (xmlIncludedClasses.Count > 0)
            {
                outStream.WriteLine(", _");
            }
            for (int i = 0; i < xmlIncludedClasses.Count; i++)
            {
                outStream.Write("\tXmlInclude(GetType({0}))", this.CheckForKeywords((string) xmlIncludedClasses[i]));
                if ((i + 1) < xmlIncludedClasses.Count)
                {
                    outStream.WriteLine(", _");
                }
            }
            outStream.WriteLine("> _");
            string str2 = this.CheckForKeywords(dotnetClassName);
            outStream.WriteLine("\t{2}Public {1}Class {0}", str2, isAbstract ? "MustInherit " : "", LanguageBase.partialClasses ? this.PartialKeyword : "");
            if ((complexTypeBaseClass != null) && (complexTypeBaseClass != ""))
            {
                outStream.WriteLine("\t\tInherits {0}", this.CheckForKeywords(complexTypeBaseClass));
            }
            if (enumerableClasses.ContainsKey(dotnetClassName))
            {
                ArrayList list = (ArrayList) enumerableClasses[dotnetClassName];
                outStream.WriteLine();
                string invalid = (string) list[0];
                invalid = LanguageBase.ReplaceInvalidChars(invalid);
                outStream.WriteLine(this.ClassEnumerabilityTemplate, new object[] { invalid, this.ConvertSystemDatatype((string) list[1]), LanguageBase.collectionSuffix, LanguageBase.hiddenMemberPrefix });
            }
        }

        public override void ClassTrailerCode(StreamWriter outStream, string dotnetClassName, ArrayList ctorList, bool defaultInitialization, bool depthFirstTraversalHooks, bool makeSchemaCompliant, string complexTypeBaseClass, bool mixed, string mixedXsdType)
        {
            if (mixed)
            {
                string str;
                outStream.WriteLine();
                if (mixedXsdType.StartsWith("System."))
                {
                    str = mixedXsdType;
                    mixedXsdType = this.ConvertSystemDatatype(mixedXsdType);
                }
                else
                {
                    str = base.FrameworkTypeMapping(mixedXsdType);
                }
                if (str == "System.DateTime")
                {
                    outStream.WriteLine(this.MixedDateTimeTemplate, new object[] { this.ConvertSystemDatatype(str), mixedXsdType, LanguageBase.hiddenMemberPrefix, LanguageBase.mixedElementFieldName });
                }
                else if (base.IsValueType(str))
                {
                    outStream.WriteLine(this.MixedValueTypeTemplate, new object[] { this.ConvertSystemDatatype(str), mixedXsdType, LanguageBase.hiddenMemberPrefix, LanguageBase.mixedElementFieldName });
                }
                else
                {
                    outStream.WriteLine(this.MixedObjectTemplate, new object[] { this.ConvertSystemDatatype(str), mixedXsdType, LanguageBase.hiddenMemberPrefix, LanguageBase.mixedElementFieldName });
                }
            }
            bool flag = (complexTypeBaseClass != null) && (complexTypeBaseClass != "");
            outStream.WriteLine();
            outStream.WriteLine("\t\t'*********************** Constructor ***********************");
            outStream.WriteLine("\t\tPublic Sub New()");
            if (flag)
            {
                outStream.WriteLine("\t\t\tMyBase.New()");
            }
            for (int i = 0; i < ctorList.Count; i++)
            {
                ClassConstructor constructor = (ClassConstructor) ctorList[i];
                if ((constructor.datatype == CtorDatatypeContext.DateTime) && (!defaultInitialization || (defaultInitialization && !constructor.required)))
                {
                    outStream.WriteLine("\t\t\t{1}{0} = DateTime.Now", LanguageBase.ReplaceInvalidChars(constructor.fieldName), LanguageBase.hiddenMemberPrefix);
                }
            }
            if (defaultInitialization)
            {
                for (int j = 0; j < ctorList.Count; j++)
                {
                    ClassConstructor constructor2 = (ClassConstructor) ctorList[j];
                    if ((constructor2.required && (((constructor2.datatype != CtorDatatypeContext.PropertyCollection) && (constructor2.datatype != CtorDatatypeContext.PropertyCollectionString)) && (constructor2.datatype != CtorDatatypeContext.PropertyCollectionComplexType))) && ((constructor2.datatype != CtorDatatypeContext.PropertyCollectionAbstractComplexType) && (constructor2.datatype != CtorDatatypeContext.Property)))
                    {
                        if (constructor2.datatype == CtorDatatypeContext.DateTime)
                        {
                            outStream.WriteLine("\t\t\t{0} = DateTime.Now", LanguageBase.ReplaceInvalidChars(constructor2.fieldName));
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.ValueType)
                        {
                            outStream.WriteLine("\t\t\t{1}{0}Specified = True", LanguageBase.ReplaceInvalidChars(constructor2.fieldName), LanguageBase.hiddenMemberPrefix);
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.ValueTypeDefault)
                        {
                            outStream.WriteLine("\t\t\t{0} = {1}", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.String)
                        {
                            if (constructor2.defaultValue == "")
                            {
                                outStream.WriteLine("\t\t\t{0} = String.Empty", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                            }
                            else
                            {
                                outStream.WriteLine("\t\t\t{0} = \"{1}\"", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                            }
                        }
                    }
                }
            }
            outStream.WriteLine("\t\tEnd Sub");
            if (makeSchemaCompliant)
            {
                outStream.WriteLine();
                outStream.WriteLine("\t\t'*********************** MakeSchemaCompliant ***********************");
                outStream.WriteLine("\t\tPublic {0}Sub MakeSchemaCompliant()", flag ? this.HideInheritedMethodKeyword : "");
                if (flag)
                {
                    outStream.WriteLine("\t\t\tMyBase.MakeSchemaCompliant()");
                }
                for (int k = 0; k < ctorList.Count; k++)
                {
                    ClassConstructor constructor3 = (ClassConstructor) ctorList[k];
                    if (constructor3.required)
                    {
                        if (constructor3.datatype == CtorDatatypeContext.PropertyCollection)
                        {
                            outStream.WriteLine("\t\t\tIf {0}{1}.Count = 0 Then {0}{1}.Add()", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionString)
                        {
                            outStream.WriteLine("\t\t\tIf {0}{1}.Count = 0 Then {0}{1}.Add(\"\")", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionComplexType)
                        {
                            outStream.WriteLine("\t\t\tIf {0}{1}.Count = 0 Then", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t\tDim _c As {1} = {0}{2}.Add()", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), this.CheckForKeywords(constructor3.fieldName), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t\t_c.MakeSchemaCompliant()");
                            outStream.WriteLine("\t\t\tElse");
                            outStream.WriteLine("\t\t\t\tFor Each _c as {0} in {1}{2}", this.CheckForKeywords(constructor3.fieldName), LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t\t\t_c.MakeSchemaCompliant()");
                            outStream.WriteLine("\t\t\t\tNext");
                            outStream.WriteLine("\t\t\tEnd If");
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionAbstractComplexType)
                        {
                            outStream.WriteLine("\t\t\tFor Each _c as {0} in {1}{2}", this.CheckForKeywords(constructor3.fieldName), LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t\t_c.MakeSchemaCompliant()");
                            outStream.WriteLine("\t\t\tNext");
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.Property)
                        {
                            outStream.WriteLine("\t\t\t{0}.MakeSchemaCompliant()", this.CheckForKeywords(constructor3.defaultValue));
                        }
                    }
                }
                outStream.WriteLine("\t\tEnd Sub");
            }
            if (depthFirstTraversalHooks)
            {
                outStream.WriteLine();
                outStream.WriteLine("\t\t'*********************** DepthFirstTraversal Event ***********************");
                outStream.WriteLine("\t\tPublic Shared {0}Event DepthFirstTraversalEvent As DepthFirstTraversalDelegate", flag ? this.HideInheritedMethodKeyword : "");
                outStream.WriteLine("\t\tPublic {0}Sub DepthFirstTraversal(parent As Object, context As Object)", flag ? this.HideInheritedMethodKeyword : "");
                outStream.WriteLine("\t\t\tRaiseEvent DepthFirstTraversalEvent(Me, parent, context)");
                if (flag)
                {
                    outStream.WriteLine("\t\t\tMyBase.DepthFirstTraversal(parent, context)");
                }
                for (int m = 0; m < ctorList.Count; m++)
                {
                    ClassConstructor constructor4 = (ClassConstructor) ctorList[m];
                    if ((constructor4.datatype == CtorDatatypeContext.PropertyCollectionComplexType) || (constructor4.datatype == CtorDatatypeContext.PropertyCollectionAbstractComplexType))
                    {
                        outStream.WriteLine("\t\t\tIf Not({0}{1}{2} Is Nothing) Then", LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue), LanguageBase.collectionSuffix);
                        outStream.WriteLine("\t\t\t\tFor Each _d As {3} in {0}{1}{2}", new object[] { LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue), LanguageBase.collectionSuffix, this.CheckForKeywords(constructor4.fieldName) });
                        outStream.WriteLine("\t\t\t\t\t_d.DepthFirstTraversal(Me, context)");
                        outStream.WriteLine("\t\t\t\tNext");
                        outStream.WriteLine("\t\t\tEnd If");
                    }
                    else if (constructor4.datatype == CtorDatatypeContext.Property)
                    {
                        outStream.WriteLine("\t\t\tIf Not({0}{1} is Nothing) Then {0}{1}.DepthFirstTraversal(Me, context)", LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue));
                    }
                }
                outStream.WriteLine("\t\tEnd Sub");
            }
            outStream.WriteLine("\tEnd Class");
        }

        public override string ConvertSystemDatatype(string systemType)
        {
            switch (systemType)
            {
                case "System.String":
                    return "String";

                case "System.SByte":
                    return "SByte";

                case "System.Byte":
                    return "Byte";

                case "System.Int16":
                    return "Short";

                case "System.UInt16":
                    return "UInt16";

                case "System.Int32":
                    return "Integer";

                case "System.UInt32":
                    return "UInt32";

                case "System.Int64":
                    return "Long";

                case "System.UInt64":
                    return "UInt64";

                case "System.Single":
                    return "Single";

                case "System.Double":
                    return "Double";

                case "System.Boolean":
                    return "Boolean";

                case "System.Decimal":
                    return "Decimal";

                case "System.Char":
                    return "Char";

                case "System.Object":
                    return "Object";

                case "System.Byte[]":
                    return "Byte()";

                case "System.DateTime":
                    return "DateTime";

                case "System.Xml.XmlQualifiedName":
                    return "System.Xml.XmlQualifiedName";

                case "System.Xml.XmlElement":
                    return "System.Xml.XmlElement";

                case "System.Xml.XmlAttribute[]":
                    return "System.Xml.XmlAttribute()";
            }
            if (systemType.StartsWith("System."))
            {
                return "String";
            }
            return this.CheckForKeywords(systemType);
        }

        public override void NamespaceHeaderCode(StreamWriter outStream, string ns, string schemaFile, Hashtable forwardDeclarations, string targetNamespace, Hashtable enumerations, bool depthFirstTraversalHooks, ArrayList importedReferences)
        {
            base.schemaTargetNamespace = targetNamespace;
            //outStream.WriteLine("' Copyright 2004, Microsoft Corporation");
            //outStream.WriteLine("' Sample Code - Use restricted to terms of use defined in the accompanying license agreement (EULA.doc)");
            //outStream.WriteLine();
            outStream.WriteLine("'--------------------------------------------------------------");
            outStream.WriteLine("' Autogenerated by XSDObjectGen version {0}", base.GetType().Assembly.GetName(false).Version);
            //outStream.WriteLine("' Schema file: {0}", schemaFile);
            //outStream.WriteLine("' Creation Date: {0}", DateTime.Now.ToString());
            outStream.WriteLine("'--------------------------------------------------------------");
            outStream.WriteLine();
            outStream.WriteLine("Imports System");
            outStream.WriteLine("Imports System.Xml.Serialization");
            outStream.WriteLine("Imports System.Collections");
            outStream.WriteLine("Imports System.Xml.Schema");
            outStream.WriteLine("Imports System.ComponentModel");
            outStream.WriteLine();
            if (ns != "")
            {
                outStream.WriteLine("Namespace {0}", ns);
            }
            outStream.WriteLine();
            outStream.WriteLine("\tPublic Module Declarations");
            outStream.WriteLine(string.Format("\t\tPublic Const SchemaVersion As String = \"{0}\"", targetNamespace));
            outStream.WriteLine("\tEnd Module");
            outStream.WriteLine();
            if (depthFirstTraversalHooks)
            {
                outStream.WriteLine("\tPublic Delegate Sub DepthFirstTraversalDelegate(instance As Object, parent As Object, context As Object)");
                outStream.WriteLine();
            }
            foreach (string str in enumerations.Keys)
            {
                outStream.WriteLine("\t<Serializable> _");
                outStream.WriteLine("\tPublic Enum {0}", str);
                ArrayList list = (ArrayList) enumerations[str];
                for (int i = 0; i < list.Count; i++)
                {
                    string[] strArray = (string[]) list[i];
                    outStream.WriteLine("\t\t<XmlEnum(Name:=\"{0}\")> {1}", strArray[0], this.CheckForKeywords(strArray[1]));
                }
                outStream.WriteLine("\tEnd Enum");
                outStream.WriteLine();
            }
            outStream.WriteLine();
        }

        public override void NamespaceTrailerCode(StreamWriter outStream, string ns)
        {
            if (ns != "")
            {
                outStream.WriteLine("End Namespace");
            }
        }

        protected override string AttributeAnyTemplate
        {
            get
            {
                return sAttributeAnyTemplate;
            }
            set
            {
                sAttributeAnyTemplate = value;
            }
        }

        protected override string AttributeAssignmentOperator
        {
            get
            {
                return ":=";
            }
        }

        protected override string AttributeDateTimeTemplate
        {
            get
            {
                return sAttributeDateTimeTemplate;
            }
            set
            {
                sAttributeDateTimeTemplate = value;
            }
        }

        protected override string AttributeObjectTemplate
        {
            get
            {
                return sAttributeObjectTemplate;
            }
            set
            {
                sAttributeObjectTemplate = value;
            }
        }

        protected override string AttributeValueTypeTemplate
        {
            get
            {
                return sAttributeValueTypeTemplate;
            }
            set
            {
                sAttributeValueTypeTemplate = value;
            }
        }

        protected override string ClassEnumerabilityTemplate
        {
            get
            {
                return sClassEnumerabilityTemplate;
            }
            set
            {
                sClassEnumerabilityTemplate = value;
            }
        }

        protected override string CollectionClassAbstractTemplate
        {
            get
            {
                return sCollectionClassAbstractTemplate;
            }
            set
            {
                sCollectionClassAbstractTemplate = value;
            }
        }

        protected override string CollectionClassTemplate
        {
            get
            {
                return sCollectionClassTemplate;
            }
            set
            {
                sCollectionClassTemplate = value;
            }
        }

        protected override string ElementAnyMaxOccursTemplate
        {
            get
            {
                return sElementAnyMaxOccursTemplate;
            }
            set
            {
                sElementAnyMaxOccursTemplate = value;
            }
        }

        protected override string ElementAnyTemplate
        {
            get
            {
                return sElementAnyTemplate;
            }
            set
            {
                sElementAnyTemplate = value;
            }
        }

        protected override string ElementDateTimeTemplate
        {
            get
            {
                return sElementDateTimeTemplate;
            }
            set
            {
                sElementDateTimeTemplate = value;
            }
        }

        protected override string ElementObjectTemplate
        {
            get
            {
                return sElementObjectTemplate;
            }
            set
            {
                sElementObjectTemplate = value;
            }
        }

        protected override string ElementValueTypeTemplate
        {
            get
            {
                return sElementValueTypeTemplate;
            }
            set
            {
                sElementValueTypeTemplate = value;
            }
        }

        protected override string FieldAbstractClassTemplate
        {
            get
            {
                return sFieldAbstractClassTemplate;
            }
            set
            {
                sFieldAbstractClassTemplate = value;
            }
        }

        protected override string FieldClassTemplate
        {
            get
            {
                return sFieldClassTemplate;
            }
            set
            {
                sFieldClassTemplate = value;
            }
        }

        protected override string FieldCollectionTemplate
        {
            get
            {
                return sFieldCollectionTemplate;
            }
            set
            {
                sFieldCollectionTemplate = value;
            }
        }

        protected override string HideInheritedMethodKeyword
        {
            get
            {
                return "Shadows ";
            }
        }

        protected override string MixedDateTimeTemplate
        {
            get
            {
                return sMixedDateTimeTemplate;
            }
            set
            {
                sMixedDateTimeTemplate = value;
            }
        }

        protected override string MixedObjectTemplate
        {
            get
            {
                return sMixedObjectTemplate;
            }
            set
            {
                sMixedObjectTemplate = value;
            }
        }

        protected override string MixedValueTypeTemplate
        {
            get
            {
                return sMixedValueTypeTemplate;
            }
            set
            {
                sMixedValueTypeTemplate = value;
            }
        }

        protected override string PartialKeyword
        {
            get
            {
                return "Partial ";
            }
        }
    }
}

