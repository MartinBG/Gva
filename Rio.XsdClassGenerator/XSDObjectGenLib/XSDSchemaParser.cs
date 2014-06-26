#pragma warning disable 618

namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    public class XSDSchemaParser
    {
        private XmlSchemaForm attributeFormDefault;
        private Hashtable classesReferencingAbstractTypes;
        private LanguageBase code;
        private Hashtable collectionClasses;
        private XmlSchemaForm elementFormDefault;
        private Hashtable enumerableClasses;
        private Hashtable enumerations;
        private Hashtable globalComplexTypeClasses;
        private Hashtable globalQualifiedAbstractTypeClasses;
        private Hashtable globalQualifiedComplexTypeClasses;
        private Hashtable namespaces = new Hashtable();
        private ArrayList namespacesList = new ArrayList();
        private bool optionConstructRequiredSchema = false;
        private bool optionDefaultInitialization = false;
        private bool optionDepthFirstTraversalHooks = false;
        private string[] outFiles;
        private StreamWriter outStream;
        private XmlSchema schema;
        private Hashtable xsdNsToClrNs = new Hashtable();

        private string AddQualifiedNamespaceReference(string elementName, string typeNs, string currentNs, GlobalXsdType xsdType)
        {
            GlobalSchemaType type = null;
            if ((typeNs == null) || (typeNs == ""))
            {
                typeNs = currentNs;
            }
            switch (xsdType)
            {
                case GlobalXsdType.ComplexType:
                    type = (GlobalSchemaType) Globals.globalSchemaTypeTable[typeNs + "&&" + elementName];
                    break;

                case GlobalXsdType.Element:
                    type = (GlobalSchemaType) Globals.globalSchemaTypeTable[typeNs + "$$" + elementName];
                    break;

                case GlobalXsdType.Enum:
                    type = (GlobalSchemaType) Globals.globalSchemaTypeTable[typeNs + "%%" + elementName];
                    break;
            }
            if (type == null)
            {
                return LanguageBase.ReplaceInvalidChars(elementName);
            }
            return (type.ClrNamespace + "." + type.ClrTypeName);
        }

        private void BuildConstructorList(string defaultValue, string fixedValue, bool required, decimal maxOccurs, string fieldName, string clrTypeName, string elmtAtrrName, ArrayList ctorList, bool isEnum)
        {
            ClassConstructor constructor = new ClassConstructor();
            if ((defaultValue != null) || (fixedValue != null))
            {
                constructor.required = true;
                if (maxOccurs > 1M)
                {
                    constructor.defaultValue = elmtAtrrName;
                    constructor.fieldName = fieldName;
                    if (clrTypeName == "System.String")
                    {
                        constructor.datatype = CtorDatatypeContext.PropertyCollectionString;
                    }
                    else
                    {
                        constructor.datatype = CtorDatatypeContext.PropertyCollection;
                    }
                }
                else if (isEnum)
                {
                    constructor.defaultValue = (defaultValue != null) ? defaultValue : fixedValue;
                    constructor.defaultValue = clrTypeName + "." + constructor.defaultValue;
                    constructor.fieldName = fieldName;
                    constructor.datatype = CtorDatatypeContext.ValueTypeDefault;
                }
                else if (this.code.IsValueType(clrTypeName))
                {
                    constructor.defaultValue = (defaultValue != null) ? defaultValue : fixedValue;
                    constructor.fieldName = fieldName;
                    constructor.datatype = CtorDatatypeContext.ValueTypeDefault;
                }
                else if (clrTypeName == "System.String")
                {
                    constructor.defaultValue = (defaultValue != null) ? defaultValue : fixedValue;
                    constructor.fieldName = fieldName;
                    constructor.datatype = CtorDatatypeContext.String;
                }
                ctorList.Add(constructor);
            }
            else
            {
                constructor.required = required;
                if (maxOccurs > 1M)
                {
                    constructor.defaultValue = elmtAtrrName;
                    constructor.fieldName = fieldName;
                    if (clrTypeName == "System.String")
                    {
                        constructor.datatype = CtorDatatypeContext.PropertyCollectionString;
                    }
                    else
                    {
                        constructor.datatype = CtorDatatypeContext.PropertyCollection;
                    }
                }
                else
                {
                    constructor.defaultValue = "";
                    constructor.fieldName = fieldName;
                    if (isEnum)
                    {
                        constructor.datatype = CtorDatatypeContext.ValueType;
                    }
                    else if (clrTypeName == "System.DateTime")
                    {
                        constructor.datatype = CtorDatatypeContext.DateTime;
                    }
                    else if (this.code.IsValueType(clrTypeName))
                    {
                        constructor.datatype = CtorDatatypeContext.ValueType;
                    }
                    else if (clrTypeName == "System.String")
                    {
                        constructor.datatype = CtorDatatypeContext.String;
                    }
                    else
                    {
                        constructor.datatype = CtorDatatypeContext.Other;
                    }
                }
                ctorList.Add(constructor);
            }
        }

        private string CalculateAnyNamespace(string ns, string parentNamespace)
        {
            switch (ns)
            {
                case "":
                    return "";

                case "##local":
                    return parentNamespace;

                case "##other":
                    return "";

                case null:
                    return "";
            }
            if (ns.StartsWith("##"))
            {
                return "";
            }
            return ns;
        }

        private string CalculateNestedChildTypeName(string name, Hashtable table, ArrayList parentClassStack)
        {
            int num = 2;
            string key = name;
            if (table.ContainsKey(key))
            {
                for (int i = parentClassStack.Count - 1; i >= 0; i--)
                {
                    key = ((string) parentClassStack[i]) + key;
                    if (!table.ContainsKey(key))
                    {
                        break;
                    }
                }
            }
            while (table.ContainsKey(key))
            {
                key = name + num.ToString();
                num++;
            }
            return key;
        }

        private string CalculateUniqueTypeOrFieldName(string name, string ns, Hashtable table)
        {
            for (int i = 2; table.ContainsKey(name) || (name == "docExtension"); i++)
            {
                name = name + i.ToString();
            }
            return name;
        }

        public string[] Execute(string xsdFile, Language language, string genNamespace, string fileName, string outputLocation, bool constructRequiredSchema, bool depthFirstTraversalHooks, bool defaultInitialization, ref Hashtable namespaceTable, Hashtable filenameTable, bool partialKeyword)
        {
            FileStream stream = null;
            string[] outFiles;
            try
            {
                stream = new FileStream(xsdFile, FileMode.Open, FileAccess.Read);
                if (stream == null)
                {
                    throw new XSDObjectGenException("Could not open the XSD schema file: " + xsdFile);
                }
                this.schema = XmlSchema.Read(stream, new ValidationEventHandler(XSDSchemaParser.ShowCompileError));
                this.schema.Compile(new ValidationEventHandler(XSDSchemaParser.ShowCompileError));
                this.elementFormDefault = this.schema.ElementFormDefault;
                this.attributeFormDefault = this.schema.AttributeFormDefault;
                if (language == Language.VB)
                {
                    this.code = new VBTemplate();
                }
                else
                {
                    if (language != Language.CS)
                    {
                        throw new XSDObjectGenException(string.Format("Language {0} not supported.", language.ToString()));
                    }
                    this.code = new CSharpTemplate();
                }
                this.optionDefaultInitialization = defaultInitialization;
                this.optionConstructRequiredSchema = constructRequiredSchema;
                this.optionDepthFirstTraversalHooks = depthFirstTraversalHooks;
                LanguageBase.partialClasses = partialKeyword;
                this.collectionClasses = new Hashtable();
                this.globalComplexTypeClasses = new Hashtable();
                this.globalQualifiedComplexTypeClasses = new Hashtable();
                this.globalQualifiedAbstractTypeClasses = new Hashtable();
                this.enumerations = new Hashtable();
                this.enumerableClasses = new Hashtable();
                this.classesReferencingAbstractTypes = new Hashtable();
                ArrayList parentClassStack = new ArrayList();
                Globals.globalSchemaTypeTable.Clear();
                Globals.globalSeparateImportedNamespaces = false;
                Globals.globalClrNamespaceList.Clear();
                ArrayList list2 = new ArrayList();
                if (namespaceTable != null)
                {
                    if (this.schema.TargetNamespace != null)
                    {
                        genNamespace = (string) namespaceTable[this.schema.TargetNamespace];
                    }
                    else
                    {
                        genNamespace = (string) namespaceTable[""];
                    }
                }
                if (this.schema.TargetNamespace != null)
                {
                    this.namespaces.Add(this.schema.TargetNamespace, list2);
                    this.namespacesList.Add(this.schema.TargetNamespace);
                    this.xsdNsToClrNs.Add(this.schema.TargetNamespace, genNamespace);
                }
                else
                {
                    this.namespaces.Add("", list2);
                    this.namespacesList.Add("");
                    this.xsdNsToClrNs.Add("", genNamespace);
                }
                this.RecurseImportedNamespaces(this.schema, list2);
                if ((this.namespacesList.Count > 1) && (namespaceTable == null))
                {
                    namespaceTable = this.xsdNsToClrNs;
                    return null;
                }
                if (namespaceTable != null)
                {
                    this.xsdNsToClrNs = namespaceTable;
                }
                foreach (string str in this.xsdNsToClrNs.Values)
                {
                    Globals.globalClrNamespaceList.Add(str);
                }
                foreach (XmlSchemaType type in this.schema.SchemaTypes.Values)
                {
                    if (type is XmlSchemaComplexType)
                    {
                        string key = this.CalculateUniqueTypeOrFieldName(type.Name, type.QualifiedName.Namespace, this.globalComplexTypeClasses);
                        this.globalComplexTypeClasses.Add(key, type.Name);
                        GlobalSchemaType type2 = new GlobalSchemaType(type.QualifiedName.Namespace, type.Name, GlobalXsdType.ComplexType, (string) this.xsdNsToClrNs[type.QualifiedName.Namespace], key);
                        Globals.globalSchemaTypeTable.Add(type2.XsdNamespaceAndTypeName, type2);
                        if (this.globalQualifiedComplexTypeClasses[type.QualifiedName] == null)
                        {
                            this.globalQualifiedComplexTypeClasses.Add(type.QualifiedName, key);
                            if (((XmlSchemaComplexType) type).IsAbstract)
                            {
                                this.globalQualifiedAbstractTypeClasses.Add(type.QualifiedName, new Hashtable());
                            }
                        }
                    }
                }
                foreach (XmlSchemaElement element in this.schema.Elements.Values)
                {
                    if (element.ElementType is XmlSchemaComplexType)
                    {
                        XmlSchemaComplexType elementType = (XmlSchemaComplexType) element.ElementType;
                        string name = element.Name;
                        if (this.globalComplexTypeClasses[element.Name] == null)
                        {
                            name = this.CalculateUniqueTypeOrFieldName(element.Name, element.QualifiedName.Namespace, this.globalComplexTypeClasses);
                            this.globalComplexTypeClasses.Add(name, element.Name);
                        }
                        GlobalSchemaType type4 = new GlobalSchemaType(element.QualifiedName.Namespace, element.Name, GlobalXsdType.Element, (string) this.xsdNsToClrNs[element.QualifiedName.Namespace], name);
                        Globals.globalSchemaTypeTable.Add(type4.XsdNamespaceAndTypeName, type4);
                        if (this.globalQualifiedComplexTypeClasses[element.QualifiedName] == null)
                        {
                            this.globalQualifiedComplexTypeClasses.Add(element.QualifiedName, name);
                        }
                        else
                        {
                            this.globalQualifiedComplexTypeClasses.Add("$$" + element.QualifiedName, name);
                        }
                        continue;
                    }
                    if (element.ElementType is XmlSchemaSimpleType)
                    {
                        XmlSchemaSimpleType simpleType = (XmlSchemaSimpleType) element.ElementType;
                        if (!this.IsEnumeration(simpleType))
                        {
                            continue;
                        }
                        GlobalSchemaType type6 = new GlobalSchemaType(element.QualifiedName.Namespace, element.Name, GlobalXsdType.Enum, (string) this.xsdNsToClrNs[element.QualifiedName.Namespace], element.Name);
                        Globals.globalSchemaTypeTable.Add(type6.XsdNamespaceAndTypeName, type6);
                    }
                }
                foreach (XmlSchemaType type7 in this.schema.SchemaTypes.Values)
                {
                    if (type7 is XmlSchemaSimpleType)
                    {
                        XmlSchemaSimpleType type8 = (XmlSchemaSimpleType) type7;
                        if (this.IsEnumeration(type8))
                        {
                            GlobalSchemaType type9 = new GlobalSchemaType(type8.QualifiedName.Namespace, type8.Name, GlobalXsdType.Enum, (string) this.xsdNsToClrNs[type8.QualifiedName.Namespace], type8.Name);
                            if (Globals.globalSchemaTypeTable[type9.XsdNamespaceAndTypeName] == null)
                            {
                                Globals.globalSchemaTypeTable.Add(type9.XsdNamespaceAndTypeName, type9);
                            }
                        }
                    }
                }
                this.outFiles = new string[this.namespaces.Count];
                int index = 0;
                for (int i = 0; i < this.namespacesList.Count; i++)
                {
                    string currFileName;
                    string str10;
                    string str11;
                    string currentNamespace = (string) this.namespacesList[i];
                    this.enumerations.Clear();
                    this.collectionClasses.Clear();
                    foreach (XmlSchemaType type10 in this.schema.SchemaTypes.Values)
                    {
                        if ((type10 is XmlSchemaSimpleType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == type10.QualifiedName.Namespace)))
                        {
                            XmlSchemaSimpleType type11 = (XmlSchemaSimpleType) type10;
                            if (this.IsEnumeration(type11))
                            {
                                string str5 = this.ParseEnumeration(type11, type11.Name);
                            }
                        }
                    }
                    foreach (XmlSchemaElement element2 in this.schema.Elements.Values)
                    {
                        if ((element2.ElementType is XmlSchemaSimpleType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == element2.QualifiedName.Namespace)))
                        {
                            XmlSchemaSimpleType type12 = (XmlSchemaSimpleType) element2.ElementType;
                            if (this.IsEnumeration(type12))
                            {
                                string str6 = this.ParseEnumeration(type12, element2.Name);
                            }
                        }
                    }
                    foreach (XmlSchemaType type13 in this.schema.SchemaTypes.Values)
                    {
                        if ((type13 is XmlSchemaComplexType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == type13.QualifiedName.Namespace)))
                        {
                            parentClassStack.Clear();
                            string dotnetClassName = (string) this.globalQualifiedComplexTypeClasses[type13.QualifiedName];
                            this.ParseComplexTypePass1((XmlSchemaComplexType) type13, dotnetClassName, parentClassStack, type13.QualifiedName, currentNamespace);
                        }
                    }
                    foreach (XmlSchemaElement element3 in this.schema.Elements.Values)
                    {
                        if (((element3.ElementType is XmlSchemaComplexType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == element3.QualifiedName.Namespace))) && ((element3.SchemaTypeName.Name == null) || (element3.SchemaTypeName.Name == "")))
                        {
                            parentClassStack.Clear();
                            XmlSchemaComplexType complex = (XmlSchemaComplexType) element3.ElementType;
                            string str8 = this.GlobalElementToClrMap(element3.QualifiedName);
                            this.ParseComplexTypePass1(complex, str8, parentClassStack, element3.QualifiedName, currentNamespace);
                        }
                    }
                    FileStream stream2 = null;
                    if ((outputLocation == "") || (outputLocation == null))
                    {
                        this.outFiles[index] = "";
                    }
                    else if (outputLocation[outputLocation.Length - 1] == '\\')
                    {
                        this.outFiles[index] = outputLocation;
                    }
                    else if (outputLocation[outputLocation.Length - 1] != '\\')
                    {
                        this.outFiles[index] = outputLocation + @"\";
                    }
                    if ((i == 0) && (this.namespacesList.Count == 1))
                    {
                        str10 = currentNamespace;
                        currFileName = genNamespace;
                        str11 = genNamespace;
                        if ((fileName != null) && (fileName != ""))
                        {
                            currFileName = fileName;
                        }
                    }
                    else
                    {
                        str10 = currentNamespace;
                        str11 = (string) this.xsdNsToClrNs[str10];
                        currFileName = (string) this.xsdNsToClrNs[str10];
                        if ((filenameTable != null) && (filenameTable.Count > 0))
                        {
                            currFileName = (string) filenameTable[str10];
                        }
                    }
                    try
                    {
                        if (language == Language.VB)
                        {
                            if (!currFileName.ToLower().EndsWith(".vb"))
                            {
                                currFileName = currFileName + ".vb";
                            }
                        }
                        else if (!currFileName.ToLower().EndsWith(".cs"))
                        {
                            currFileName = currFileName + ".cs";
                        }
                    }
                    catch //(Exception ex)
                    {
                    }
                    this.outFiles[index] = this.outFiles[index] + currFileName;
                    stream2 = new FileStream(this.outFiles[index], FileMode.Create);
                    this.outStream = new StreamWriter(stream2);
                    string schemaFile = stream.Name;
                    int num3 = 0;
                    while ((num3 = schemaFile.IndexOf(@"\")) >= 0)
                    {
                        schemaFile = schemaFile.Substring(num3 + 1);
                    }
                    if ((genNamespace == null) || (genNamespace == ""))
                    {
                        this.code.NamespaceHeaderCode(this.outStream, genNamespace, schemaFile, null, str10, this.enumerations, this.optionDepthFirstTraversalHooks, (ArrayList) this.namespaces[str10]);
                    }
                    else
                    {
                        this.code.NamespaceHeaderCode(this.outStream, str11, schemaFile, null, str10, this.enumerations, this.optionDepthFirstTraversalHooks, (ArrayList) this.namespaces[str10]);
                    }
                    foreach (DictionaryEntry entry in this.collectionClasses)
                    {
                        this.code.CollectionSubclass(this.outStream, ((CollectionClass) entry.Value).FieldName, ((CollectionClass) entry.Value).Datatype, ((CollectionClass) entry.Value).IsAbstract);
                    }
                    this.enumerations.Clear();
                    foreach (XmlSchemaType type15 in this.schema.SchemaTypes.Values)
                    {
                        if ((type15 is XmlSchemaSimpleType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == type15.QualifiedName.Namespace)))
                        {
                            XmlSchemaSimpleType type16 = (XmlSchemaSimpleType) type15;
                            if (this.IsEnumeration(type16) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == type16.QualifiedName.Namespace)))
                            {
                                this.enumerations.Add(LanguageBase.ReplaceInvalidChars(type16.Name), "");
                            }
                        }
                    }
                    foreach (XmlSchemaElement element4 in this.schema.Elements.Values)
                    {
                        if ((element4.ElementType is XmlSchemaSimpleType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == element4.QualifiedName.Namespace)))
                        {
                            XmlSchemaSimpleType type17 = (XmlSchemaSimpleType) element4.ElementType;
                            if ((this.IsEnumeration(type17) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == element4.QualifiedName.Namespace))) && (this.enumerations[LanguageBase.ReplaceInvalidChars(element4.Name)] == null))
                            {
                                this.enumerations.Add(LanguageBase.ReplaceInvalidChars(element4.Name), "");
                            }
                        }
                    }
                    foreach (XmlSchemaType type18 in this.schema.SchemaTypes.Values)
                    {
                        if ((type18 is XmlSchemaComplexType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == type18.QualifiedName.Namespace)))
                        {
                            parentClassStack.Clear();
                            string className = (string) this.globalQualifiedComplexTypeClasses[type18.QualifiedName];
                            bool globalElementAndSchemaTypeHaveSameName = false;
                            if (this.schema.Elements[type18.QualifiedName] != null)
                            {
                                globalElementAndSchemaTypeHaveSameName = true;
                            }
                            this.ParseComplexTypePass2((XmlSchemaComplexType) type18, className, type18.Name, true, false, type18.QualifiedName.Namespace, parentClassStack, "", "", false, globalElementAndSchemaTypeHaveSameName);
                        }
                    }
                    foreach (XmlSchemaElement element5 in this.schema.Elements.Values)
                    {
                        if ((element5.ElementType is XmlSchemaComplexType) && (!Globals.globalSeparateImportedNamespaces || (currentNamespace == element5.QualifiedName.Namespace)))
                        {
                            parentClassStack.Clear();
                            string str14 = this.GlobalElementToClrMap(element5.QualifiedName);
                            string typedGlobalElement = "";
                            if ((element5.SchemaTypeName.Name != null) && (element5.SchemaTypeName.Name != ""))
                            {
                                typedGlobalElement = (string) this.globalQualifiedComplexTypeClasses[element5.SchemaTypeName];
                                if (element5.QualifiedName == element5.SchemaTypeName)
                                {
                                    continue;
                                }
                            }
                            this.ParseComplexTypePass2((XmlSchemaComplexType) element5.ElementType, str14, element5.Name, false, false, element5.QualifiedName.Namespace, parentClassStack, typedGlobalElement, element5.SchemaTypeName.Namespace, element5.IsNillable, false);
                        }
                    }
                    this.code.NamespaceTrailerCode(this.outStream, str11);
                    index++;
                    this.outStream.Close();
                    stream.Close();
                }
                //EventLog.WriteEntry("XSDSchemaParser", string.Format("Done. Writing files", new object[0]));
                outFiles = this.outFiles;
            }
            catch (XSDObjectGenException exception)
            {
                throw exception;
            }
            catch (FileNotFoundException exception2)
            {
                throw new XSDObjectGenException(exception2.Message);
            }
            catch (UnauthorizedAccessException exception3)
            {
                throw new XSDObjectGenException(exception3.Message);
            }
            catch (XmlSchemaException exception4)
            {
                if (this.outStream != null)
                {
                    this.outStream.WriteLine();
                    this.outStream.WriteLine("LineNumber = {0}", exception4.LineNumber);
                    this.outStream.WriteLine("LinePosition = {0}", exception4.LinePosition);
                    this.outStream.WriteLine("Message = {0}", exception4.Message);
                    this.outStream.WriteLine("Source = {0}", exception4.Source);
                }
                throw new XSDObjectGenException(string.Format(".NET Framework XSD Schema compile error.\nError occurred : {0}", exception4.Message));
            }
            catch (XmlException exception5)
            {
                if (this.outStream != null)
                {
                    this.outStream.WriteLine();
                    this.outStream.WriteLine("LineNumber = {0}", exception5.LineNumber);
                    this.outStream.WriteLine("LinePosition = {0}", exception5.LinePosition);
                    this.outStream.WriteLine("Message = {0}", exception5.Message);
                    this.outStream.WriteLine("Source = {0}", exception5.Source);
                }
                throw new XSDObjectGenException(string.Format(".NET Framework could not read the XSD file.  Bad XML file.\nError occurred : {0}", exception5.Message));
            }
            catch (Exception exception6)
            {
                //EventLog.WriteEntry("Unexpected XSDObjectGen exception", exception6.Message, EventLogEntryType.Error);
                if (this.outStream != null)
                {
                    this.outStream.WriteLine();
                    this.outStream.WriteLine("Error message : {0}", exception6.Message);
                    this.outStream.WriteLine("Source : {0}", exception6.Source);
                    this.outStream.WriteLine("Stack : {0}", exception6.StackTrace);
                }
                throw new Exception("Unexpected XSDObjectGen Exception", exception6);
            }
            finally
            {
                if (this.outStream != null)
                {
                    this.outStream.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return outFiles;
        }

        private string GlobalElementToClrMap(XmlQualifiedName qn)
        {
            if (this.globalQualifiedComplexTypeClasses["$$" + qn] != null)
            {
                return (string) this.globalQualifiedComplexTypeClasses["$$" + qn];
            }
            return (string) this.globalQualifiedComplexTypeClasses[qn];
        }

        private bool IsEnumeration(XmlSchemaSimpleType simpleType)
        {
            if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
            {
                XmlSchemaSimpleTypeRestriction content = (XmlSchemaSimpleTypeRestriction) simpleType.Content;
                for (int i = 0; i < content.Facets.Count; i++)
                {
                    if (content.Facets[i] is XmlSchemaEnumerationFacet)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ParseAttributeSimpleType(XmlSchemaAttribute attribute, string dotnetAttributeName, ArrayList ctorList, string parentNamespace)
        {
            XmlSchemaSimpleType attributeType = (XmlSchemaSimpleType) attribute.AttributeType;
            string invalid = ((attributeType.Name != null) && (attributeType.Name != "")) ? attributeType.Name : attribute.Name;
            string typeNs = (attribute.QualifiedName.Namespace != "") ? attribute.QualifiedName.Namespace : parentNamespace;
            if (this.IsEnumeration(attributeType))
            {
                string str3;
                if ((attributeType.Name == null) || (attributeType.Name == ""))
                {
                    if (this.enumerations.ContainsKey(LanguageBase.ReplaceInvalidChars(invalid)))
                    {
                        invalid = this.CalculateUniqueTypeOrFieldName(LanguageBase.ReplaceInvalidChars(invalid), "", this.enumerations);
                    }
                    this.enumerations.Add(LanguageBase.ReplaceInvalidChars(invalid), "");
                }
                if (((typeNs == parentNamespace) && (attributeType.QualifiedName.Namespace != typeNs)) && (attributeType.QualifiedName.Namespace != null))
                {
                    str3 = this.AddQualifiedNamespaceReference(invalid, attributeType.QualifiedName.Namespace, parentNamespace, GlobalXsdType.Enum);
                }
                else
                {
                    str3 = this.AddQualifiedNamespaceReference(invalid, typeNs, parentNamespace, GlobalXsdType.Enum);
                }
                this.code.ClassAttributeFieldCode(this.outStream, str3, "", attribute.Name, dotnetAttributeName, this.attributeFormDefault, true, typeNs);
                this.BuildConstructorList(attribute.DefaultValue, attribute.FixedValue, attribute.Use == XmlSchemaUse.Required, 0M, dotnetAttributeName, str3, attribute.Name, ctorList, true);
            }
            else if (attributeType.Content is XmlSchemaSimpleTypeRestriction)
            {
                XmlSchemaSimpleTypeRestriction content = (XmlSchemaSimpleTypeRestriction) attributeType.Content;
                string xsdType = "";
                if (content.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
                {
                    xsdType = content.BaseTypeName.Name;
                }
                else if ((attributeType.BaseSchemaType as XmlSchemaSimpleType).Content is XmlSchemaSimpleTypeRestriction)
                {
                    XmlSchemaSimpleTypeRestriction restriction2 = (XmlSchemaSimpleTypeRestriction) (attributeType.BaseSchemaType as XmlSchemaSimpleType).Content;
                    if (restriction2.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
                    {
                        xsdType = restriction2.BaseTypeName.Name;
                    }
                    else
                    {
                        xsdType = attributeType.Datatype.ValueType.Name.ToLower();
                    }
                }
                else
                {
                    xsdType = "string";
                }
                string dotNetDatatype = this.code.FrameworkTypeMapping(xsdType);
                this.code.ClassAttributeFieldCode(this.outStream, dotNetDatatype, xsdType, attribute.Name, dotnetAttributeName, this.attributeFormDefault, false, typeNs);
                this.BuildConstructorList(attribute.DefaultValue, attribute.FixedValue, attribute.Use == XmlSchemaUse.Required, 0M, dotnetAttributeName, dotNetDatatype, attribute.Name, ctorList, false);
            }
            else
            {
                this.code.ClassAttributeFieldCode(this.outStream, "System.String", "", attribute.Name, dotnetAttributeName, this.attributeFormDefault, false, typeNs);
            }
        }

        private void ParseAttributesPass1(XmlSchemaObjectCollection attributes)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i] is XmlSchemaAttributeGroupRef)
                {
                    XmlSchemaAttributeGroupRef ref2 = (XmlSchemaAttributeGroupRef) attributes[i];
                    XmlSchemaAttributeGroup group = (XmlSchemaAttributeGroup) this.schema.AttributeGroups[ref2.RefName];
                    this.ParseAttributesPass1(group.Attributes);
                }
                else if (attributes[i] is XmlSchemaAttribute)
                {
                    XmlSchemaAttribute attribute = (XmlSchemaAttribute) attributes[i];
                    XmlSchemaAttribute attribute2 = (XmlSchemaAttribute) this.schema.Attributes[attribute.QualifiedName];
                    if (attribute2 == null)
                    {
                        attribute2 = attribute;
                    }
                    if (attribute2.AttributeType is XmlSchemaSimpleType)
                    {
                        XmlSchemaSimpleType attributeType = (XmlSchemaSimpleType) attribute2.AttributeType;
                        this.ParseEnumeration(attributeType, attribute2.Name);
                    }
                }
            }
        }

        private void ParseAttributesPass2(XmlSchemaObjectCollection attributes, XmlSchemaAnyAttribute anyAttribute, ArrayList ctorList, Hashtable dotnetFieldList, string parentNamespace)
        {
            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i] is XmlSchemaAttributeGroupRef)
                {
                    XmlSchemaAttributeGroupRef ref2 = (XmlSchemaAttributeGroupRef) attributes[i];
                    XmlSchemaAttributeGroup group = (XmlSchemaAttributeGroup) this.schema.AttributeGroups[ref2.RefName];
                    this.ParseAttributesPass2(group.Attributes, group.AnyAttribute, ctorList, dotnetFieldList, parentNamespace);
                }
                else if (attributes[i] is XmlSchemaAttribute)
                {
                    XmlSchemaAttribute attribute = (XmlSchemaAttribute) attributes[i];
                    XmlSchemaAttribute attribute2 = (XmlSchemaAttribute) this.schema.Attributes[attribute.QualifiedName];
                    XmlSchemaForm attributeFormDefault = this.attributeFormDefault;
                    if (attribute2 == null)
                    {
                        attribute2 = attribute;
                    }
                    string ns = (attribute2.QualifiedName.Namespace != "") ? attribute2.QualifiedName.Namespace : parentNamespace;
                    string key = this.CalculateUniqueTypeOrFieldName(attribute2.Name, "", dotnetFieldList);
                    dotnetFieldList.Add(key, attribute2.QualifiedName);
                    if (attribute2.AttributeType is XmlSchemaSimpleType)
                    {
                        this.ParseAttributeSimpleType(attribute2, key, ctorList, parentNamespace);
                    }
                    else
                    {
                        string name = attribute2.SchemaTypeName.Name;
                        string dotNetDatatype = LanguageBase.ReplaceInvalidChars(this.code.FrameworkTypeMapping(name));
                        this.code.ClassAttributeFieldCode(this.outStream, dotNetDatatype, name, attribute2.Name, key, attributeFormDefault, false, ns);
                        this.BuildConstructorList(attribute2.DefaultValue, attribute2.FixedValue, attribute2.Use == XmlSchemaUse.Required, 0M, key, dotNetDatatype, attribute2.Name, ctorList, false);
                    }
                }
            }
            if (anyAttribute != null)
            {
                string str5 = this.CalculateUniqueTypeOrFieldName("AnyAttr", "", dotnetFieldList);
                dotnetFieldList.Add(str5, "AnyAttr");
                string str6 = this.CalculateAnyNamespace(anyAttribute.Namespace, parentNamespace);
                this.code.ClassAttributeFieldCode(this.outStream, "System.Xml.XmlAttribute[]", "", "AnyAttr", str5, XmlSchemaForm.Unqualified, false, str6);
            }
        }

        private void ParseComplexTypePass1(XmlSchemaComplexType complex, string dotnetClassName, ArrayList parentClassStack, XmlQualifiedName qname, string currentNamespace)
        {
            ArrayList childClasses = new ArrayList();
            parentClassStack.Add(dotnetClassName);
            XmlSchemaComplexType baseSchemaType = complex.BaseSchemaType as XmlSchemaComplexType;
            if (baseSchemaType != null)
            {
                Hashtable hashtable = null;
                hashtable = (Hashtable) this.globalQualifiedAbstractTypeClasses[baseSchemaType.QualifiedName];
                if ((hashtable != null) && !hashtable.ContainsKey(dotnetClassName))
                {
                    hashtable.Add(dotnetClassName, currentNamespace);
                }
            }
            this.ParseAttributesPass1(complex.Attributes);
            if (complex.Particle is XmlSchemaGroupBase)
            {
                this.ParseGroupBasePass1((XmlSchemaGroupBase) complex.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
            }
            else if (complex.Particle is XmlSchemaGroupRef)
            {
                XmlSchemaGroup group = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) complex.Particle).RefName];
                this.ParseGroupBasePass1(group.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
            }
            else if (complex.ContentModel is XmlSchemaSimpleContent)
            {
                XmlSchemaSimpleContent contentModel = (XmlSchemaSimpleContent) complex.ContentModel;
                if (contentModel.Content is XmlSchemaSimpleContentRestriction)
                {
                    XmlSchemaSimpleContentRestriction content = (XmlSchemaSimpleContentRestriction) contentModel.Content;
                    this.ParseAttributesPass1(content.Attributes);
                }
                else if (contentModel.Content is XmlSchemaSimpleContentExtension)
                {
                    XmlSchemaSimpleContentExtension extension = (XmlSchemaSimpleContentExtension) contentModel.Content;
                    this.ParseAttributesPass1(extension.Attributes);
                }
            }
            else if (complex.ContentModel is XmlSchemaComplexContent)
            {
                XmlSchemaComplexContent content2 = (XmlSchemaComplexContent) complex.ContentModel;
                if (content2.Content is XmlSchemaComplexContentRestriction)
                {
                    XmlSchemaComplexContentRestriction restriction2 = (XmlSchemaComplexContentRestriction) content2.Content;
                    if (restriction2.Attributes != null)
                    {
                        this.ParseAttributesPass1(restriction2.Attributes);
                    }
                    if (restriction2.Particle != null)
                    {
                        if (restriction2.Particle is XmlSchemaGroupBase)
                        {
                            this.ParseGroupBasePass1((XmlSchemaGroupBase) restriction2.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
                        }
                        else if (restriction2.Particle is XmlSchemaGroupRef)
                        {
                            XmlSchemaGroup group2 = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) restriction2.Particle).RefName];
                            this.ParseGroupBasePass1(group2.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
                        }
                    }
                }
                else if (content2.Content is XmlSchemaComplexContentExtension)
                {
                    XmlSchemaComplexContentExtension extension2 = (XmlSchemaComplexContentExtension) content2.Content;
                    if (extension2.Attributes != null)
                    {
                        this.ParseAttributesPass1(extension2.Attributes);
                    }
                    if (extension2.Particle != null)
                    {
                        if (extension2.Particle is XmlSchemaGroupBase)
                        {
                            this.ParseGroupBasePass1((XmlSchemaGroupBase) extension2.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
                        }
                        else if (extension2.Particle is XmlSchemaGroupRef)
                        {
                            XmlSchemaGroup group3 = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) extension2.Particle).RefName];
                            this.ParseGroupBasePass1(group3.Particle, dotnetClassName, childClasses, parentClassStack, complex.QualifiedName.Namespace, currentNamespace);
                        }
                    }
                }
            }
            for (int i = 0; i < childClasses.Count; i++)
            {
                ChildComplexType type2 = (ChildComplexType) childClasses[i];
                this.ParseComplexTypePass1(type2.ComplexType, type2.DotnetClassName, parentClassStack, type2.Qname, currentNamespace);
            }
        }

        private void ParseComplexTypePass2(XmlSchemaComplexType complex, string className, string elementName, bool isSchemaType, bool isLocalComplexType, string classNamespace, ArrayList parentClassStack, string typedGlobalElement, string typedGlobalElementNamespace, bool isElementNullable, bool globalElementAndSchemaTypeHaveSameName)
        {
            ArrayList childClasses = new ArrayList();
            parentClassStack.Add(className);
            ArrayList ctorList = new ArrayList();
            Hashtable dotnetFieldList = new Hashtable();
            string str = "";
            string typeNs = "";
            bool baseIsAbstract = false;
            XmlSchemaType baseSchemaType = complex.BaseSchemaType as XmlSchemaType;
            if (typedGlobalElement != "")
            {
                str = typedGlobalElement;
                typeNs = typedGlobalElementNamespace;
            }
            else if ((baseSchemaType != null) && (baseSchemaType is XmlSchemaComplexType))
            {
                str = baseSchemaType.Name;
                typeNs = baseSchemaType.QualifiedName.Namespace;
                baseIsAbstract = ((XmlSchemaComplexType) baseSchemaType).IsAbstract;
            }
            ArrayList xmlIncludedClasses = new ArrayList();
            ArrayList list4 = new ArrayList();
            list4 = (ArrayList) this.classesReferencingAbstractTypes[className];
            if (list4 != null)
            {
                foreach (XmlQualifiedName name in list4)
                {
                    Hashtable hashtable2 = (Hashtable) this.globalQualifiedAbstractTypeClasses[name];
                    foreach (string str3 in hashtable2.Keys)
                    {
                        xmlIncludedClasses.Add(((string) this.xsdNsToClrNs[(string) hashtable2[str3]]) + "." + str3);
                    }
                }
            }
            className = LanguageBase.ReplaceInvalidChars(className);
            if ((str != null) && (str != ""))
            {
                str = LanguageBase.ReplaceInvalidChars(this.AddQualifiedNamespaceReference(str, typeNs, classNamespace, GlobalXsdType.ComplexType));
            }
            this.code.ClassHeaderCode(this.outStream, className, elementName, str, baseIsAbstract, isSchemaType, complex.IsAbstract, isLocalComplexType, this.enumerableClasses, classNamespace, this.elementFormDefault, "", isElementNullable, xmlIncludedClasses, globalElementAndSchemaTypeHaveSameName);
            bool mixed = false;
            string mixedXsdType = "string";
            if (complex.IsMixed || (complex.ContentModel is XmlSchemaSimpleContent))
            {
                mixed = true;
                if (complex.ContentModel is XmlSchemaSimpleContent)
                {
                    XmlSchemaSimpleContent contentModel = (XmlSchemaSimpleContent) complex.ContentModel;
                    if (contentModel.Content is XmlSchemaSimpleContentExtension)
                    {
                        XmlSchemaSimpleContentExtension content = (XmlSchemaSimpleContentExtension) contentModel.Content;
                        if (content.BaseTypeName.Namespace.StartsWith("http://www.w3.org"))
                        {
                            mixedXsdType = content.BaseTypeName.Name;
                        }
                        else if ((baseSchemaType != null) && baseSchemaType.Datatype.ValueType.FullName.StartsWith("System."))
                        {
                            mixedXsdType = baseSchemaType.Datatype.ValueType.FullName;
                        }
                    }
                }
            }
            if (typedGlobalElement != "")
            {
                this.code.ClassTrailerCode(this.outStream, className, new ArrayList(), false, this.optionDepthFirstTraversalHooks, this.optionConstructRequiredSchema, str, false, "");
            }
            else
            {
                this.ParseAttributesPass2(complex.Attributes, complex.AnyAttribute, ctorList, dotnetFieldList, classNamespace);
                if (complex.Particle is XmlSchemaGroupBase)
                {
                    this.ParseGroupBasePass2((XmlSchemaGroupBase) complex.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                }
                else if (complex.Particle is XmlSchemaGroupRef)
                {
                    XmlSchemaGroup group = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) complex.Particle).RefName];
                    this.ParseGroupBasePass2(group.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                }
                else if (complex.ContentModel is XmlSchemaSimpleContent)
                {
                    XmlSchemaSimpleContent content2 = (XmlSchemaSimpleContent) complex.ContentModel;
                    if (content2.Content is XmlSchemaSimpleContentRestriction)
                    {
                        XmlSchemaSimpleContentRestriction restriction = (XmlSchemaSimpleContentRestriction) content2.Content;
                        this.ParseAttributesPass2(restriction.Attributes, restriction.AnyAttribute, ctorList, dotnetFieldList, classNamespace);
                    }
                    else if (content2.Content is XmlSchemaSimpleContentExtension)
                    {
                        XmlSchemaSimpleContentExtension extension2 = (XmlSchemaSimpleContentExtension) content2.Content;
                        this.ParseAttributesPass2(extension2.Attributes, extension2.AnyAttribute, ctorList, dotnetFieldList, classNamespace);
                    }
                }
                else if (complex.ContentModel is XmlSchemaComplexContent)
                {
                    XmlSchemaComplexContent content3 = (XmlSchemaComplexContent) complex.ContentModel;
                    if (content3.Content is XmlSchemaComplexContentRestriction)
                    {
                        XmlSchemaComplexContentRestriction restriction2 = (XmlSchemaComplexContentRestriction) content3.Content;
                        if (restriction2.Attributes != null)
                        {
                            this.ParseAttributesPass2(restriction2.Attributes, restriction2.AnyAttribute, ctorList, dotnetFieldList, classNamespace);
                        }
                        if (restriction2.Particle != null)
                        {
                            if (restriction2.Particle is XmlSchemaGroupBase)
                            {
                                this.ParseGroupBasePass2((XmlSchemaGroupBase) restriction2.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                            }
                            else if (restriction2.Particle is XmlSchemaGroupRef)
                            {
                                XmlSchemaGroup group2 = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) restriction2.Particle).RefName];
                                this.ParseGroupBasePass2(group2.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                            }
                        }
                    }
                    else if (content3.Content is XmlSchemaComplexContentExtension)
                    {
                        XmlSchemaComplexContentExtension extension3 = (XmlSchemaComplexContentExtension) content3.Content;
                        if (extension3.Attributes != null)
                        {
                            this.ParseAttributesPass2(extension3.Attributes, extension3.AnyAttribute, ctorList, dotnetFieldList, classNamespace);
                        }
                        if (extension3.Particle != null)
                        {
                            if (extension3.Particle is XmlSchemaGroupBase)
                            {
                                this.ParseGroupBasePass2((XmlSchemaGroupBase) extension3.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                            }
                            else if (extension3.Particle is XmlSchemaGroupRef)
                            {
                                XmlSchemaGroup group3 = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) extension3.Particle).RefName];
                                this.ParseGroupBasePass2(group3.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, classNamespace, null);
                            }
                        }
                    }
                }
                this.code.ClassTrailerCode(this.outStream, className, ctorList, this.optionDefaultInitialization, this.optionDepthFirstTraversalHooks, this.optionConstructRequiredSchema, str, mixed, mixedXsdType);
                for (int i = 0; i < childClasses.Count; i++)
                {
                    ChildComplexType type2 = (ChildComplexType) childClasses[i];
                    this.ParseComplexTypePass2(type2.ComplexType, type2.DotnetClassName, type2.ElementName, false, true, type2.Namespace, parentClassStack, "", "", false, false);
                }
            }
        }

        private void ParseElementSimpleType(XmlSchemaElement element, XmlSchemaElement elementRef, decimal maxOccurs, string dotnetElementName, ArrayList ctorList, string parentNamespace)
        {
            XmlSchemaSimpleType elementType = (XmlSchemaSimpleType) element.ElementType;
            string invalid = ((elementType.Name != null) && (elementType.Name != "")) ? elementType.Name : element.Name;
            string typeNs = (element.QualifiedName.Namespace != "") ? element.QualifiedName.Namespace : parentNamespace;
            if (this.IsEnumeration(elementType))
            {
                string str4;
                if ((elementType.Name == null) || (elementType.Name == ""))
                {
                    string key = LanguageBase.ReplaceInvalidChars(invalid);
                    if (this.enumerations.ContainsKey(key))
                    {
                        key = this.CalculateUniqueTypeOrFieldName(key, "", this.enumerations);
                    }
                    this.enumerations.Add(key, "");
                }
                if (((typeNs == parentNamespace) && (elementType.QualifiedName.Namespace != typeNs)) && (elementType.QualifiedName.Namespace != null))
                {
                    str4 = this.AddQualifiedNamespaceReference(invalid, elementType.QualifiedName.Namespace, parentNamespace, GlobalXsdType.Enum);
                }
                else
                {
                    str4 = this.AddQualifiedNamespaceReference(invalid, typeNs, parentNamespace, GlobalXsdType.Enum);
                }
                this.code.ClassElementFieldCode(this.outStream, str4, "", element.Name, dotnetElementName, maxOccurs, 1M, this.elementFormDefault, true, typeNs, element.IsNillable);
                this.BuildConstructorList(element.DefaultValue, element.FixedValue, (elementRef.MinOccurs > 0M) && (element.MinOccurs > 0M), maxOccurs, dotnetElementName, str4, element.Name, ctorList, true);
            }
            else if (elementType.Content is XmlSchemaSimpleTypeRestriction)
            {
                string name;
                XmlSchemaSimpleTypeRestriction content = (XmlSchemaSimpleTypeRestriction) elementType.Content;
                if (content.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
                {
                    name = content.BaseTypeName.Name;
                }
                else
                {
                    XmlSchemaSimpleTypeRestriction restriction2 = (XmlSchemaSimpleTypeRestriction) (elementType.BaseSchemaType as XmlSchemaSimpleType).Content;
                    name = restriction2.BaseTypeName.Name;
                }
                string dotNetDatatype = this.code.FrameworkTypeMapping(name);
                this.code.ClassElementFieldCode(this.outStream, dotNetDatatype, name, element.Name, dotnetElementName, maxOccurs, 1M, this.elementFormDefault, false, typeNs, element.IsNillable);
                this.BuildConstructorList(element.DefaultValue, element.FixedValue, (elementRef.MinOccurs > 0M) && (element.MinOccurs > 0M), maxOccurs, dotnetElementName, dotNetDatatype, element.Name, ctorList, false);
            }
            else
            {
                this.code.ClassElementFieldCode(this.outStream, "System.String", "", element.Name, dotnetElementName, maxOccurs, 1M, this.elementFormDefault, false, typeNs, element.IsNillable);
            }
        }

        private string ParseEnumeration(XmlSchemaSimpleType simpleType, string name)
        {
            ArrayList list = null;
            name = LanguageBase.ReplaceInvalidChars(name);
            if ((simpleType.Name == null) || (simpleType.Name == ""))
            {
                if (this.enumerations.ContainsKey(name))
                {
                    name = this.CalculateUniqueTypeOrFieldName(name, "", this.enumerations);
                }
            }
            else
            {
                name = LanguageBase.ReplaceInvalidChars(simpleType.Name);
            }
            if (simpleType.Content is XmlSchemaSimpleTypeRestriction)
            {
                XmlSchemaSimpleTypeRestriction content = (XmlSchemaSimpleTypeRestriction) simpleType.Content;
                for (int i = 0; i < content.Facets.Count; i++)
                {
                    if (content.Facets[i] is XmlSchemaEnumerationFacet)
                    {
                        if (list == null)
                        {
                            if (this.enumerations.ContainsKey(name))
                            {
                                return name;
                            }
                            list = new ArrayList();
                            this.enumerations.Add(name, list);
                        }
                        XmlSchemaEnumerationFacet facet = (XmlSchemaEnumerationFacet) content.Facets[i];
                        if ((facet.Value != null) && (facet.Value.Trim() != ""))
                        {
                            string[] strArray = new string[] { facet.Value.Trim(), facet.Value.Trim() };
                            strArray[1] = strArray[1].Replace(' ', '_');
                            if ((strArray[1][0] >= '0') && (strArray[1][0] <= '9'))
                            {
                                strArray[1] = LanguageBase.renameItemPrefix + strArray[1];
                            }
                            list.Add(strArray);
                        }
                    }
                }
            }
            return name;
        }

        private void ParseGroupBasePass1(XmlSchemaGroupBase groupBase, string dotnetClassName, ArrayList childClasses, ArrayList parentClassStack, string parentNamespace, string currentNamespace)
        {
            for (int i = 0; i < groupBase.Items.Count; i++)
            {
                if (groupBase.Items[i] is XmlSchemaElement)
                {
                    XmlSchemaElement element = (XmlSchemaElement) groupBase.Items[i];
                    XmlSchemaElement element2 = (XmlSchemaElement) this.schema.Elements[element.QualifiedName];
                    if (element2 == null)
                    {
                        element2 = element;
                    }
                    string nameSpace = (element2.QualifiedName.Namespace != "") ? element2.QualifiedName.Namespace : parentNamespace;
                    if ((element2.ElementType is XmlSchemaComplexType) && (element2.SchemaTypeName.Namespace != "http://www.w3.org/2001/XMLSchema"))
                    {
                        XmlSchemaComplexType elementType = (XmlSchemaComplexType) element2.ElementType;
                        string str2 = "";
                        if ((element2 == element) && (this.schema.SchemaTypes[elementType.QualifiedName] == null))
                        {
                            str2 = this.CalculateNestedChildTypeName(element2.Name, this.globalComplexTypeClasses, parentClassStack);
                            childClasses.Add(new ChildComplexType(elementType, element2.Name, str2, nameSpace, element2.QualifiedName));
                            this.globalComplexTypeClasses.Add(str2, str2);
                            string str3 = "";
                            foreach (string str4 in parentClassStack)
                            {
                                str3 = str3 + str4;
                            }
                            this.globalQualifiedComplexTypeClasses.Add(element2.QualifiedName + str3, str2);
                        }
                        if ((element.MaxOccurs > 1M) || (groupBase.MaxOccurs > 1M))
                        {
                            string key = dotnetClassName;
                            if (str2 != "")
                            {
                                key = str2;
                            }
                            if ((groupBase.Items.Count == 1) && !this.enumerableClasses.ContainsKey(key))
                            {
                                ArrayList list = new ArrayList();
                                list.Add(element2.Name);
                                if (str2 != "")
                                {
                                    list.Add(str2);
                                }
                                else if (((this.schema.Elements[element.QualifiedName] != null) && (elementType.Name != null)) && (elementType.Name != ""))
                                {
                                    string str6 = this.AddQualifiedNamespaceReference(element2.Name, element2.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Element);
                                    list.Add(str6);
                                }
                                else if ((elementType.Name != null) && (elementType.Name != ""))
                                {
                                    string str7 = this.AddQualifiedNamespaceReference(elementType.Name, elementType.QualifiedName.Namespace, currentNamespace, GlobalXsdType.ComplexType);
                                    list.Add(str7);
                                }
                                else
                                {
                                    string str8 = this.AddQualifiedNamespaceReference(element2.Name, element2.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Element);
                                    list.Add(str8);
                                }
                                this.enumerableClasses.Add(dotnetClassName, list);
                            }
                            if (str2 != "")
                            {
                                if (!this.collectionClasses.Contains(str2))
                                {
                                    this.collectionClasses.Add(str2, new CollectionClass(str2, str2, false));
                                }
                            }
                            else if (((this.schema.Elements[element.QualifiedName] != null) && (elementType.Name != null)) && (elementType.Name != ""))
                            {
                                string str9 = this.GlobalElementToClrMap(element.QualifiedName);
                                string datatype = this.AddQualifiedNamespaceReference(element.QualifiedName.Name, element.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Element);
                                if (!this.collectionClasses.Contains(str9))
                                {
                                    this.collectionClasses.Add(str9, new CollectionClass(str9, datatype, elementType.IsAbstract));
                                }
                            }
                            else if ((elementType.Name != null) && (elementType.Name != ""))
                            {
                                string str11 = (string) this.globalQualifiedComplexTypeClasses[elementType.QualifiedName];
                                string str12 = this.AddQualifiedNamespaceReference(elementType.QualifiedName.Name, elementType.QualifiedName.Namespace, currentNamespace, GlobalXsdType.ComplexType);
                                if (!this.collectionClasses.Contains(str11))
                                {
                                    this.collectionClasses.Add(str11, new CollectionClass(str11, str12, elementType.IsAbstract));
                                }
                            }
                            else
                            {
                                string str13 = this.GlobalElementToClrMap(element.QualifiedName);
                                string str14 = this.AddQualifiedNamespaceReference(element.QualifiedName.Name, element.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Element);
                                if (!this.collectionClasses.Contains(str13))
                                {
                                    this.collectionClasses.Add(str13, new CollectionClass(str13, str14, false));
                                }
                            }
                        }
                        if (elementType.IsAbstract)
                        {
                            ArrayList list2 = null;
                            if (this.classesReferencingAbstractTypes[dotnetClassName] == null)
                            {
                                list2 = new ArrayList();
                                list2.Add(elementType.QualifiedName);
                                this.classesReferencingAbstractTypes.Add(dotnetClassName, list2);
                            }
                            else
                            {
                                ((ArrayList) this.classesReferencingAbstractTypes[dotnetClassName]).Add(elementType.QualifiedName);
                            }
                        }
                    }
                    else
                    {
                        string name = element2.SchemaTypeName.Name;
                        string str16 = this.code.FrameworkTypeMapping(name);
                        if (element2.ElementType is XmlSchemaSimpleType)
                        {
                            XmlSchemaSimpleType simpleType = (XmlSchemaSimpleType) element2.ElementType;
                            if (Globals.globalSeparateImportedNamespaces)
                            {
                                if ((simpleType.QualifiedName.Namespace == "") || ((element2.QualifiedName.Namespace == currentNamespace) && (simpleType.QualifiedName.Namespace == currentNamespace)))
                                {
                                    this.ParseEnumeration(simpleType, element2.Name);
                                }
                            }
                            else
                            {
                                this.ParseEnumeration(simpleType, element2.Name);
                            }
                        }
                        if ((element.MaxOccurs > 1M) || (groupBase.MaxOccurs > 1M))
                        {
                            if (element2.ElementType is XmlSchemaSimpleType)
                            {
                                XmlSchemaSimpleType type3 = (XmlSchemaSimpleType) element2.ElementType;
                                if (!this.collectionClasses.Contains(element2.Name))
                                {
                                    if (this.IsEnumeration(type3))
                                    {
                                        if (type3.Name != null)
                                        {
                                            string str17 = this.AddQualifiedNamespaceReference(type3.QualifiedName.Name, type3.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Enum);
                                            this.collectionClasses.Add(element2.Name, new CollectionClass(element2.Name, str17, false));
                                        }
                                        else
                                        {
                                            this.collectionClasses.Add(element2.Name, new CollectionClass(element2.Name, element2.Name, false));
                                        }
                                    }
                                    else
                                    {
                                        this.collectionClasses.Add(element2.Name, new CollectionClass(element2.Name, type3.Datatype.ValueType.FullName, false));
                                    }
                                }
                                if ((groupBase.Items.Count == 1) && !this.enumerableClasses.ContainsKey(dotnetClassName))
                                {
                                    ArrayList list3 = new ArrayList();
                                    list3.Add(element2.Name);
                                    if (this.IsEnumeration(type3))
                                    {
                                        if (type3.Name != null)
                                        {
                                            string str18 = this.AddQualifiedNamespaceReference(type3.QualifiedName.Name, type3.QualifiedName.Namespace, currentNamespace, GlobalXsdType.Enum);
                                            list3.Add(str18);
                                        }
                                        else
                                        {
                                            list3.Add(element2.Name);
                                        }
                                    }
                                    else
                                    {
                                        list3.Add(type3.Datatype.ValueType.FullName);
                                    }
                                    this.enumerableClasses.Add(dotnetClassName, list3);
                                }
                            }
                            else
                            {
                                if (!this.collectionClasses.Contains(element2.Name))
                                {
                                    this.collectionClasses.Add(element2.Name, new CollectionClass(element2.Name, str16, false));
                                }
                                if ((groupBase.Items.Count == 1) && !this.enumerableClasses.ContainsKey(dotnetClassName))
                                {
                                    ArrayList list4 = new ArrayList();
                                    list4.Add(element2.Name);
                                    list4.Add(str16);
                                    this.enumerableClasses.Add(dotnetClassName, list4);
                                }
                            }
                        }
                    }
                }
                else if (groupBase.Items[i] is XmlSchemaAny)
                {
                    XmlSchemaAny any = (XmlSchemaAny) groupBase.Items[i];
                    if ((any.MaxOccurs <= 1M) || this.collectionClasses.Contains("Any"))
                    {
                    }
                }
                else if (groupBase.Items[i] is XmlSchemaGroupRef)
                {
                    XmlSchemaGroup group = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) groupBase.Items[i]).RefName];
                    this.ParseGroupBasePass1(group.Particle, dotnetClassName, childClasses, parentClassStack, parentNamespace, currentNamespace);
                }
                else if (groupBase.Items[i] is XmlSchemaGroupBase)
                {
                    this.ParseGroupBasePass1((XmlSchemaGroupBase) groupBase.Items[i], dotnetClassName, childClasses, parentClassStack, parentNamespace, currentNamespace);
                }
            }
        }

        private void ParseGroupBasePass2(XmlSchemaGroupBase groupBase, string className, ArrayList ctorList, ArrayList childClasses, ArrayList parentClassStack, Hashtable dotnetFieldList, string parentNamespace, Hashtable classReferencesAdded)
        {
            if (classReferencesAdded == null)
            {
                classReferencesAdded = new Hashtable();
            }
            for (int i = 0; i < groupBase.Items.Count; i++)
            {
                decimal maxOccurs = groupBase.MaxOccurs;
                if (groupBase.Items[i] is XmlSchemaElement)
                {
                    XmlSchemaElement elementRef = (XmlSchemaElement) groupBase.Items[i];
                    XmlSchemaElement element = (XmlSchemaElement) this.schema.Elements[elementRef.QualifiedName];
                    if (element == null)
                    {
                        element = elementRef;
                    }
                    maxOccurs = (elementRef.MaxOccurs > maxOccurs) ? elementRef.MaxOccurs : maxOccurs;
                    string nameSpace = (element.QualifiedName.Namespace != "") ? element.QualifiedName.Namespace : parentNamespace;
                    if ((element.ElementType is XmlSchemaComplexType) && (element.SchemaTypeName.Namespace != "http://www.w3.org/2001/XMLSchema"))
                    {
                        string dotnetClassName = "";
                        XmlSchemaComplexType elementType = (XmlSchemaComplexType) element.ElementType;
                        if (!classReferencesAdded.ContainsKey(element.Name))
                        {
                            classReferencesAdded.Add(element.Name, element.Name);
                            if ((element == elementRef) && (this.schema.SchemaTypes[elementType.QualifiedName] == null))
                            {
                                string str3 = "";
                                foreach (string str4 in parentClassStack)
                                {
                                    str3 = str3 + str4;
                                }
                                dotnetClassName = (string) this.globalQualifiedComplexTypeClasses[element.QualifiedName + str3];
                                childClasses.Add(new ChildComplexType(elementType, element.Name, dotnetClassName, nameSpace, element.QualifiedName));
                                dotnetClassName = LanguageBase.ReplaceInvalidChars(dotnetClassName);
                                this.code.ClassComplexTypeFieldCode(this.outStream, element.Name, dotnetClassName, dotnetClassName, className, maxOccurs, 1M, this.elementFormDefault, nameSpace, element.IsNillable, false);
                            }
                            else
                            {
                                string str5 = "";
                                if (((elementType.QualifiedName.Name == null) || (elementType.QualifiedName.Name == "")) && ((elementRef.QualifiedName.Name != null) && (elementRef.QualifiedName.Name != "")))
                                {
                                    nameSpace = elementRef.QualifiedName.Namespace;
                                    str5 = this.GlobalElementToClrMap(elementRef.QualifiedName);
                                    dotnetClassName = this.AddQualifiedNamespaceReference(element.Name, nameSpace, parentNamespace, GlobalXsdType.Element);
                                }
                                else if (((this.schema.Elements[elementRef.QualifiedName] != null) && (elementType.QualifiedName.Name != null)) && (elementType.QualifiedName.Name != ""))
                                {
                                    nameSpace = elementRef.QualifiedName.Namespace;
                                    str5 = this.GlobalElementToClrMap(elementRef.QualifiedName);
                                    dotnetClassName = this.AddQualifiedNamespaceReference(element.Name, nameSpace, parentNamespace, GlobalXsdType.Element);
                                }
                                else
                                {
                                    if ((elementRef.QualifiedName.Name == null) || !(elementRef.QualifiedName.Name != ""))
                                    {
                                        throw new ArgumentException("An element points to a global type or global element that isn't properly qualified");
                                    }
                                    nameSpace = parentNamespace;
                                    str5 = (string) this.globalQualifiedComplexTypeClasses[elementType.QualifiedName];
                                    dotnetClassName = elementType.Name;
                                    if ((elementType.QualifiedName.Namespace != nameSpace) && (elementType.QualifiedName.Namespace != null))
                                    {
                                        dotnetClassName = this.AddQualifiedNamespaceReference(dotnetClassName, elementType.QualifiedName.Namespace, parentNamespace, GlobalXsdType.ComplexType);
                                    }
                                    else
                                    {
                                        dotnetClassName = this.AddQualifiedNamespaceReference(dotnetClassName, nameSpace, parentNamespace, GlobalXsdType.ComplexType);
                                    }
                                }
                                string collectionContainedType = "";
                                if (maxOccurs > 1M)
                                {
                                    collectionContainedType = dotnetClassName;
                                    dotnetClassName = str5;
                                }
                                this.code.ClassComplexTypeFieldCode(this.outStream, element.Name, dotnetClassName, collectionContainedType, className, maxOccurs, 1M, this.elementFormDefault, nameSpace, element.IsNillable, elementType.IsAbstract);
                            }
                            ClassConstructor constructor = new ClassConstructor();
                            if ((elementRef.MinOccurs > 0M) && (element.MinOccurs > 0M))
                            {
                                constructor.required = true;
                            }
                            else
                            {
                                constructor.required = false;
                            }
                            if (maxOccurs > 1M)
                            {
                                constructor.defaultValue = element.Name;
                                constructor.fieldName = dotnetClassName;
                                if (elementType.IsAbstract)
                                {
                                    constructor.datatype = CtorDatatypeContext.PropertyCollectionAbstractComplexType;
                                }
                                else
                                {
                                    constructor.datatype = CtorDatatypeContext.PropertyCollectionComplexType;
                                }
                            }
                            else
                            {
                                constructor.defaultValue = element.Name;
                                constructor.fieldName = dotnetClassName;
                                constructor.datatype = CtorDatatypeContext.Property;
                            }
                            ctorList.Add(constructor);
                        }
                    }
                    else
                    {
                        string key = this.CalculateUniqueTypeOrFieldName(element.Name, "", dotnetFieldList);
                        dotnetFieldList.Add(key, element.QualifiedName);
                        if (element.ElementType is XmlSchemaSimpleType)
                        {
                            this.ParseElementSimpleType(element, elementRef, maxOccurs, key, ctorList, parentNamespace);
                        }
                        else
                        {
                            string name = element.SchemaTypeName.Name;
                            string dotNetDatatype = LanguageBase.ReplaceInvalidChars(this.code.FrameworkTypeMapping(name));
                            this.code.ClassElementFieldCode(this.outStream, dotNetDatatype, name, element.Name, key, maxOccurs, 1M, this.elementFormDefault, false, nameSpace, element.IsNillable);
                            this.BuildConstructorList(element.DefaultValue, element.FixedValue, (elementRef.MinOccurs > 0M) && (element.MinOccurs > 0M), maxOccurs, key, dotNetDatatype, element.Name, ctorList, false);
                        }
                    }
                }
                else if (groupBase.Items[i] is XmlSchemaAny)
                {
                    XmlSchemaAny any = (XmlSchemaAny) groupBase.Items[i];
                    string str10 = this.CalculateUniqueTypeOrFieldName("Any", "", dotnetFieldList);
                    dotnetFieldList.Add(str10, "Any");
                    string ns = this.CalculateAnyNamespace(any.Namespace, parentNamespace);
                    this.code.ClassElementFieldCode(this.outStream, "System.Xml.XmlElement", "", "Any", str10, any.MaxOccurs, 1M, this.elementFormDefault, false, ns, false);
                }
                else if (groupBase.Items[i] is XmlSchemaGroupRef)
                {
                    XmlSchemaGroup group = (XmlSchemaGroup) this.schema.Groups[((XmlSchemaGroupRef) groupBase.Items[i]).RefName];
                    this.ParseGroupBasePass2(group.Particle, className, ctorList, childClasses, parentClassStack, dotnetFieldList, parentNamespace, classReferencesAdded);
                }
                else if (groupBase.Items[i] is XmlSchemaGroupBase)
                {
                    this.ParseGroupBasePass2((XmlSchemaGroupBase) groupBase.Items[i], className, ctorList, childClasses, parentClassStack, dotnetFieldList, parentNamespace, classReferencesAdded);
                }
            }
        }

        private void RecurseImportedNamespaces(XmlSchema s, ArrayList references)
        {
            for (int i = 0; i < s.Includes.Count; i++)
            {
                if (s.Includes[i] is XmlSchemaImport)
                {
                    XmlSchemaImport import = (XmlSchemaImport) s.Includes[i];
                    if (import != null)
                    {
                        references.Add(import.Namespace);
                    }
                    ArrayList list = new ArrayList();
                    if ((import != null) && (this.namespaces[import.Namespace] == null))
                    {
                        if (import.Namespace == "http://www.w3.org/XML/1998/namespace")
                        {
                            goto Label_00DD;
                        }
                        this.namespaces.Add(import.Namespace, list);
                        this.namespacesList.Add(import.Namespace);
                        Globals.globalSeparateImportedNamespaces = true;
                        this.xsdNsToClrNs.Add(import.Namespace, LanguageBase.ScrubNamespace(import.Namespace));
                    }
                    if ((import.Schema != null) && (import.Schema.Includes.Count > 0))
                    {
                        this.RecurseImportedNamespaces(import.Schema, list);
                    }
                Label_00DD:;
                }
            }
        }

        private static void ShowCompileError(object sender, ValidationEventArgs e)
        {
            throw new XmlSchemaException("Schema Validation Error: " + e.Message, null);
        }
    }
}

