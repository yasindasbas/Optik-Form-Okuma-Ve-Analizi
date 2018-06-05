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
    public partial class frmEskiKayitlar : Form
    {
        public frmEskiKayitlar()
        {
            InitializeComponent();
        }
        //geri dönmek içim kullanılan buton olayı
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //cOkunanlar sınıfından nesne oluşturup kayıtlara erişmek için
        private void frmEskiKayitlar_Load(object sender, EventArgs e)
        {
            cOkunanlar o = new cOkunanlar();
            o.eskiKayitlariGetir(lvKayitlar);
        }
        //arama işlemi için
        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            cOkunanlar o = new cOkunanlar();
            o.eskiKayitlariGetir(lvKayitlar, txtArama.Text);
        }
        //kaydedilmiş formu tekrar okumak için
        private void lvKayitlar_DoubleClick(object sender, EventArgs e)
        {
            cOkunanlar._kontrol = 1;
            cOkunanlar._str = lvKayitlar.SelectedItems[0].SubItems[7].Text;
            frmOptikOkuma frm = new frmOptikOkuma();
            this.Close();
            frm.Show();
        }
    }

}
