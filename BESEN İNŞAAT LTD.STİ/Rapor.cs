using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class Rapor : Form
    {
        public Rapor()
        {
            InitializeComponent();
        }
        SQL connect = new SQL();
        private void Rapor_Load(object sender, EventArgs e)
        {
            DataSet Ds = new DataSet();
            connect.baglanticontrol();
            string Querry = "exec AylaraGöreSatılanUrunMiktarları";
            SqlCommand cmd = new SqlCommand(Querry, connect.baglantıadresi());
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            adap.Fill(Ds);

            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "Raporlama.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            if (Ds.Tables.Count > 0)
            {
                ReportDataSource datasource = new ReportDataSource("AylaraGoreUrun", Ds.Tables[0]);
                reportViewer1.LocalReport.DataSources.Add(datasource);
                reportViewer1.RefreshReport();
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
