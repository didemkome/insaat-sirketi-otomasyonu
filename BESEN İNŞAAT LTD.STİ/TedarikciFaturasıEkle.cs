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
    public partial class TedarikciFaturasıEkle : Form
    {
        public TedarikciFaturasıEkle()
        {
            InitializeComponent();
        }
        
        SqlDataReader dr;
        SQL connect = new SQL();
        private void TedarikciFaturasıEkle_Load(object sender, EventArgs e)
        {

            connect.baglantıacmak();
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT distinct FirmaUnvani,Türü,UrunKodu,UrunAdi FROM TedarikciFirma,Urun";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_tedarikcisec.Items.Add(dr["FirmaUnvani"]);
                cmbtedarikcifaturası_urunkodu.Items.Add(dr["UrunKodu"]);
                cmb_tedarikcifaturası_alınanurun.Items.Add(dr["UrunAdi"]);
            }
            dr.Close();
            foreach (Control item in this.pnl_faturaekle2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
            foreach (Control item in this.pnl_faturaekle3.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
            cmb_tedarikcifaturası_alınanurun.SelectedIndex = -1;
            cmb_tedarikcisec.SelectedIndex = -1;
            cmb_tkdv.SelectedIndex = -1;
            cmbtedarikcifaturası_urunkodu.SelectedIndex = -1;
            connect.baglantıkapamak();
        }
        public void stringkontrol(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar);
        }
        public void numerickontrol(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar);

        }
        public void karakterkontrol(object sender, KeyPressEventArgs e, TextBox txt)
        {
            if ((Char)Keys.Back != e.KeyChar)
            {
                if (!Char.IsDigit(e.KeyChar) || txt.Text.Length > 10)
                {
                    e.Handled = true;
                }
            }

        }
        public void sembol(object sender, KeyPressEventArgs e)
        {
            if ((Char)Keys.Back != e.KeyChar)
            {
                if (Char.IsDigit(e.KeyChar) == false && char.IsLetter(e.KeyChar) == false && Char.IsSymbol(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }
        public void harfvesayı(object sender, KeyPressEventArgs e)
        {
            if ((Char)Keys.Back != e.KeyChar)
            {
                if (Char.IsDigit(e.KeyChar) == false && char.IsLetter(e.KeyChar) == false)
                {
                    e.Handled = true;
                }
            }
        }
            public bool nullcontrol(TextBox txt)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return false;
            }
            return true;
        }

        string ödendibilgisi = "";
        public bool checkkontrol()

        {
            if (chk_tödendi.Checked == true)
            {
                ödendibilgisi = "Ödendi";
                return true;
            }
            else
            { ödendibilgisi = "Ödenmedi";
            }
            return false;
        }
        private void btn_tfKaydet_Click(object sender, EventArgs e)
        {
            if (nullcontrol(txt_tfaturaacikla) == false && nullcontrol(txt_urunmiktari) == false && nullcontrol(txt_alissatisbirimi) == false && nullcontrol(txt_ÖdenecekTutar) == false && cmbtedarikcifaturası_urunkodu.SelectedIndex == -1 && cmb_tedarikcifaturası_alınanurun.SelectedIndex == -1 && cmb_tkdv.SelectedIndex == -1 && cmb_tedarikcisec.SelectedIndex == -1)

            {
                MessageBox.Show("LUTFEN * İLE BELİRTİLEN ALANLARI DOLDURUNUZ !");
            }
            else
            {
                try
                {
                    connect.baglanticontrol();
                    string tedarikci = cmb_tedarikcisec.SelectedItem.ToString();
                    SqlCommand kommutt = new SqlCommand();
                    kommutt.CommandText = "SELECT TedarikciID FROM TedarikciFirma where FirmaUnvani='" + tedarikci + "'";
                    kommutt.Connection = connect.baglantıadresi();
                    kommutt.CommandType = CommandType.Text;
                    dr = kommutt.ExecuteReader();

                    string tedarikciID = "";
                    while (dr.Read())
                    {
                        tedarikciID = (dr["TedarikciID"]).ToString();
                    }
                    
                    SqlCommand komut = new SqlCommand("TedarikciFirmaFaturasiEkle", connect.baglantıadresi());
                    komut.CommandType = CommandType.StoredProcedure;
                    dr.Close();
                    komut.CommandText = "TedarikciFirmaFaturasiEkle";
                    komut.Parameters.AddWithValue("@TedarikciFirmaUnvani", SqlDbType.NVarChar).Value = cmb_tedarikcisec.SelectedItem.ToString();
                    komut.Parameters.AddWithValue("@FaturaAciklamasi", SqlDbType.NVarChar).Value = txt_tfaturaacikla.Text.Trim();
                    komut.Parameters.AddWithValue("@DüzenlemeTarihi", SqlDbType.DateTime).Value = Convert.ToDateTime(datetime_dznlemetarihi.Text.Trim());
                    komut.Parameters.AddWithValue("@VadeTarihi", SqlDbType.DateTime).Value = Convert.ToDateTime(dateTimePicker_tvadetarihi.Text.Trim());
                    komut.Parameters.AddWithValue("@KDV", SqlDbType.Int).Value = cmb_tkdv.Text.Trim();
                    komut.Parameters.AddWithValue("@KDVTutari", SqlDbType.Money).Value = txt_tkdvtutari.Text.Trim();
                    komut.Parameters.AddWithValue("@ÖdendiBilgisi", SqlDbType.NVarChar).Value = ödendibilgisi.Trim();
                    komut.Parameters.AddWithValue("@IskontoOrani", SqlDbType.Float).Value = txt_tiskontaorani.Text.Trim();
                    komut.Parameters.AddWithValue("@IskontoTutari", SqlDbType.Money).Value = txt_tiskontotutari.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunAdi", SqlDbType.NVarChar).Value = cmb_tedarikcifaturası_alınanurun.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunKodu", SqlDbType.Int).Value = cmbtedarikcifaturası_urunkodu.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunMiktari", SqlDbType.Int).Value = txt_urunmiktari.Text.Trim();
                    komut.Parameters.AddWithValue("@ToplamIskontoTutari", SqlDbType.Money).Value = txt_tiskontotutari.Text.Trim();
                    komut.Parameters.AddWithValue("@TedarikciID", SqlDbType.Int).Value = Convert.ToInt16(tedarikciID);
                    komut.Parameters.AddWithValue("@ÖdenecekTutar", SqlDbType.Money).Value = txt_ÖdenecekTutar.Text.Trim();
                    komut.Parameters.AddWithValue("@MalzemeHizmetTutari", SqlDbType.Money).Value = txt_Hizmettutari.Text.Trim();
                    komut.Parameters.AddWithValue("@MalzemeHizmetToplamTutari", SqlDbType.Money).Value = txt_hizmettoplamtutar.Text.Trim();
                    komut.ExecuteNonQuery();
                  
                    MessageBox.Show("Fatura Olusturuldu!");
                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Fatura Olusturulamadı!"); this.Close();

                }
                finally
                {
                    dr.Close();
                }
            }
        }
        private void chk_tödendi_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tödendi.Checked == true)
            {
                lbl_vadetarihi.Visible = false;
                dateTimePicker_tvadetarihi.Visible = false;
            }
            else
            {
                lbl_vadetarihi.Visible = true;
            }
        }

     
    }
}
