using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using ProyectoReporte.Service;
using ProyectoReporte.BL;

namespace ProyectoReporte
{
    class Program
    {
        private static string sURL;
        private static string sFileName;
        private static string sPathFile;
        private static string sExtension;
        private static int intHours;
        private static DataTable dtInformation;
        private static DateTime dDateHour;
        static void Main(string[] args)
        {
            int i = 0;
            dDateHour = DateTime.Now;
            IFile iFile = new BL_File();
            IReport iReport = new BL_Report();
            InitValues();
            dtInformation = iFile.GetTable();
            while (i <= intHours)
            {
                dDateHour = dDateHour.AddHours(-1 * i);
                string sUrlDownload = GetUrlDownload();
                string sPathFile = GetPathFile();
                if (iFile.DownloadFile(sUrlDownload, sPathFile))
                    if (iFile.UnzipFile(sPathFile))
                    {
                        DataTable dtAux = iFile.LoadInformation(sPathFile.Substring(0, sPathFile.Length - 3), dDateHour);
                        if (dtAux != null)
                        {
                            dtInformation.Merge(dtAux);
                            i++;
                        }                            
                    }
            }
            iReport.LangDomainViewCount(dtInformation);
            Console.ReadLine();
            iReport.PageTileCountodView(dtInformation);
            Console.ReadLine();
        }

        private static DateTime GetInitialDateTime(string[] values)
        {
            string sInicitalDate = values[0];
            string sInicitalHour = values[1];
            return Convert.ToDateTime(sInicitalDate + " " + sInicitalHour);
        }

        private static void InitValues()
        {
            IConfigurationRoot settings = InitConfig();
            sFileName = settings.GetSection("MySettings")["FileName"];
            sExtension = settings.GetSection("MySettings")["Extension"];
            sURL = settings.GetSection("MySettings")["URL"];
            sPathFile = settings.GetSection("MySettings")["PathFile"];
            intHours = int.Parse(settings.GetSection("MySettings")["Hours"]);
        }

        private static IConfigurationRoot InitConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: false);
            
            return builder.Build();
        }

        private static string GetUrlDownload()
        {
            string strReturn = sURL + dDateHour.ToString("yyyy") + "/" + dDateHour.ToString("yyyy-MM") + "/" + sFileName + dDateHour.ToString("yyyyMMdd") + "-" + dDateHour.ToString("HH0000") + sExtension;
            Console.WriteLine("URL: " + strReturn);
            return strReturn;
        }

        private static string GetPathFile()
        {
            string strReturn = sPathFile + sFileName + dDateHour.ToString("yyyyMMdd") + "-" + dDateHour.ToString("HH0000") + sExtension;
            Console.WriteLine("Path: " + strReturn);
            return strReturn;
        }
    }
}
