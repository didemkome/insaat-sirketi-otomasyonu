using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class TedarikciEkle : Form
    {
        SQL connect = new SQL();

        public TedarikciEkle()
        {
            InitializeComponent();
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
        private void TedarikciEkle_Load(object sender, EventArgs e)
        {
            connect.baglantıacmak();

            foreach (Control item in this.pnl_tedarikciekle1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            foreach (Control item in this.pnl_tedarikciekle2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            foreach (Control item in this.pnl_tedarikciekle3.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            foreach (Control item in this.pnl_tedarikciekle4.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
            foreach (Control item in this.pnl_tyetkilikisi.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
        }
        string turutext = "";
        public bool nullcontrol(TextBox txt)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return false;
            }
            return true;
        }
        public bool radiobutkontrol()
        {

            if (radioted_gercek.Checked == true)
            {
                turutext = "Gercek Kisi";
                return true;
            }
            else if (radioted_tuzel.Checked == true)
            {
                turutext = "Tüzel Kisi";
                return true;
            }
            return false;
        }

        private void btn_tKaydet_Click(object sender, EventArgs e)
        {
            if (nullcontrol(txt_TFirmaUnvani) == false || nullcontrol(txt_ttelefonno) == false || radiobutkontrol()==false
                || nullcontrol(txt_tvergino) == false /*|| IsValidEmail(txtyetkiliposta_m.Text) == false || IsValidEmail(txt_teposta_m.Text) == false*/)

            {
                MessageBox.Show("LUTFEN * İLE BELİRTİLEN ALANLARI DOLDURUNUZ !");
            }
            else
            {
                try
                {
                    connect.baglanticontrol();
                    SqlCommand komut = new SqlCommand("TedarikciFirmaEkle", connect.baglantıadresi());
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.Parameters.AddWithValue("@FirmaUnvani", SqlDbType.NVarChar).Value = txt_TFirmaUnvani.Text.Trim();
                    komut.Parameters.AddWithValue("@FirmaVergiNo", SqlDbType.Int).Value = txt_tvergino.Text.Trim();
                    komut.Parameters.AddWithValue("@Faks", SqlDbType.VarChar).Value = txt_tfaksno.Text.Trim();
                    komut.Parameters.AddWithValue("@Eposta", SqlDbType.NVarChar).Value = txt_teposta.Text.Trim();
                    komut.Parameters.AddWithValue("@TelefonNumarasi", SqlDbType.VarChar).Value = txt_ttelefonno.Text.Trim();
                    komut.Parameters.AddWithValue("@Mahalle", SqlDbType.NVarChar).Value = txt_tmahalle.Text.Trim();
                    komut.Parameters.AddWithValue("@Sokak", SqlDbType.NVarChar).Value = txt_tsokak.Text.Trim();
                    komut.Parameters.AddWithValue("@KapiNo", SqlDbType.Int).Value = txt_tkapino.Text.Trim();
                    komut.Parameters.AddWithValue("@İlce", SqlDbType.NVarChar).Value = txt_tilce.Text.Trim();
                    komut.Parameters.AddWithValue("@İl", SqlDbType.NVarChar).Value = txt_til.Text.Trim();
                    komut.Parameters.AddWithValue("@IBAN", SqlDbType.NVarChar).Value = txt_tIBAN.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliAdiSoyadi", SqlDbType.NVarChar).Value = txt_yetkilikisiadi.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliEposta", SqlDbType.NVarChar).Value = txt_yetkilikisieposta.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliTelefon", SqlDbType.VarChar).Value = txt_yetkilikisitelefon.Text.Trim();
                    komut.Parameters.AddWithValue("@Türü", SqlDbType.NVarChar).Value = turutext.Trim();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Tedarikci Eklendi ! ");
                    this.Close();
                }
                catch (Exception )
                {
                    MessageBox.Show("Tedarikci Eklenemedi ! ");
                    this.Close();
                }
            }
        }

        private void txt_ttelefonno_KeyPress(object sender, KeyPressEventArgs e)
        {

            karakterkontrol(sender, e, txt_ttelefonno);
        }

        private void txt_tfaksno_KeyPress(object sender, KeyPressEventArgs e)
        {

            karakterkontrol(sender, e,txt_tfaksno);
        }

        private void txt_yetkilikisitelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            karakterkontrol(sender, e, txt_yetkilikisitelefon  );
        }
    }
}