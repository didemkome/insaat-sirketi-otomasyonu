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
    public partial class YöneticiAnasayfa : Form
    {
        Excelveri exceleveritransferi = new Excelveri();
        urunekle UrunEkle = new urunekle();
        TedarikciEkle TedarikciEkle = new TedarikciEkle();
        TedarikciFaturasıEkle TFaturaekle = new TedarikciFaturasıEkle();
        MusteriEkle Musteriekle = new MusteriEkle();
        MusteriFaturasıEkle MFaturaekle = new MusteriFaturasıEkle();
        DataTable dt = new DataTable();
        SQL connect = new SQL();
        SqlCommand komut = new SqlCommand();
        SqlDataReader dr;
        SqlDataAdapter da;

        public YöneticiAnasayfa()
        {
            InitializeComponent();
        }
        public void datagridfiltesiz(string butonname)
        {
            if (butonname == "Müsteri")
            {

                dataGridView_musteriler.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGridView_musteriler.DataSource = null;

                dataGridView_musteriler.Refresh();

                da = new SqlDataAdapter("Select MusteriID,[FirmaUnvani],[Türü],[Eposta],[TelefonNumarasi],[Faks],[IBAN],[Mahallle],[Sokak],[KapiNo],[İlce],[İl],[FirmaVergiNo],[CariBakiyesi],[YetkiliAdiSoyadi],[YetkiliEposta],[YetkiliTelefon]From Müsteri where SilindiBilgisi=0", connect.baglantıadresi());
                da.Fill(dt);
                dataGridView_musteriler.DataSource = dt;

            }
            else if (butonname == "Urun")
            {
                datagrid_urun.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                datagrid_urun.DataSource = null;

                datagrid_urun.Refresh();
                da = new SqlDataAdapter("Select [UrunID],[UrunKodu],[UrunAdi],[UrunKategorisi],[StokMiktari],[StokTakibi],[AlısFiyati],[SatisFiyati],[UrunBirimi],[UrunAmbalajBilgisi]From Urun where SilindiBilgisi=0", connect.baglantıadresi());
                da.Fill(dt);
                datagrid_urun.DataSource = dt;
            }
            else if (butonname == "Satıs")
            {
                dataGrid_satıs.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGrid_satıs.DataSource = null;

                dataGrid_satıs.Refresh();
                da = new SqlDataAdapter("Select MusteriID,[FirmaUnvani],[FaturaAciklamasi],[DüzenlemeTarihi],[VadeTarihi] ,[GenelToplam],[ÖdendiBilgisi] From MusteriFaturasi ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_satıs.DataSource = dt;
            }

            else if (butonname == "TedarikciFirma")
            {
                dataGrid_tedarik.ClearSelection();
                dt.Clear();
                dt.Columns.Clear();
                dataGrid_tedarik.DataSource = null;

                dataGrid_tedarik.Refresh();
                da = new SqlDataAdapter("Select TedarikciID,[FirmaUnvani],[Türü],[Eposta],[TelefonNumarasi],[Faks],[IBAN],[Mahalle],[Sokak],[KapiNo] ,[İl],[İlce],[FirmaVergiNo],[YetkiliAdiSoyadi][YetkiliEposta],[YetkiliTelefon] From TedarikciFirma where SilindiBilgisi=0 ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_tedarik.DataSource = dt;
            }
            dt.Dispose();
            da.Dispose();
        }
        private void YöneticiAnasayfa_Load(object sender, EventArgs e)
        {
            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;
            connect.baglanticontrol();
            datagridfiltesiz("Satıs");

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = false;
            connect.baglantıkapamak();
            this.reportViewer_Raporlama.RefreshReport();
        }

        private void btnSatislar_Click(object sender, EventArgs e)
        {
            comboBox_satıs.Text = "Filtrele";
            connect.baglanticontrol();
            datagridfiltesiz("Satıs");

            Satıslar_anasayfa.Visible = true;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;


            dataGrid_satıs.Visible = true;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = false;

            connect.baglantıkapamak();
        }

        private void btnMüsteriler_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            datagridfiltesiz("Müsteri");

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = true;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = true;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = false;

            comboboxmusteriler();
            connect.baglantıkapamak();
        }

        private void btnTedarikciler_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            datagridfiltesiz("TedarikciFirma");

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = true;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = true;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = false;
            comboboxtedarikci();
            connect.baglantıkapamak();

        }

        private void btnRaporlama_Click(object sender, EventArgs e)
        {

        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            //connect.baglanticontrol();
            //datagridfiltesiz("Urunler");

            //Satıslar_anasayfa.Visible = false;
            //Musteriler_anasayfa.Visible = false;
            //urunler_anasayfa.Visible = true;
            //Tedarikciler_anasayfa.Visible = false;
            //kasa_anasayfa.Visible = false;
            //Raporlama_anasayfa.Visible = false;

            //dataGrid_satıs.Visible = false;
            //dataGrid_tedarik.Visible = false;
            //datagrid_urun.Visible = true;
            //dataGridView_musteriler.Visible = false;
            //dataGridView_kasa.Visible = false;
            //reportViewer_Raporlama.Visible = false;
            //comboboxurun();
            //connect.baglantıkapamak();

        }

        private void btn_tedarikciler_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            TedarikciEkle.ShowDialog();
            datagridfiltesiz("TedarikciFirma");
            comboboxtedarikci();
            connect.baglantıkapamak();
        }

        private void btn_MüsteriEkle_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            Musteriekle.ShowDialog();
            datagridfiltesiz("Müsteri");
            comboboxmusteriler();
            connect.baglantıkapamak();
        }

        private void btn_yeni_fatura_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            MFaturaekle.ShowDialog();
            datagridfiltesiz("Satıs");
            connect.baglantıkapamak();
        }

        private void btn_kasaa_Click(object sender, EventArgs e)
        {
            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = true;
            Raporlama_anasayfa.Visible = false;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = true;
            reportViewer_Raporlama.Visible = false;
        }

        private void btnUrunlerana_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            datagridfiltesiz("Urun");

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = true;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = false;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = true;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = false;

            comboboxurun();
            connect.baglantıkapamak();
        }

        private void btn_raporlamaana_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();

            Satıslar_anasayfa.Visible = false;
            Musteriler_anasayfa.Visible = false;
            urunler_anasayfa.Visible = false;
            Tedarikciler_anasayfa.Visible = false;
            kasa_anasayfa.Visible = false;
            Raporlama_anasayfa.Visible = true;

            dataGrid_satıs.Visible = false;
            dataGrid_tedarik.Visible = false;
            datagrid_urun.Visible = false;
            dataGridView_musteriler.Visible = false;
            dataGridView_kasa.Visible = false;
            reportViewer_Raporlama.Visible = true;
        }

        private void btn_urunekle_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            UrunEkle.ShowDialog();
            datagridfiltesiz("Urun");
            comboboxurun();

            connect.baglantıkapamak();
        }
        private void cmb_Musteriler_SelectedIndexChanged(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            dataGridView_musteriler.ClearSelection();
            dt.Clear();
            dt.Columns.Clear();
            dataGridView_musteriler.DataSource = null;
            dataGridView_musteriler.Refresh();

            string FirmaUnvani;
            FirmaUnvani = cmb_Musteriler.SelectedItem.ToString();
            da = new SqlDataAdapter("Select * From Müsteri where FirmaUnvani = '" + FirmaUnvani + "'", connect.baglantıadresi());
            da.Fill(dt);
            dataGridView_musteriler.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            btn_musterisil.Enabled = true;
            btn_MüsteriEkle.Enabled = true;
            btn_musteriguncelle.Enabled = true;
            dr.Close();
            connect.baglantıkapamak();
        }

        private void comboBox_satıs_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            dataGrid_satıs.DataSource = null;
            dataGrid_satıs.ClearSelection();
            dt.Columns.Clear();
            dataGrid_satıs.Refresh();
            btn_yeni_fatura.Visible = true;
            connect.baglanticontrol();
            if (comboBox_satıs.SelectedItem.ToString() == "Tedarikci Firma")
            {
                da = new SqlDataAdapter("Select [TedarikciFirmaUnvani],[FaturaAciklamasi],[DüzenlemeTarihi],[VadeTarihi],[ÖdendiBilgisi],[UrunAdi],[UrunMiktari] From TedarikciFirmaFaturasi ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_satıs.DataSource = dt;
            }
            else
            {
                da = new SqlDataAdapter("Select [FirmaUnvani],[FaturaAciklamasi],[DüzenlemeTarihi],[VadeTarihi] ,[GenelToplam],[ÖdendiBilgisi] From MusteriFaturasi ", connect.baglantıadresi());
                da.Fill(dt);
                dataGrid_satıs.DataSource = dt;
            }
            dt.Dispose();
            da.Dispose();

            connect.baglantıkapamak();
        }

        private void cmb_tedarikciler_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            dataGrid_tedarik.DataSource = null;
            dataGrid_tedarik.ClearSelection();
            dt.Columns.Clear();
            dataGrid_tedarik.Refresh();
            connect.baglanticontrol();
            da = new SqlDataAdapter("Select * From TedarikciFirma where FirmaUnvani='" + cmb_tedarikciler.SelectedItem.ToString() + "'", connect.baglantıadresi());
            da.Fill(dt);
            dataGrid_tedarik.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            dr.Close();
            connect.baglantıkapamak();
        }

        private void cmb_urunler_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            datagrid_urun.DataSource = null;
            datagrid_urun.ClearSelection();
            dt.Columns.Clear();
            datagrid_urun.Refresh();
            connect.baglanticontrol();
            da = new SqlDataAdapter("Select * From Urun where UrunKategorisi = '" + cmb_urunler.SelectedItem.ToString() + "'", connect.baglantıadresi());
            da.Fill(dt);
            datagrid_urun.DataSource = dt;
            dt.Dispose();
            da.Dispose();
            //btn_urunekle.Visible = true;
            dr.Close();
            connect.baglantıkapamak();
        }

        private void btn_tveriaktar_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_tedarik.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("TedarikciFirma"));
            }

            else if (radio_exceleaktar_tedarik.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGrid_tedarik, "TedarikciFirma");
            }
        }

        private void btn_tedarikcisil_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            string TedarikciSil = "";

            foreach (DataGridViewRow drow in dataGrid_tedarik.SelectedRows)
            {
                int TedarikciID = Convert.ToInt32(drow.Cells[0].Value);
                TedarikciSil = connect.TedarikciSil(TedarikciID);
            }
            MessageBox.Show(TedarikciSil);
            datagridfiltesiz("TedarikciFirma");
            comboboxtedarikci();
            dr.Close();
            connect.baglantıkapamak();
        }

        private void btn_Tfaturaekle_Click_1(object sender, EventArgs e)
        {
            
            connect.baglanticontrol();
            TFaturaekle.ShowDialog();
            datagridfiltesiz("TedarikciFirma");
            comboboxtedarikci();
            connect.baglantıkapamak();
        }

        private void btn_tedarikcigüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                da = new SqlDataAdapter("Select * From TedarikciFirma", connect.baglantıadresi());
                dataGrid_tedarik.DataSource = dt;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(dt);
                MessageBox.Show("Kayıt güncellendi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);
                datagridfiltesiz("TedarikciFirma");
                connect.baglantıkapamak();
            }
            catch
            {
                MessageBox.Show("Hata! Kayıt Güncellenemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Mveriaktar_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_musteriler.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("Müsteri"));
            }

            else if (radio_exceleaktar_musteri.Checked == true)
            {
                exceleveritransferi.ExportToExcel(dataGridView_musteriler, "Müsteri");
            }
        }

        private void btn_musterisil_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            cmb_Musteriler.Text = "Filtrele";
            foreach (DataGridViewRow drow in dataGridView_musteriler.SelectedRows)
            {
                int MusteriID = Convert.ToInt32(drow.Cells[0].Value);

                connect.MusteriSil(MusteriID);
            }
            MessageBox.Show("Müsteri Silindi");
            connect.baglanticontrol();
            datagridfiltesiz("Müsteri");

            comboboxmusteriler();
            connect.baglantıkapamak();
        }

        private void btn_musteriguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                da = new SqlDataAdapter("Select * From Müsteri", connect.baglantıadresi());
                dataGridView_musteriler.DataSource = dt;
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

        private void btn_urunveriaktar_Click(object sender, EventArgs e)
        {
            if (radio_exceldenaktar_urun.Checked == true)
            {
                MessageBox.Show(exceleveritransferi.Exceldenveriaktar("Urun"));
            }

            else if (radio_exceleaktar_satıs.Checked == true)
            {
                exceleveritransferi.ExportToExcel(datagrid_urun, "Urun");
            }
        }

        private void btn_urunsil_Click(object sender, EventArgs e)
        {
            connect.baglanticontrol();
            string UrunSil = "";
            foreach (DataGridViewRow drow in datagrid_urun.SelectedRows)
            {
                int UrunID = Convert.ToInt32(drow.Cells[10].Value);
                UrunSil = connect.UrunSil(UrunID);
            }
            MessageBox.Show(UrunSil);
            datagridfiltesiz("Urun");
            comboboxurun();
            connect.baglantıkapamak();
        }

        private void btn_urunguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                connect.baglanticontrol();
                da = new SqlDataAdapter("Select * From Urun", connect.baglantıadresi());
                datagrid_urun.DataSource = dt;
                SqlCommandBuilder cmdb = new SqlCommandBuilder(da);
                da.Update(dt);
                MessageBox.Show("Kayıt güncellendi!", "Bilgilendirme Penceresi", MessageBoxButtons.OK);
                datagridfiltesiz("Urun");
                connect.baglantıkapamak();
            }
            catch
            {
                MessageBox.Show("Hata! Kayıt Güncellenemedi", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btn_satıslarveriaktar_Click(object sender, EventArgs e)
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
        public void comboboxurun()
        {
            cmb_urunler.Items.Clear();
            cmb_urunler.Text = "Filtrele";
            komut.CommandText = "SELECT distinct UrunKategorisi FROM Urun ";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;
            connect.baglanticontrol();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_urunler.Items.Add(dr["UrunKategorisi"]);
            }
            dr.Close();
        }
        public void comboboxtedarikci()
        {
            cmb_tedarikciler.Items.Clear();

            cmb_tedarikciler.Text = "Filtrele";
            komut.CommandText = "SELECT distinct FirmaUnvani FROM TedarikciFirma where SilindiBilgisi=0";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            connect.baglanticontrol();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_tedarikciler.Items.Add(dr["FirmaUnvani"]);
            }
        }
        public void comboboxmusteriler()
        {
            cmb_Musteriler.Items.Clear();

            cmb_Musteriler.Text = "Filtrele";
            komut.CommandText = "SELECT distinct FirmaUnvani FROM Müsteri where SilindiBilgisi=0";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_Musteriler.Items.Add(dr["FirmaUnvani"]);
            }
            dr.Close();
        }
    }
}