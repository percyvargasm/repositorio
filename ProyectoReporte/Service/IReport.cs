using System.Data;

namespace ProyectoReporte.Service
{
    public interface IReport
    {
        public void LangDomainViewCount(DataTable dtInformation);

        public void PageTileCountodView(DataTable dtInformation);
    }
}
