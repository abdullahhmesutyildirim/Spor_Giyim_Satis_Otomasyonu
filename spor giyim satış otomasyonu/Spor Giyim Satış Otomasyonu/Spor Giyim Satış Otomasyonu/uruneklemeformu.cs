using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spor_Giyim_Satış_Otomasyonu
{
    public partial class uruneklemeformu : Form
    {
        public uruneklemeformu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");

        bool durum;

        private void barkodkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (tbBarkodNo.Text == read["barkodno"].ToString() || tbBarkodNo.Text=="")
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbKategori.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void uruneklemeformu_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMarka.Items.Clear();
            cmbMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri where kategori = '"+cmbKategori.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void btnYeniEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,miktari,alisfiyati,satisfiyati,tarih) values(@barkodno,@kategori,@marka,@urunadi,@miktari,@alisfiyati,@satisfiyati,@tarih) ", baglanti);

                komut.Parameters.AddWithValue("@barkodno", tbBarkodNo.Text);
                komut.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                komut.Parameters.AddWithValue("@marka", cmbMarka.Text);
                komut.Parameters.AddWithValue("@urunadi", tbUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(tbMiktari.Text));
                komut.Parameters.AddWithValue("@alisfiyati", double.Parse(tbAlisFiyati.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(tbSatisFiyati.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("ürün eklendi");
                cmbMarka.Items.Clear();
            }
            else
            {
                MessageBox.Show("Böyle bir barkod no var", "uyarı");
            }


           
            foreach (Control item in groupBox1.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
                if(item is ComboBox)
                {
                    item.Text = "";
                }

            }
        }

        private void tbBarkodNo2_TextChanged(object sender, EventArgs e)
        {
            if (tbBarkodNo2.Text=="")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '"+tbBarkodNo2.Text+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                tbKategori.Text = read["kategori"].ToString();
                tbMarka.Text = read["marka"].ToString();
                tbUrunAdi2.Text = read["urunadi"].ToString();
                tbMiktari2.Text = read["miktari"].ToString();
                tbAlisFiyati2.Text = read["alisfiyati"].ToString();
                tbSatisFiyati2.Text = read["satisfiyati"].ToString();

            }
            baglanti.Close();
        }

        private void btnVarOlanaEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set miktari= miktari+ '"+int.Parse(tbMiktari2.Text)+"' where barkodno='"+tbBarkodNo2.Text+"' ",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();

            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            MessageBox.Show("var olan ürüne ekleme yapıldı");
        }
    }
}
