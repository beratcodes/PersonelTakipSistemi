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

namespace Personel_Kayit
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-NCL6B1V\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True;Encrypt=False;");

        void temizle()
        {
            txtİD.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtMeslek.Text = "";
            msktxtMaas.Text = "";
            cmbSehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd, PerSoyad, PerSehir, PerMaas, PerMeslek, PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            

            komut.Parameters.AddWithValue("@p1", txtAd.Text); // Komut nesnesinden gelen parametreleri değer olarak ata.
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", msktxtMaas.Text);
            komut.Parameters.AddWithValue("@p5", txtMeslek.Text);
            komut.Parameters.AddWithValue("@p6", label8.Text);
            
            komut.ExecuteNonQuery(); // Komut nesnesindeki sorguyu çalıştırmak için kullanılır. Ekleme silme ve güncelleme işlemlerinde kullanılır.
            baglanti.Close();
            MessageBox.Show("Personel Eklendi!", "Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtİD.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktxtMaas.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();


        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if(label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            else if(label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel Where Perİd=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1", txtİD.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand guncelle = new SqlCommand("Update Tbl_Personel set PerAd=@a1, PerSoyad=@a2, PerSehir=@a3, PerMaas=@a4, PerDurum=@a5, PerMeslek=@a6 where Perİd=@a7",baglanti);
            guncelle.Parameters.AddWithValue("@a1",txtAd.Text);
            guncelle.Parameters.AddWithValue("@a2", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@a3", cmbSehir.Text);
            guncelle.Parameters.AddWithValue("@a4", msktxtMaas.Text);
            guncelle.Parameters.AddWithValue("@a5", label8.Text);
            guncelle.Parameters.AddWithValue("@a6", txtMeslek.Text);
            guncelle.Parameters.AddWithValue("@a7", txtİD.Text);

            guncelle.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnİstatistik_Click(object sender, EventArgs e)
        {
            İstatistik frmİstatistik = new İstatistik();
            frmİstatistik.Show(); // İstatistik Formuna geçiş yapmak için kullanılır.
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frmGrafik = new FrmGrafikler();
            frmGrafik.Show(); // Grafikler Formuna Geçiş yapar.
        }
    }
}
