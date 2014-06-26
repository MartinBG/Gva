namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Xml.Schema;

    internal class CSharpTemplate : LanguageBase
    {
        private string[] keywords = new string[] { 
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "Declarations", "default", 
            "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "get", "goto", 
            "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", 
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "set", "short", "sizeof", "stackalloc", "static", "string", "struct", 
            "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "value", "virtual", "void", "while"
         };
        private Hashtable keywordsTable;
        private static string sAttributeAnyTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyAttribute()]
		public System.Xml.XmlAttribute[] {5};";

        private static string sAttributeDateTimeTemplate =
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName=""{2}""{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? {7}{0};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {7}{0}Specified {{ get {{ return {7}{0}.HasValue; }} }}
		
		[XmlIgnore]
		public DateTime? {5}
		{{ 
		get {{ return {7}{0}; }}
		set {{ {7}{0} = value; }}
		}}
		
";

        private static string sAttributeObjectTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName=""{2}""{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {7}{0};
		
		[XmlIgnore]
		public {1} {5}
		{{ 
			get {{ return {7}{0}; }}
			set {{ {7}{0} = value; }}
		}}";

        private static string sAttributeValueTypeTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName=""{2}""{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {7}{0};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {7}{0}Specified;
		
		[XmlIgnore]
		public {1} {5}
		{{ 
			get {{ return {7}{0}; }}
			set {{ {7}{0} = value; {7}{0}Specified = true; }}
		}}";

        private static string sClassEnumerabilityTemplate = "";

        private static string sCollectionClassAbstractTemplate = 
@"	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public {4}class {1}{2} : System.Collections.Generic.List<{0}>
	{{
	}}";

        private static string sCollectionClassTemplate = 
@"	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public {5}class {1}{2} : System.Collections.Generic.List<{0}>
	{{
	}}";

        private static string sElementAnyMaxOccursTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement({6})]
		public System.Xml.XmlElement[] {5};";

        private static string sElementAnyTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement({6})]
		public System.Xml.XmlElement {5};";

        private static string sElementDateTimeTemplate =
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName=""{2}""{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? {7}{0};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {7}{0}Specified {{ get {{ return {7}{0}.HasValue; }} }}
		
		[XmlIgnore]
		public DateTime? {5}
		{{ 
			get {{ return {7}{0}; }}
			set {{ {7}{0} = value; }}
		}}
		
";
        private static string sElementObjectTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName=""{2}"",IsNullable={8}{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {7}{0};
		
		[XmlIgnore]
		public {1} {5}
		{{ 
			get {{ return {7}{0}; }}
			set {{ {7}{0} = value; }}
		}}";

        private static string sElementValueTypeTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName=""{2}"",IsNullable=false{3}{4}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {7}{0};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {7}{0}Specified;
		
		[XmlIgnore]
		public {1} {5}
		{{ 
			get {{ return {7}{0}; }}
			set {{ {7}{0} = value; {7}{0}Specified = true; }}
		}}";

        private static string sFieldAbstractClassTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof({1}),ElementName=""{2}"",IsNullable={7}{3}{5})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {6}{0};
		
		[XmlIgnore]
		public {1} {4}
		{{
			get {{ return {6}{0}; }}
			set {{{6}{0} = value;}}
		}}";

        private static string sFieldClassTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof({1}),ElementName=""{2}"",IsNullable={7}{3}{5})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {1} {6}{0};
		
		[XmlIgnore]
		public {1} {4}
		{{
			get {{return {6}{0};}}
			set {{{6}{0} = value;}}
		}}";

        private static string sFieldCollectionTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof({10}),ElementName=""{2}"",IsNullable={9}{3}{5}{6})]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {0}{8} {7}{4}{8};
		
		[XmlIgnore]
		public {0}{8} {4}{8}
		{{
			get
			{{
				if ({7}{4}{8} == null) {7}{4}{8} = new {0}{8}();
				return {7}{4}{8};
			}}
			set {{{7}{4}{8} = value;}}
		}}";

        private static string sMixedDateTimeTemplate =
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlText(DataType=""{1}"")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? {2}{3};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {2}{3}Specified {{ get {{ return {2}{3}.HasValue; }} }}
		
		[XmlIgnore]
		public DateTime? {3}
		{{ 
			get {{ return {2}{3}; }}
			set {{ {2}{3} = value; }}
		}}
		
