using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Model
{
    public class Urun
    {

        public string id { get; set; }
        public string qrkod { get; set; }
        public string barkodKod { get; set; }
        public DateTime olusturmaTarih { get; set; }
        public DateTime guncellenmeTarih { get; set; }
        public string urunIsim { get; set; }
        public int kilo { get; set; }
        public int fiyat { get; set; }
        public int ciro { get; set; }





    }
}
