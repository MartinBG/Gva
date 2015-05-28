using SBOffice;
using SBOfficeSecurity;
using SBWinCertStorage;
using SBX509;
using SBXMLCore;
using SBXMLSec;
using SBXMLSig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigningTools.XmlSigner
{
    public class XmlSigner
    {
        public string SerialNumber { get; private set; }
        public string PinCode { get; private set; }

        static XmlSigner()
        {
            //Set XMLBlackbox License Key
            SBUtils.Unit.SetLicenseKey("0BA127E1449CC31AC691C3ACD622F7F6FE0192F14C4A7C64868C999F1F7D9FA876AE8794C15B4ECEA64F4270467B554EDA32747A6868AF4E5BAE46B1019A6340DBE0C55AEA594863921E0EA838B25C4760A21D0478EB7274CB84C71DED134715A0D07EB9060C12B229233860A545A06B83B027271AF29FFA34F47B647C9B5E1E295D4CBB094AAE245CC36734D8481AD002A2C73E00BBBCB5135B1EBE2D7058CCCED540DCF7D2AD380D469629D588FA842DE44009FBFC11DA9C74B5C31DB48EFF15854D6D74BB869C0D979EA8741E8FCC8A31399C46FCAF4B47FAC2E7378456E6B9782FC5D6FE2CAF61E1E17E600B76E2FB0EF6F8D4E983A7702D6E1C93A4D1CD");
        }

        public XmlSigner(string serialNumber, string pinCode)
        {
            this.SerialNumber = serialNumber;
            this.PinCode = pinCode;
        }

        public Stream Sign(Stream inputStream, Encoding encoding, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces)
        {
            using (MemoryStream mInputStream = new MemoryStream())
            {
                inputStream.CopyTo(mInputStream);
                mInputStream.Position = 0;

                //Load xml in TElXMLDOMDocument
                using (TElXMLDOMDocument xmlDocument = new TElXMLDOMDocument())
                {
                    xmlDocument.LoadFromStream(mInputStream, encoding.HeaderName, true);

                    using (TElXMLKeyInfoX509Data x509KeyData = new TElXMLKeyInfoX509Data(false))
                    using (TElWinCertStorage winCertStorage = new TElWinCertStorage())
                    using (TElX509Certificate x509Certificate = GetX509Certificate(winCertStorage))
                    {
                        if (x509Certificate == null)
                        {
                            throw new Exception("Certificate not found.");
                        }

                        x509KeyData.IncludeKeyValue = true;
                        x509KeyData.Certificate = x509Certificate;

                        using (TElXMLSigner xmlSigner = GetXmlSigner(x509KeyData, GetXmlReference(xmlDocument)))
                        {
                            //Save signature value to definedxml node
                            TElXMLNamespaceMap map = new TElXMLNamespaceMap();
                            foreach (var ns in signatureXPathNamespaces)
                            {
                                map.AddNamespace(ns.Key, ns.Value);
                            }
                            TElXMLDOMNode signatureNode = xmlDocument.SelectNodes(signatureXPath, map)[0];

                            xmlSigner.Save(ref signatureNode);

                            MemoryStream outputStream = new MemoryStream();
                            xmlDocument.SaveToStream(outputStream, SBXMLDefs.Unit.xcmNone, encoding.HeaderName);

                            return outputStream;
                        }
                    }
                }
            }
        }

        #region private members

        private TElX509Certificate GetX509Certificate(TElWinCertStorage winCertStorage)
        {
            winCertStorage.StorageType = TSBStorageType.stSystem;
            winCertStorage.ReadOnly = true;
            winCertStorage.SystemStores.Text = "MY";
            winCertStorage.AccessType = TSBStorageAccessType.atLocalMachine;

            TElX509Certificate certificate = null;
            for (int i = 0; i < winCertStorage.Count; i++)
            {
                TElX509Certificate cert = winCertStorage.get_Certificates(i);

                if (SBUtils.Unit.BinaryToString(cert.SerialNumber).ToLower() == this.SerialNumber.ToLower())
                {
                    certificate = cert;

                    //Pass pinCode to certificate
                    certificate.KeyMaterial.KeyExchangePIN = this.PinCode;
                    certificate.KeyMaterial.SignaturePIN = this.PinCode;

                    break;
                }
            }

            return certificate;
        }

        private TElXMLSigner GetXmlSigner(TElXMLKeyInfoX509Data x509KeyData, TElXMLReference xmlReference)
        {
            //Init TElXMLSigner object
            TElXMLSigner xmlSigner = new TElXMLSigner();
            xmlSigner.CanonicalizationMethod = SBXMLDefs.Unit.xcmCanon;
            xmlSigner.SignatureType = SBXMLSec.Unit.xstEnveloped;
            xmlSigner.SignatureMethodType = SBXMLSec.Unit.xmtSig;
            xmlSigner.SignatureMethod = SBXMLSec.Unit.xsmRSA_SHA1;
            xmlSigner.IncludeKey = true;
            xmlSigner.KeyData = x509KeyData;
            xmlSigner.References = new TElXMLReferenceList();
            xmlSigner.References.Add(xmlReference);

            //Sign xmlReference
            xmlSigner.UpdateReferencesDigest();
            xmlSigner.GenerateSignature();
            xmlSigner.Signature.SignaturePrefix = String.Empty;
            xmlSigner.Signature.ID = Guid.NewGuid().ToString();

            return xmlSigner;
        }

        private TElXMLReference GetXmlReference(TElXMLDOMDocument xmlDocument)
        {
            //Create xmlReference which will be signed
            TElXMLReference xmlReference = new TElXMLReference();
            xmlReference.DigestMethod = SBXMLSec.Unit.xdmSHA1;
            xmlReference.URINode = xmlDocument.DocumentElement;
            xmlReference.URI = String.Empty;
            xmlReference.TransformChain.Add(new SBXMLTransform.TElXMLEnvelopedSignatureTransform());

            return xmlReference;
        }

        #endregion
    }
}
