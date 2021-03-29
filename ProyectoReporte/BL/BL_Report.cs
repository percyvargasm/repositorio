using System;
using System.Data;
using System.Linq;
using ProyectoReporte.Service;

namespace ProyectoReporte.BL
{
    public class BL_Report : IReport
    {
        public void LangDomainViewCount(DataTable dtInformation)
        {
            var vPrint = (from row in dtInformation.AsEnumerable()
                          group row by new
                          {
                              Period = row.Field<DateTime>("fecha"),
                              Language = row.Field<string>("domainCode"),
                              Domain = row.Field<string>("pageTitle")
                          } into grp
                          select new
                          {
                              Period = grp.Key.Period,
                              Language = grp.Key.Language,
                              Domain = grp.Key.Domain,
                              ViewCount = grp.Sum(r => r.Field<int>("viewCount"))
                          }
                          ).ToList();
            Console.WriteLine("Period\tLanguage\tDomain\tViewCount");
            foreach (var value in vPrint)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", value.Period, value.Language, value.Domain, value.ViewCount);
            }
        }

        public void PageTileCountodView(DataTable dtInformation)
        {
            var vPrint = (from row in dtInformation.AsEnumerable()
                          group row by new
                          {
                              Period = row.Field<DateTime>("fecha"),
                              PageTitle = row.Field<string>("pageTitle")
                          } into grp
                          select new
                          {
                              Period = grp.Key.Period,
                              PageTitle = grp.Key.PageTitle,
                              ViewCount = grp.Sum(r => r.Field<int>("viewCount"))
                          }
                          ).ToList();
            Console.WriteLine("Period\tLanguage\tDomain\tViewCount");
            foreach (var value in vPrint)
            {
                Console.WriteLine("{0}\t{1}\t{2}", value.Period, value.PageTitle, value.ViewCount);
            }
        }
    }
}
