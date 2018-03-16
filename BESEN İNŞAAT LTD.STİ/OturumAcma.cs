using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace BESEN_İNŞAAT_LTD.STİ
{
    public partial class OturumAcma : Form
    {
        public OturumAcma()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;
        SqlCommand komut = new SqlCommand();
        
        
        private void OturumAcma_Load(object sender, EventArgs e)
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
            pc_Login.BackColor = Color.Transparent;
        }

      

        private void btn_oturum_Click(object sender, EventArgs e)
        {
            string giris,sifre,yetki,sql = "SELECT * FROM Kullanıcı WHERE Kullanıcı_Adı='"+txt_Username.Text+"'AND Sifre='"+txt_Password.Text+"'";

            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();
            giris = txt_Username.Text;
            sifre = txt_Password.Text;

            if(oku.Read())
            
            {
                yetki = oku["Rol"].ToString();
                if (yetki == "yonetici")
            {


                    this.Hide();
                    YöneticiAnasayfa ana = new YöneticiAnasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }
                }
                else if(yetki == "kullanıcı")
                {
                    this.Hide();
                    Anasayfa ana = new Anasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }
                }
            } 
            else
            {
                MessageBox.Show("Kullanıcı veya Sifre Yanlıs \nLutfen Tekrar Deneyiniz !");

            }
            oku.Close();

        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {
            txt_Username.Text += "";
        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            txt_Password.Text += "";
        }

        private void customOvoidButton1_Click(object sender, EventArgs e)
        {
            string giris, sifre, yetki, sql = "SELECT * FROM Kullanıcı WHERE Kullanıcı_Adı='" + txt_Username.Text + "'AND Sifre='" + txt_Password.Text + "'";

            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();
            giris = txt_Username.Text;
            sifre = txt_Password.Text;
            if (oku.Read())

            {
                yetki = oku["Rol"].ToString();
                if (yetki == "yonetici")
                {


                    this.Hide();
                    YöneticiAnasayfa ana = new YöneticiAnasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }


                }
                else if (yetki == "kullanıcı")
                {
                    this.Hide();
                    Anasayfa ana = new Anasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı veya Sifre Yanlıs \nLutfen Tekrar Deneyiniz !");

            }
            oku.Close();

        }
        private void label3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void timer_login_Tick(object sender, EventArgs e)
        {
    //        string giris, sifre, yetki, sql = "SELECT * FROM Kullanıcı WHERE Kullanıcı_Adı='" + txt_Username.Text + "'AND Sifre='" + txt_Password.Text + "'";

    //        SqlCommand com = new SqlCommand(sql, baglanti);
    //        SqlDataReader oku = com.ExecuteReader();
    //        giris = txt_Username.Text;
    //        sifre = txt_Password.Text;
    //        if(giris != "" && sifre != "")
    //        {
    //            timer_login.Stop();
    //            txt_Username.Text = giris;
    //            txt_Password.Text = sifre;
    //        }
    //        if (oku.Read())

    //        {
    //            yetki = oku["Rol"].ToString();
    //            if (yetki == "yonetici")
    //            {


    //                this.Hide();
    //                YöneticiAnasayfa ana = new YöneticiAnasayfa();
    //                DialogResult di = ana.ShowDialog();
    //                if (di == DialogResult.OK)
    //                {
    //                    this.ShowDialog();
    //                    baglanti.Close();
    //                }
    //                else
    //                {
    //                    Application.Exit();
    //                    baglanti.Close();
    //                }


    //            }
    //            else if (yetki == "kullanıcı")
    //            {
    //                this.Hide();
    //                Anasayfa ana = new Anasayfa();
    //                DialogResult di = ana.ShowDialog();
    //                if (di == DialogResult.OK)
    //                {
    //                    this.ShowDialog();
    //                    baglanti.Close();
    //                }
    //                else
    //                {
    //                    Application.Exit();
    //                    baglanti.Close();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            MessageBox.Show("Kullanıcı veya Sifre Yanlıs \nLutfen Tekrar Deneyiniz !");

    //        }
    //        oku.Close();
      }

        private void lbl_Login_Click(object sender, EventArgs e)
        {

        }

        private void lbl_sifremiunuttum_Click(object sender, EventArgs e)
        {
            SifremiUnuttum sifremiunuttum = new SifremiUnuttum();
            this.Hide();
            sifremiunuttum.ShowDialog();
        }

        private void btn_oturumac_Click(object sender, EventArgs e)
        {
            string giris, sifre, yetki, sql = "SELECT * FROM Kullanıcı WHERE Kullanıcı_Adı='" + txt_Username.Text + "'AND Sifre='" + txt_Password.Text + "'";

            SqlCommand com = new SqlCommand(sql, baglanti);
            SqlDataReader oku = com.ExecuteReader();
            giris = txt_Username.Text;
            sifre = txt_Password.Text;
            if (oku.Read())

            {
                yetki = oku["Rol"].ToString();
                if (yetki == "yonetici")
                {


                    this.Hide();
                    YöneticiAnasayfa ana = new YöneticiAnasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }


                }
                else if (yetki == "kullanıcı")
                {
                    this.Hide();
                    Anasayfa ana = new Anasayfa();
                    DialogResult di = ana.ShowDialog();
                    if (di == DialogResult.OK)
                    {
                        this.ShowDialog();
                        baglanti.Close();
                    }
                    else
                    {
                        Application.Exit();
                        baglanti.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı veya Sifre Yanlıs \nLutfen Tekrar Deneyiniz !");

            }
            oku.Close();

        }
    }
}