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

namespace SigningTools.OfficeSigner
{
    public class OfficeSigner
    {
        public string SerialNumber { get; private set; }
        public string PinCode { get; private set; }

        public OfficeSigner(string serialNumber, string pinCode)
        {
            this.SerialNumber = serialNumber;
            this.PinCode = pinCode;
        }

        static OfficeSigner()
        {
            //Set OfficeBlackbox License Key
            //Trial key
            SBUtils.Unit.SetLicenseKey("AF479A648A42969644F109C690E12B1402F11DBD9EB213B43821FD62B787AE111989D1C38A5E2278F9D19F3D1D6AD85D87B7DA6DBAEDC72150960800413FB48E6067B17B03A5AB32A4417F35B4A17DA29FF2C9512DBC2D7AAE5CE117889C2FDC64CB65C6F6A9F1891D0CEEE134994DFF0DC19B95ABFDC55161B144E9482299618BE29FA9C8EFB89EB666049899C11907610B664CDAA723A1E18820A18A671B68C88C661854CC1B4DC48BA8806ED30AF02DAB7B25A63DE63258CE2F616F93D040DA6BC54212072542DBD41F7A343485A23C9AEF476404980F00B0125997FC7A4869186411F543FB4ED74A897E46B75351983715EEF95E6E443B25D156D010A57A");
        }

        public void SignInPlace(Stream inputStream)
        {
            TElOfficeDocument officeDocument = new TElOfficeDocument();
            officeDocument.Open(inputStream, false);

            using (TElWinCertStorage winCertStorage = new TElWinCertStorage())
            {
                using (TElXMLKeyInfoX509Data x509KeyData = new TElXMLKeyInfoX509Data(false))
                {
                    TElX509Certificate x509Certificate = GetX509Certificate(winCertStorage);
                    if (x509Certificate == null)
                    {
                        throw new Exception("Certificate not found.");
                    }

                    x509KeyData.IncludeKeyValue = true;
                    x509KeyData.Certificate = x509Certificate;

                    if (officeDocument.OpenXMLDocument != null)
                    {
                        TElOfficeOpenXMLSignatureHandler openXMLSigHandler = new TElOfficeOpenXMLSignatureHandler();
                        officeDocument.AddSignature(openXMLSigHandler, true);
                        openXMLSigHandler.AddDocument();

                        openXMLSigHandler.Sign(x509Certificate);
                        officeDocument.Flush();
                    }
                    else if (officeDocument.BinaryDocument != null)
                    {
                        TElOfficeBinaryCryptoAPISignatureHandler BinCryptoAPISigHandler = new TElOfficeBinaryCryptoAPISignatureHandler();
                        officeDocument.AddSignature(BinCryptoAPISigHandler, true);

                        BinCryptoAPISigHandler.ExpireTime = DateTime.UtcNow.AddYears(100);
                        BinCryptoAPISigHandler.Sign(x509Certificate);
                        officeDocument.Flush();
                    }
                    else
                    {
                        throw new FormatException();
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

        #endregion
    }
}
