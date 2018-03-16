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
    public partial class urunekle : Form
    {
        
        public urunekle()
        {
            InitializeComponent();
        }
        SQL connect = new SQL();
      
        public  void stringkontrol(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsDigit(e.KeyChar);
        }
        public  void numerickontrol(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar);
        }
        public  bool nullcontrol(TextBox txt)
        {

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return false;
            }return true;
    
        }
        public  bool combocontrol(ComboBox cmb)
        {
            if (cmb.SelectedIndex == -1)
            {
                return true;
            }return false;
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
        string StokTakibi ;
        public bool radiobutkontrol()
        {

            if (radio_urun_yapılsın.Checked == true)
            {
                StokTakibi = "Yapılsın";
                return true;
            }
            else if (radio_urun_yapılmasın.Checked == true)
            {
                StokTakibi = "Yapılmasın";
                return true;
            }
            return false;
        }
        private void urunekle_Load(object sender, EventArgs e)
        {
            connect.baglantıacmak();
            foreach (Control item in this.pnl_urunler1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            foreach (Control item in this.pnl_urunekle2.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            foreach (Control item in this.pnl_urunekle3.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Clear();
                }


            }
            

        }

        private void btn_urunKaydet_Click(object sender, EventArgs e)
        {
            if (nullcontrol(txt_urunadi) == false || nullcontrol(txt_urunkodu_urunekle) == false
               || radiobutkontrol() == false || combocontrol(cmb_urunkategorisi_urunekle) == true || nullcontrol(txt_baslangıcstok_urunekle) == false || nullcontrol(txt_urunbrimi_urunekle) == false || nullcontrol(txt_urunambalaj) == false || nullcontrol(txt_Alisfiyati) == false || nullcontrol(txt_satisfiyati) == false)

            {
                MessageBox.Show("LUTFEN * İLE BELİRTİLEN ALANLARI DOLDURUNUZ !");
            }
            else
            {


                try
                {
                    connect.baglanticontrol();
                    SqlCommand komut = new SqlCommand("UrunEkle", connect.baglantıadresi());
                    komut.CommandType = CommandType.StoredProcedure;
                    komut.Parameters.AddWithValue("@UrunKodu", SqlDbType.NVarChar).Value = txt_urunkodu_urunekle.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunKategorisi", SqlDbType.Int).Value = cmb_urunkategorisi_urunekle.SelectedItem.ToString().Trim();
                    komut.Parameters.AddWithValue("@UrunAdi", SqlDbType.Int).Value = txt_urunadi.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunAmbalajBilgisi", SqlDbType.NVarChar).Value = txt_urunambalaj.Text.Trim();
                    komut.Parameters.AddWithValue("@UrunBirimi", SqlDbType.Int).Value = txt_urunbrimi_urunekle.Text.Trim();
                    komut.Parameters.AddWithValue("@StokMiktari", SqlDbType.NVarChar).Value = txt_baslangıcstok_urunekle.Text.Trim();
                    komut.Parameters.AddWithValue("@AlısFiyati", SqlDbType.NVarChar).Value = txt_Alisfiyati.Text.Trim();
                    komut.Parameters.AddWithValue("@SatisFiyati", SqlDbType.Int).Value = txt_satisfiyati.Text.Trim();
                    komut.Parameters.AddWithValue("@StokTakibi", SqlDbType.NVarChar).Value = StokTakibi.Trim();
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ürün Eklendi !");
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ürün Eklenemedi !");
                    this.Close();
                }

            }
        }

       
    }
}
