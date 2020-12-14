using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Kitap
    {
        public int kitapid { get; set; }
        public string isim { get; set; }
        public string yazar { get; set; }
        public DateTime basimTarihi { get; set; }
        public Kategori kategori { get; set; }
        public int sayfaSayisi { get; set; }

        public Kitap(int kitapid, string isim, string yazar, DateTime basimTarihi, Kategori kategori, int sayfaSayisi)
        {
            this.kitapid = kitapid;
            this.isim = isim;
            this.yazar = yazar;
            this.basimTarihi = basimTarihi;
            this.kategori = kategori;
            this.sayfaSayisi = sayfaSayisi;
        }

        public override string ToString()
        {
            return this.isim + "  /" + kategori ;
        }
    }
    
    public enum Kategori
    {
        PopülerBilim = 0,
        Korku = 1,
        Polisiye = 2,
        Roman = 3,
        DünyaKlasikleri = 4,
        Felsefe = 5,
        Tarih = 6,
        Edebiyat = 7,
        Psikoloji = 8,
        Sanat = 9,
        Sağlık = 10,
        Gezi = 11,
        DinMitoloji = 12,
        Kurgu=13,
        Romantik=14,
        Şiir=15,
    }
}
