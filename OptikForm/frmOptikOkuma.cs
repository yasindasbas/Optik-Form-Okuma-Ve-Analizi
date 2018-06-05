using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace OptikForm
{
    public partial class frmOptikOkuma : Form
    {
        public frmOptikOkuma()
        {
            InitializeComponent();
        }
        //Bitmap sınıfından nesne oluşturup boyut veriliyor.
        Bitmap resim = new Bitmap(600, 849);
        List<string> dizi_harfler = new List<string>(new string[] {
                "A", "B", "C","Ç","D","E","F","G","Ğ","H","I","İ","J","K","L","M","N","O","Ö","P","R","S","Ş","T","U","Ü","V","Y","Z"});
        List<string> dizi_tc = new List<string>(new string[] {
                "0","1","2","3","4","5","6","7","8","9",""});
        List<string> cevap_anahtariTr = new List<string>(new string[] { });
        List<string> cevap_anahtariMt = new List<string>(new string[] { });
        List<string> cevap_anahtariIng = new List<string>(new string[] { });

        //string ResimYolu = Application.StartupPath.ToString() + "\\resimler\\res.jpg";
        private void Form1_Load(object sender, EventArgs e)
        {

            //btnDegistir.Visible = false;
            if (cOkunanlar._kontrol == 1)
            {
                pictureBox1.Image = Image.FromFile(cOkunanlar._str);
                Graphics.FromImage(resim).DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Width, pictureBox1.Height);
                optik_oku.Visible = true;
            }
            /*else if (cOkunanlar._kontrol == 0)
            {
                pictureBox1.Image = Image.FromFile(ResimYolu);
                Graphics.FromImage(resim).DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Width, pictureBox1.Height);
            }*/
        }
        //Optik okuma fonksiyonları tetikleniyor 
        private void OptikOku_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////
            toplamDogru = 0;
            toplamYanlis = 0;
            toplamNet = 0;
            toplamBos = 0;
            //
            SilSupur();
            //Ad soyad ve tc Bölümleri için yazılı fonksiyon çalıştırılıyor.
            Oku_Fonk(60, 101, 29, 10, 101, dizi_harfler, txt_ad);                           // Ad okur
            Oku_Fonk(60, 450, 29, 10, 450, dizi_harfler, txt_soyad);                        // Soyad okur
            Oku_Fonk(224, 101, 10, 11, 101, dizi_tc, txt_tc);                               // TC okur

            //Dersleri okumak için yazılı fonksiyon çalıştırılıyor.
            Oku_Ders(279, 277, 20, 4, 279, cevap_anahtariTr, rich_tr_kisi, rich_tr_cevap, "TR");    // Türkçe dersini okur.
            Oku_Ders(356, 277, 20, 4, 356, cevap_anahtariMt, rich_mat_kisi, rich_mat_cevap, "Mat");  // Matematik dersini okur.
            Oku_Ders(433, 277, 20, 4, 433, cevap_anahtariIng, rich_ing_kisi, rich_ing_cevap, "Ing");  // İngilizce dersini okur.

            if (cOkunanlar._kontrol == 0)
            {
                cOkunanlar o = new cOkunanlar();
                o.AdSoyad = txt_ad.Text + " " + txt_soyad.Text;
                o.Dogru = toplamDogru;
                o.Yanlis = toplamYanlis;
                o.Bos = toplamBos;
                o.Net = toplamNet;
                o.DosyaAdi = listBox1.SelectedItem.ToString();
                o.formKaydet(o);
            }
        }
        //
        private void Oku_Fonk(int x, int y, int tekrar_asagi, int tekrar_sag, int sifirlama, List<string> dizi, TextBox textBox)
        {
            int count = 0;
            int count_bosluk = 0;
            int oran = 11, tara = 4, ortalamaR = 0, ortalamaG = 0, ortalamaB = 0, ortalama = 0;
            Color c;
            // int red, green, blue;      

            // Alttaki for'da count_bosluk yeni eklendi.

            for (int i = 0; i < tekrar_sag; i++)
            {
                for (int j = 0; j < tekrar_asagi; j++)
                {
                    int xTara = x + tara;
                    int yTara = y + tara;
                    int toplamR = 0, toplamG = 0, toplamB = 0;
                    for (int a = x; a < xTara; a++)
                    {
                        for (int b = y; b < yTara; b++)
                        {
                            c = resim.GetPixel(a, b);
                            toplamR += c.R;
                            toplamB += c.B;
                            toplamG += c.G;
                        }
                    }
                    //okunan alanların ortalaması alınıyor.
                    ortalamaR = toplamR / (tara * tara);
                    ortalamaG = toplamG / (tara * tara);
                    ortalamaB = toplamB / (tara * tara);
                    ortalama = (ortalamaR + ortalamaG + ortalamaB) / 3;
                    int w = sifirlama;
                    //ortalama ile işaretlenip işaretlenmediğine karar veriliyor
                    if (ortalama < 100)
                    {
                        for (int a = 0; a < tekrar_sag; a++)
                        {
                            for (int b = 0; b < tekrar_asagi; b++)
                            {
                                //işaretlenmiş ise textBox a yazılıyor 
                                if (y == w)
                                {
                                    textBox.Text += dizi[count];
                                }
                                count++;
                                w += oran;
                            }
                            count = 0;
                        }
                        count_bosluk--;
                    }
                    else
                    {
                        count_bosluk++;
                    }
                    //29 harf olduğu için 29 kere kontrol sağlanıyor
                    if (count_bosluk == 29)
                    {
                        textBox.Text += " ";
                        count_bosluk = 0;
                    }
                    if (j == 28)
                    {
                        count_bosluk = 0;
                    }
                    y += oran;
                }
                y = sifirlama;
                x += oran;
                pictureBox1.Image = resim;
                this.Refresh();
            }

            // Bu ifler de yeni eklendi. // textbox da düzenlemeler yapılıyor.
            if (textBox == txt_tc)
            {
                Regex r = new Regex(@"\s+");
                string s = r.Replace(textBox.Text, @"");
                textBox.Text = s;
            }
            if (textBox == txt_ad || textBox == txt_soyad)
            {
                string s = textBox.Text;
                textBox.Text = s.TrimEnd();
            }

        }
        //////////////////////////////////////////////////
        /*float TRDogru = 0, TRYanlis = 0, TRBos = 0, TRNet = 0;
        float MatDogru = 0, MatYanlis = 0, MatBos = 0, MatNet = 0;
        float IngDogru = 0, IngYanlis = 0, IngBos = 0, IngNet = 0;*/
        float toplamDogru = 0, toplamYanlis = 0, toplamNet = 0, toplamBos = 0;

        string Dirpath;
        int imgindex;
        // dosya seçme işlemi gerçekleştiriliyor.
        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr != DialogResult.Cancel)
            {
                cOkunanlar._kontrol = 0;
                SilSupur();
                //optik_oku.Visible = false;
                //listBox1.Items.Clear();
                Dirpath = folderBrowserDialog1.SelectedPath;
                string[] files = Directory.GetFiles(Dirpath, "*.Jpg");
                //klasör de bulunan dosyalar listbox a ekleniyor. 
                foreach (string file in files)
                {
                    int pos = file.LastIndexOf("||");
                    string FName = file.Substring(pos + 1);
                    listBox1.Items.Add(FName);
                }
                //listBox1.SelectedIndex=imgindex = 0;

                btnDegistir.Visible = true;
            }
        }
        //listbox da seçili olan dosyayı işleme alıyoruz.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SilSupur();
            pictureBox1.Image = Image.FromFile(listBox1.SelectedItem.ToString());
            Graphics.FromImage(resim).DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Width, pictureBox1.Height);
            Oku();
        }
        //bir sonra ki dosyaya geçme işlemini sağlıyoruz.
        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (imgindex < listBox1.Items.Count - 1)
            {
                imgindex++;
                listBox1.SelectedIndex = imgindex;
                if (imgindex == listBox1.Items.Count - 1)
                {
                    btnDegistir.Visible = false;
                    imgindex = 0;
                }
            }
            /*else
            {
                imgindex = 0;
                btnDegistir.Visible = false;
            }*/
        }
        //ders alanlarını okumak için yazılan fonksiyon
        private void Oku_Ders(int x, int y, int tekrar_asagi, int tekrar_sag, int sifirlama, List<string> cevap_anahtari, RichTextBox rich_kisi, RichTextBox rich_cevap, string yeni)
        {
            int oran = 11, count = 0, kontrol = 0, soru = 1, tara = 3, ortalama = 0;
            Color c;
            //cevapları tutmak için liste tanımlıyoruz
            List<string> cevaplar = new List<string>();
            //karşılaştırılacak elemanlar için liste tanımlayıp dolduruyoruz
            List<string> secenekler = new List<string>(new string[] {
                "A","B","C","D","BOS","GECERSIZ"
            });

            int a = 0;
            //görüntü üzerinde dolaşma işlemini sağlıyoruz
            for (int i = 0; i < tekrar_asagi; i++)
            {
                int sonX = 0, sonY = 0;
                for (int j = 0; j < tekrar_sag; j++)
                {
                    int xTara = x + tara;
                    int yTara = y + tara;
                    int toplamR = 0, toplamG = 0, toplamB = 0, ortalamaR = 0, ortalamaG = 0, ortalamaB = 0;
                    // kontrol edilecek alanın sayısal değerlerine bakıyoruz.
                    for (int aa = x; aa < xTara; aa++)
                    {
                        for (int b = y; b < yTara; b++)
                        {
                            c = resim.GetPixel(aa, b);
                            toplamR += c.R;
                            toplamB += c.B;
                            toplamG += c.G;
                        }
                    }
                    //değerlerin ortolamasını alıyoruz
                    ortalamaR = toplamR / (tara * tara);
                    ortalamaG = toplamG / (tara * tara);
                    ortalamaB = toplamB / (tara * tara);
                    ortalama = (ortalamaR + ortalamaG + ortalamaB) / 3;
                    int w = sifirlama;
                    //işaretlenip işaretlenmediği kontrol ediliyor.
                    if (ortalama < 100)
                    {
                        if (kontrol < 2)
                        {
                            kontrol++;
                            cevaplar.Add(secenekler[count]);
                            if (cevaplar.Count > 0)
                            {
                                //tek bir alan mı işaretlendi yoksa iki alanı bireden mi işaretledi diye bakıyoruz.
                                if (kontrol != 2)
                                {
                                    //işaretlenen alan ile cevap uyuşuyorsa yeşil daire içerisine alıyoruz
                                    if (cevaplar[a] == cevap_anahtari[a])
                                    {
                                        Graphics g;
                                        Pen kalemim = new Pen(Color.Green, 2);
                                        g = pictureBox1.CreateGraphics();
                                        g.DrawEllipse(kalemim, new Rectangle(x - 6, y - 4, 8, 8));
                                        g.Dispose();
                                        sonX = x - 6;
                                        sonY = y - 4;
                                    }
                                    //cevap yanlışsa ve ya hatalıysa kırmızı alan içerisine alıyoruz
                                    else
                                    {
                                        Graphics g;
                                        Pen kalemim = new Pen(Color.Red, 2);
                                        g = pictureBox1.CreateGraphics();
                                        g.DrawEllipse(kalemim, new Rectangle(x - 6, y - 4, 8, 8));
                                        g.Dispose();
                                        sonX = x - 6;
                                        sonY = y - 4;
                                    }
                                }
                                //iki şık birden işaretlendi ise ikisini de kırmızı alan içerisine alıyoruz
                                else
                                {
                                    Graphics g;
                                    Pen kalemim = new Pen(Color.Red, 2);
                                    g = pictureBox1.CreateGraphics();
                                    if (sonX != 0)
                                    {
                                        g.DrawEllipse(kalemim, new Rectangle(sonX, sonY, 8, 8));
                                        g.DrawEllipse(kalemim, new Rectangle(x - 6, y - 4, 8, 8));
                                    }
                                    g.Dispose();
                                }
                            }
                        }

                        if (kontrol == 2)
                        {
                            cevaplar.RemoveAt(cevaplar.Count - 1);
                            cevaplar.RemoveAt(cevaplar.Count - 1);
                            cevaplar.Add(secenekler[5]);
                        }
                    }
                    //4 şık olduğu için 4 kere dönmeyi sağlamak için kontrol yapıyoruz.
                    if (j == 3)
                    {
                        if (kontrol == 0)
                        {
                            cevaplar.Add(secenekler[4]);
                        }
                    }
                    x += oran;
                    count++;
                }
                a++;
                kontrol = 0;
                x = sifirlama;
                y += oran;
                count = 0;
            }
            foreach (var item in cevaplar)
            {
                rich_kisi.Text += "\n" + soru + ". " + item;
                soru++;
            }
            soru = 1;
            foreach (var item in cevap_anahtari)
            {
                rich_cevap.Text += "\n" + soru + ". " + item;
                soru++;
            }
            ///////////////////////////////////////////////
            float dogru = 0, yanlis = 0, bos = 0, net = 0;
            int z = 0;
            //bos geçersiz doğru yanlış sayılarını elde ediyoruz.
            foreach (var item in cevaplar)
            {
                if (item == "BOS")
                {
                    bos++;
                }
                else if (item == "GECERSIZ")
                {
                    yanlis++;
                }
                else if (item == cevap_anahtari[z])
                {
                    dogru++;
                }
                else if (item != cevap_anahtari[z])
                {
                    yanlis++;
                }
                z++;
            }
            net = dogru - (yanlis / 4);
            lblToplamBos.Visible = true;
            lblToplamDogru.Visible = true;
            lblToplamNet.Visible = true;
            lblToplamYanlis.Visible = true;
            //turkçe için sonuç verilerini label lara yazıyoruz.
            if (yeni == "TR")
            {
                lblTurkceDogru.Text = "Türkçe Doğru : " + dogru.ToString();
                lblTurkceYanlis.Text = "Türkçe Yanlış : " + yanlis.ToString();
                lblTurkceBos.Text = "Türkçe Boş : " + bos.ToString();
                lblTurkceNet.Text = "Türkçe Net : " + net.ToString();
                lblTurkceBos.Visible = true;
                lblTurkceDogru.Visible = true;
                lblTurkceNet.Visible = true;
                lblTurkceYanlis.Visible = true;
                toplamDogru += dogru;
                toplamYanlis += yanlis;
                toplamBos += bos;
                toplamNet += net;
                /*TRDogru = dogru;
                TRYanlis = yanlis;
                TRBos = bos;
                TRNet = net;*/
            }
            //Matematik için sonuç verilerini label lara yazıyoruz.
            else if (yeni == "Mat")
            {
                lblMatematikDogru.Text = "Matematik Doğru : " + dogru.ToString();
                lblMatematikYanlis.Text = "Matematik Yanlış : " + yanlis.ToString();
                lblMatematikBos.Text = "Matematik Boş : " + bos.ToString();
                lblMatematikNet.Text = "Matematik Net : " + net.ToString();
                lblMatematikBos.Visible = true;
                lblMatematikDogru.Visible = true;
                lblMatematikNet.Visible = true;
                lblMatematikYanlis.Visible = true;
                toplamDogru += dogru;
                toplamYanlis += yanlis;
                toplamBos += bos;
                toplamNet += net;
                /*MatDogru = dogru;
                MatYanlis = yanlis;
                MatBos = bos;
                MatNet = net;*/
            }
            //ingilizce için sonuç verilerini label lara yazıyoruz.
            else
            {
                lblIngDogru.Text = "İngilizce Doğru : " + dogru.ToString();
                lblIngYanlis.Text = "İngilizce Yanlış : " + yanlis.ToString();
                lblIngBos.Text = "İngilizce Boş : " + bos.ToString();
                lblIngNet.Text = "İngilizce Net : " + net.ToString();
                lblIngBos.Visible = true;
                lblIngDogru.Visible = true;
                lblIngNet.Visible = true;
                lblIngYanlis.Visible = true;
                toplamDogru += dogru;
                toplamYanlis += yanlis;
                toplamBos += bos;
                toplamNet += net;
                /*IngDogru = dogru;
                IngYanlis = yanlis;
                IngBos = bos;
                IngNet = net;*/
            }
            //toplam değerlerini label lara yazıyoruz
            lblToplamDogru.Text = "Toplam Doğru : " + toplamDogru.ToString();
            lblToplamYanlis.Text = "Toplam Tanlış : " + toplamYanlis.ToString();
            lblToplamBos.Text = "Toplam Boş : " + toplamBos.ToString();
            lblToplamNet.Text = "Toplam Net : " + toplamNet.ToString();
        }

        private void Temizle_Click(object sender, EventArgs e)
        {
            SilSupur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //cevap anahtarı kullanıcıdan alınıyor.
        private void button1_Click(object sender, EventArgs e)
        {
            cevap_anahtariTr.Clear();
            char[] boluculer = { ' ' };
            string cevapTr, cevapMt, cevapIng;
            string[] parcalaTr, parcalaMt, parcalaIng;
            cevapTr = cTur.Text;
            cevapMt = cMat.Text;
            cevapIng = cIng.Text;
            parcalaTr = cevapTr.Split(boluculer);
            parcalaMt = cevapMt.Split(boluculer);
            parcalaIng = cevapIng.Split(boluculer);
            foreach (string i in parcalaTr)
            {
                cevap_anahtariTr.Add(i);
            }
            foreach (string i in parcalaMt)
            {
                cevap_anahtariMt.Add(i);
            }
            foreach (string i in parcalaIng)
            {
                cevap_anahtariIng.Add(i);
            }

        }
        //tüm araçları temizliyor.
        void SilSupur()
        {
            txt_ad.Clear();
            txt_soyad.Clear();
            txt_tc.Clear();
            rich_tr_kisi.Clear();
            rich_tr_cevap.Clear();
            rich_mat_kisi.Clear();
            rich_mat_cevap.Clear();
            rich_ing_kisi.Clear();
            rich_ing_cevap.Clear();
            lblTurkceBos.Visible = false;
            lblTurkceDogru.Visible = false;
            lblTurkceNet.Visible = false;
            lblTurkceYanlis.Visible = false;
            lblMatematikBos.Visible = false;
            lblMatematikDogru.Visible = false;
            lblMatematikNet.Visible = false;
            lblMatematikYanlis.Visible = false;
            lblIngBos.Visible = false;
            lblIngDogru.Visible = false;
            lblIngNet.Visible = false;
            lblIngYanlis.Visible = false;
            lblToplamBos.Visible = false;
            lblToplamDogru.Visible = false;
            lblToplamNet.Visible = false;
            lblToplamYanlis.Visible = false;
        }
        //okuma işlemlerini geçekleştirmek için yazılan fonkiyonumuz.
        void Oku()
        {
            /////////////////////////////////////////////////
            toplamDogru = 0;
            toplamYanlis = 0;
            toplamNet = 0;
            toplamBos = 0;

            SilSupur();
            Oku_Fonk(60, 101, 29, 10, 101, dizi_harfler, txt_ad);                           // Ad okur
            Oku_Fonk(60, 450, 29, 10, 450, dizi_harfler, txt_soyad);                        // Soyad okur
            Oku_Fonk(224, 101, 10, 11, 101, dizi_tc, txt_tc);                               // TC okur
            Oku_Ders(279, 277, 20, 4, 279, cevap_anahtariTr, rich_tr_kisi, rich_tr_cevap, "TR");    // Türkçe dersini okur.
            Oku_Ders(356, 277, 20, 4, 356, cevap_anahtariMt, rich_mat_kisi, rich_mat_cevap, "Mat");  // Matematik dersini okur.
            Oku_Ders(433, 277, 20, 4, 433, cevap_anahtariIng, rich_ing_kisi, rich_ing_cevap, "Ing");  // İngilizce dersini okur.

            if (cOkunanlar._kontrol == 0)
            {
                cOkunanlar o = new cOkunanlar();
                o.AdSoyad = txt_ad.Text + " " + txt_soyad.Text;
                o.Dogru = toplamDogru;
                o.Yanlis = toplamYanlis;
                o.Bos = toplamBos;
                o.Net = toplamNet;
                o.DosyaAdi = listBox1.SelectedItem.ToString();
                o.formKaydet(o);
            }
        }
    }
}
