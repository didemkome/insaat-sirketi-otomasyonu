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
using System.Text.RegularExpressions;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class MusteriEkle : Form
    {


        public MusteriEkle()
        {
            InitializeComponent();
        }
        SQL connect = new SQL();
        string turutext = "";
        public void radiobutkontrol(RadioButton rd)
        {

            if (rd.Checked == true)
            {
                turutext = "Gercek Kisi";

            }
            else if (rd.Checked == true)
            {
                turutext = "Tüzel Kisi";

            }
            else
            {

                turutext = "Tüzel Kisi";
            }

        }
        public  void stringkontrol(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar);
        }
        public void numerickontrol(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar);

        }
        public void karakterkontrol(object sender, KeyPressEventArgs e,TextBox txt)
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
       private void MusteriEkle_Load(object sender, EventArgs e)
        {
            connect.baglantıacmak();

            foreach (Control item in this.pnl_Mutericiekle.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
            foreach (Control item in this.pnl_musteriekle2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }
            }
            foreach (Control item in this.pnl_musteriekle3.Controls)
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
        }        
        public bool nullcontrol(TextBox txt)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return false;
            }
            return true;
        }
        private void btn_mKaydet_Click(object sender, EventArgs e)
        {
            if (nullcontrol(txt_TFirmaUnvani_m) == false || nullcontrol(txt_caribakiye) == false ||nullcontrol(txt_mvergino) == false || nullcontrol(txt_ttelefonno_m) == false /*|| IsValidEmail(txtyetkiliposta_m.Text) == false || IsValidEmail(txt_teposta_m.Text) == false*/)

            {
                MessageBox.Show("LUTFEN * İLE BELİRTİLEN ALANLARI DOLDURUNUZ !");
            }
            else
            {
                try
                {
                    connect.baglanticontrol();
                    SqlCommand komut = new SqlCommand("MusteriEkle", connect.baglantıadresi());
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.Parameters.AddWithValue("@FirmaUnvani", SqlDbType.NVarChar).Value = txt_TFirmaUnvani_m.Text.Trim();
                    komut.Parameters.AddWithValue("@FirmaVergiNo", SqlDbType.Int).Value = txt_mvergino.Text.Trim();
                    komut.Parameters.AddWithValue("@Faks", SqlDbType.VarChar).Value = txt_tfaksno_m.Text.Trim();
                    komut.Parameters.AddWithValue("@Eposta", SqlDbType.NVarChar).Value = txt_teposta_m.Text.Trim();
                    komut.Parameters.AddWithValue("@TelefonNumarasi", SqlDbType.VarChar).Value = txt_ttelefonno_m.Text.Trim();
                    komut.Parameters.AddWithValue("@Mahallle", SqlDbType.NVarChar).Value = txt_mmahalle.Text.Trim();
                    komut.Parameters.AddWithValue("@Sokak", SqlDbType.NVarChar).Value = txt_msokak.Text.Trim();
                    komut.Parameters.AddWithValue("@KapiNo", SqlDbType.Int).Value = txt_mkapino.Text.Trim();
                    komut.Parameters.AddWithValue("@İlce", SqlDbType.NVarChar).Value = txt_milce.Text.Trim();
                    komut.Parameters.AddWithValue("@İl", SqlDbType.NVarChar).Value = txt_mil.Text.Trim();
                    komut.Parameters.AddWithValue("@IBAN", SqlDbType.NVarChar).Value = txt_mIBAN.Text.Trim();
                    komut.Parameters.AddWithValue("@CariBakiyesi", SqlDbType.Money).Value = txt_caribakiye.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliAdiSoyadi", SqlDbType.NVarChar).Value = txtyetkiliad_m.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliEposta", SqlDbType.NVarChar).Value = txtyetkiliposta_m.Text.Trim();
                    komut.Parameters.AddWithValue("@YetkiliTelefon", SqlDbType.VarChar).Value = txtyetkilitel_m.Text.Trim();
                    komut.Parameters.AddWithValue("@Türü", SqlDbType.NVarChar).Value = turutext.Trim();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Musteri Olusturuldu !", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    this.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Musteri Olusturulamadı !");
                    this.Close();
                }
            }
        }

        private void txt_ttelefonno_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            karakterkontrol(sender, e, txt_ttelefonno_m);

        }

        private void txt_tfaksno_m_KeyPress(object sender, KeyPressEventArgs e)
        {

            karakterkontrol(sender, e, txt_tfaksno_m);
        }

        private void txtyetkilitel_m_KeyPress(object sender, KeyPressEventArgs e)
        {

            karakterkontrol(sender, e, txtyetkilitel_m);
        }

        //bool IsValidEmail(string email)
        //{

        //    var addr = new System.Net.Mail.MailAddress(email);
        //    return addr.Address == email;


        //}


    }
}