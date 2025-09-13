using DLL.interfaces;
using DLL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.repository
{
    public class ShopRepository : IRepository<Info_Product>
    {
        private ShopContext _shopContext;
        public ShopRepository(ShopContext shopContext)
        {
            this._shopContext = shopContext;
        }
        public void Add(Info_Product value)
        {
            this._shopContext.Add(value);
            this._shopContext.SaveChanges();
        }

        public void Delete(Info_Product value)
        {
            this._shopContext.Remove(value);
            this._shopContext.SaveChanges();
        }

        public Info_Product FindByName(string name)
        {
            return this._shopContext.Products.Where(p => p.Name_Disk == name).FirstOrDefault();
        }
        public Info_Product FindByAvtor(string avtor)
        {
            return this._shopContext.Products.Where(p => p.Name_Avtor == avtor).FirstOrDefault();
        }
        public Info_Product FindByGenre(string genre)
        {
            return this._shopContext.Products.Where(p => p.Genre == genre).FirstOrDefault();
        }
        public Info_Product FindForOtherMethod(int id)
        {
            return this._shopContext.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Info_Product> GetInfo()
        {
            return _shopContext.Products;
        }


        public void Edit(Info_Product value)
        {
            if(value != null)
            {
                Info_Product tempShop = FindForOtherMethod(value.Id);
                if(tempShop != null)
                {
                    tempShop.Name_Disk = value.Name_Disk;
                    tempShop.Name_Avtor = value.Name_Avtor;
                    tempShop.Name_Collective = value.Name_Collective;
                    tempShop.Price = value.Price;
                    tempShop.Issue = value.Issue;
                    tempShop.Number_Sound = value.Number_Sound;
                    tempShop.Genre = value.Genre;
                    tempShop.Cost = value.Cost;
                    _shopContext.Update(tempShop);
                    _shopContext.SaveChanges();
                }
            }
        }
    }
}
