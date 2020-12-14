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
    

    public partial class ogr_ekle : Form
    {
        bool text1Validated = false;
        bool text2Validated = false;
        bool text3Validated = false;
        bool text4Validated = false;
        bool combo1Validated = false;
        bool combo2Validated = false;

        public ogr_ekle()
        {
            InitializeComponent();
        }
        public ogr_ekle(Ogrenci ogrenci)
        {
            InitializeComponent();
            setFields(ogrenci);

        }

        private void setFields(Ogrenci o)
        {
            textBox1.Text = o.ogrid.ToString();
            //textBox1.Enabled = false;
            textBox1.ReadOnly = true;
            textBox1.Enabled = false;
            textBox2.Text = o.isim;
            textBox3.Text = o.soyisim;
            dateTimePicker1.Value = o.dogumTarihi.Date;
            comboBox1.SelectedIndex= getIndex(o.cinsiyet.cinsiyet);
            textBox4.Text = o.email;
            comboBox2.SelectedItem = o.bolum;
            button1.Text = "Güncelle";

            button1.Click -= button1_Click;
            button1.Click += guncelle_buttonClickEvent;

            

        }

        private void ogr_ekle_Load(object sender, EventArgs e)
        {
            //textBox1.Validating += all_validating;
            //textBox2.Validating += all_validating;
            //textBox3.Validating += all_validating;
            //textBox4.Validating += all_validating;
            //comboBox1.Validating += all_validating;
            //comboBox2.Validating += all_validating;
            
            
            textBox2.KeyPress += new KeyPressEventHandler(isimsoyisimEventi);
            textBox3.KeyPress += new KeyPressEventHandler(isimsoyisimEventi);
        }
        private void guncelle_buttonClickEvent(object sender ,EventArgs e)
        {
            
            this.ValidateChildren();
            bool izin = text1Validated && text2Validated && text3Validated && text4Validated && combo1Validated && combo2Validated;


            if (izin)
            {
                label9.Visible = false;
                Ogrenci temp = new Ogrenci(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, new Cinsiyet(Convert.ToChar(comboBox1.SelectedItem.ToString().Substring(0, 1))), comboBox2.SelectedItem.ToString(), dateTimePicker1.Value.Date);
                Database.guncelle(temp);
                Debug.WriteLine("guncelle eventi");
                this.Close();
            }
            else
            {
                label9.Visible = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
           this.ValidateChildren();
           bool izin = text1Validated && text2Validated && text3Validated && text4Validated && combo1Validated && combo2Validated;

            
           if(izin)
            {
                label9.Visible = false;
                Ogrenci temp = new Ogrenci(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, textBox4.Text, new Cinsiyet(Convert.ToChar(comboBox1.SelectedItem.ToString().Substring(0, 1))), comboBox2.SelectedItem.ToString(), dateTimePicker1.Value.Date);
                Database.ekle(temp);
                Debug.WriteLine("ekle eventi");
                this.Close();
            }
           else
            {
                label9.Visible = true;
            }
        }

        

        
        private void isimsoyisimEventi(object sender,KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private int getIndex(char g)
        {
            if (g == 'e' || g == 'E')
                return 0;
            else if (g == 'k' || g == 'K')
                return 1;
            else return 2;
        }

        //private void all_validating(object sender,CancelEventArgs e)
        //{
        //    if(sender is TextBox)
        //    {
        //        TextBox textBox= (sender as TextBox);
        //        if (textBox.Text.Trim() == String.Empty)
        //        {
        //            errorProvider1.SetError(textBox, "Bu alan gerekli !");

        //        }
        //        else
        //        {
        //            errorProvider1.SetError(textBox, null);
                    
        //        }

        //    }
        //    else if(sender is ComboBox)
        //    {
        //        ComboBox combo = (sender as ComboBox);
        //        if (combo.SelectedIndex == -1)
        //        {
        //            errorProvider1.SetError(combo, "Bu alan gerekli !");
                    
        //        }
        //        else
        //        {
        //            errorProvider1.SetError(combo, null);
        //        }
        //    }
        //}

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox.Text.Trim() == String.Empty)
            {
                errorProvider1.SetError(textBox, "Bu alan gerekli !");
                text1Validated = false;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                text1Validated = true;
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox.Text.Trim() == String.Empty)
            {
                errorProvider1.SetError(textBox, "Bu alan gerekli !");
                text2Validated = false;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                text2Validated = true;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox.Text.Trim() == String.Empty)
            {
                errorProvider1.SetError(textBox, "Bu alan gerekli !");
                text3Validated = false;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                text3Validated = true;
            }
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            ComboBox combo = (sender as ComboBox);
            if (combo.SelectedIndex == -1)
            {
                errorProvider1.SetError(combo, "Bu alan gerekli !");
                combo1Validated = false;
            }
            else
            {
                errorProvider1.SetError(combo, null);
                combo1Validated = true;
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox.Text.Trim() == String.Empty)
            {
                errorProvider1.SetError(textBox, "Bu alan gerekli !");
                text4Validated = false;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                text4Validated = true;
            }
        }

        private void comboBox2_Validating(object sender, CancelEventArgs e)
        {
            ComboBox combo = (sender as ComboBox);
            if (combo.SelectedIndex == -1)
            {
                errorProvider1.SetError(combo, "Bu alan gerekli !");
                combo2Validated = false;
            }
            else
            {
                errorProvider1.SetError(combo, null);
                combo2Validated = true;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
