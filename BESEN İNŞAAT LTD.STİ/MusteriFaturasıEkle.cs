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
using System.Net.Mail;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class MusteriFaturasıEkle : Form
    {

        SqlDataReader dr;
        SQL connect = new SQL();
        public MusteriFaturasıEkle()
        {
            InitializeComponent();
        }
        public bool nullcontrol(TextBox txt)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return false;
            }
            return true;
        }
        public void stringkontrol(object sender, KeyPressEventArgs e)
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
   
        private void MusteriFaturasıEkle_Load(object sender, EventArgs e)
        {
            connect.baglantıacmak();
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT distinct FirmaUnvani,Türü,UrunKodu,UrunAdi FROM Müsteri,Urun";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmb_musterifatura_musterisec.Items.Add(dr["FirmaUnvani"]);
                cmbmusterifaturası_urunkodu.Items.Add(dr["UrunKodu"]);
                cmb_musterifaturası_alınanurun.Items.Add(dr["UrunAdi"]);
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
            cmb_musterifaturası_alınanurun.SelectedIndex = -1;
            cmb_musterifatura_musterisec.SelectedIndex = -1;
            cmb_mkdv.SelectedIndex = -1;
            cmbmusterifaturası_urunkodu.SelectedIndex = -1;
            connect.baglantıkapamak();
        }
        string ödendibilgisi = "";
      

      
        private void btn_mfKaydet_Click(object sender, EventArgs e)
        {
            if (nullcontrol(txtmusterifaturası_acıklama) == false && nullcontrol(txt_urunmiktari) == false && nullcontrol(txt_musterifaturası_teslimeden) == false && nullcontrol(txt_musterifaturası_teslimalan) == false && nullcontrol(txt_satisbirimi) == false && nullcontrol(txt_musterifaturası_caribakiye) == false && nullcontrol(txt_musterifaturası_toplamtutar) == false && cmbmusterifaturası_urunkodu.SelectedIndex == -1 && cmb_musterifaturası_alınanurun.SelectedIndex == -1 && cmb_mkdv.SelectedIndex == -1 && cmb_musterifatura_musterisec.SelectedIndex == -1)

            {
                MessageBox.Show("LUTFEN * İLE BELİRTİLEN ALANLARI DOLDURUNUZ !");
            }
            else
            {
                try
                {
                    connect.baglanticontrol();
                    string musteriadi = cmb_musterifatura_musterisec.SelectedItem.ToString();
                    SqlCommand komuttext = new SqlCommand();

                    komuttext.CommandText = "SELECT MusteriID FROM Müsteri where FirmaUnvani='" + musteriadi + "'";
                    komuttext.Connection = connect.baglantıadresi();
                    komuttext.CommandType = CommandType.Text;
                    dr = komuttext.ExecuteReader();

                    string musteriID = "";
                    while (dr.Read())
                    {
                        musteriID = (dr["MusteriID"]).ToString();
                    }
                    SqlCommand komut = new SqlCommand("MusteriFaturasiOlustur", connect.baglantıadresi());
                    komut.CommandType = CommandType.StoredProcedure;
                    dr.Close();
                    komut.Parameters.AddWithValue("@FaturaAciklamasi", SqlDbType.NVarChar).Value = txtmusterifaturası_acıklama.Text.Trim();
                    komut.Parameters.AddWithValue("@DüzenlemeTarihi", SqlDbType.DateTime).Value = Convert.ToDateTime(datetime_musterifatura_dznlemetarihi.Text.Trim());
                    komut.Parameters.AddWithValue("@VadeTarihi", SqlDbType.DateTime).Value = Convert.ToDateTime(dateTimePicker_mvadetarihi.Text.Trim());
                    komut.Parameters.AddWithValue("@TeslimEden", SqlDbType.NVarChar).Value = txt_musterifaturası_teslimeden.Text.Trim();
                    komut.Parameters.AddWithValue("@TeslimAlan", SqlDbType.Int).Value = txt_musterifaturası_teslimalan.Text.Trim();
                    komut.Parameters.AddWithValue("@ÖdendiBilgisi", SqlDbType.NVarChar).Value = ödendibilgisi.Trim();
                    komut.Parameters.AddWithValue("@KDV", SqlDbType.Float).Value = cmb_mkdv.Text.Trim();
                    komut.Parameters.AddWithValue("@GenelToplam", SqlDbType.Money).Value = txt_musterifaturası_toplamtutar.Text.Trim();
                    komut.Parameters.AddWithValue("@ToplamFiyat", SqlDbType.Money).Value = txt_musterifaturası_toplamtutar.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunAdi", SqlDbType.NVarChar).Value = cmb_musterifaturası_alınanurun.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunKodu", SqlDbType.NVarChar).Value = cmbmusterifaturası_urunkodu.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunMiktari", SqlDbType.Int).Value = txt_urunmiktari.Text.Trim();
                    komut.Parameters.AddWithValue("@FirmaUnvani", SqlDbType.NVarChar).Value = cmb_musterifatura_musterisec.SelectedItem.ToString().Trim();
                    komut.Parameters.AddWithValue("@MusteriID", SqlDbType.Int).Value = int.Parse(musteriID);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Fatura OLusturuldu"); this.Close();

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

        private void chk_mödendi_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_mödendi.Checked == true)
            {
                panel_vadetarihi.Visible = false;
            }
            else
            {
                panel_vadetarihi.Visible = true;
            }
        }
        public bool Gonder(string konu, string icerik, string icerik2, string icerik3)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("aayseilkay@gmail.com");
            connect.baglantıacmak();
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM Müsteri'";
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;
            dr = komut.ExecuteReader();
            string musteriEposta = "";
            while (dr.Read())
            {
                musteriEposta = (dr["Eposta"]).ToString();
                ePosta.To.Add("Eposta");
            }
            dr.Close();

            ePosta.Subject = konu;

            ePosta.Body = ("Sayin " + icerik + "," + Environment.NewLine + Environment.NewLine + icerik2 + " tarihinde aldığınız ürünlerin toplam ücreti " + icerik3 + " TL tutmuştur." + Environment.NewLine + "BEŞEN İNŞAAT LTD.STİ tercih ettiğiniz için teşekkür ederiz.");
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("aayseilkay@gmail.com", "ilkay1995");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
            }
            return kontrol;
        }
        private void btn_emailgönder_Click(object sender, EventArgs e)
        {
            connect.baglantıacmak();
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM MusteriFaturasi WHERE FirmaUnvani='" + cmb_musterifatura_musterisec.Text.ToString() + "'AND DüzenlemeTarihi='" + datetime_musterifatura_dznlemetarihi.Value.Date.ToString("yyyy-MM-dd") + "'" + "' AND GenelToplam= '" + txt_musterifaturası_toplamtutar.ToString();
            komut.Connection = connect.baglantıadresi();
            komut.CommandType = CommandType.Text;

            dr = komut.ExecuteReader();
            string musteriFirmaUnvani,tarih, geneltoplam;
            while (dr.Read())
            {
                musteriFirmaUnvani = (dr["FirmaUnvani"]).ToString();
                tarih = (dr["DüzenlemeTarihi"]).ToString();
                geneltoplam = (dr["GenelToplam"]).ToString();
                MessageBox.Show("Mailiniz Yollanmıştır.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Gonder("Unutmuş Olduğunuz Şifreniz Ektedir", musteriFirmaUnvani, tarih, geneltoplam);
            }
            dr.Close();
        }
    }
}