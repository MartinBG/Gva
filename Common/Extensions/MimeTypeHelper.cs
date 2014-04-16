using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class MimeTypeHelper
    {
        private static Dictionary<string, string> mimeTypes = new Dictionary<string, string>()
            {
                { ".xul", MIME_APPLICATION_VND_MOZZILLA_XUL_XML },
                { ".json", MIME_APPLICATION_JSON },
                { ".ice", MIME_X_CONFERENCE_X_COOLTALK },
                { ".movie", MIME_VIDEO_X_SGI_MOVIE },
                { ".avi", MIME_VIDEO_X_MSVIDEO },
                { ".wmv", MIME_VIDEO_X_MS_WMV },
                { ".m4u", MIME_VIDEO_VND_MPEGURL },
                { ".mxu", MIME_VIDEO_VND_MPEGURL },
                { ".htc", MIME_TEXT_X_COMPONENT },
                { ".etx", MIME_TEXT_X_SETEXT },
                { ".wmls", MIME_TEXT_VND_WAP_WMLSCRIPT },
                { ".wml", MIME_TEXT_VND_WAP_XML },
                { ".tsv", MIME_TEXT_TAB_SEPARATED_VALUES },
                { ".sgm", MIME_TEXT_SGML },
                { ".sgml", MIME_TEXT_SGML },
                { ".css", MIME_TEXT_CSS },
                { ".ifb", MIME_TEXT_CALENDAR },
                { ".ics", MIME_TEXT_CALENDAR },
                { ".wrl", MIME_MODEL_VRLM },
                { ".vrlm", MIME_MODEL_VRLM },
                { ".silo", MIME_MODEL_MESH },
                { ".mesh", MIME_MODEL_MESH },
                { ".msh", MIME_MODEL_MESH },
                { ".iges", MIME_MODEL_IGES },
                { ".igs", MIME_MODEL_IGES },
                { ".rgb", MIME_IMAGE_X_RGB },
                { ".ppm", MIME_IMAGE_X_PORTABLE_PIXMAP },
                { ".pgm", MIME_IMAGE_X_PORTABLE_GRAYMAP },
                { ".pbm", MIME_IMAGE_X_PORTABLE_BITMAP },
                { ".pnm", MIME_IMAGE_X_PORTABLE_ANYMAP },
                { ".ico", MIME_IMAGE_X_ICON },
                { ".ras", MIME_IMAGE_X_CMU_RASTER },
                { ".wbmp", MIME_IMAGE_WAP_WBMP },
                { ".djv", MIME_IMAGE_VND_DJVU },
                { ".djvu", MIME_IMAGE_VND_DJVU },
                { ".svg", MIME_IMAGE_SVG_XML },
                { ".ief", MIME_IMAGE_IEF },
                { ".cgm", MIME_IMAGE_CGM },
                { ".bmp", MIME_IMAGE_BMP },
                { ".xyz", MIME_CHEMICAL_X_XYZ },
                { ".pdb", MIME_CHEMICAL_X_PDB },
                { ".ra", MIME_AUDIO_X_PN_REALAUDIO },
                { ".ram", MIME_AUDIO_X_PN_REALAUDIO },
                { ".m3u", MIME_AUDIO_X_MPEGURL },
                { ".aifc", MIME_AUDIO_X_AIFF },
                { ".aif", MIME_AUDIO_X_AIFF },
                { ".aiff", MIME_AUDIO_X_AIFF },
                { ".mp3", MIME_AUDIO_MPEG },
                { ".mp2", MIME_AUDIO_MPEG },
                { ".mp1", MIME_AUDIO_MPEG },
                { ".mpga", MIME_AUDIO_MPEG },
                { ".kar", MIME_AUDIO_MIDI },
                { ".mid", MIME_AUDIO_MIDI },
                { ".midi", MIME_AUDIO_MIDI },
                { ".dtd", MIME_APPLICATION_XML_DTD },
                { ".xsl", MIME_APPLICATION_XML },
                { ".xml", MIME_APPLICATION_XML },
                { ".xslt", MIME_APPLICATION_XSLT_XML },
                { ".xht", MIME_APPLICATION_XHTML_XML },
                { ".xhtml", MIME_APPLICATION_XHTML_XML },
                { ".src", MIME_APPLICATION_X_WAIS_SOURCE },
                { ".ustar", MIME_APPLICATION_X_USTAR },
                { ".ms", MIME_APPLICATION_X_TROFF_MS },
                { ".me", MIME_APPLICATION_X_TROFF_ME },
                { ".man", MIME_APPLICATION_X_TROFF_MAN },
                { ".roff", MIME_APPLICATION_X_TROFF },
                { ".tr", MIME_APPLICATION_X_TROFF },
                { ".t", MIME_APPLICATION_X_TROFF },
                { ".texi", MIME_APPLICATION_X_TEXINFO },
                { ".texinfo", MIME_APPLICATION_X_TEXINFO },
                { ".tex", MIME_APPLICATION_X_TEX },
                { ".tcl", MIME_APPLICATION_X_TCL },
                { ".sv4crc", MIME_APPLICATION_X_SV4CRC },
                { ".sv4cpio", MIME_APPLICATION_X_SV4CPIO },
                { ".sit", MIME_APPLICATION_X_STUFFIT },
                { ".swf", MIME_APPLICATION_X_SHOCKWAVE_FLASH },
                { ".shar", MIME_APPLICATION_X_SHAR },
                { ".sh", MIME_APPLICATION_X_SH },
                { ".cdf", MIME_APPLICATION_X_NETCDF },
                { ".nc", MIME_APPLICATION_X_NETCDF },
                { ".latex", MIME_APPLICATION_X_LATEX },
                { ".skm", MIME_APPLICATION_X_KOAN },
                { ".skt", MIME_APPLICATION_X_KOAN },
                { ".skd", MIME_APPLICATION_X_KOAN },
                { ".skp", MIME_APPLICATION_X_KOAN },
                { ".js", MIME_APPLICATION_X_JAVASCRIPT },
                { ".hdf", MIME_APPLICATION_X_HDF },
                { ".gtar", MIME_APPLICATION_X_GTAR },
                { ".spl", MIME_APPLICATION_X_FUTURESPLASH },
                { ".dvi", MIME_APPLICATION_X_DVI },
                { ".dxr", MIME_APPLICATION_X_DIRECTOR },
                { ".dir", MIME_APPLICATION_X_DIRECTOR },
                { ".dcr", MIME_APPLICATION_X_DIRECTOR },
                { ".csh", MIME_APPLICATION_X_CSH },
                { ".cpio", MIME_APPLICATION_X_CPIO },
                { ".pgn", MIME_APPLICATION_X_CHESS_PGN },
                { ".vcd", MIME_APPLICATION_X_CDLINK },
                { ".bcpio", MIME_APPLICATION_X_BCPIO },
                { ".rm", MIME_APPLICATION_VND_RNREALMEDIA },
                { ".ppt", MIME_APPLICATION_VND_MSPOWERPOINT },
                { ".mif", MIME_APPLICATION_VND_MIF },
                { ".grxml", MIME_APPLICATION_SRGS_XML },
                { ".gram", MIME_APPLICATION_SRGS },
                { ".smil", MIME_APPLICATION_RDF_SMIL },
                { ".smi", MIME_APPLICATION_RDF_SMIL },
                { ".rdf", MIME_APPLICATION_RDF_XML },
                { ".ogg", MIME_APPLICATION_X_OGG },
                { ".oda", MIME_APPLICATION_ODA },
                { ".dmg", MIME_APPLICATION_OCTET_STREAM },
                { ".lzh", MIME_APPLICATION_OCTET_STREAM },
                { ".so", MIME_APPLICATION_OCTET_STREAM },
                { ".lha", MIME_APPLICATION_OCTET_STREAM },
                { ".dms", MIME_APPLICATION_OCTET_STREAM },
                { ".bin", MIME_APPLICATION_OCTET_STREAM },
                { ".mathml", MIME_APPLICATION_MATHML_XML },
                { ".cpt", MIME_APPLICATION_MAC_COMPACTPRO },
                { ".hqx", MIME_APPLICATION_MAC_BINHEX40 },
                { ".jnlp", MIME_APPLICATION_JNLP },
                { ".ez", MIME_APPLICATION_ANDREW_INSET },
                { ".txt", MIME_TEXT_PLAIN },
                { ".ini", MIME_TEXT_PLAIN },
                { ".c", MIME_TEXT_PLAIN },
                { ".h", MIME_TEXT_PLAIN },
                { ".cpp", MIME_TEXT_PLAIN },
                { ".cxx", MIME_TEXT_PLAIN },
                { ".cc", MIME_TEXT_PLAIN },
                { ".chh", MIME_TEXT_PLAIN },
                { ".java", MIME_TEXT_PLAIN },
                { ".csv", MIME_TEXT_PLAIN },
                { ".bat", MIME_TEXT_PLAIN },
                { ".cmd", MIME_TEXT_PLAIN },
                { ".asc", MIME_TEXT_PLAIN },
                { ".rtf", MIME_TEXT_RTF },
                { ".rtx", MIME_TEXT_RICHTEXT },
                { ".html", MIME_TEXT_HTML },
                { ".htm", MIME_TEXT_HTML },
                { ".zip", MIME_APPLICATION_ZIP },
                { ".rar", MIME_APPLICATION_X_RAR_COMPRESSED },
                { ".gzip", MIME_APPLICATION_X_GZIP },
                { ".gz", MIME_APPLICATION_X_GZIP },
                { ".tgz", MIME_APPLICATION_TGZ },
                { ".tar", MIME_APPLICATION_X_TAR },
                { ".gif", MIME_IMAGE_GIF },
                { ".jpeg", MIME_IMAGE_JPEG },
                { ".jpg", MIME_IMAGE_JPEG },
                { ".jpe", MIME_IMAGE_JPEG },
                { ".tiff", MIME_IMAGE_TIFF },
                { ".tif", MIME_IMAGE_TIFF },
                { ".png", MIME_IMAGE_PNG },
                { ".au", MIME_AUDIO_BASIC },
                { ".snd", MIME_AUDIO_BASIC },
                { ".wav", MIME_AUDIO_X_WAV },
                { ".mov", MIME_VIDEO_QUICKTIME },
                { ".qt", MIME_VIDEO_QUICKTIME },
                { ".mpeg", MIME_VIDEO_MPEG },
                { ".mpg", MIME_VIDEO_MPEG },
                { ".mpe", MIME_VIDEO_MPEG },
                { ".abs", MIME_VIDEO_MPEG },
                { ".doc", MIME_APPLICATION_MSWORD },
                { ".xls", MIME_APPLICATION_VND_MSEXCEL },
                { ".eps", MIME_APPLICATION_POSTSCRIPT },
                { ".ai", MIME_APPLICATION_POSTSCRIPT },
                { ".ps", MIME_APPLICATION_POSTSCRIPT },
                { ".pdf", MIME_APPLICATION_PDF },
                { ".exe", MIME_APPLICATION_OCTET_STREAM },
                { ".dll", MIME_APPLICATION_OCTET_STREAM },
                { ".class", MIME_APPLICATION_OCTET_STREAM },
                { ".jar", MIME_APPLICATION_JAVA_ARCHIVE }
            };

        public static string GetFileExtenstionByMimeType(string mimeType)
        {
            if (!mimeTypes.ContainsValue(mimeType))
                return string.Empty;

            return mimeTypes.FirstOrDefault(e => e.Value == mimeType).Key;
        }

        public static string GetFileMimeTypeByExtenstion(string extension)
        {
            if (!mimeTypes.ContainsKey(extension))
                return string.Empty;

            return mimeTypes[extension];
        }

        public const string MIME_APPLICATION_ANDREW_INSET = "application/andrew-inset";
        public const string MIME_APPLICATION_JSON = "application/json";
        public const string MIME_APPLICATION_ZIP = "application/zip";
        public const string MIME_APPLICATION_X_GZIP = "application/x-gzip";
        public const string MIME_APPLICATION_TGZ = "application/tgz";
        public const string MIME_APPLICATION_MSWORD = "application/msword";
        public const string MIME_APPLICATION_POSTSCRIPT = "application/postscript";
        public const string MIME_APPLICATION_PDF = "application/pdf";
        public const string MIME_APPLICATION_JNLP = "application/jnlp";
        public const string MIME_APPLICATION_MAC_BINHEX40 = "application/mac-binhex40";
        public const string MIME_APPLICATION_MAC_COMPACTPRO = "application/mac-compactpro";
        public const string MIME_APPLICATION_MATHML_XML = "application/mathml+xml";
        public const string MIME_APPLICATION_OCTET_STREAM = "application/octet-stream";
        public const string MIME_APPLICATION_ODA = "application/oda";
        public const string MIME_APPLICATION_RDF_XML = "application/rdf+xml";
        public const string MIME_APPLICATION_JAVA_ARCHIVE = "application/java-archive";
        public const string MIME_APPLICATION_RDF_SMIL = "application/smil";
        public const string MIME_APPLICATION_SRGS = "application/srgs";
        public const string MIME_APPLICATION_SRGS_XML = "application/srgs+xml";
        public const string MIME_APPLICATION_VND_MIF = "application/vnd.mif";
        public const string MIME_APPLICATION_VND_MSEXCEL = "application/vnd.ms-excel";
        public const string MIME_APPLICATION_VND_MSPOWERPOINT = "application/vnd.ms-powerpoint";
        public const string MIME_APPLICATION_VND_RNREALMEDIA = "application/vnd.rn-realmedia";
        public const string MIME_APPLICATION_X_BCPIO = "application/x-bcpio";
        public const string MIME_APPLICATION_X_CDLINK = "application/x-cdlink";
        public const string MIME_APPLICATION_X_CHESS_PGN = "application/x-chess-pgn";
        public const string MIME_APPLICATION_X_CPIO = "application/x-cpio";
        public const string MIME_APPLICATION_X_CSH = "application/x-csh";
        public const string MIME_APPLICATION_X_DIRECTOR = "application/x-director";
        public const string MIME_APPLICATION_X_DVI = "application/x-dvi";
        public const string MIME_APPLICATION_X_FUTURESPLASH = "application/x-futuresplash";
        public const string MIME_APPLICATION_X_GTAR = "application/x-gtar";
        public const string MIME_APPLICATION_X_HDF = "application/x-hdf";
        public const string MIME_APPLICATION_X_JAVASCRIPT = "application/x-javascript";
        public const string MIME_APPLICATION_X_KOAN = "application/x-koan";
        public const string MIME_APPLICATION_X_LATEX = "application/x-latex";
        public const string MIME_APPLICATION_X_NETCDF = "application/x-netcdf";
        public const string MIME_APPLICATION_X_OGG = "application/x-ogg";
        public const string MIME_APPLICATION_X_SH = "application/x-sh";
        public const string MIME_APPLICATION_X_SHAR = "application/x-shar";
        public const string MIME_APPLICATION_X_SHOCKWAVE_FLASH = "application/x-shockwave-flash";
        public const string MIME_APPLICATION_X_STUFFIT = "application/x-stuffit";
        public const string MIME_APPLICATION_X_SV4CPIO = "application/x-sv4cpio";
        public const string MIME_APPLICATION_X_SV4CRC = "application/x-sv4crc";
        public const string MIME_APPLICATION_X_TAR = "application/x-tar";
        public const string MIME_APPLICATION_X_RAR_COMPRESSED = "application/x-rar-compressed";
        public const string MIME_APPLICATION_X_TCL = "application/x-tcl";
        public const string MIME_APPLICATION_X_TEX = "application/x-tex";
        public const string MIME_APPLICATION_X_TEXINFO = "application/x-texinfo";
        public const string MIME_APPLICATION_X_TROFF = "application/x-troff";
        public const string MIME_APPLICATION_X_TROFF_MAN = "application/x-troff-man";
        public const string MIME_APPLICATION_X_TROFF_ME = "application/x-troff-me";
        public const string MIME_APPLICATION_X_TROFF_MS = "application/x-troff-ms";
        public const string MIME_APPLICATION_X_USTAR = "application/x-ustar";
        public const string MIME_APPLICATION_X_WAIS_SOURCE = "application/x-wais-source";
        public const string MIME_APPLICATION_VND_MOZZILLA_XUL_XML = "application/vnd.mozilla.xul+xml";
        public const string MIME_APPLICATION_XHTML_XML = "application/xhtml+xml";
        public const string MIME_APPLICATION_XSLT_XML = "application/xslt+xml";
        public const string MIME_APPLICATION_XML = "application/xml";
        public const string MIME_APPLICATION_XML_DTD = "application/xml-dtd";
        public const string MIME_IMAGE_BMP = "image/bmp";
        public const string MIME_IMAGE_CGM = "image/cgm";
        public const string MIME_IMAGE_GIF = "image/gif";
        public const string MIME_IMAGE_IEF = "image/ief";
        public const string MIME_IMAGE_JPEG = "image/jpeg";
        public const string MIME_IMAGE_TIFF = "image/tiff";
        public const string MIME_IMAGE_PNG = "image/png";
        public const string MIME_IMAGE_SVG_XML = "image/svg+xml";
        public const string MIME_IMAGE_VND_DJVU = "image/vnd.djvu";
        public const string MIME_IMAGE_WAP_WBMP = "image/vnd.wap.wbmp";
        public const string MIME_IMAGE_X_CMU_RASTER = "image/x-cmu-raster";
        public const string MIME_IMAGE_X_ICON = "image/x-icon";
        public const string MIME_IMAGE_X_PORTABLE_ANYMAP = "image/x-portable-anymap";
        public const string MIME_IMAGE_X_PORTABLE_BITMAP = "image/x-portable-bitmap";
        public const string MIME_IMAGE_X_PORTABLE_GRAYMAP = "image/x-portable-graymap";
        public const string MIME_IMAGE_X_PORTABLE_PIXMAP = "image/x-portable-pixmap";
        public const string MIME_IMAGE_X_RGB = "image/x-rgb";
        public const string MIME_AUDIO_BASIC = "audio/basic";
        public const string MIME_AUDIO_MIDI = "audio/midi";
        public const string MIME_AUDIO_MPEG = "audio/mpeg";
        public const string MIME_AUDIO_X_AIFF = "audio/x-aiff";
        public const string MIME_AUDIO_X_MPEGURL = "audio/x-mpegurl";
        public const string MIME_AUDIO_X_PN_REALAUDIO = "audio/x-pn-realaudio";
        public const string MIME_AUDIO_X_WAV = "audio/x-wav";
        public const string MIME_CHEMICAL_X_PDB = "chemical/x-pdb";
        public const string MIME_CHEMICAL_X_XYZ = "chemical/x-xyz";
        public const string MIME_MODEL_IGES = "model/iges";
        public const string MIME_MODEL_MESH = "model/mesh";
        public const string MIME_MODEL_VRLM = "model/vrml";
        public const string MIME_TEXT_PLAIN = "text/plain";
        public const string MIME_TEXT_RICHTEXT = "text/richtext";
        public const string MIME_TEXT_RTF = "text/rtf";
        public const string MIME_TEXT_HTML = "text/html";
        public const string MIME_TEXT_CALENDAR = "text/calendar";
        public const string MIME_TEXT_CSS = "text/css";
        public const string MIME_TEXT_SGML = "text/sgml";
        public const string MIME_TEXT_TAB_SEPARATED_VALUES = "text/tab-separated-values";
        public const string MIME_TEXT_VND_WAP_XML = "text/vnd.wap.wml";
        public const string MIME_TEXT_VND_WAP_WMLSCRIPT = "text/vnd.wap.wmlscript";
        public const string MIME_TEXT_X_SETEXT = "text/x-setext";
        public const string MIME_TEXT_X_COMPONENT = "text/x-component";
        public const string MIME_VIDEO_QUICKTIME = "video/quicktime";
        public const string MIME_VIDEO_MPEG = "video/mpeg";
        public const string MIME_VIDEO_VND_MPEGURL = "video/vnd.mpegurl";
        public const string MIME_VIDEO_X_MSVIDEO = "video/x-msvideo";
        public const string MIME_VIDEO_X_MS_WMV = "video/x-ms-wmv";
        public const string MIME_VIDEO_X_SGI_MOVIE = "video/x-sgi-movie";
        public const string MIME_X_CONFERENCE_X_COOLTALK = "x-conference/x-cooltalk";
    }
}
