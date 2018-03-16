using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class Anasayfa : Form
    {
        Excelveri exceleveritransferi = new Excelveri();
        urunekle UrunEkle = new urunekle();
        TedarikciFaturasıEkle Faturaekle = new TedarikciFaturasıEkle();
        MusteriEkle Musteriekle = new MusteriEkle();
        MusteriFaturasıEkle m_fatura = new MusteriFaturasıEkle();
        DataTable dt = new DataTable();
        SQL connect = new SQL();
        SqlCommand komut = new SqlCommand();
        SqlDataReader dr;
        SqlDataAdapter da;

        public Anasayfa()
        {
            InitializeComponent();
        }
        public void datagridfiltesiz(string butonname)
        {
            if (butonname == "Müsteri")
            {

                dataGrid_musteriler.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGrid_musteriler.DataSource = null;

                dataGrid_musteriler.Refresh();

                da = new SqlDataAdapter("Select * From Müsteri ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_musteriler.DataSource = dt;
            }
            else if (butonname == "Urun")
            {
                dataGrid_urun.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGrid_urun.DataSource = null;

                dataGrid_urun.Refresh();
                da = new SqlDataAdapter("Select * From Urun ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_urun.DataSource = dt;
            }
            else if (butonname == "Satıs")
            {
                dataGrid_satıs.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGrid_satıs.DataSource = null;
                dataGrid_satıs.Refresh();
                da = new SqlDataAdapter("Select * From MusteriFaturasi ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_satıs.DataSource = dt;
            }
            dt.Dispose();
            da.Dispose();
        }
        private void Anasayfa_Load(object sender, EventArgs e)
        {
            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            connect.baglanticontrol();
            datagridfiltesiz("Satıs");

            Satıslar_anasayfa.Visible = true;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;

            dataGrid_satıs.Visible = true;
            dataGrid_musteriler.Visible = false;
            dataGrid_urun.Visible = false;

            komut.CommandText = "SELECT distinct FirmaUnvani FROM Müsteri";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_satıslar.Items.Add(dr["FirmaUnvani"]);
            }

            dr.Close();
            connect.baglantıkapamak();
        }
        private void btnSatislar_Click(object sender, EventArgs e)
        {

            connect.baglanticontrol();
            datagridfiltesiz("Satıs");

            Satıslar_anasayfa.Visible = true;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;

            dataGrid_satıs.Visible = true;
            dataGrid_musteriler.Visible = false;
            dataGrid_urun.Visible = false;

            komut.CommandText = "SELECT FirmaUnvani FROM Müsteri";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_satıslar.Items.Add(dr["FirmaUnvani"]);
            }

            dr.Close();
            connect.baglantıkapamak();
        }

        private void btnMüsteriler_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();


            datagridfiltesiz("Müsteri");


            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = true;
            urunler_anasayfa.Visible = false;


            dataGrid_satıs.Visible = false;
            dataGrid_musteriler.Visible = true;
            dataGrid_urun.Visible = false;

            komut.CommandText = "SELECT distinct FirmaUnvani FROM Müsteri";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_Musteriler.Items.Add(dr["FirmaUnvani"]);
            }
            dr.Close();
            connect.baglantıkapamak();
        }

        private void btnUrunler_Click_1(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            datagridfiltesiz("Urun");

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = true;

            dataGrid_satıs.Visible = false;
            dataGrid_musteriler.Visible = false;
            dataGrid_urun.Visible = true;
            komut.CommandText = "SELECT distinct UrunKategorisi FROM Urun ";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_urunler.Items.Add(dr["UrunKategorisi"]);
            }
            dr.Close();
            connect.baglantıkapamak();
        }

        private void btn_musteriekle_Click(object sender, EventArgs e)
        {

            connect.baglanticontrol();
            Musteriekle.ShowDialog();
            datagridfiltesiz("Müsteri");
            connect.baglantıkapamak();
        }

        private void btn_urunekle_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            UrunEkle.ShowDialog();
            datagridfiltesiz("Urun");
            connect.baglantıkapamak();
        }

        private void btn_yeni_fatura_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            m_fatura.ShowDialog();
            datagridfiltesiz("Satıs");
            connect.baglantıkapamak();
        }

        private void cmb_Musteriler_SelectedIndexChanged(object sender, EventArgs e)
        {

            connect.baglanticontrol();
            dataGrid_musteriler.ClearSelection();
            dt.Clear();
            dt.Columns.Clear();
            dataGrid_musteriler.DataSource = null;

            dataGrid_musteriler.Refresh();
            string FirmaUnvani;
            FirmaUnvani = cmb_Musteriler.SelectedItem.ToString();
            da = new SqlDataAdapter("Select * From Müsteri where FirmaUnvani = '" + FirmaUnvani + "'", connect.baglantıadresi());
            da.Fill(dt);
            dataGrid_musteriler.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            btn_MusteriSil.Enabled = true;
            btn_musteriekle.Enabled = true;
            btn_musteriguncelle.Enabled = true;
            dr.Close();
            connect.baglantıkapamak();
        }

        private void cmb_urunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            dataGrid_urun.ClearSelection();
            dt.Clear();
            dt.Columns.Clear();
            dataGrid_urun.DataSource = null;

            dataGrid_urun.Refresh();
            string UrunKategorisi;
            UrunKategorisi = cmb_urunler.SelectedItem.ToString();
            da = new SqlDataAdapter("Select * From Urun  where UrunKategorisi = '" + UrunKategorisi + "'", connect.baglantıadresi());
            da.Fill(dt);
            dataGrid_urun.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            btn_urunekle.Visible = true;
            btn_urunguncelle.Visible = true;
            btn_urunsil.Visible = true;
            dr.Close();
            connect.baglantıkapamak();
        }

        private void cmb_satıslar_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            dataGrid_satıs.DataSource = null;
            dataGrid_satıs.ClearSelection();
            dt.Columns.Clear();
            dataGrid_satıs.Refresh();
            btn_yeni_fatura.Visible = true;
            connect.baglanticontrol();
            da = new SqlDataAdapter("Select * From Müsteri where FirmaUnvani='" + cmb_satıslar.SelectedItem.ToString() + "'", connect.baglantıadresi());
            da.Fill(dt);
            dataGrid_satıs.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            dr.Close();
            connect.baglantıkapamak();
        }

        private void btn_MusteriSil_Click(object sender, EventArgs e)
        {

            connect.baglanticontrol();
            string musterisil = "";
            cmb_Musteriler.Text = "Filtrele";
            foreach (DataGridViewRow drow in dataGrid_musteriler.SelectedRows)
            {
                int MusteriID = Convert.ToInt32(drow.Cells[0].Value);
                musterisil = connect.MusteriSil(MusteriID);
            }
            MessageBox.Show(musterisil);
            datagridfiltesiz("Müsteri");
            connect.baglantıkapamak();
        }

        private void btn_Search_musteriler_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_musteriler.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("Müsteri"));
            }
            else if (radio_exceleaktar_musteri.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGrid_musteriler, "Müsteri");
            }
        }

        private void btn_search_satıslar_Click(object sender, EventArgs e)
        {

            if (radio_exceldenaktar_satıs.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("MusteriFaturasi"));
            }
            else if (radio_exceleaktar_satıs.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGrid_satıs, "MusteriFaturasi");
            }
        }

        private void btn_urunler_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_urun.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("Urun"));
            }

            else if (radio_exceleaktar_satıs.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGrid_urun, "Urun");
            }
        }

        private void btn_urunsil_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            string urunsil = " ";
            cmb_urunler.Text = "Filtrele";
            foreach (DataGridViewRow drow in dataGrid_urun.SelectedRows)
            {
                int UrunID = Convert.ToInt32(drow.Cells[10].Value);
                urunsil = connect.UrunSil(UrunID);
            }
            MessageBox.Show(urunsil);
            datagridfiltesiz("Urun");
            connect.baglantıkapamak();
        }


        private void btn_musteriler_veriaktar_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_musteriler.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("MusteriFaturasi"));
            }
            else if (radio_exceleaktar_musteri.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGrid_satıs, "MusteriFaturasi");
            }
        }

        private void btn_musteriguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                da = new SqlDataAdapter("Select * From Müsteri", connect.baglantıadresi());
                dataGrid_musteriler.DataSource = dt;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(dt);
                MessageBox.Show("Kayıt güncellendi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);

                
                datagridfiltesiz("Müsteri");
                connect.baglantıkapamak();
            }
            catch
            {
                MessageBox.Show("Hata! Kayıt Güncellenemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_urunguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                da = new SqlDataAdapter("Select * From Urun", connect.baglantıadresi());
                dataGrid_urun.DataSource = dt;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(dt);
                
                MessageBox.Show("Kayıt güncellendi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);
                datagridfiltesiz("Urun");
                cmb_urunler.Refresh();
                connect.baglantıkapamak();
            }
            catch
            {
                MessageBox.Show("Hata! Kayıt Güncellenemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


