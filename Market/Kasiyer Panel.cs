using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market
{
    public partial class KasiyerPanel : Form
    {
        public KasiyerPanel()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btn_fruit_Click(object sender, EventArgs e)
        {
            FruitPanel fruits_Panel = new FruitPanel();

            fruits_Panel.Show();
            this.Hide();
        }
    }
}
