using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    sealed class Database
    {
        public static OleDbConnection conn=Form1.getConnection();
        public static OleDbDataAdapter adapter;

        public static Ogrenci GetOgrenci(int id)
        {
            adapter = new OleDbDataAdapter("select * from ogrenciler where ogrenci_no="+id, conn);
            DataTable dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            DataRow row = dt.Rows[0];
            return new Ogrenci(Convert.ToInt32((row[0]).ToString()), row[1].ToString(), row[2].ToString(),
                    row[3].ToString(), new Cinsiyet(Convert.ToChar(row[4])), row[5].ToString(), Convert.ToDateTime(row[6].ToString()));
        }
        public static List<Ogrenci> listele()
        {
            List<Ogrenci> liste = new List<Ogrenci>();
            adapter = new OleDbDataAdapter("select * from ogrenciler", conn);
            DataTable ds = new DataTable();
            conn.Open();
            adapter.Fill(ds);
            conn.Close();
            
            foreach(DataRow row in ds.Rows)
            {
                //Ogrenci obj = new Ogrenci();
                //obj.ogrid = Convert.ToInt32((row[0]).ToString());
                //obj.isim = row[1].ToString();
                //obj.soyisim = row[2].ToString();
                //obj.email = row[3].ToString();
                //obj.cinsiyet = setSex(Convert.ToInt32((row[4]).ToString()));
                //obj.bolum = row[5].ToString();
                //obj.dogumTarihi = Convert.ToDateTime(row[6].ToString());
                //liste.Add(obj);

                liste.Add(new Ogrenci(Convert.ToInt32((row[0]).ToString()), row[1].ToString(), row[2].ToString(),
                    row[3].ToString(),new Cinsiyet(Convert.ToChar(row[4])), row[5].ToString(), Convert.ToDateTime(row[6].ToString())));
            }

            return liste;
        }
        public static void ekle(Ogrenci ogrenci)
        {
            conn.Open();
            string sorgu = "insert into ogrenciler(ogrenci_no,isim,soyisim,email,cinsiyet,bolum,dogum_tarihi) values(@no,@isim,@soyisim,@email," +
                "@cinsiyet,@bolum,@dogum_tarihi);";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            cmd.Parameters.AddWithValue("@no", ogrenci.ogrid);
            cmd.Parameters.AddWithValue("@isim",ogrenci.isim);
            cmd.Parameters.AddWithValue("@soyisim", ogrenci.soyisim);
            cmd.Parameters.AddWithValue("@email", ogrenci.email);
            cmd.Parameters.AddWithValue("@cinsiyet", ogrenci.cinsiyet.cinsiyet);
            cmd.Parameters.AddWithValue("@bolum", ogrenci.bolum);
            cmd.Parameters.AddWithValue("@dogum_tarihi", ogrenci.dogumTarihi);

            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt Eklendi");

                }
                else
                {
                    MessageBox.Show("Kayıt Eklenemedi");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show("Girdiğiniz öğrenci numarası başka bir öğrenciye aittir.");
            }
            finally
            {
                conn.Close();
            }
        }
        public static void guncelle(Ogrenci ogrenci)
        {
            conn.Open();
            string sorgu = "update ogrenciler set isim=@isim,soyisim=@soyisim,email=@email,cinsiyet=@cinsiyet,bolum=@bolum,dogum_tarihi=@dogum where ogrenci_no=@id";

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            
            cmd.Parameters.AddWithValue("@isim", ogrenci.isim);
            cmd.Parameters.AddWithValue("@soyisim", ogrenci.soyisim);
            cmd.Parameters.AddWithValue("@email", ogrenci.email);
            cmd.Parameters.AddWithValue("@cinsiyet", ogrenci.cinsiyet.cinsiyet);
            cmd.Parameters.AddWithValue("@bolum", ogrenci.bolum);
            cmd.Parameters.AddWithValue("@dogum_tarihi", ogrenci.dogumTarihi);
            cmd.Parameters.AddWithValue("@no", ogrenci.ogrid);


            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt Guncellendi.");

                }
                else
                {
                    MessageBox.Show("Kayıt Guncellenemedi");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void sil(int id)
        {
            conn.Open();
            string sorgu = "delete from ogrenciler where ogrenciler.ogrenci_no=@kimlik";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            cmd.Parameters.AddWithValue("@kimlik", id);
            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                   MessageBox.Show("Kayıt silindi");
                    
                }
                else
                {
                    MessageBox.Show("Kayıt silinemedi");
                }
                
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        //KITAP METODLARI
        public static DataTable alim_gecmisi(int kitapid,int ogrenciid)
        {
            string q="";
            if (kitapid != 0)
                q = "SELECT ogrenci_kitap.ogrenci_no, ogrenciler.isim, ogrenciler.soyisim, ogrenci_kitap.alim_tarihi, ogrenci_kitap.teslim_tarihi, ogrenci_kitap.durum FROM ogrenciler INNER JOIN ogrenci_kitap ON ogrenciler.ogrenci_no = ogrenci_kitap.ogrenci_no WHERE(((ogrenci_kitap.kitap_no) = " + kitapid + ")); UNION ALL SELECT kayit_gecmisi.ogrenci_no, ogrenciler.isim, ogrenciler.soyisim, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.durum FROM kayit_gecmisi INNER JOIN ogrenciler ON kayit_gecmisi.ogrenci_no = ogrenciler.ogrenci_no WHERE(((kayit_gecmisi.kitap_no) = " + kitapid + "));";
            else if (ogrenciid != 0)
                q = "SELECT * FROM ogrenci_kitap where ogrenci_kitap.ogrenci_no = " + ogrenciid + " UNION ALL SELECT kayit_gecmisi.ogrenci_no, kayit_gecmisi.kitap_no, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.ceza, kayit_gecmisi.durum FROM kayit_gecmisi where kayit_gecmisi.ogrenci_no = " + ogrenciid + "";
            else
                return null;
            conn.Open();
            adapter = new OleDbDataAdapter(q, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();

            return dt;
        }
        public static int kitapSayisi()
        {
            string q = "SELECT Count(*) AS Expr1 FROM kitaplar;";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            OleDbDataReader reader;
            cmd.CommandText = q;
            reader = cmd.ExecuteReader(CommandBehavior.Default);
            int adet=-1;

            try
            {
              while(reader.Read())
                {
                    adet = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);

                adet = -1;
                
            }
            finally
            {
                conn.Close();
            }
            
            return adet;
           
        }
        public static int alinmisKitapSayisi()
        {
            string q = "SELECT Count(*)  FROM ogrenci_kitap;";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            OleDbDataReader reader;
            cmd.CommandText = q;
            reader = cmd.ExecuteReader(CommandBehavior.Default);
            int adet = -1;

            try
            {
                while (reader.Read())
                {
                    adet = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);

                adet = -1;

            }
            finally
            {
                conn.Close();
            }

            return adet;

        }
        public static void ekle2(Kitap kitap)
        {
            conn.Open();
            string sorgu = "insert into kitaplar(kitap_no,isim,yazar,basim_tarihi,kategori,sayfa) values(@no,@isim,@yazar,@basim_tarihi," +
                "@kategori,@sayfa);";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            cmd.Parameters.AddWithValue("@no", kitap.kitapid);
            cmd.Parameters.AddWithValue("@isim", kitap.isim);
            cmd.Parameters.AddWithValue("@yazar", kitap.yazar);
            cmd.Parameters.AddWithValue("@basim_tarihi", kitap.basimTarihi);
            cmd.Parameters.AddWithValue("@kategori",kitap.kategori);
            cmd.Parameters.AddWithValue("@sayfa",kitap.sayfaSayisi);
            

            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt Eklendi");

                }
                else
                {
                    MessageBox.Show("Kayıt Eklenemedi");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show("Bu ID değeri başka bir kitaba aittir.");
            }
            finally
            {
                conn.Close();
            }
        }
        public static Kitap GetKitap(int id)
        {
            adapter = new OleDbDataAdapter("select * from kitaplar where kitap_no=" + id, conn);
            DataTable dt = new DataTable();
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            DataRow row = dt.Rows[0];

            return new Kitap(Convert.ToInt32(row[0].ToString()), row[1].ToString(), row[2].ToString(), Convert.ToDateTime(row[3].ToString()), (Kategori)Convert.ToInt32(row[4].ToString()), Convert.ToInt32(row[5].ToString()));
        }
        public static List<Kitap> listele2()
        {
            List<Kitap> liste = new List<Kitap>();
            adapter = new OleDbDataAdapter("select * from kitaplar", conn);
            DataTable ds = new DataTable();
            conn.Open();
            adapter.Fill(ds);
            conn.Close();

            foreach (DataRow row in ds.Rows)
            {
                //Ogrenci obj = new Ogrenci();
                //obj.ogrid = Convert.ToInt32((row[0]).ToString());
                //obj.isim = row[1].ToString();
                //obj.soyisim = row[2].ToString();
                //obj.email = row[3].ToString();
                //obj.cinsiyet = setSex(Convert.ToInt32((row[4]).ToString()));
                //obj.bolum = row[5].ToString();
                //obj.dogumTarihi = Convert.ToDateTime(row[6].ToString());
                //liste.Add(obj);

                liste.Add(new Kitap(Convert.ToInt32(row[0].ToString()),row[1].ToString(),row[2].ToString(),Convert.ToDateTime(row[3].ToString()),(Kategori)Convert.ToInt32(row[4].ToString()),Convert.ToInt32(row[5].ToString())));
            }

            return liste;
        }
        public static void sil2(int id)
        {
            conn.Open();
            string sorgu = "delete from kitaplar where kitap_no=@kimlik";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            cmd.Parameters.AddWithValue("@kimlik", id);
            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt silindi");

                }
                else
                {
                    MessageBox.Show("Kayıt silinemedi");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static void guncelle2(Kitap kitap)
        {
            conn.Open();
            string sorgu = "update kitaplar set isim=@isim,yazar=@yazar,basim_tarihi=@basim_tarihi,kategori=@kategori,sayfa=@sayfa where kitap_no=@id";

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sorgu;
            
            cmd.Parameters.AddWithValue("@isim", kitap.isim);
            cmd.Parameters.AddWithValue("@yazar", kitap.yazar);
            cmd.Parameters.AddWithValue("@basim_tarihi", kitap.basimTarihi);
            cmd.Parameters.AddWithValue("@kategori", kitap.kategori);
            cmd.Parameters.AddWithValue("@sayfa", kitap.sayfaSayisi);
            cmd.Parameters.AddWithValue("@no", kitap.kitapid);
            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt Guncellendi.");

                }
                else
                {
                    MessageBox.Show("Kayıt Guncellenemedi");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        
        public static int kacKitapAldi(int ogrenciid)
        {
            int adet = -1;
            string q = "SELECT count(ogrenci_kitap.ogrenci_no) FROM ogrenci_kitap where ogrenci_kitap.ogrenci_no ="+ogrenciid+"";
            conn.Open();
            adapter = new OleDbDataAdapter(q, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            foreach(DataRow row in dt.Rows)
            {
                adet = Convert.ToInt32(row[0]);
            }
            return adet;
        }
        ///alim teslim metodlari
        public static DialogResult kitapOgrenciKaydi(int kitapid,int ogrenciid)
        {
            int adet = kacKitapAldi(ogrenciid);
            if (adet==Ayar.max_kitap_hakki)
            {
                MessageBox.Show("Kitap alım hakkınızı doldurdunuz.\r\n Alınan kitap sayısı : " + adet);
                return DialogResult.Yes;
            }
            conn.Open();
            string q = "insert into ogrenci_kitap(ogrenci_no,kitap_no,alim_tarihi,teslim_tarihi) values(@ogrenciid,@kitapid,@alim,@teslim)";
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = q;
            cmd.Parameters.AddWithValue("@ogrenciid", ogrenciid);
            cmd.Parameters.AddWithValue("@kitapid", kitapid);
            cmd.Parameters.AddWithValue("@alim", DateTime.Today);
            cmd.Parameters.AddWithValue("@teslim", DateTime.Today.AddDays(15));
            try
            {

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Kayıt Eklendi");

                }
                else
                {
                    MessageBox.Show("Kayıt Eklenemedi");
                }

            }
            catch (Exception e)
            {
                DialogResult r = MessageBox.Show("Bu Kitap başka bir öğrenci tarafından alınmıştır. Teslim tarihi bilgisine bakmak ister misiniz?", "Mevcut Değil", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (r==DialogResult.Yes)
                {
                    conn.Close();
                    Kitap kb = GetKitap(kitapid);
                    kitap_bilgisi k_bilgi = new kitap_bilgisi(kb);
                    k_bilgi.ShowDialog();

                    return DialogResult.Yes;
                }
                else
                {
                    return DialogResult.No;
                }
                
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    
                }
            }
            return DialogResult.Yes;
        }
        public static double cezaSorgula(int ogrId,int kayit_id)
        {
            Debug.WriteLine(kayit_id);
            Debug.WriteLine(ogrId);
            string q = "SELECT kayit_gecmisi.ceza FROM kayit_gecmisi where kayit_gecmisi.id="+kayit_id+" and kayit_gecmisi.ogrenci_no="+ ogrId + "";
            adapter = new OleDbDataAdapter(q, conn);
            double ceza = 0;
            conn.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                ceza = Convert.ToDouble(row[0]);
            }
            conn.Close();

            return ceza;
        }
        public static DataTable kitapOgrenciListele(int filtre)
        {
            string q = "";
            if (filtre == 0)
                q = "SELECT ' ',ogrenci_kitap.ogrenci_no, ogrenciler.isim, ogrenci_kitap.kitap_no, kitaplar.isim, ogrenci_kitap.alim_tarihi, ogrenci_kitap.teslim_tarihi, ogrenci_kitap.ceza, ogrenci_kitap.durum FROM ogrenciler INNER JOIN(kitaplar INNER JOIN ogrenci_kitap ON kitaplar.kitap_no = ogrenci_kitap.kitap_no) ON ogrenciler.ogrenci_no = ogrenci_kitap.ogrenci_no;";
            else if (filtre == 1)
                q = "SELECT kayit_gecmisi.id, kayit_gecmisi.ogrenci_no, ogrenciler.isim, kayit_gecmisi.kitap_no, kitaplar.isim, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.ceza, kayit_gecmisi.durum FROM(kayit_gecmisi INNER JOIN kitaplar ON kayit_gecmisi.kitap_no = kitaplar.kitap_no) INNER JOIN ogrenciler ON kayit_gecmisi.ogrenci_no = ogrenciler.ogrenci_no;";
            //q = "SELECT ' ',kayit_gecmisi.ogrenci_no, ogrenciler.isim, kayit_gecmisi.kitap_no, kitaplar.isim, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.ceza, kayit_gecmisi.durum FROM(kayit_gecmisi INNER JOIN kitaplar ON kayit_gecmisi.kitap_no = kitaplar.kitap_no) INNER JOIN ogrenciler ON kayit_gecmisi.ogrenci_no = ogrenciler.ogrenci_no;";
            else if (filtre == 2)
                q = "SELECT ' ',ogrenci_kitap.ogrenci_no, ogrenciler.isim, ogrenci_kitap.kitap_no, kitaplar.isim, ogrenci_kitap.alim_tarihi, ogrenci_kitap.teslim_tarihi, ogrenci_kitap.ceza, ogrenci_kitap.durum FROM ogrenciler INNER JOIN(kitaplar INNER JOIN ogrenci_kitap ON kitaplar.kitap_no = ogrenci_kitap.kitap_no) ON ogrenciler.ogrenci_no = ogrenci_kitap.ogrenci_no; UNION ALL SELECT kayit_gecmisi.id,kayit_gecmisi.ogrenci_no, ogrenciler.isim, kayit_gecmisi.kitap_no, kitaplar.isim, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.ceza, kayit_gecmisi.durum FROM(kayit_gecmisi INNER JOIN kitaplar ON kayit_gecmisi.kitap_no = kitaplar.kitap_no) INNER JOIN ogrenciler ON kayit_gecmisi.ogrenci_no = ogrenciler.ogrenci_no;";
                    
                //q = "SELECT ogrenci_kitap.ogrenci_no, ogrenciler.isim, ogrenci_kitap.kitap_no, kitaplar.isim, ogrenci_kitap.alim_tarihi, ogrenci_kitap.teslim_tarihi, ogrenci_kitap.ceza, ogrenci_kitap.durum FROM ogrenciler INNER JOIN(kitaplar INNER JOIN ogrenci_kitap ON kitaplar.kitap_no = ogrenci_kitap.kitap_no) ON ogrenciler.ogrenci_no = ogrenci_kitap.ogrenci_no; UNION ALL SELECT kayit_gecmisi.ogrenci_no, ogrenciler.isim, kayit_gecmisi.kitap_no, kitaplar.isim, kayit_gecmisi.alim_tarihi, kayit_gecmisi.teslim_tarihi, kayit_gecmisi.ceza, kayit_gecmisi.durum FROM(kayit_gecmisi INNER JOIN kitaplar ON kayit_gecmisi.kitap_no = kitaplar.kitap_no) INNER JOIN ogrenciler ON kayit_gecmisi.ogrenci_no = ogrenciler.ogrenci_no;";





            adapter = new OleDbDataAdapter(q, conn);
            DataTable ds = new DataTable();
            conn.Open();
            adapter.Fill(ds);
            conn.Close();

            return ds;
        }
        public static DateTime alimTarihiSorgula(int ogrenciId,int id,string tabloIsmi)
        {
            string q = "select alim_tarihi from "+ tabloIsmi+" where ogrenci_no="+ogrenciId+"";
            if(tabloIsmi.Equals("kayit_gecmisi"))
            {
                q = q + " and id=" + id+"";
            }
            else
            {
                q += " and kitap_no=" + id + "";
            }
            adapter = new OleDbDataAdapter(q, conn);
            
            conn.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            DataRow row = dt.Rows[0];

            return Convert.ToDateTime(row[0].ToString());
        }
        public static DateTime teslimTarihiSorgula(int ogrenciId, int id, string tabloIsmi)
        {
            string q = "select teslim_tarihi from " + tabloIsmi + " where ogrenci_no=" + ogrenciId+"";
            if (tabloIsmi.Equals("kayit_gecmisi"))
            {
                q = q + " and id=" + id + "";
            }
            else
            {
                q += " and kitap_no=" + id + "";
            }
            adapter = new OleDbDataAdapter(q, conn);

            conn.Open();
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            DataRow row = dt.Rows[0];

            return Convert.ToDateTime(row[0].ToString());
        }
        public static void teslimEt(int ogrid,int kitapid,double ceza,DateTime alim,DateTime teslim)
        {
            string q1 = "insert into kayit_gecmisi(ogrenci_no,kitap_no,alim_tarihi,teslim_tarihi,ceza) values(@ogrid,@kitapid," +
                "@alim,@teslim,@ceza)";
            string q2 = "delete from ogrenci_kitap where ogrenci_no=" + ogrid;
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            OleDbCommand cmd2 = conn.CreateCommand();

            cmd.Parameters.AddWithValue("@ogrid", ogrid);
            cmd.Parameters.AddWithValue("@kitapid", kitapid);
            cmd.Parameters.AddWithValue("@alim", alim);
            cmd.Parameters.AddWithValue("@teslim", teslim);
            cmd.Parameters.AddWithValue("@ceza", ceza);

            cmd.CommandText = q1;
            cmd2.CommandText = q2;


            try
            {

                if (cmd.ExecuteNonQuery() > 0 && cmd2.ExecuteNonQuery()>0)
                {
                    MessageBox.Show("Kitap Teslim Edildi.");

                }
                else
                {
                    MessageBox.Show("Kitap Teslim Edilemedi.");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public static void teslimGuncelle(int sure)
        {
            string q = "update ogrenci_kitap set teslim_tarihi = DateAdd(\"d\","+sure+",[alim_tarihi])";
            OleDbCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = q;
            try
            {

                if (cmd.ExecuteNonQuery() > 0 )
                {
                    MessageBox.Show("Teslim Tarihi güncellendi.");

                }
                else
                {
                    MessageBox.Show("Teslim Tarihi güncellenemedi.");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
