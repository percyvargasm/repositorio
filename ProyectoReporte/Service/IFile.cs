using System;
using System.Data;

namespace ProyectoReporte.Service
{
    public interface IFile
    {
        public bool DownloadFile(string sURL, string sPath);

        public bool UnzipFile(string sFile);

        public DataTable LoadInformation(string sFile, DateTime dtDateHour);

        public DataTable GetTable();
    }
}
