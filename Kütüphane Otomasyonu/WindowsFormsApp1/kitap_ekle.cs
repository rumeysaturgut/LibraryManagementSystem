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
    public partial class kitap_ekle : Form
    {
        bool text1Validated = false;
        bool text2Validated = false;
        bool text3Validated = false;
        bool text4Validated = false;
        bool combo1Validated = false;

        public kitap_ekle(int id)
        {
            InitializeComponent();
            comboDoldur();
            textBox1.Text = id.ToString();
        
        }

        public kitap_ekle(Kitap kitap)
        {
            InitializeComponent();
            comboDoldur();
            setViews(kitap);
        }
        
        private void comboDoldur()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(Kategori));
            comboBox1.SelectedIndex = -1;

        }
        private void setViews(Kitap k)
        {
            textBox1.Text = k.kitapid.ToString();
            textBox2.Text = k.isim;
            textBox3.Text = k.yazar;
            dateTimePicker1.Value = k.basimTarihi.Date;
            comboBox1.SelectedIndex =(int)k.kategori;
            textBox4.Text = k.sayfaSayisi.ToString();

            button1.Click -= button1_Click;
            button1.Click += guncelle;
            button1.Text = "Güncelle";

            textBox1.ReadOnly = true;
            textBox1.Enabled = false;
        }

        private void kitap_ekle_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
            dateTimePicker1.CustomFormat = "yyyy";

            textBox2.KeyPress += new KeyPressEventHandler(isimsoyisimEventi);
            textBox3.KeyPress += new KeyPressEventHandler(isimsoyisimEventi);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            bool izin = text1Validated && text2Validated && text3Validated && text4Validated && combo1Validated;

            if (izin)
            {
                label9.Visible = false;
                Kategori kategori = (Kategori)(comboBox1.SelectedItem);
                Kitap kitap = new Kitap(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, dateTimePicker1.Value.Date, kategori, Convert.ToInt32(textBox4.Text));
                Database.ekle2(kitap);
                Debug.WriteLine("guncelle eventi");
                this.Close();
            }
            else
            {
                label9.Visible = true;
            }

        }
       private void guncelle(object sender ,EventArgs e)
        {
            this.ValidateChildren();
            bool izin = text1Validated && text2Validated && text3Validated && text4Validated && combo1Validated;

            if (izin)
            {
                label9.Visible = false;
                Kitap kitap = new Kitap(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, dateTimePicker1.Value.Date, (Kategori)(Convert.ToInt32(comboBox1.SelectedItem)), Convert.ToInt32(textBox4.Text));
                Database.guncelle2(kitap);
                Debug.WriteLine("guncelle eventi");
                this.Close();
            }
            else
            {
                label9.Visible = true;
            }

        }
        private void isimsoyisimEventi(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
