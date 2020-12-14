using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class alim_dialog : Form
    {
        int ogrNo=0;
        int kitapNo=0;
        List<Ogrenci> liste;
        List<Kitap> liste2;
        public alim_dialog()
        {
            InitializeComponent();
        }

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void alim_dialog_Load(object sender, EventArgs e)
        {
            liste = Database.listele();
            liste2 = Database.listele2();
            foreach(Ogrenci ogr in liste)
            {
                listBox2.Items.Add(ogr);
            }
            foreach(Kitap k in liste2)
            {
                listBox1.Items.Add(k);
            }
           
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox2.FindString(textBox2.Text);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                ogrNo = ((Ogrenci)listBox2.SelectedItem).ogrid;
                string ad = ((Ogrenci)listBox2.SelectedItem).isim;
                panel4.Visible = true;

                label6.Text = "Kimlik No: " + ogrNo.ToString() + "\r\n" + "Isim : " + ad;
            }
            catch (Exception)
            {

                
            }
            
        }

      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox1.FindString(textBox1.Text);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                kitapNo = ((Kitap)listBox1.SelectedItem).kitapid;
                string ad = ((Kitap)listBox1.SelectedItem).isim;
                panel3.Visible = true;
                label5.Text = "kitap No: " + kitapNo.ToString() + "\r\n" + "Isim : " + ad;
            }
            catch (Exception)
            {
                
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
            Database.kitapOgrenciKaydi(kitapNo, ogrNo);
            progressBar1.Value = 100;
            this.Close();
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            if (ogrNo == 0)
            {
                MessageBox.Show("Önce bir öğrenci seçiniz.");
            }
            else
            {
                progressBar1.Value = 50;
                panel2.Visible = false;
                panel1.Visible = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            if(listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Önce bir kitap seçiniz.");
            }
            else
            {
                progressBar1.Value = 100;
                DialogResult r = Database.kitapOgrenciKaydi(kitapNo, ogrNo);                
                if (r != DialogResult.No)
                {

                    this.Close();
                }
            }
            
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
