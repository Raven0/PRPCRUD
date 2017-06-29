using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perpus
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBook book = new FormBook();
            book.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormWriter w = new FormWriter();
            w.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormBDetail d = new FormBDetail();
            d.Show();
        }
    }
}
