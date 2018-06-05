﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OptikForm
{
    class cOkunanlar
    {
        string conString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ysnda\\Desktop\\2014123056_Yasin_Daşbaş\\OptikForm\\kayitlar.mdf;Integrated Security=True");

        public static string _str = Application.StartupPath.ToString() + "\\resimler\\res_yanlis.jpg";
        public static int _kontrol;

        #region Fields
        private int _id;
        private string _AdSoyad;
        private float _Dogru;
        private float _Yanlis;
        private float _Bos;
        private float _Net;
        private string _DosyaAdi;
        #endregion

        #region Properties
        public int id { get { return _id; } set { _id = value; } }
        public string AdSoyad { get { return _AdSoyad; } set { _AdSoyad = value; } }
        public float Dogru { get { return _Dogru; } set { _Dogru = value; } }
        public float Yanlis { get { return _Yanlis; } set { _Yanlis = value; } }
        public float Bos { get { return _Bos; } set { _Bos = value; } }
        public float Net { get { return _Net; } set { _Net = value; } }
        public string DosyaAdi { get { return _DosyaAdi; } set { _DosyaAdi = value; } }
        #endregion


        public void formKaydet(cOkunanlar oku)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("insert into tblOkunanlar(adSoyad,dogru,yanlis,bos,net,tarih,dosyaAdi) values(@adSoyad,@dogru,@yanlis,@bos,@net,@tarih,@dosyaAdi)", con);
            try
            {
                if(con.State== ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@adSoyad", SqlDbType.VarChar).Value = oku._AdSoyad;
                cmd.Parameters.Add("@dogru", SqlDbType.Float).Value = oku._Dogru;
                cmd.Parameters.Add("@yanlis", SqlDbType.Float).Value = oku._Yanlis;
                cmd.Parameters.Add("@bos", SqlDbType.Float).Value = oku._Bos;
                cmd.Parameters.Add("@net", SqlDbType.Float).Value = oku._Net;
                cmd.Parameters.Add("@tarih", SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
                cmd.Parameters.Add("@dosyaAdi", SqlDbType.Text).Value = oku._DosyaAdi;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
        }

        public void eskiKayitlariGetir(ListView lv)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("Select * from tblOkunanlar", con);
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["adSoyad"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["dogru"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["yanlis"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["bos"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["net"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["tarih"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["dosyaAdi"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
        }

        public void eskiKayitlariGetir(ListView lv, string adSoyad)
        {
            lv.Items.Clear();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("Select * from tblOkunanlar where adSoyad like '%' + @AdSoyad + '%'", con);
            SqlDataReader dr = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Parameters.Add("@AdSoyad", SqlDbType.VarChar).Value = adSoyad;
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read())
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["adSoyad"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["dogru"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["yanlis"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["bos"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["net"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["tarih"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["dosyaAdi"].ToString());
                    sayac++;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
        }
    }
}
