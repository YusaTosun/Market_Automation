using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ZXing;
using Market.Controller;
using Market.Model;


namespace Market
{
    public partial class FruitPanel : Form
    {
        int sayi1;
        int sayi2;
        int islemTip;

        FilterInfoCollection fic;
        VideoCaptureDevice vcd; 



        public FruitPanel()
        {
            InitializeComponent();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void SecilenTus(object sender, EventArgs e)
        {
            if (txt_islem.Text == "0")

            {
                txt_islem.Text = "";
            }

            txt_islem.Text += ((Button)sender).Text;
        }

        private void btn_topla_Click(object sender, EventArgs e)
        {
            islemTip = 1;
            sayi1 = Convert.ToInt32(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_islem.Text = "0";
        }

        private void btn_esittir_Click(object sender, EventArgs e)
        {
            if (islemTip == 1)
            {
                sayi2 = Convert.ToInt32(txt_islem.Text);
                txt_islem.Text = (sayi1 + sayi2).ToString();
            }

            else if (islemTip == 2)
            {
                sayi2 = Convert.ToInt32(txt_islem.Text);
                txt_islem.Text = (sayi1 - sayi2).ToString();
            }

            else if (islemTip == 3)
            {
                sayi2 = Convert.ToInt32(txt_islem.Text);
                txt_islem.Text = (sayi1 * sayi2).ToString();
            }

            else if (islemTip == 4)
            {
                sayi2 = Convert.ToInt32(txt_islem.Text);
                txt_islem.Text = (sayi1 / sayi2).ToString();
            }



        }

        private void btn_eksi_Click(object sender, EventArgs e)
        {
            islemTip = 2;
            sayi1 = Convert.ToInt32(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_carpma_Click(object sender, EventArgs e)
        {
            islemTip = 3;
            sayi1 = Convert.ToInt32(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_bolme_Click(object sender, EventArgs e)
        {
            islemTip = 4;
            sayi1 = Convert.ToInt32(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_Geri_Click(object sender, EventArgs e)
        {

            if (txt_islem.Text.Length>1)
            {
                txt_islem.Text = txt_islem.Text.Substring(0, txt_islem.Text.Length - 1);
            }

            else
            {
                txt_islem.Text = "0";
            }
        }

        private void lbl_saat_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (picbx_Kamera.Image!=null)
            {

                BarcodeReader reader = new BarcodeReader();
               Result result = reader.Decode((Bitmap)picbx_Kamera.Image);

                if(result!=null)
                {
                    txt_barkod.Text = result.ToString();
                    timer1.Stop();
                }
            }
        }

        private void FruitPanel_Load(object sender, EventArgs e) // FruitVegetables Panel @@@@@@
        {
            timer1.Start();

            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo camera in fic)
            {
                cmb_KameraAc.Items.Add(camera.Name);
            }
                       
        }

        private void btn_KameraAc_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fic[cmb_KameraAc.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame1;
            vcd.Start();
            timer1.Start();
        }

        private void Vcd_NewFrame1(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
          picbx_Kamera.Image = (Bitmap) eventArgs.Frame.Clone(); 
        }

        private void btn_kameraKapat_Click(object sender, EventArgs e)
        {
            vcd.Stop();

            picbx_Kamera.Image = Image.FromFile(@"C:\Users\Lenovo\Desktop\SQL\market\resimler\camera.ico");
        }

        private void txt_barkod_TextChanged(object sender, EventArgs e)
        {
            controller controller = new controller();
            Urun urun = controller.UrunuGetir(txt_barkod.Text);
            txt_islem.Text = urun.fiyat.ToString();
            txt_urunisim.Text = urun.urunIsim.ToString();
            
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
