using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.IO.Compression;

namespace HazardIdentifySystemApi.Commons
{
    public class CheckFileHelper
    {
        public static readonly List<string> validMimeTypeLs = new List<string>
                   {
                            "image/jpeg",
                            "image/png" ,
                            "image/gif",
                            "application/pdf",
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ,
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                            "video/mp4" ,
                        };

        public static bool IsValidDocXFile(string filePath)
        {
            try
            {
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                // check mimeType is of docx
                if (mimeType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    return false;
                if (new FileInfo(filePath).Extension?.ToLower().Contains(".docx") == false)
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.CanSeek)
                        fileStream.Position = 0;

                    byte[] buffer = new byte[4];
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = 0; // reset stream position

                    // if element of buffer all equal 0 -> file  doest not have content
                    // ZIP magic number for 365 office files : 	50 4B 03 04 
                    var zipHeaderRs = buffer[0] == 0x50 &&
                           buffer[1] == 0x4B &&
                           buffer[2] == 0x03 &&
                           buffer[3] == 0x04;
                    if (zipHeaderRs == false) return false;

                    // ZipArchive thuộc thư viện System.IO.Compression
                    using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                    {
                        // Check for Word-specific structure
                        var hasContentTypes = archive.GetEntry("[Content_Types].xml") != null;
                        var hasWordDocument = archive.GetEntry("word/document.xml") != null;

                        return hasContentTypes && hasWordDocument;
                    }

                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool IsValidXlsxFile(string filePath)
        {
            try
            {  
               

                string mimeType = MimeMapping.GetMimeMapping(filePath);
                // check mimeType is of xlsx
                if (mimeType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    return false;
                if (new FileInfo(filePath).Extension?.ToLower().Contains(".xlsx") == false)
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.CanSeek)
                        fileStream.Position = 0;

                    byte[] buffer = new byte[4];
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = 0; // reset stream position

                    // if element of buffer all equal 0 -> file  doest not have content
                    // ZIP magic number for Office 365 files : 	50 4B 03 04 
                    var zipHeaderRs = buffer[0] == 0x50 &&
                           buffer[1] == 0x4B &&
                           buffer[2] == 0x03 &&
                           buffer[3] == 0x04;
                    if (zipHeaderRs == false) return false;

                    using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                    {
                        // Check for Word-specific structure
                        var hasContentTypes = archive.GetEntry("[Content_Types].xml") != null;
                        var hasWordDocument = archive.GetEntry("xl/workbook.xml") != null;

                        return hasContentTypes && hasWordDocument;
                    }


                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public static bool IsValidPptXFile(string filePath)
        {
            try
            {
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                // check mimeType is of pptx
                if (mimeType != "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                    return false;
                if (new FileInfo(filePath).Extension?.ToLower().Contains(".pptx") == false)
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.CanSeek)
                        fileStream.Position = 0;

                    byte[] buffer = new byte[4];
                    fileStream.Read(buffer, 0, 4);
                    fileStream.Position = 0; // reset stream position

                    // if element of buffer all equal 0 -> file  doest not have content
                    // ZIP magic number for Office 365 file : 	50 4B 03 04 
                    var zipHeaderRs = buffer[0] == 0x50 &&
                           buffer[1] == 0x4B &&
                           buffer[2] == 0x03 &&
                           buffer[3] == 0x04;
                    if (zipHeaderRs == false) return false;

                    using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                    {
                        // Check for Word-specific structure
                        var hasContentTypes = archive.GetEntry("[Content_Types].xml") != null;
                        var hasWordDocument = archive.GetEntry("ppt/presentation.xml") != null;

                        return hasContentTypes && hasWordDocument;
                    }


                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }



        public static bool IsValidPdfFile(string filePath)
        {
            try
            {
                // Check MIME type
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                if (mimeType != "application/pdf")
                    return false;

                // Check file extension
                if (new FileInfo(filePath).Extension?.ToLower() != ".pdf")
                    return false;

                // Check PDF magic number
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.CanSeek)
                        fileStream.Position = 0;

                    byte[] buffer = new byte[5];
                    int bytesRead = fileStream.Read(buffer, 0, 5);
                    fileStream.Position = 0;

                    if (bytesRead < 5)
                        return false;

                    // Check for %PDF- header
                    return buffer[0] == 0x25 && // %
                           buffer[1] == 0x50 && // P
                           buffer[2] == 0x44 && // D
                           buffer[3] == 0x46 && // F
                           buffer[4] == 0x2D;   // -
                }
            }
            catch
            {
                return false;
            }
        }



        public static bool IsValidJpegFile(string filePath)
        {
            try
            {
                // Check MIME type
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                if (mimeType != "image/jpeg")
                    return false;

                // Check file extension
                string extension = Path.GetExtension(filePath)?.ToLower();
                if (extension != ".jpg" && extension != ".jpeg")
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.Length < 4) // JPEG files are at least a few bytes
                        return false;

                    byte[] startBytes = new byte[2];
                    byte[] endBytes = new byte[2];

                    // Read the first two bytes
                    fileStream.Read(startBytes, 0, 2);

                    // Read the last two bytes
                    fileStream.Seek(-2, SeekOrigin.End);
                    fileStream.Read(endBytes, 0, 2);

                    // Check JPEG start and end markers
                    bool hasValidStart = startBytes[0] == 0xFF && startBytes[1] == 0xD8;
                    bool hasValidEnd = endBytes[0] == 0xFF && endBytes[1] == 0xD9;

                    return hasValidStart && hasValidEnd;
                }
            }
            catch
            {
                return false;
            }
        }



        public  static bool IsValidPngFile(string filePath)
        {
            try
            {
                // Check MIME type
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                if (mimeType != "image/png")
                    return false;

                // Check file extension
                string extension = Path.GetExtension(filePath)?.ToLower();
                if (extension != ".png")
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.Length < 8) // PNG header is 8 bytes
                        return false;

                    byte[] pngSignature = new byte[8];
                    fileStream.Read(pngSignature, 0, 8);

                    // PNG header bytes: 89 50 4E 47 0D 0A 1A 0A
                    return pngSignature[0] == 0x89 &&
                           pngSignature[1] == 0x50 &&
                           pngSignature[2] == 0x4E &&
                           pngSignature[3] == 0x47 &&
                           pngSignature[4] == 0x0D &&
                           pngSignature[5] == 0x0A &&
                           pngSignature[6] == 0x1A &&
                           pngSignature[7] == 0x0A;
                }
            }
            catch
            {
                return false;
            }
        }



