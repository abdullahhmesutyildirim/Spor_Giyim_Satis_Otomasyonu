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
    public partial class musterilistelemeformu : Form
    {
        public musterilistelemeformu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");
        
        DataSet daset = new DataSet();
        private void musterilistelemeformu_Load(object sender, EventArgs e)
        {
            kayit_goster();
        }

        private void kayit_goster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri", baglanti);
            adtr.Fill(daset, "musteri");
            dataGridView1.DataSource = daset.Tables["musteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tbTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            tbAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            tbTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            tbAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            tbEposta.Text = dataGridView1.CurrentRow.Cells["eposta"].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update musteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,eposta=@eposta where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("tc", tbTc.Text);
            komut.Parameters.AddWithValue("adsoyad", tbAdSoyad.Text);
            komut.Parameters.AddWithValue("telefon", tbTelefon.Text);
            komut.Parameters.AddWithValue("adres", tbAdres.Text);
            komut.Parameters.AddWithValue("eposta", tbEposta.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            daset.Tables["musteri"].Clear();
            kayit_goster();

            MessageBox.Show("kayıt güncelleme işlemi başarılı");

            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteri where tc='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["musteri"].Clear();
            kayit_goster();
            MessageBox.Show("kayıt silme işlemi başarılı");
        }

        private void tbTcAra_TextChanged(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where tc like '%"+tbTcAra.Text+"%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();

        }

        private void tbTc_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where adsoyad like '%" + adsoyadagorearama.Text + "%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }
    }
}
