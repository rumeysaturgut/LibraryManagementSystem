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
    public partial class alim_teslim : Form
    {
        teslim_form teslimForm;
        alim_dialog dialog;
        public alim_teslim()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void alim_teslim_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            populateList(comboBox1.SelectedIndex);
            
        }
        private void populateList(int secim)
        {
            int c = 0;
            DataTable dt = Database.kitapOgrenciListele(secim);
            //dataGridView1.DataSource = dt;
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                double ceza = 0;
                DateTime today = DateTime.Today;
                DateTime alim = Convert.ToDateTime(row[5].ToString());
                DateTime teslim = Convert.ToDateTime(row[6].ToString());
                Boolean durum = Convert.ToBoolean(row[8]);
                int kayit_id = -1;
                if (durum)
                {
                    kayit_id = Convert.ToInt32(row[0].ToString());
                    ceza = Convert.ToDouble(row[7]);
                }
                else
                {
                    if (today <= (alim.AddDays(Ayar.teslim_suresi)))
                    {
                        ceza = 0;
                    }
                    else
                    {
                        ceza = ((today - alim.AddDays(Ayar.teslim_suresi)).TotalDays) * Ayar.ceza_miktari;

                    }
                }

                dataGridView1.Rows.Add(kayit_id.ToString(),row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), alim.ToShortDateString(), teslim.ToShortDateString(), ceza.ToString(), row[8].ToString());
                renklendir(alim,teslim, c,durum);
                c++;

            }
            dataGridView1.ClearSelection();
        }

        private void renklendir(DateTime alim,DateTime teslim, int c,Boolean durum)
        {
            if(durum)
                dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.LightGreen;
            else if (DateTime.Today>teslim)
                dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.Red;
            else if (DateTime.Today.AddDays(3)>teslim)
                dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.Yellow;
            else if (DateTime.Today<teslim)
                dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.LightBlue;
            

        }
        private void button1_Click(object sender, EventArgs e)
        {
            dialog = new alim_dialog();
            dialog.FormClosing += new FormClosingEventHandler(dialogKapaniyor);
            dialog.ShowDialog();
        }
        private void dialogKapaniyor(object sender , FormClosingEventArgs e)
        {
            populateList(comboBox1.SelectedIndex);
            dialog = null; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                populateList(0);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                populateList(1);
            }
            else if (comboBox1.SelectedIndex==2)
            {
                populateList(2);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int indx = dataGridView1.SelectedRows.Count;
            if (indx > 0)
            {
                int ogrenciId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                int kitapId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                Boolean durum = Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[8].Value);
                int kayit_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                DateTime alim;
                DateTime teslim;
                if (durum)
                {
                    alim = Database.alimTarihiSorgula(ogrenciId,kayit_id,"kayit_gecmisi");
                    teslim = Database.teslimTarihiSorgula(ogrenciId, kayit_id, "kayit_gecmisi");
                }
                else
                {
                    alim = Database.alimTarihiSorgula(ogrenciId,kitapId,"ogrenci_kitap");
                    teslim = Database.teslimTarihiSorgula(ogrenciId, kitapId, "ogrenci_kitap");
                }
                Ogrenci ogr = Database.GetOgrenci(ogrenciId);
                Kitap k = Database.GetKitap(kitapId);
                

                teslimForm = new teslim_form(ogr, k, alim,teslim,durum,kayit_id);
                teslimForm.FormClosing += new FormClosingEventHandler(teslimFormKapaniyor);
                teslimForm.ShowDialog();
            }

            else
            {
                MessageBox.Show("Bir Kayıt seçiniz.");
            }
        }
        private void teslimFormKapaniyor(object sender, FormClosingEventArgs e)
        {
            populateList(comboBox1.SelectedIndex);
            teslimForm = null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //Boolean durum = Convert.ToBoolean((sender as DataGridView).SelectedRows[0].Cells[7].Value);
            //if (durum)
            //{
            //    label2.Text = "Teslim Edildi";
            //    label2.ForeColor = Color.Green;
            //    button2.Text = "Teslim Bilgisi";
            //}

            //int kitapId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            //Debug.WriteLine(kitapId.ToString());

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Boolean durum = Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[8].Value);
                if (durum)
                {
                    label2.Text = "-- Teslim Edildi --";
                    label2.ForeColor = Color.Green;
                    button2.Text = "TESLİM BİLGİSİ";
                }
                else
                {
                    label2.Text = "-- Teslim Edilmedi --";
                    label2.ForeColor = Color.Red;
                    button2.Text = "KİTAP TESLİMİ";
                }
            }
            catch (Exception)
            {

                
            }
        }

        
    }
}
