using Market.Enumaration;
using Market.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Market.dao
{
    public class Repository
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;


        List<LoginTable> loginTableList = new List<LoginTable>(); // tekrar bak @@@@@@@@@@@@

        // Baştan tanımlamazsam NULL Hatası alıyorum bu kısmı unutma !!!!!!!!!!!@@@@@@@@@@@@


        public Repository()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-BFIQFNI\SQLEXPRESS;Initial Catalog=market;User ID=sa;password=1");

            // SQL Bağlantısında Database seçmeyi unutma @@@@@@@@@@@@@
        }

        public void baglantiAyarla()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            else

            {
                con.Close();
            }

        }

        public User login(string kullaniciAdi, string sifre)
        {
            con.Open();
            cmd = new SqlCommand("select * from logintable where kullaniciAdi=@kullaniciAdi and sifre =@sifre", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                User user = new User();

                user.id = (int)dr["id"];
                user.kullaniciAdi = dr["kullaniciAdi"].ToString();
                user.sifre = dr["sifre"].ToString();
                user.yetki = dr["yetki"].ToString();
                user.EmailAdresi = dr["emailAdres"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                user.status = loginStatus.basarili;
                con.Close();
                return user;
            }

            else
            {
                User user = new User();
                user.status = loginStatus.basarisiz;
                con.Close();
                return user;
            }




            
        }

        public List<LoginTable> getLoginTable()
        {
            LoginTable loginTable = new LoginTable(); // buraya bak



            con.Open();
            cmd = new SqlCommand("guvenlikSorusuGetir_sp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                LoginTable logintable = new LoginTable();       // buraya bak
                logintable.id = Convert.ToInt32(dr["id"].ToString());
                logintable.kullaniciAdi = dr["kullaniciAdi"].ToString();
                logintable.sifre = dr["sifre"].ToString();
                logintable.yetki = dr["yetki"].ToString();
                logintable.EmailAdresi = dr["emailAdres"].ToString();
                logintable.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                logintable.guvenlikCevabi = dr["guvenlikCevabi"].ToString();

                loginTableList.Add(logintable);

            }

            con.Close();
            return loginTableList;

        }

        public loginStatus DoAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabi)
        {
            con.Open();


            cmd = new SqlCommand("select count(*) from loginTable where kullaniciAdi=@kullaniciAdi and guvenlikSorusu=@guvenlikSorusu and guvenlikCevabi=@guvenlikCevabi", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", guvenlikCevabi);

            int result = (int)cmd.ExecuteScalar();




            con.Close();

            if (result == 1)
            {
                return loginStatus.basarili;
            }

            else
            {
                return loginStatus.basarisiz;
            }

        }

        public loginStatus ChangePassword(string kullaniciAdi, string sifre)
        {

            con.Open();
            cmd = new SqlCommand("sifreGuncelle_sp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kulAd", kullaniciAdi); // Ozellikle kullanıcı adını böyle tanımlayacağım !!!!
            cmd.Parameters.AddWithValue("@sifre", sifre);
            int donenDeger = cmd.ExecuteNonQuery();

            con.Close();

            return loginStatus.basarili; // Buraya daha sonra alternatif EKLE @@@@@@@@@@

        }

        public string GetEmailAdress(string kullaniciAdi)
        {
            con.Open();
            cmd = new SqlCommand("sp_getEmailAdress", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi); // AddWithValue olmasına dikkat et.
            string AlınanAdres = (string)cmd.ExecuteScalar();           // Alışkanlıktan add yazabiliyorum





            con.Close();

            return AlınanAdres;
        }

        public Urun UrunuGetir(string barkod)
        {
            con.Open();
            cmd = new SqlCommand("select * from urun where barkodKod=@code",con);
            cmd.Parameters.AddWithValue("@code",barkod);
            dr = cmd.ExecuteReader();
            Urun urun = new Urun();

            while (dr.Read())
            {
                
                urun.id = dr["id"].ToString();
                urun.qrkod = dr["qrkod"].ToString();
                urun.barkodKod = dr["barkodKod"].ToString();
                urun.urunIsim = dr["urunIsim"].ToString();
                urun.kilo = Convert.ToInt32( dr["kilo"].ToString());
                urun.fiyat = Convert.ToInt32( dr["fiyat"].ToString());
               
            }



            con.Close();
            return urun;
        }

    }
}
