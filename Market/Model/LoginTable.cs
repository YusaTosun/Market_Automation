using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Model
{
    public class LoginTable
    {

        public int id { get; set; }
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
        public string yetki { get; set; }
        public string EmailAdresi { get; set; }
        public string guvenlikSorusu { get; set; }
        public string guvenlikCevabi { get; set; }

    }
}
