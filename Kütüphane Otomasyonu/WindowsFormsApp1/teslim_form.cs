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
    public partial class teslim_form : Form
    {
        Ogrenci ogr;
        Kitap kitap;
        DateTime alim;
        DateTime teslim;
        DateTime today = DateTime.Today;
        double ceza;
        Boolean durum;
        int kayit_id;
        public teslim_form(Ogrenci ogr,Kitap k,DateTime alim,DateTime teslim,Boolean durum,int kayit_id)
        {
            InitializeComponent();
            this.ogr = ogr;
            this.kitap = k;
            this.durum = durum;
            this.alim = alim;
            this.teslim = teslim;
            this.kayit_id = kayit_id;
            setViews();
        }

        private void setViews()
        {
            label8.Text = kitap.kitapid.ToString();
            label9.Text = kitap.isim;
            label10.Text = kitap.yazar;
            label11.Text = kitap.basimTarihi.Date.ToShortDateString();
            label12.Text = kitap.kategori.ToString();
            label13.Text = kitap.sayfaSayisi.ToString();

            label18.Text = ogr.ogrid.ToString();
            label17.Text = ogr.isim + " " + ogr.soyisim;
            label16.Text = ogr.email;
            label15.Text = ogr.dogumTarihi.ToShortDateString();
            label14.Text = ogr.cinsiyet.ToString().ToUpper();
            label1.Text = ogr.bolum;

            
            label29.Text = alim.ToShortDateString();
            label30.Text = teslim.ToShortDateString();
            

            if (durum)
            {
                button1.Enabled = false;
                
                ceza = Database.cezaSorgula(ogr.ogrid, kayit_id);
                if(ceza==0)
                {
                    label28.Text = "Ceza Yok";
                    label28.ForeColor = Color.Green;
                }
                else
                {
                    label28.Text = "Ceza Miktarı :" + "\r\n" + ceza;
                    label28.ForeColor = Color.Red;
                }
            }
            else if(today<=(alim.AddDays(Ayar.teslim_suresi)))
            {
                label28.Text = "Ceza Yok";
                label28.ForeColor = Color.Green;

                Debug.WriteLine("2. if");
            }
            else
            {
                ceza = ((today - alim.AddDays(Ayar.teslim_suresi)).TotalDays) * Ayar.ceza_miktari;
                label28.Text = "Ceza Miktarı :" + "\r\n" + ceza;
                label28.ForeColor = Color.Red;

                Debug.WriteLine("3. if");
            }

        }
        
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void teslim_form_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ogr != null && kitap !=null)
            {
                Database.teslimEt(ogr.ogrid, kitap.kitapid, ceza, alim, today);
                this.Close();
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
