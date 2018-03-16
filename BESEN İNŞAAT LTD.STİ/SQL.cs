using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BESEN_İNŞAAT_LTD.STİ
{
    class SQL
    {

        SqlCommand komut = new SqlCommand();


        SqlConnection baglanti = new SqlConnection("server=DESKTOP-M5E1QKG;Initial Catalog=Besen.İnsaat.Ltd.Sti.;Integrated Security=true");

        public SqlConnection baglantıadresi()
        {
            return baglanti;
        }
        public void baglanticontrol()
        {
            if (baglanti.State == System.Data.ConnectionState.Closed)
            {
                baglantıacmak();
            }
        }
        public string baglantıacmak()
        {

            try
            {
                baglanti.Open();
                return "BAGLANTI OLUSTURULDU";

            }
            catch (Exception)
            {
                return "BAGLANTI OLUSTURULAMADI ! ";
            }

        }
        public string MusteriSil(int MusteriID)
        {
            try
            {
                baglanticontrol();
                string sql = "UPDATE Müsteri SET SilindiBilgisi=1 WHERE MusteriID="+MusteriID+"";
                komut = new SqlCommand(sql, baglantıadresi());
                komut.ExecuteNonQuery();
                return "Müsteri Silindi ! ";
            }
            catch (Exception )
            {
                return "Hata olustu !\nLutfen tekrar deneyiniz .";
            }
            finally

            {
                baglantıkapamak();
            }
        }
        public string TedarikciSil(int TedarikciID)
        {
            try
            {
                baglanticontrol();
                string sql = "UPDATE Tedarikci SET SilindiBilgisi = 1 WHERE TedarikciID = "+TedarikciID+"";;
                komut = new SqlCommand(sql, baglantıadresi());
                komut.Parameters.AddWithValue("@TedarikciID", TedarikciID);
                komut.ExecuteNonQuery();
                return "Tedarikçi Firma Silindi ! ";
            }
            catch (Exception)
            {
                return "Hata olustu ! \n Lutfen tekrar deneyiniz .";
            }
            finally

            {
                baglantıkapamak();
            }
        }
        public string UrunSil(int UrunID)
        {
            try
            {
                baglanticontrol();
                string sql = "UPDATE Urun SET SilindiBilgisi = 1 WHERE UrunID = " + UrunID;
                komut = new SqlCommand(sql, baglantıadresi());
                komut.Parameters.AddWithValue("@UrunID", UrunID);
                komut.ExecuteNonQuery();
                return "Ürün Silindi ! ";
            }
            catch (Exception)
            {
                return "Hata olustu ! \n Lutfen tekrar deneyiniz .";
            }
            finally

            {
                baglantıkapamak();
            }
        }
        public void baglantıkapamak()
        {
            try
            {

                baglanti.Close();

            }
            catch (Exception)
            {

            }
        }
    }
}
