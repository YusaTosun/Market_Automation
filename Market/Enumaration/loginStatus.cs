using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Enumaration
{
    public enum loginStatus
    {
        basarili, basarisiz, eksikParametre, // Başarı Durumu 
        AdminGirisi, KasiyerGirisi, YetersizYetki // Yetki Durumu

    }
}
