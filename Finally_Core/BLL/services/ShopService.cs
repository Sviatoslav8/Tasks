using BLL.interfaces;
using BLL.models;
using DLL.interfaces;
using DLL.models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.services
{
    public class ShopService : IService<Product>
    {
        private IRepository<Info_Product> _productRepository;
        private List<Product> _products;
        public ShopService(IRepository<Info_Product> repository)
        {
            this._productRepository = repository;
            this._products = new List<Product>();
        }
        public Product FindForOtherMethod(int id)
        {
            return InfoProductToProduct(_productRepository.FindForOtherMethod(id));
        }
        private Info_Product ProductToInfoProduct(Product product)
        {
            List<SoldProduct> soldlist = new List<SoldProduct>();
            List<PostponeDisk> postlist = new List<PostponeDisk>();
            List<NowProduct> nowlist = new List<NowProduct>();
            if(product.Solds != null)
            {
                foreach(var item in product.Solds)
                {
                    SoldProduct soldProduct = new SoldProduct()
                    {
                        Id = item.Id,
                        dateSold = item.dateSold,
                        People = new PeopleRegister
                        {
                            Id=item.People.Id,
                            Name = item.People.Name,
                            LastName = item.People.LastName,
                            Phone = item.People.Phone,
                            Birthday = item.People.Birthday,
                        }
                    };
                    soldlist.Add(soldProduct);
                }
            }
            if (product.Postpones != null)
            {
                foreach (var item in product.Postpones)
                {
                    PostponeDisk postp = new PostponeDisk()
                    {
                        Id = item.Id,
                        datePostpone = item.datePostpone,
                        People = new PeopleRegister
                        {
                            Name = item.People.Name,
                            LastName = item.People.LastName,
                            Phone = item.People.Phone,
                            Birthday = item.People.Birthday,
                        }
                    };
                    postlist.Add(postp);
                }
            }
            if (product.Nnows != null)
            {
                foreach (var item in product.Nnows)
                {
                    NowProduct now = new NowProduct()
                    {
                        Id = item.Id,
                    };
                    nowlist.Add(now);
                }
            }
            return new Info_Product()
            {
                Id = product.Id,
                Name_Avtor = product.Name_Avtor,
                Name_Collective = product.Name_Collective,
                Name_Disk = product.Name_Disk,
                Number_Sound = product.Number_Sound,
                Genre = product.Genre,
                Issue = product.Issue,
                Cost = product.Cost,
                Price = product.Price,
                SoldProducts = soldlist,
                PostponeDisks = postlist,
                NnowProducts = nowlist
            };
        }
        private Product InfoProductToProduct(Info_Product infoproduct)
        {
            List<Sold> soldList = new List<Sold>();
            List<Postpone> postsLsit = new List<Postpone>();
            List<Now> nowList = new List<Now>();
            if(infoproduct.SoldProducts != null)
            {
                foreach(var item in infoproduct.SoldProducts)
                {
                    Sold sold = new Sold()
                    {
                        Id = item.Id,
                        dateSold = item.dateSold,
                        People = new People
                        {
                            Name = item.People.Name,
                            LastName = item.People.LastName,
                            Phone = item.People.Phone,
                            Birthday = item.People.Birthday,
                        }
                    };
                    soldList.Add(sold);
                }
            }
            if(infoproduct.PostponeDisks != null)
            {
                foreach(var item in infoproduct.PostponeDisks)
                {
                    Postpone postp = new Postpone()
                    {
                        Id=item.Id,
                        datePostpone = item.datePostpone,
                        People = new People
                        {
                            Name = item.People.Name,
                            LastName = item.People.LastName,
                            Phone = item.People.Phone,
                            Birthday = item.People.Birthday,
                        }
                    };
                    postsLsit.Add(postp);
                }
            }
            if(infoproduct.NnowProducts != null)
            {
                foreach(var item in infoproduct.NnowProducts)
                {
                    Now now = new Now()
                    {
                        Id = item.Id,
                    };
                    nowList.Add(now);
                }
            }
            return new Product()
            {
                Id = infoproduct.Id,
                Name_Disk = infoproduct.Name_Disk,
                Name_Collective = infoproduct.Name_Collective,
                Name_Avtor = infoproduct.Name_Avtor,
                Number_Sound = infoproduct.Number_Sound,
                Genre = infoproduct.Genre,
                Issue = infoproduct.Issue,
                Cost = infoproduct.Cost,
                Price = infoproduct.Price,
                Solds = soldList,
                Postpones = postsLsit,
                Nnows = nowList
            };
        }

        public void Add(Product value)
        {
            _productRepository.Add(ProductToInfoProduct(value));
        }

        public void Delete(Product value)
        {
            _productRepository.Delete(ProductToInfoProduct(value));
        }

        public Product FindByName(string name)
        {
            return InfoProductToProduct(_productRepository.FindByName(name));
        }
        public Product FindByAvtor(string avtor)
        {
            return InfoProductToProduct(_productRepository.FindByAvtor(avtor));
        }
        public Product FindByGenre(string genre)
        {
            return InfoProductToProduct(_productRepository.FindByGenre(genre));
        }

        public IEnumerable<Product> GetInfo()
        {
            _products.Clear();
            foreach(Info_Product productinfo in _productRepository.GetInfo())
            {
                _products.Add(InfoProductToProduct(productinfo));
            }
            return _products;
        }

        public void Edit(Product value)
        {
            _productRepository.Edit(ProductToInfoProduct(value));
        }
    }
}
