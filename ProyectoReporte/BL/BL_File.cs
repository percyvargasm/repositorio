using System;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Data;
using ProyectoReporte.Service;

namespace ProyectoReporte.BL
{
    public class BL_File : IFile
    {
        public bool DownloadFile(string sURLm, string sPath)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(sURLm, sPath);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UnzipFile(string sFile)
        {
            try
            {
                FileInfo fileToDecompress = new FileInfo(sFile);
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    string currentFileName = fileToDecompress.FullName;
                    string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                            Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable LoadInformation(string sFile, DateTime dtDateHour)
        {
            try
            {
                string sLine;
                DataTable dtLoadInformation = GetTable();
                StreamReader srFile = new StreamReader(sFile);
                while ((sLine = srFile.ReadLine()) != null)
                {
                    sLine.Replace("\t", "");
                    string[] sValues = sLine.Split(" ");
                    if (sValues.Length == 4)
                        dtLoadInformation.Rows.Add(dtDateHour, sValues[0].ToString(), sValues[1].ToString(), int.Parse(sValues[2].ToString()), int.Parse(sValues[3].ToString()));
                }
                return dtLoadInformation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetTable()
        {
            DataTable dtInformation = new DataTable();
            dtInformation.Columns.Add("fecha", typeof(DateTime));
            dtInformation.Columns.Add("domainCode", typeof(string));
            dtInformation.Columns.Add("pageTitle", typeof(string));
            dtInformation.Columns.Add("viewCount", typeof(int));
            dtInformation.Columns.Add("responseBytes", typeof(int));
            return dtInformation;
        }
    }
}
