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
using System.IO;

namespace WindowsFormsApp1
{
    public partial class FrmEtutSistem : Form
    {
        public FrmEtutSistem()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source =  DESKTOP-S90JHB4; Initial Catalog = Etut_Sistem; Integrated Security = True");
        void derslistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Dersler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbDers.ValueMember = "DERSID";
            CmbDers.DisplayMember = "DERSAD";
            CmbDers.DataSource = dt;
        }

        //Etüt Listesi
        void etutlistesi()
        {
            SqlDataAdapter da3 = new SqlDataAdapter("execute Etut ", baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            derslistesi();
            etutlistesi();
        }

        private void CmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select * from Tbl_Ogretmen where BRANSID=" + CmbDers.SelectedValue, baglanti);

            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            CmbOgretmen.ValueMember = "OGRTID";
            CmbOgretmen.DisplayMember = "AD";
            CmbOgretmen.DataSource = dt2;
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Etut(DERSID,OGRETMENID,TARIH,SAAT) VALUES (@DERSID,@OGRETMENID,@TARIH,@SAAT)", baglanti);

            komut.Parameters.AddWithValue("@DERSID", CmbDers.SelectedValue);
            komut.Parameters.AddWithValue("@OGRETMENID", CmbOgretmen.SelectedValue);
            komut.Parameters.AddWithValue("@TARIH", MskTarih.Text);
            komut.Parameters.AddWithValue("@SAAT", MskSaat.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt kaydı oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtetutid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

        }

        private void BtnEtutVer_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Tbl_Etut set OGRENCIID=@OGRENCIID,DURUM=@DURUM where ID=@ID" , baglanti);
            komut.Parameters.AddWithValue("@OGRENCIID",Txtogrenciid.Text );
            komut.Parameters.AddWithValue("@DURUM", "True");
            komut.Parameters.AddWithValue("@ID", Txtetutid.Text);


            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Etüt verme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }

        private void btnfotograf_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void btnogrenci_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Ogrenci(AD,SOYAD,SINIF,TELEFON,MAIL) VALUES (@AD,@SOYAD,@SINIF,@TELEFON,@MAIL)", baglanti);

            komut.Parameters.AddWithValue("@AD", txtad.Text);
            komut.Parameters.AddWithValue("@SOYAD",txtsoyad.Text );
            komut.Parameters.AddWithValue("@SINIF", txtsınıf.Text);
            komut.Parameters.AddWithValue("@TELEFON",msktelefon.Text );
            komut.Parameters.AddWithValue("@MAIL",txtmail.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci kaydı başarı ile oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void btnogretmenekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Ogretmen(AD,SOYAD,BRANSID) VALUES (@AD,@SOYAD,@BRANSID)", baglanti);

            komut.Parameters.AddWithValue("@AD", txtad2.Text);
            komut.Parameters.AddWithValue("@SOYAD", txtsoyad2.Text);
            komut.Parameters.AddWithValue("@BRANSID", Cmbders2.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğretmen kaydı başarı ile oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
    }
}

