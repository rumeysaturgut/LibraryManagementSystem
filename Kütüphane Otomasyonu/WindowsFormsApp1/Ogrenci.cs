using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    
    public class Ogrenci
    {
        public int ogrid { get; set; }
        public string isim { get; set; }
        public string soyisim { get; set; }
        public string email { get; set; }
        public string bolum { get; set; }
        public Cinsiyet cinsiyet { get; set; }
        public DateTime dogumTarihi { get; set; }

        public Ogrenci()
        {

        }
        public Ogrenci(int ogrid, string isim, string soyisim, string email, Cinsiyet cinsiyet, string bolum, DateTime dogumTarihi)
        {
            this.ogrid = ogrid;
            this.isim = isim;
            this.soyisim = soyisim;
            this.email = email;
            this.bolum = bolum;
            this.cinsiyet = cinsiyet;
            this.dogumTarihi = dogumTarihi;
        }

       
        public  static string Getcinsiyet(Cinsiyet c)
        {
            int x= Convert.ToInt32(c);
            switch (x)
            {
                case 1:
                    return "erkek";
                    break;
                case 2:
                    return "diger";
                    break;
                case 0:
                    return "kadin";
                    break;
                default:
                    return "";
            }

        }

        public override string ToString()
        {
            return isim + " " + ogrid;
        }
    }
   
}
