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
    public partial class ayarlar : Form
    {
        public ayarlar()
        {
            InitializeComponent();
        }


        int oldValue;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TeslimSuresi = Convert.ToInt32(numericUpDown1.Value);
            Properties.Settings.Default.Save();
            Ayar.teslim_suresi = Convert.ToInt32(numericUpDown1.Value);

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ceza = Convert.ToInt32(numericUpDown3.Value);
            Properties.Settings.Default.Save();
            Ayar.ceza_miktari = Convert.ToInt32(numericUpDown3.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.kitaphak = Convert.ToInt32(numericUpDown2.Value);
            Properties.Settings.Default.Save();
            Ayar.max_kitap_hakki = Convert.ToInt32(numericUpDown2.Value);
        }

        private void ayarlar_Load(object sender, EventArgs e)
        {
            
            numericUpDown1.Value = Properties.Settings.Default.TeslimSuresi;
            oldValue = Convert.ToInt32(numericUpDown1.Value);
            numericUpDown2.Value = Properties.Settings.Default.kitaphak;
            numericUpDown3.Value = Properties.Settings.Default.ceza;
        }

       

        private void ayarlar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.WriteLine("eski value =" + oldValue);
            Debug.WriteLine("yeni value =" + numericUpDown1.Value);
            if (numericUpDown1.Value != oldValue)
            {
                Database.teslimGuncelle(Convert.ToInt32(numericUpDown1.Value));
            }
        }
    }
}
