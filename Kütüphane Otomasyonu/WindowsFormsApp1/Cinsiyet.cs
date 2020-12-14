using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Cinsiyet
    {
        public char cinsiyet { get; set; }
        public Cinsiyet(char cinsiyet)
        {
            this.cinsiyet = cinsiyet;
        }

        public override string ToString()
        {
            return cinsiyet.ToString().ToUpper();
        }
    }
}
