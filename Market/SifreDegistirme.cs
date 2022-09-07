using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Market.Controller;
using Market.Enumaration;
using Market.Model;
using System.Net;


namespace Market
{
    public partial class SifreDeğistirme : Form
    {
        

        int DogrulamaKodu;
        public SifreDeğistirme()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void SifreDegistirme_Load(object sender, EventArgs e)
        {
            controller Controller = new controller();

            List<LoginTable> loginTableList = Controller.getLoginTable();
            
            grpbox_sifreDegistirmeAlani.Enabled = false;
            grpBox_emailAlani.Enabled = false;

            foreach (LoginTable lt in loginTableList)
            {

                cmb_GuvenlikSorusu.Items.Add(lt.guvenlikSorusu.ToString());

            }
            cmb_GuvenlikSorusu.SelectedIndex = 0;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller controller = new controller();

            loginStatus result = controller.DoAuthentication(txt_KullaniciAdi.Text.Trim().ToLower(), cmb_GuvenlikSorusu.SelectedItem.ToString(),txt_GuvenlikCevabi.Text.ToLower());

            if (result==loginStatus.basarili)
            {   

                grpBox_emailAlani.Enabled = true;

                string AlınanAdres = controller.GetEmailAdress(txt_KullaniciAdi.Text);
                txt_EmailAdres.Text = AlınanAdres;
            }

            else if(result==loginStatus.basarisiz)
            {
                MessageBox.Show("Girdiğiniz Bilgiler Yanlış,Kontrol Ediniz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            else 
            {
                MessageBox.Show("Girdiğiniz Bilgiler Eksik", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void btn_DogrulamaKodGndr_Click(object sender, EventArgs e)
        {
           

            Random rdn = new Random();
           DogrulamaKodu = rdn.Next(111111,666666);

            MailAddress mailAlan = new MailAddress(txt_EmailAdres.Text, "Murat SARIGUL");
            MailAddress mailGönderen = new MailAddress("yusamarkettest@hotmail.com","Yusa TOSUN");
            MailMessage mesaj = new MailMessage();

            mesaj.To.Add(mailAlan);
            mesaj.From = mailGönderen;
            mesaj.Subject = "Şifre Değiştirme";
            mesaj.Body = "Şifre Değiştirme İçin Onay Kodunuz : " + DogrulamaKodu;


            SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587); // sptp.live.com Yapınca göndermiyor @@@@@@@@@@@@@@@


            smtp.Credentials = new System.Net.NetworkCredential("yusamarkettest@hotmail.com","yusamarket123");
            smtp.EnableSsl = true;
            smtp.Send(mesaj);
            MessageBox.Show("Doğrulama Kodu Gönderildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            



        }

        private void btn_Onayla_Click(object sender, EventArgs e)
        {
           if  (txt_OnayKodu.Text == DogrulamaKodu.ToString())
           
            {
                grpbox_sifreDegistirmeAlani.Enabled = true;

            }

           else { MessageBox.Show("Onay Kodu Yanlış","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
        
        private void btn_DegistirSifre_Click(object sender, EventArgs e)
        {
            controller controller = new controller();
            
            if (txt_YeniSifre.Text==txt_YeniSifreTekrar.Text)
            {
               loginStatus result = controller.ChangePassword(txt_KullaniciAdi.Text,txt_YeniSifre.Text);

                if (result==loginStatus.basarili)

                {
                    MessageBox.Show("Şifreniz Değiştirilmiştir","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

                else if  (result==loginStatus.basarisiz)
                {
                    MessageBox.Show("Şifre Değiştirilemedi","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);

                }

                else { MessageBox.Show("Eksik Parametre Girdiniz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            }
            else 
            {
                MessageBox.Show("Şifreler eşleşmemektedir","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
    }
}