        public static bool IsValidGifFile(string filePath)
        {
            try
            {
                // Check MIME type
                string mimeType = MimeMapping.GetMimeMapping(filePath);
                if (mimeType != "image/gif")
                    return false;

                // Check file extension
                string extension = Path.GetExtension(filePath)?.ToLower();
                if (extension != ".gif")
                    return false;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.Length < 6) // GIF header is 6 bytes
                        return false;

                    byte[] header = new byte[6];
                    fileStream.Read(header, 0, 6);

                    // Check for GIF87a or GIF89a
                    string headerString = System.Text.Encoding.ASCII.GetString(header);
                    return headerString == "GIF87a" || headerString == "GIF89a";
                }
            }
            catch
            {
                return false;
            }
        }


        public static bool IsValidMp4File(string filePath)
        {
            try
            {

                // Check MIME type
                string mimeType = MimeMapping.GetMimeMapping(filePath);

                if (mimeType != "video/mp4")
                    return false;

                // Check file extension
                if (new FileInfo(filePath).Extension?.ToLower() != ".mp4")
                    return false;

                // Check file signature
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream.Length < 12)
                        return false;

                    byte[] buffer = new byte[12];
                    fileStream.Read(buffer, 0, 12);

                    // Check for 'ftyp' box at bytes 4-7
                    if (buffer[4] == 'f' && buffer[5] == 't' && buffer[6] == 'y' && buffer[7] == 'p')
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }


    }
}