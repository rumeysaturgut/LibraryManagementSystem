using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ogrenciler : Form
    {
        
        ogr_ekle ogrekle;
        List<Ogrenci> liste;
        readonly DataTable dt = new DataTable();
        public ogrenciler()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ogrekle = new ogr_ekle();
            ogrekle.FormClosing += new FormClosingEventHandler(ogrenciEkleKapaniyor);
            ogrekle.ShowDialog();
            
        }

        private void ogrenciler_Load(object sender, EventArgs e)
        {
            linkLabel1.Visible = false;
            dataGridView1.AllowUserToAddRows = false;
            radioButton2.Checked = true;
            populateTable();
            
        }
       
        private void populateTable()
        {
            dataGridView1.Rows.Clear();
            liste = Database.listele();
           
            foreach(Ogrenci ogr in liste)
            {
                dataGridView1.Rows.Add(ogr.ogrid.ToString(),ogr.isim,ogr.soyisim,ogr.email,ogr.cinsiyet,ogr.bolum,ogr.dogumTarihi.Date.ToShortDateString());
            }
            dataGridView1.ClearSelection();
        }

        private void ogrenciEkleKapaniyor(object sender, FormClosingEventArgs e)
        {
            populateTable();

            ogrekle = null;
        }
     

        private void button2_Click(object sender, EventArgs e)
        {
           
                int indx = dataGridView1.SelectedRows.Count;
                if (indx > 0)
                {
                    if (MessageBox.Show(@"Kaydi silmek istediginizden emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                    string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    int id = Convert.ToInt32(selected);
                    Database.sil(id);
                    populateTable();
                    }
                }
               
                else
                {
                    MessageBox.Show("Silinecek öğrenciyi seçiniz.");
                }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
               
                ogrekle = new ogr_ekle(Database.GetOgrenci(id));
                ogrekle.FormClosing += new FormClosingEventHandler(ogrenciEkleKapaniyor);
                ogrekle.ShowDialog();

            }

            else
            {
                MessageBox.Show("Güncellenecek öğrenciyi seçiniz.");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Ogrenci ogr = Database.GetOgrenci(id);

                    ogrenci_bilgisi ogrencibilgisi = new ogrenci_bilgisi(ogr);
                    ogrencibilgisi.ShowDialog();
                }
                catch (NullReferenceException)
                {

                }
            }

            else
            {
                MessageBox.Show("Bilgisi gösterilecek öğrenciyi seçiniz.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                string text = textBox1.Text;
                if(text.Contains(" "))
                {
                    string[] isimSoyisim = textBox1.Text.Split(' ');
                    string isim = isimSoyisim[0];
                    string soyisim = isimSoyisim[1];

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if(row.Cells[1].Value != null && row.Cells[1].Value.ToString().Equals(isim,StringComparison.InvariantCultureIgnoreCase) && row.Cells[2].Value.ToString().Equals(soyisim,StringComparison.InvariantCultureIgnoreCase))
                        {
                            row.Selected = true;
                            linkLabel1.Visible = true;
                            return;
                        }
                    }
                    MessageBox.Show("Bulunamadı.");
                }
                else
                {
                    string isim = text;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        Debug.WriteLine(row.Cells[1].ToString());
                        if (row.Cells[1].Value!=null && row.Cells[1].Value.ToString().Equals(isim, StringComparison.InvariantCultureIgnoreCase))
                        {
                            row.Selected = true;
                            linkLabel1.Visible = true;
                            return;
                        }
                    }
                    MessageBox.Show("Bulunamadı.");

                }
                
               
            }
            else if(radioButton2.Checked)
            {
                int no = 0;
                try
                {
                   no = Convert.ToInt32(textBox1.Text);
                }
                catch (Exception)
                {

                    MessageBox.Show("Girdiğiniz Bilgilerin Doğruluğundan emin olun");
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Debug.WriteLine(row.Cells[1].ToString());
                    if (Convert.ToInt32(row.Cells[0].Value)==no && row.Cells[0].Value != null)
                    {
                        row.Selected = true;
                        linkLabel1.Visible = true;
                        return;
                    }
                }
                MessageBox.Show("Bulunamadı.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            linkLabel1.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    Ogrenci ogr = Database.GetOgrenci(id);

                    ogrenci_bilgisi ogrencibilgisi = new ogrenci_bilgisi(ogr);
                    ogrencibilgisi.ShowDialog();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            linkLabel1.Visible = false; 
        }
    }
}
