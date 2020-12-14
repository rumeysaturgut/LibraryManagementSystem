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
    public partial class ogrenci_bilgisi : Form
    {
        Ogrenci ogrenci;
        public ogrenci_bilgisi(Ogrenci ogrenci)
        {
            InitializeComponent();
            this.ogrenci = ogrenci;
            setViews();
        }

        private void setViews()
        {
            label8.Text = ogrenci.ogrid.ToString();
            label9.Text = ogrenci.isim;
            label1.Text = ogrenci.soyisim;
            label12.Text = ogrenci.cinsiyet.ToString();
            label13.Text = ogrenci.email;
            label11.Text = ogrenci.dogumTarihi.ToShortDateString();
            label10.Text = ogrenci.bolum;

            dataGridView1.AllowUserToAddRows = false;
        }

        private void ogrenci_bilgisi_Load(object sender, EventArgs e)
        {
            DataTable dt = Database.alim_gecmisi(0,ogrenci.ogrid);
            int c = 0;
            foreach (DataRow row in dt.Rows)
            {
                Boolean durum = Convert.ToBoolean(row[5]);
                DateTime teslim = Convert.ToDateTime(row[3]);
                dataGridView1.Rows.Add(Convert.ToInt32(row[1].ToString()), Convert.ToDateTime(row[2]).ToShortDateString(), teslim.ToShortDateString(),Convert.ToDouble(row[4]).ToString(),durum.ToString());
                if (durum)
                    dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (DateTime.Today > teslim)
                    dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.Red;
                else if (DateTime.Today.AddDays(3) > teslim)
                    dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.Yellow;
                else if (DateTime.Today < teslim)
                    dataGridView1.Rows[c].DefaultCellStyle.BackColor = Color.LightBlue;

                c++;

            }

        }
        
    }
}
//string text = textBox1.Text;
//(dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("isim = '{0}'", text);