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
    public partial class satislistelemeformu : Form
    {
        public satislistelemeformu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();

        private void satislistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis", baglanti);
            adtr.Fill(daset, "satis");
            dataGridView1.DataSource = daset.Tables["satis"];

            baglanti.Close();
        }

        private void satislistelemeformu_Load(object sender, EventArgs e)
        {
            satislistele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where tarih between  '" + tarihegorearama1.Text + "' AND '" + tarihegorearama2.Text + "' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where tc like '%" + textBox1.Text + "%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataTable Tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where barkodno like '%" + textBox2.Text + "%' ", baglanti);
            adtr.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            baglanti.Close();
        }
    }
}
