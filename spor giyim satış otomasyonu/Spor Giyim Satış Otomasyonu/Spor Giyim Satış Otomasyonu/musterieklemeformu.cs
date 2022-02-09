using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Spor_Giyim_Satış_Otomasyonu
{
    public partial class musterieklemeformu : Form
    {
        public musterieklemeformu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Spor_Giyim_Stok_Takip;Integrated Security=True");

        private void musterieklemeformu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into musteri(tc,adsoyad,telefon,adres,eposta) values (@tc,@adsoyad,@telefon,@adres,@eposta)", baglanti);
            komut.Parameters.AddWithValue("tc", tbTc.Text);
            komut.Parameters.AddWithValue("adsoyad", tbAdSoyad.Text);
            komut.Parameters.AddWithValue("telefon", tbTelefon.Text);
            komut.Parameters.AddWithValue("adres", tbAdres.Text);
            komut.Parameters.AddWithValue("eposta", tbEposta.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kayıt işlemi başarılı");

            foreach (Control item in this.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
