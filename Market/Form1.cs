using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Market.Controller;
using Market.Enumaration;
using Market.Model;

namespace Market
{
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
           controller controller = new controller();
           loginStatus result = controller.login(txt_kullaniciAdi.Text,txt_sifre.Text);

          if (result==loginStatus.AdminGirisi)
            {
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                this.Hide();
            }

            else if (result==loginStatus.KasiyerGirisi)
            {
                KasiyerPanel kasiyerPanel = new KasiyerPanel();
                kasiyerPanel.Show();
                this.Hide();
            }

            else if (result==loginStatus.YetersizYetki)
            {
                MessageBox.Show("Bu işlem için Yetkiniz Yetersiz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            else if (result==loginStatus.basarisiz)
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Sifre","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
            else 
            {
                MessageBox.Show("Eksik Parametre Girdiniz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SifreDeğistirme SD = new SifreDeğistirme();

            SD.Show();

            this.Hide();
        }
    }
}
