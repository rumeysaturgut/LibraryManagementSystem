using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApp1
{
    public partial class graph : Form
    {
        public graph()
        {
            InitializeComponent();
        }

        private void graph_Load(object sender, EventArgs e)
        {
            int tumu = Database.kitapSayisi();
            int alinmis = Database.alinmisKitapSayisi();
            int musait = tumu - alinmis;

            GraphPane myPane = zedGraphControl1.GraphPane;
            string[] labels = { "Verilmiş", "Verilmeye Hazır", "Tümü" };


            myPane.Title.Text = "KÜTÜPHANE KİTAP GRAFI";
            myPane.Title.FontSpec.FontColor = Color.DarkTurquoise;
            myPane.Title.FontSpec.Size = 26;
            myPane.YAxis.Title.Text = "Kitap";
            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Purple;

            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Title.Text = "Durum";
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.FontColor = Color.Purple;
            myPane.XAxis.Scale.Max = 2;
            myPane.YAxis.Scale.Max = tumu + ((int)tumu * 0.1);
            myPane.YAxis.Scale.MajorStep = tumu / 5;
            myPane.YAxis.Scale.MinorStep = tumu / 10;




            PointPairList pairList = new PointPairList();
            pairList.Add(new PointPair(0.5, alinmis));

            PointPairList pairList2 = new PointPairList();
            pairList2.Add(new PointPair(0.6, musait));

            PointPairList pairList3 = new PointPairList();
            pairList3.Add(new PointPair(0.8, tumu));




            BarItem myBar = myPane.AddBar("Müsait", pairList, Color.Green);
            BarItem myBar2 = myPane.AddBar("Alınmış", pairList2, Color.Red);
            BarItem myBar3 = myPane.AddBar("Tümü", pairList3, Color.Blue);

        }
    }
}
