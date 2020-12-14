using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class kitap_bilgisi : Form
    {
        public kitap_bilgisi(Kitap kitap)
        {
            InitializeComponent();
            setViews(kitap);

        }

        private void setViews(Kitap kitap)
        {
            label8.Text = kitap.kitapid.ToString();
            label9.Text = kitap.isim;
            label10.Text = kitap.yazar;
            label11.Text = kitap.basimTarihi.Date.ToShortDateString();
            label12.Text = kitap.kategori.ToString();
            label13.Text = kitap.sayfaSayisi.ToString();

            DataTable dt = Database.alim_gecmisi(kitap.kitapid,0);
            int c = 0;
            foreach(DataRow row in dt.Rows)
            {
                Boolean durum = Convert.ToBoolean(row[5]);
                DateTime teslim = Convert.ToDateTime(row[4]);
                dataGridView1.Rows.Add(Convert.ToInt32(row[0].ToString()), row[1].ToString(), row[2].ToString(), Convert.ToDateTime(row[3]).ToShortDateString(), teslim.ToShortDateString());
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

        private void kitap_bilgisi_Load(object sender, EventArgs e)
        {

        }
    }
}
