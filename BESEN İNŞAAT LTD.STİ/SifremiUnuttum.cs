using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.SqlClient;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class SifremiUnuttum : Form
    {
        SQL connect = new SQL();
        SqlConnection baglanti;

        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        private void SifremiUnuttum_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection("server=DESKTOP-M5E1QKG;Initial Catalog=Besen.İnsaat.Ltd.Sti.;Integrated Security=true");
                baglanti.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("BAGLANTI OLUSTURULAMADI ! ");
            }
        }
        public bool Gonder(string konu, string icerik, string icerik2)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("aayseilkay@gmail.com");
            ePosta.To.Add(txt_email.Text);

            ePosta.Subject = konu;

            ePosta.Body = ("Kullanıcı Adınız: " + icerik2 + " Şifreniz: " + icerik);
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
        private void lbl_sifremiunuttumhide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_yolla_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            string kullaniciadi, sifre, yetki, sql = "SELECT * FROM Kullanıcı WHERE TcNo='" + txt_tcno.Text.ToString() + "'AND Email='" + txt_email.Text.ToString() + "'";
            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();

            if (oku.Read())
            {
                yetki = oku["Rol"].ToString();
                if (yetki == "yonetici")
                {
                    kullaniciadi = oku["Kullanıcı_Adı"].ToString();
                    sifre = oku["Sifre"].ToString();
                    MessageBox.Show("Şifreniz Mailinize Yollanmıştır.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
                    Gonder("Unutmuş Olduğunuz Şifreniz Ektedir", sifre, kullaniciadi);
                    this.Hide();
                    OturumAcma oturumacma = new OturumAcma();
                    oturumacma.ShowDialog();
                }
                else if (yetki == "kullanıcı")
                {
                    kullaniciadi = oku["Kullanıcı_Adı"].ToString();
                    sifre = oku["Sifre"].ToString();
                    MessageBox.Show("Şifreniz Mailinize Yollanmıştır.", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                    Gonder("Unutmuş Olduğunuz Şifreniz Ektedir", sifre, kullaniciadi);
                    this.Hide();
                    OturumAcma oturumacma = new OturumAcma();
                    oturumacma.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı!", "Bilgilendirme Penceresi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            oku.Close();
        }
    }
}
