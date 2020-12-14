using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class kitaplar : Form
    {
        kitap_ekle kitapEkle;
        public kitaplar()
        {
            InitializeComponent();
        }

        private void kitaplar_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            populateTable();
        }
        private void populateTable()
        {
            dataGridView1.Rows.Clear();
            List<Kitap> liste = Database.listele2();

            foreach (Kitap k in liste)
            {
                dataGridView1.Rows.Add(k.kitapid.ToString(), k.isim, k.yazar, k.basimTarihi.Date.ToShortDateString(), k.kategori, k.sayfaSayisi.ToString());
            }
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rand = new Random().Next(10000, 99999);
            kitapEkle = new kitap_ekle(rand);
           
            kitapEkle.FormClosing += new FormClosingEventHandler(kitapEkleKapaniyor);

            kitapEkle.ShowDialog();
        }
        private void kitapEkleKapaniyor(object sender,FormClosingEventArgs e)
        {
            populateTable();
            kitapEkle = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                int indx = dataGridView1.SelectedRows.Count;
                if (indx >0)
                {
                    if (MessageBox.Show(@"Kaydi silmek istediginizden emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                    string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    int id = Convert.ToInt32(selected);
                    Database.sil2(id);
                    populateTable();
                    }
                }

                else
                {
                    MessageBox.Show("Silinecek Kitabi seçiniz.");
                }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                

                kitapEkle = new kitap_ekle(Database.GetKitap(id));
                kitapEkle.FormClosing += new FormClosingEventHandler(kitapEkleKapaniyor);

                kitapEkle.ShowDialog();

            }

            else
            {
                MessageBox.Show("Güncellenecek öğrenciyi seçiniz.");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    

                    kitap_bilgisi kb = new kitap_bilgisi(Database.GetKitap(id));
                    kb.ShowDialog();

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
            if (radioButton1.Checked)
            {
                string text = textBox1.Text;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        Debug.WriteLine(row.Cells[1].ToString());
                        if (row.Cells[1].Value != null && row.Cells[1].Value.ToString().Equals(text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            row.Selected = true;
                            linkLabel1.Visible = true;
                            return;
                        }                                            
                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().ToLower().Contains(text.ToLower()))
                        {
                            row.Selected = true;
                            linkLabel1.Visible = true;
                            return;
                        }
                    }

                    MessageBox.Show("Bulunamadı.");
            }

            else if (radioButton2.Checked)
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
                    if (Convert.ToInt32(row.Cells[0].Value) == no && row.Cells[0].Value != null)
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
                    Kitap ktp = Database.GetKitap(id);

                    kitap_bilgisi kitap_Bilgisi = new kitap_bilgisi(ktp);
                    kitap_Bilgisi.ShowDialog();
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
