using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptikForm
{
    public partial class frmAnaEkran : Form
    {
        public frmAnaEkran()
        {
            InitializeComponent();
        }
        //Optik Form Sayfasına erişmek için kullanılan buton olayı
        private void button2_Click(object sender, EventArgs e)
        {
            cOkunanlar._kontrol = 0;
            frmOptikOkuma frm = new frmOptikOkuma();
            frm.Show();
        }
        //eski kayıtlar sayfasına erişim için kullanılan buton olayı
        private void button1_Click(object sender, EventArgs e)
        {
            frmEskiKayitlar frm = new frmEskiKayitlar();
            frm.Show();
        }
        private void frmAnaEkran_Load(object sender, EventArgs e)
        {

        }
    }
}
