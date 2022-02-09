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
    public partial class urunlistelemeformu : Form
    {
        public urunlistelemeformu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();

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

        private void urunlistelemeformu_Load(object sender, EventArgs e)
        {
            urunlistele();
            kategorigetir();
        }

        private void urunlistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", baglanti);
            adtr.Fill(daset, "urun");
            dataGridView1.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbBarkodNo2.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            tbKategori.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            tbMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            tbUrunAdi2.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            tbMiktari2.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            tbAlisFiyati2.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            tbSatisFiyati2.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set urunadi=@urunadi,miktari=@miktari,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", tbBarkodNo2.Text);
            komut.Parameters.AddWithValue("@urunadi", tbUrunAdi2.Text);
            komut.Parameters.AddWithValue("@miktari", int.Parse(tbMiktari2.Text));
            komut.Parameters.AddWithValue("@alisfiyati",double.Parse( tbAlisFiyati2.Text));
            komut.Parameters.AddWithValue("@satisfiyati",double.Parse( tbSatisFiyati2.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();

            daset.Tables["urun"].Clear();
            urunlistele();

            MessageBox.Show("güncelleme başarılı");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void btnMarkaGuncelle_Click(object sender, EventArgs e)
        {
            if (tbBarkodNo2.Text != "")
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update urun set kategori=@kategori,marka=@marka where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", tbBarkodNo2.Text);
                komut.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                komut.Parameters.AddWithValue("@marka", cmbMarka.Text);


                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("güncelleme başarılı");
                daset.Tables["urun"].Clear();
                urunlistele();
            }
            else
            {
                MessageBox.Show("barkod no yazılı değil");
            }

           
            foreach (Control item in this.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMarka.Items.Clear();
            cmbMarka.Text = "";
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from markabilgileri where kategori = '" + cmbKategori.SelectedItem + "' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from urun where barkodno='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["urun"].Clear();
            urunlistele();
            MessageBox.Show("kayıt silme işlemi başarılı");
        }

        private void tbBarkodNoAra_TextChanged(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where barkodno like '%" + tbBarkodNoAra.Text + "%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where urunadi like '%" + tburunadiara.Text + "%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }

        private void ikitariharasisorgu1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where tarih between  '" + ikitariharasisorgu1.Text + "' AND '" + ikitariharasisorgu2.Text + "' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }
    }
}
