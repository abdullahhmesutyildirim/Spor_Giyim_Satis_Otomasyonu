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
    public partial class Form1 : Form
    {
        faturaformu faturaformu;
        public Form1()
        {
            InitializeComponent();
        }
            SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from sepet", baglanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            baglanti.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            musterieklemeformu ekle = new musterieklemeformu();
            ekle.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            musterilistelemeformu listele = new musterilistelemeformu();
            listele.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            uruneklemeformu ekle = new uruneklemeformu();
            ekle.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kategoriformu kategori = new kategoriformu();
            kategori.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            markaformu marka = new markaformu();
            marka.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            urunlistelemeformu listele = new urunlistelemeformu();
            listele.ShowDialog();
        }

        private void hesapla()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select sum(toplamfiyat) from sepet",baglanti);
                lblGenenelToplam.Text = komut.ExecuteScalar() + " TL";
                baglanti.Close();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sepetlistele();
        }

        private void tbTc_TextChanged(object sender, EventArgs e)
        {
            if (tbTc.Text=="")
            {
                tbAdSoyad.Text = "";
                tbTelefon.Text = "";
            }
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from musteri where tc like '"+tbTc.Text+"'  ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                tbAdSoyad.Text = read["adsoyad"].ToString();
                tbTelefon.Text = read["telefon"].ToString();

            }
            baglanti.Close();
        }

        private void tbBarkodNo_TextChanged(object sender, EventArgs e)
        {
            temizle();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from urun where barkodno like '" + tbBarkodNo.Text + "'  ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                tbUrunAdi.Text = read["urunadi"].ToString();
                tbSatısFiyat.Text = read["satisfiyati"].ToString();

            }
            baglanti.Close();
        }

        private void temizle()
        {
            if (tbBarkodNo.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != tbMiktar)
                        {
                            item.Text = "";
                        }
                    }

                }
            }
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true; 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sepet",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (tbBarkodNo.Text==read["barkodno"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            barkodkontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sepet(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyat,tarih) values (@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyat,@tarih) ", baglanti);
                komut.Parameters.AddWithValue("@tc", tbTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", tbAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", tbTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", tbBarkodNo.Text);
                komut.Parameters.AddWithValue("@urunadi", tbUrunAdi.Text);
                komut.Parameters.AddWithValue("@miktari", int.Parse(tbMiktar.Text));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(tbSatısFiyat.Text));
                komut.Parameters.AddWithValue("@toplamfiyat", double.Parse(tbToplamFiyat.Text));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                komut.ExecuteNonQuery();
                baglanti.Close();
                
                
            }
            else
            {
                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update sepet set miktari = miktari +  '"+int.Parse(tbMiktar.Text)+ "'  where barkodno = '" + tbBarkodNo.Text + "' ", baglanti);
                komut2.ExecuteNonQuery();

                SqlCommand komut3 = new SqlCommand("update sepet set toplamfiyat = miktari *  satisfiyati where barkodno = '"+tbBarkodNo.Text+"' ", baglanti);
                komut3.ExecuteNonQuery();
                baglanti.Close();
            }

            
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
            faturaformu.listBox1.Items.Add("ÜRÜN :            " + tbUrunAdi.Text);
            faturaformu.listBox1.Items.Add("FİYATI :            " + tbSatısFiyat.Text);
            faturaformu.listBox1.Items.Add("MİKTARI :            " + tbMiktar.Text);
            faturaformu.listBox1.Items.Add("TOPLAM FİYATI :            " + tbTc.Text);
            faturaformu.listBox1.Items.Add("TUTAR :            " + lblGenenelToplam.Text);
            faturaformu.Show();
            tbMiktar.Text = "1";
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    if (item != tbMiktar)
                    {
                        item.Text = "";
                    }
                }

            }
        }

        private void tbMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbToplamFiyat.Text = (double.Parse(tbMiktar.Text) * double.Parse(tbSatısFiyat.Text)).ToString();

            }
            catch (Exception)
            {

                ;
            }
        }

        private void tbSatısFiyat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbToplamFiyat.Text = (double.Parse(tbMiktar.Text) * double.Parse(tbSatısFiyat.Text)).ToString();

            }
            catch (Exception)
            {

                ;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet where barkodno = '" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "' ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("ürün sepetten çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void btnSatisIptal_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sepet ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            MessageBox.Show("ürünler sepetten çıkarıldı");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            satislistelemeformu listele = new satislistelemeformu();
            listele.ShowDialog();
        }

        private void btnSatisYap_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into satis(tc,adsoyad,telefon,barkodno,urunadi,miktari,satisfiyati,toplamfiyati,tarih) values (@tc,@adsoyad,@telefon,@barkodno,@urunadi,@miktari,@satisfiyati,@toplamfiyati,@tarih) ", baglanti);
                komut.Parameters.AddWithValue("@tc", tbTc.Text);
                komut.Parameters.AddWithValue("@adsoyad", tbAdSoyad.Text);
                komut.Parameters.AddWithValue("@telefon", tbTelefon.Text);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                komut.Parameters.AddWithValue("@urunadi", dataGridView1.Rows[i].Cells["urunadi"].Value.ToString());
                komut.Parameters.AddWithValue("@miktari", int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()));
                komut.Parameters.AddWithValue("@satisfiyati", double.Parse(dataGridView1.Rows[i].Cells["satisfiyati"].Value.ToString()));
                komut.Parameters.AddWithValue("@toplamfiyati", double.Parse(dataGridView1.Rows[i].Cells["toplamfiyat"].Value.ToString()));
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                komut.ExecuteNonQuery();
                SqlCommand komut2 = new SqlCommand("update urun set miktari= miktari- '" + int.Parse(dataGridView1.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "' ", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("satış işlemi gerçekleşti");
                
                
                
            }
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("delete from sepet ", baglanti);
            komut3.ExecuteNonQuery();
            baglanti.Close();

            daset.Tables["sepet"].Clear();
            sepetlistele();
            hesapla();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            faturaformu = new faturaformu();
            faturaformu.Owner = this;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblGenenelToplam_Click(object sender, EventArgs e)
        {

        }
    }
}
