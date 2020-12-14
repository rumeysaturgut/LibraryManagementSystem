using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static OleDbConnection conn;
        

        public static OleDbConnection getConnection()
        {
            string Application_dir = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + @"\final.accdb";
                Console.WriteLine(connString);
                conn = new OleDbConnection();

                //Debug database linki
                conn.ConnectionString = connString;
                
                //setup database linki
                //conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application_dir + "final.accdb";
                return conn;
            }
            catch (Exception)
            {
                MessageBox.Show("Veritabanına bağlanılamadı.");
                return null;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ogrenciler ogr = new ogrenciler();
            ogr.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getConnection();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new kitaplar().ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new alim_teslim().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new ayarlar().Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new graph().Show();
        }
    }
    
}
