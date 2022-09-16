using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace arabalar_projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=vttasitlar;Uid=root;Pwd=bist.2022");
        MySqlCommand komut;
        string sorguCumlesi;
    private void goster()
        {
            sorguCumlesi = "select * from arabalar";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sorguCumlesi, baglanti);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridArabalar.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            goster();
        }

        private void dataGridArabalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMarka.Text = dataGridArabalar.CurrentRow.Cells["marka"].Value.ToString();
            txtModel.Text = dataGridArabalar.CurrentRow.Cells["model"].Value.ToString();
            txtYil.Text = dataGridArabalar.CurrentRow.Cells["yil"].Value.ToString();
            txtRenk.Text = dataGridArabalar.CurrentRow.Cells["renk"].Value.ToString();
        }
        private void temizle()
        {
            txtMarka.Clear();
            txtModel.Clear();
            txtYil.Clear();
            txtRenk.Text = "";

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(baglanti.State!=ConnectionState.Open)
            { baglanti.Open(); }
            sorguCumlesi = "insert into arabalar(marka,model,yil,renk) values(@marka,@model,@yil,@renk)";
            komut = new MySqlCommand(sorguCumlesi, baglanti);
            komut.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@model", txtModel.Text);
            komut.Parameters.AddWithValue("@yil", txtYil.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            goster();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (baglanti.State != ConnectionState.Open)
            { baglanti.Open(); }
            sorguCumlesi = "delete from arabalar where id=@id";
            komut = new MySqlCommand(sorguCumlesi, baglanti);
            komut.Parameters.AddWithValue("@id", dataGridArabalar.CurrentRow.Cells["id"].Value.ToString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            goster();
         }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (baglanti.State != ConnectionState.Open)
            { baglanti.Open(); }
            sorguCumlesi = "update arabalar set marka=@marka,model=@model,yil=@yil,renk=@renk where id=@id";
            komut = new MySqlCommand(sorguCumlesi, baglanti);
            komut.Parameters.AddWithValue("@id", dataGridArabalar.CurrentRow.Cells["id"].Value.ToString());
            komut.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@model", txtModel.Text);
            komut.Parameters.AddWithValue("@yil", txtYil.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            goster();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            arama(txtAra.Text);
        }
        private void arama(string aramaKelimesi)
        {
            if (baglanti.State != ConnectionState.Open)
            { baglanti.Open(); }
            komut = new MySqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "select * from arabalar where marka like '" + aramaKelimesi + "%'";
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(komut);
            DataTable dt = new DataTable();
            dataadapter.Fill(dt);
            baglanti.Close();
            dataGridArabalar.DataSource = dt;
        }
    }
}
