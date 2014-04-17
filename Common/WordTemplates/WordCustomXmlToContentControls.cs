using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.WordTemplates
{
    public class WordCustomXmlToContentControls
    {

        public static void Replace(Stream template, bool hasNestedControls = true)
        {
            if (hasNestedControls)
            {
                int replacedElements;
                do
                {
                    replacedElements = ReplaceHelper(template, hasNestedControls);
                }
                while (replacedElements != 0);
            }
            else
            {
                ReplaceHelper(template, hasNestedControls);
            }
        }

        public static int ReplaceHelper(Stream template, bool hasNestedControls)
        {
            int replacedElements = 0;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(template, true))
            {
                MainDocumentPart main = doc.MainDocumentPart;
                var customXmlElements = main.Document.Descendants<CustomXmlElement>().Where(cb => !cb.Descendants<CustomXmlElement>().Any()).ToList();
                foreach (var customXmlElement in customXmlElements)
                {
                    string elementXmlPath = String.Concat(customXmlElement.Ancestors<CustomXmlElement>().Select(ce => ce.GetAttribute("element", customXmlElement.NamespaceUri).Value + "/").ToArray().Reverse()),
                           title = customXmlElement.GetAttribute("element", customXmlElement.NamespaceUri).Value,
                           tagName;
                    OpenXmlElement newElement = null;
                    Random random = new Random();
                    if(!hasNestedControls){
                        tagName = elementXmlPath + customXmlElement.GetAttribute("element", customXmlElement.NamespaceUri).Value;
                    }
                    else{
                        tagName = title;
                    }
                    int newId = random.Next(Int32.MinValue, Int32.MaxValue);

                    SdtProperties sdtPr = new SdtProperties(
                        new SdtAlias() { Val = title },
                        new Tag() { Val = tagName },
                        new SdtId() { Val = newId });

                    if (customXmlElement.GetType() == typeof(CustomXmlBlock))
                    {
                        SdtContentBlock sdtContent = new SdtContentBlock();
                        transferChildElements(customXmlElement, sdtContent);
                        newElement = new SdtBlock(sdtPr, sdtContent);
                    }
                    else if (customXmlElement.GetType() == typeof(CustomXmlRun))
                    {
                        SdtContentRun sdtContent = new SdtContentRun();
                        transferChildElements(customXmlElement, sdtContent);
                        newElement = new SdtRun(sdtPr, sdtContent);
                    }
                    else if (customXmlElement.GetType() == typeof(CustomXmlRow))
                    {
                        SdtContentRow sdtContent = new SdtContentRow();
                        transferChildElements(customXmlElement, sdtContent);
                        newElement = new SdtRow(sdtPr, sdtContent);
                    }
                    else if (customXmlElement.GetType() == typeof(CustomXmlCell))
                    {
                        SdtContentCell sdtContent = new SdtContentCell();
                        transferChildElements(customXmlElement, sdtContent);
                        newElement = new SdtCell(sdtPr, sdtContent);
                    }
                    customXmlElement.Parent.InsertAfter(newElement, customXmlElement);
                    customXmlElement.Remove();
                }
                replacedElements = customXmlElements.Count;
            }
            return replacedElements;
        }

        public static void transferChildElements(CustomXmlElement from, OpenXmlCompositeElement to)
        {
            OpenXmlElement[] elements = from.ChildElements.ToArray();
            foreach (var e in elements)
            {
                e.Remove();
            }
            to.Append(elements);
        }
    }
}
