using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using Rio.XsdClassGenerator.XSDObjectGenLib;

namespace Rio.XsdClassGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ////string inputFolder = @"G:\Projects\Portal\Common\RioObjects\Schemas";
            //string inputFolder = @"C:\Users\miroslav.georgiev\Desktop\Schemas\Schemas";
            //string outputFolder = @"C:\Users\miroslav.georgiev\Desktop\TestClasses";
            //GenerateClasses(inputFolder, outputFolder);

            GenerateClasses(args[0], args[1]);
        }

        static void GenerateClasses(string inputFolder, string outputFolder)
        {
            foreach (var file in Directory.GetFiles(outputFolder))
            {
                File.Delete(file);
            }

            var xsdFilesForCleanup = GenerateSchemas(inputFolder, outputFolder);

            var csFilesForCleanup = GenerateClasses(outputFolder);

            var fileForCleanup =
                xsdFilesForCleanup
                .Concat(csFilesForCleanup);

            foreach (var file in fileForCleanup)
            {
                File.Delete(file);
            }
        }

        static List<string> GenerateClasses(string outputFolder)
        {
            var schemaFiles = Directory.GetFiles(outputFolder);

            Hashtable filenameTable = new Hashtable();
            Hashtable namespaceTable = new Hashtable();

            foreach (string xsdFile in schemaFiles)
            {
                XmlSchema xmlSchema = GetSchemaFromFile(xsdFile);
                FileInfo file = new FileInfo(xsdFile);

                string xsdNamespace = xmlSchema.TargetNamespace;

                if (string.IsNullOrEmpty(xsdNamespace))
                    continue;

                string xsdFileName = file.Name;

                filenameTable.Add(xsdNamespace, GetClassFileName(xsdNamespace, xsdFileName));

                namespaceTable.Add(xsdNamespace, GetClassNamespace(xsdNamespace, xsdFileName));
            }

            filenameTable.Add("", "All.cs");
            namespaceTable.Add("", "All");

            Directory.SetCurrentDirectory(outputFolder);
            XSDSchemaParser parser = new XSDSchemaParser();
            parser.Execute(
                "all.xsd",
                Language.CS,
                null,
                null,
                outputFolder,
                false,
                false,
                false,
                ref namespaceTable,
                filenameTable,
                true);


            List<string> filesForCleanUp = new List<string>
            {
                "All.cs",
            };

            return filesForCleanUp;
        }

        static string GetClassNamespace(string xsdNamespace, string xsdFileName)
        {
            Uri uri = new Uri(xsdNamespace);
            string ns = uri.AbsolutePath.Split('/').Where(s => !string.IsNullOrEmpty(s)).Last().Replace('-', '_');

            if (ns.Contains('R'))
                return ns;
            else if (Char.IsDigit(ns.First()))
                return "R_" + ns;
            else
                return ns;
        }

        static string GetClassFileName(string xsdNamespace, string xsdFileName)
        {
            return xsdFileName.Replace(".xsd", ".cs");
        }

        static List<string> GenerateSchemas(string inputFolder, string outputFolder)
        {
            var schemaFiles = Directory.GetFiles(inputFolder);

            List<string> filesForCleanUp = new List<string>();
            Dictionary<string, XmlSchema> namespaceToSchema = new Dictionary<string, XmlSchema>();
            Dictionary<string, string> namespaceToFile = new Dictionary<string, string>();

            foreach (string schemaFile in schemaFiles)
            {
                try
                {
                    XmlSchema xmlSchema = GetSchemaFromFile(schemaFile);
                    FileInfo file = new FileInfo(schemaFile);

                    namespaceToSchema.Add(xmlSchema.TargetNamespace, xmlSchema);
                    namespaceToFile.Add(xmlSchema.TargetNamespace, file.Name);
                }
                catch (Exception ex)
                {
                    // in order to catch exception message
                    throw ex;
                }
            }

            XmlSchema allSchema = new XmlSchema();
            allSchema.ElementFormDefault = XmlSchemaForm.Qualified;
            HashSet<string> allSchemaElementNames = new HashSet<string>();

            foreach (var kvp in namespaceToSchema)
            {
                string schemaNamespace = kvp.Key;
                XmlSchema xmlSchema = kvp.Value;
                string schemaLocation = namespaceToFile[schemaNamespace];

                foreach (XmlSchemaComplexType complexType in xmlSchema.Items.OfType<XmlSchemaComplexType>())
                {
                    string typeName = complexType.Name;
                    string elementName = typeName;

                    int i = 0;
                    while (allSchemaElementNames.Contains(elementName))
                    {
                        elementName = string.Format("{0}{1}", typeName, i++);
                    }

                    allSchemaElementNames.Add(elementName);

                    allSchema.Items.Add(new XmlSchemaElement() { SchemaTypeName = new XmlQualifiedName(complexType.Name, schemaNamespace), Name = elementName });
                }
                allSchema.Includes.Add(new XmlSchemaImport { Namespace = schemaNamespace, SchemaLocation = schemaLocation });

                foreach (XmlSchemaImport include in xmlSchema.Includes)
                {
                    if (namespaceToSchema.ContainsKey(include.Namespace))
                    {
                        include.SchemaLocation = namespaceToFile[include.Namespace];
                    }
                }

                string schemaFile = Path.Combine(outputFolder, schemaLocation);
                filesForCleanUp.Add(schemaFile);
                using (FileStream fs = new FileStream(schemaFile, FileMode.Create))
                {
                    xmlSchema.Write(fs);
                }
            }

            string allSchemaFile = Path.Combine(outputFolder, "All.xsd");
            filesForCleanUp.Add(allSchemaFile);
            using (FileStream fs = new FileStream(allSchemaFile, FileMode.Create))
            {
                allSchema.Write(fs);
            }

            return filesForCleanUp;
        }

        static XmlSchema GetSchemaFromFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
                return XmlSchema.Read(fs, null);
        }
    }
}