";

        private static string sMixedObjectTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlText(DataType=""{1}"")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {0} {2}{3};
		
		[XmlIgnore]
		public {0} {3}
		{{ 
			get {{ return {2}{3}; }}
			set {{ {2}{3} = value; }}
		}}";

        private static string sMixedValueTypeTemplate = 
@"		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlText(DataType=""{1}"")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public {0} {2}{3};
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool {2}{3}Specified;
		
		[XmlIgnore]
		public {0} {3}
		{{ 
			get {{ return {2}{3}; }}
			set {{ {2}{3} = value; {2}{3}Specified = true; }}
		}}";

        public CSharpTemplate()
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
                invalid = "@" + invalid;
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
            string str = "";
            if (isSchemaType)
            {
                if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
                {
                    str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                }
                if (globalElementAndSchemaTypeHaveSameName)
                {
                    outStream.WriteLine("\t[XmlRoot(ElementName=\"{0}\"{1},IsNullable={2}),Serializable]", elementName, str, isElementNullable.ToString().ToLower());
                    outStream.WriteLine("\t[XmlType(TypeName=\"{0}\"{1})]", dotnetClassName, str);
                }
                else
                {
                    outStream.WriteLine("\t[XmlType(TypeName=\"{0}\"{1}),Serializable]\r\n\t[EditorBrowsable(EditorBrowsableState.{2})]", dotnetClassName, str, baseIsAbstract ? "Always" : "Advanced");
                }
            }
            else if (isLocalComplexType)
            {
                if ((elementFormDefault == XmlSchemaForm.Qualified) || (elementFormDefault == XmlSchemaForm.Unqualified))
                {
                    str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                }
                outStream.WriteLine("\t[XmlType(TypeName=\"{0}\"{1}),Serializable]", dotnetClassName, str);
            }
            else
            {
                str = this.CalculateNamespace(base.schemaTargetNamespace, ns, false);
                outStream.WriteLine("\t[XmlRoot(ElementName=\"{0}\"{1},IsNullable={2}),Serializable]", elementName, str, isElementNullable.ToString().ToLower());
            }
            foreach (string str2 in xmlIncludedClasses)
            {
                outStream.WriteLine("\t[XmlInclude(typeof({0}))]", this.CheckForKeywords(str2));
            }
            string str3 = this.CheckForKeywords(dotnetClassName);
            outStream.Write("\tpublic {1}{2}class {0}", str3, isAbstract ? "abstract " : "", LanguageBase.partialClasses ? this.PartialKeyword : "");
            if ((complexTypeBaseClass != null) && (complexTypeBaseClass != ""))
            {
                outStream.Write(" : {0}", this.CheckForKeywords(complexTypeBaseClass));
            }
            outStream.WriteLine();
            outStream.WriteLine("\t{");
            if (enumerableClasses.ContainsKey(dotnetClassName))
            {
                ArrayList list = (ArrayList) enumerableClasses[dotnetClassName];
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
            outStream.WriteLine("\t\tpublic {0}(){1}", this.CheckForKeywords(dotnetClassName), flag ? " : base()" : "");
            outStream.WriteLine("\t\t{");
            for (int i = 0; i < ctorList.Count; i++)
            {
                ClassConstructor constructor = (ClassConstructor) ctorList[i];
                if ((constructor.datatype == CtorDatatypeContext.DateTime) && (!defaultInitialization || (defaultInitialization && !constructor.required)))
                {
                    //angel: DateTime are now nullable and should be left unitialized
                    //outStream.WriteLine("\t\t\t{1}{0} = DateTime.Now;", LanguageBase.ReplaceInvalidChars(constructor.fieldName), LanguageBase.hiddenMemberPrefix);
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
                            //angel: DateTime are now nullable and should be left unitialized
                            //outStream.WriteLine("\t\t\t{0} = DateTime.Now;", LanguageBase.ReplaceInvalidChars(constructor2.fieldName));
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.ValueType)
                        {
                            outStream.WriteLine("\t\t\t{1}{0}Specified = true;", LanguageBase.ReplaceInvalidChars(constructor2.fieldName), LanguageBase.hiddenMemberPrefix);
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.ValueTypeDefault)
                        {
                            outStream.WriteLine("\t\t\t{0} = {1};", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                        }
                        else if (constructor2.datatype == CtorDatatypeContext.String)
                        {
                            if (constructor2.defaultValue == "")
                            {
                                outStream.WriteLine("\t\t\t{0} = string.Empty;", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                            }
                            else
                            {
                                outStream.WriteLine("\t\t\t{0} = \"{1}\";", this.CheckForKeywords(constructor2.fieldName), constructor2.defaultValue);
                            }
                        }
                    }
                }
            }
            outStream.WriteLine("\t\t}");
            if (makeSchemaCompliant)
            {
                outStream.WriteLine();
                outStream.WriteLine("\t\t{0}public void MakeSchemaCompliant()", flag ? this.HideInheritedMethodKeyword : "");
                outStream.WriteLine("\t\t{");
                if (flag)
                {
                    outStream.WriteLine("\t\t\tbase.MakeSchemaCompliant();");
                }
                for (int k = 0; k < ctorList.Count; k++)
                {
                    ClassConstructor constructor3 = (ClassConstructor) ctorList[k];
                    if (constructor3.required)
                    {
                        if (constructor3.datatype == CtorDatatypeContext.PropertyCollection)
                        {
                            outStream.WriteLine("\t\t\tif ({0}{1}.Count == 0) {0}{1}.Add();", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionString)
                        {
                            outStream.WriteLine("\t\t\tif ({0}{1}.Count == 0) {0}{1}.Add(\"\");", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionComplexType)
                        {
                            outStream.WriteLine("\t\t\tif ({0}{1}.Count == 0)", LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t{");
                            outStream.WriteLine("\t\t\t\t{0} _c = {1}{2}.Add();", this.CheckForKeywords(constructor3.fieldName), LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                            outStream.WriteLine("\t\t\t\t_c.MakeSchemaCompliant();");
                            outStream.WriteLine("\t\t\t}");
                            outStream.WriteLine("\t\t\telse foreach ({0} _c in {1}{2}) _c.MakeSchemaCompliant();", this.CheckForKeywords(constructor3.fieldName), LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.PropertyCollectionAbstractComplexType)
                        {
                            outStream.WriteLine("\t\t\tforeach ({0} _c in {1}{2}) _c.MakeSchemaCompliant();", this.CheckForKeywords(constructor3.fieldName), LanguageBase.ReplaceInvalidChars(constructor3.defaultValue), LanguageBase.collectionSuffix);
                        }
                        else if (constructor3.datatype == CtorDatatypeContext.Property)
                        {
                            outStream.WriteLine("\t\t\t{0}.MakeSchemaCompliant();", this.CheckForKeywords(constructor3.defaultValue));
                        }
                    }
                }
                outStream.WriteLine("\t\t}");
            }
            if (depthFirstTraversalHooks)
            {
                outStream.WriteLine();
                outStream.WriteLine("\t\t{0}public static event DepthFirstTraversalDelegate DepthFirstTraversalEvent;", flag ? this.HideInheritedMethodKeyword : "");
                outStream.WriteLine("\t\t{0}public void DepthFirstTraversal(object parent, object context)", flag ? this.HideInheritedMethodKeyword : "");
                outStream.WriteLine("\t\t{");
                outStream.WriteLine("\t\t\tif (DepthFirstTraversalEvent != null) DepthFirstTraversalEvent(this, parent, context);");
                if (flag)
                {
                    outStream.WriteLine("\t\t\tbase.DepthFirstTraversal(parent, context);");
                }
                for (int m = 0; m < ctorList.Count; m++)
                {
                    ClassConstructor constructor4 = (ClassConstructor) ctorList[m];
                    if ((constructor4.datatype == CtorDatatypeContext.PropertyCollectionComplexType) || (constructor4.datatype == CtorDatatypeContext.PropertyCollectionAbstractComplexType))
                    {
                        outStream.WriteLine("\t\t\tif ({0}{1}{2} != null) foreach ({3} _d in {4}{5}{6}) _d.DepthFirstTraversal(this, context);", new object[] { LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue), LanguageBase.collectionSuffix, this.CheckForKeywords(constructor4.fieldName), LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue), LanguageBase.collectionSuffix });
                    }
                    else if (constructor4.datatype == CtorDatatypeContext.Property)
                    {
                        outStream.WriteLine("\t\t\tif ({0}{1} != null) {0}{1}.DepthFirstTraversal(this, context);", LanguageBase.hiddenMemberPrefix, LanguageBase.ReplaceInvalidChars(constructor4.defaultValue));
                    }
                }
                outStream.WriteLine("\t\t}");
            }
            outStream.WriteLine("\t}");
        }

        public override string ConvertSystemDatatype(string systemType)
        {
            switch (systemType)
            {
                case "System.String":
                    return "string";

                case "System.SByte":
                    return "sbyte";

                case "System.Byte":
                    return "byte";

                case "System.Int16":
                    return "short";

                case "System.UInt16":
                    return "ushort";

                case "System.Int32":
                    return "int";

                case "System.UInt32":
                    return "uint";

                case "System.Int64":
                    return "long";

                case "System.UInt64":
                    return "ulong";

                case "System.Single":
                    return "float";

                case "System.Double":
                    return "double";

                case "System.Boolean":
                    return "bool";

                case "System.Decimal":
                    return "decimal";

                case "System.Char":
                    return "char";

                case "System.Object":
                    return "object";

                case "System.Byte[]":
                    return "byte[]";

                case "System.DateTime":
                    return "DateTime";

                case "System.Xml.XmlQualifiedName":
                    return "System.Xml.XmlQualifiedName";

                case "System.Xml.XmlElement":
                    return "System.Xml.XmlElement";

                case "System.Xml.XmlAttribute[]":
                    return "System.Xml.XmlAttribute[]";
            }
            if (systemType.StartsWith("System."))
            {
                return "string";
            }
            return this.CheckForKeywords(systemType);
        }

        public override void NamespaceHeaderCode(StreamWriter outStream, string ns, string schemaFile, Hashtable forwardDeclarations, string targetNamespace, Hashtable enumerations, bool depthFirstTraversalHooks, ArrayList importedReferences)
        {
            base.schemaTargetNamespace = targetNamespace;
            //outStream.WriteLine("// Copyright 2004, Microsoft Corporation");
            //outStream.WriteLine("// Sample Code - Use restricted to terms of use defined in the accompanying license agreement (EULA.doc)");
            //outStream.WriteLine();
            outStream.WriteLine("//--------------------------------------------------------------");
            outStream.WriteLine("// Autogenerated by XSDObjectGen version {0}", base.GetType().Assembly.GetName(false).Version);
            //outStream.WriteLine("// Schema file: {0}", schemaFile);
            //outStream.WriteLine("// Creation Date: {0}", DateTime.Now.ToString());
            outStream.WriteLine("//--------------------------------------------------------------");
            outStream.WriteLine();
            outStream.WriteLine("using System;");
            outStream.WriteLine("using System.Xml.Serialization;");
            outStream.WriteLine("using System.Collections;");
            outStream.WriteLine("using System.Xml.Schema;");
            outStream.WriteLine("using System.ComponentModel;");
            outStream.WriteLine();
            outStream.WriteLine("namespace {0}", ns);
            outStream.WriteLine("{");
            outStream.WriteLine();
            outStream.WriteLine("\tpublic struct Declarations");
            outStream.WriteLine("\t{");
            outStream.WriteLine(string.Format("\t\tpublic const string SchemaVersion = \"{0}\";", targetNamespace));
            outStream.WriteLine("\t}");
            outStream.WriteLine();
            if (depthFirstTraversalHooks)
            {
                outStream.WriteLine("\tpublic delegate void DepthFirstTraversalDelegate(object instance, object parent, object context);");
                outStream.WriteLine();
            }
            foreach (string str in enumerations.Keys)
            {
                outStream.WriteLine("\t[Serializable]");
                outStream.WriteLine("\tpublic enum {0}", str);
                outStream.WriteLine("\t{");
                ArrayList list = (ArrayList) enumerations[str];
                for (int i = 0; i < list.Count; i++)
                {
                    string[] strArray = (string[]) list[i];
                    if (i == (list.Count - 1))
                    {
                        outStream.WriteLine("\t\t[XmlEnum(Name=\"{0}\")] {1}", strArray[0], this.CheckForKeywords(strArray[1]));
                    }
                    else
                    {
                        outStream.WriteLine("\t\t[XmlEnum(Name=\"{0}\")] {1},", strArray[0], this.CheckForKeywords(strArray[1]));
                    }
                }
                outStream.WriteLine("\t}");
                outStream.WriteLine();
            }
            outStream.WriteLine();
        }

        public override void NamespaceTrailerCode(StreamWriter outStream, string ns)
        {
            outStream.WriteLine("}");
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
                return "=";
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
                return "new ";
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
                return "partial ";
            }
        }
    }
}

