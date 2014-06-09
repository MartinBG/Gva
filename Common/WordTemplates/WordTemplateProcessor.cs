using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json.Linq;

namespace Common.WordTemplates
{
    public class WordTemplateTransformer
    {
        private Stream template;

        public WordTemplateTransformer(Stream template)
        {
            this.template = template;
        }

        public Stream Template
        {
            get
            {
                return this.template;
            }
        }

        public void Transform(JObject context)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(template, true))
            {
                MainDocumentPart main = doc.MainDocumentPart;

                foreach (var sdtElement in this.ClosestDescendants<SdtElement>(main.Document).ToList())
                {
                    this.TransformElement(sdtElement, context);
                }

                foreach (var sdtElement in main.Document.Descendants<SdtElement>().Reverse())
                {
                    this.RemoveContentControl(sdtElement);
                }
            }
        }

        private IEnumerable<T> ClosestDescendants<T>(OpenXmlElement root) where T : OpenXmlElement
        {
            foreach (var child in root.ChildElements)
            {
                if (child is T)
                {
                    yield return (T)child;
                }
                else
                {
                    foreach (var descendant in this.ClosestDescendants<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        private void TransformElement(SdtElement sdtElement, JToken parentContext)
        {
            var sdtProperties = sdtElement.GetFirstChild<SdtProperties>();
            var tag = sdtProperties != null ? sdtProperties.GetFirstChild<Tag>() : null;

            if (tag == null)
            {
                return;
            }
            JToken context = null;
            if (parentContext != null)
            {
                context = parentContext.SelectToken(tag.Val);
            }

            if (!sdtElement.Descendants<SdtElement>().Any())
            {
                if (sdtElement is SdtBlock)
                {
                    SdtBlock sdtBlock = (SdtBlock)sdtElement;

                    Paragraph p = sdtBlock.SdtContentBlock.GetFirstChild<Paragraph>();

                    Run r = p.GetFirstChild<Run>();
                    this.SetRunText(r, context);

                    this.RemoveAllChildrenButFirst<Run>(p);
                    this.RemoveAllChildrenButFirst<Paragraph>(sdtBlock.SdtContentBlock);
                }
                else if (sdtElement is SdtRun)
                {
                    SdtRun sdtRun = (SdtRun)sdtElement;
                    Run r = sdtRun.SdtContentRun.GetFirstChild<Run>();

                    bool preserveContent = false;
                    var checkbox = sdtRun.SdtProperties.GetFirstChild<DocumentFormat.OpenXml.Office2010.Word.SdtContentCheckBox>();
                    if (checkbox != null)
                    {
                        preserveContent = true;
                    }
                    this.SetRunText(r, context, preserveContent);

                    this.RemoveAllChildrenButFirst<Run>(sdtRun.SdtContentRun);
                }
                else if (sdtElement is SdtCell)
                {
                    SdtCell sdtCell = (SdtCell)sdtElement;
                    TableCell cell = sdtCell.SdtContentCell.GetFirstChild<TableCell>();
                    Paragraph p = cell.GetFirstChild<Paragraph>();
                    Run r = p.GetFirstChild<Run>();
                    this.SetRunText(r, context);

                    this.RemoveAllChildrenButFirst<Run>(p);
                    this.RemoveAllChildrenButFirst<Paragraph>(cell);
                }
            }
            else
            {
                if (context is JArray)
                {
                    OpenXmlElement insertAfter = sdtElement;
                    foreach (var arrayItem in context)
                    {
                        var clone = sdtElement.CloneNode(true);
                        sdtElement.Parent.InsertAfter(clone, insertAfter);
                        insertAfter = clone;

                        foreach (SdtElement childSdtElement in this.ClosestDescendants<SdtElement>(clone).ToList())
                        {
                            this.TransformElement(childSdtElement, arrayItem);
                        }
                    }

                    if(!context.Any()){
                        sdtElement.Parent.Append(new Paragraph());
                    }

                    sdtElement.Remove();
                }
                else
                {
                    if (context == null || !context.Any())
                    {
                        sdtElement.Parent.Append(new Paragraph());
                        sdtElement.Remove();
                    }
                    else
                    {
                        foreach (SdtElement childSdtElement in this.ClosestDescendants<SdtElement>(sdtElement).ToList())
                        {
                            this.TransformElement(childSdtElement, context);
                        }
                    }
                }
            }
        }

        private void RemoveContentControl(SdtElement sdtElement)
        {
            OpenXmlElement sdtContent = null;
            if (sdtElement.GetFirstChild <SdtContentRun>() != null)
            {
                sdtContent = sdtElement.GetFirstChild<SdtContentRun>();
            }
            else if (sdtElement.GetFirstChild<SdtContentBlock>() != null)
            {
                sdtContent = sdtElement.GetFirstChild<SdtContentBlock>();
            }
            else if (sdtElement.GetFirstChild<SdtContentRow>() != null)
            {
                sdtContent = sdtElement.GetFirstChild<SdtContentRow>();
            }
            else if (sdtElement.GetFirstChild<SdtContentCell>() != null)
            {
                sdtContent = sdtElement.GetFirstChild<SdtContentCell>();
            }
            else
            {
                return;
            }

            var checkBox = sdtElement.SdtProperties.GetFirstChild<DocumentFormat.OpenXml.Office2010.Word.SdtContentCheckBox>();
            if (checkBox == null)
            {
                OpenXmlElement parent = sdtElement.Parent;
                OpenXmlElementList childElements = sdtContent.ChildElements;

                foreach (OpenXmlElement childElement in childElements)
                {

                    if (checkBox != null)
                    {
                        parent.InsertBefore((OpenXmlElement)checkBox.Clone(), sdtElement);
                    }

                    parent.InsertBefore((OpenXmlElement)childElement.Clone(), sdtElement);
                }

                sdtElement.Remove();
            }
            else
            {
                sdtElement.SdtProperties.RemoveAllChildren();
                sdtElement.SdtProperties.InsertAt((DocumentFormat.OpenXml.Office2010.Word.SdtContentCheckBox)checkBox.Clone(), 0);
            }
        }

        private void SetRunText(Run r, JToken context, bool preserveContent = false)
        {
            Text runText = r.GetFirstChild<Text>();
            if (runText == null)
            {
                runText = new Text();
                r.AppendChild(runText);
            }
            runText.SetAttribute(new OpenXmlAttribute("space", XNamespace.Xml.NamespaceName, "preserve"));

            if (preserveContent && (context == null || context.ToString() == string.Empty))
            {
                return;
            }
            if (context == null)
            {
                runText.Text = string.Empty;
            }
            else if (context.Type == JTokenType.Date)
            {
                runText.Text = ((DateTime)context).ToString("dd.MM.yyyy");
            }
            else if (context.Type == JTokenType.Boolean)
            {
                runText.Text = (bool)context ? "☒" : runText.Text;
            }
            else
            {
                runText.Text = context.ToString();
            }
        }

        private void RemoveAllChildrenButFirst<T>(OpenXmlCompositeElement content) where T : OpenXmlElement
        {
            foreach (var e in content.Descendants<T>().Skip(1).Reverse())
            {
                e.Remove();
            }
        }
    }
}
