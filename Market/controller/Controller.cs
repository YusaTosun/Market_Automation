using Market.dao;
using Market.Enumaration;
using Market.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Controller
{
    public class controller
    {

        Repository repository;

        public controller()
        {
            repository = new Repository(); // Bunun mantığına tekrar bak @@@@@
        }

        public loginStatus login(string kullaniciAdi, string sifre)
        {
            User user = new User();
           user= repository.login(kullaniciAdi,sifre);

            

            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(sifre))
            {
                if (kullaniciAdi == user.kullaniciAdi && sifre == user.sifre)

                {
                    if (user.yetki == "admin")
                    {
                        user.status = loginStatus.AdminGirisi;
                        return user.status;
                    }
                     
                    else if (user.yetki=="kasiyer")
                    {
                        user.status = loginStatus.KasiyerGirisi;
                        return user.status;
                    }

                    else
                    {
                        user.status = loginStatus.YetersizYetki;
                        return user.status;
                    }

                }

                else
                {
                    user.status = loginStatus.basarisiz;
                    return user.status;
                }


            }

            else
            {
                user.status = loginStatus.eksikParametre;
                return user.status;
            }

        }


        public List<LoginTable> getLoginTable()
        {
            List<LoginTable> loginTableList = repository.getLoginTable();

            return loginTableList;
        }


        public loginStatus DoAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabi)
        {
            if (kullaniciAdi != "" && guvenlikCevabi != "" && guvenlikCevabi != "")
            {

                loginStatus result = repository.DoAuthentication(kullaniciAdi, guvenlikSorusu, guvenlikCevabi);

                if (result == loginStatus.basarili)
                {
                    return result;
                }

                else
                {
                    return loginStatus.basarisiz;

                }


            }

            else
            {
                return loginStatus.eksikParametre;
            }


        }


        public loginStatus ChangePassword(string kullaniciAdi, string sifre)
        {
            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(sifre))
            {
                return repository.ChangePassword(kullaniciAdi, sifre);

            }

            else
            {
                return loginStatus.eksikParametre;
            }

        }

        public string GetEmailAdress(string KullaniciAdi)
        {
            string AlınanAdres = repository.GetEmailAdress(KullaniciAdi);

            return AlınanAdres;
        }

        public Urun UrunuGetir(string barkod)
        {
            if(!string.IsNullOrEmpty(barkod))
            {
                return repository.UrunuGetir();
            }

            else
            {
                return null;
            }
        }
    }
}
