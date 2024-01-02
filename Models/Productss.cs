using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jewelly.Models
{
    public class Productss
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string secondImg { get; set; }
        public decimal Price { get; set; }
        public string Path { get; set; }

    }
    public class Join
    {
        public JwelleyEntities db = new JwelleyEntities();
        public List<Productss> SelectProduct(decimal? min, decimal? max, int? brandid, int? cateid, int? stoneid, int? jewelry, int? gold, string prices)
        {
            if (prices != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.MRP > 0
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).OrderBy(p => p.Price).ToList();

                return product;
            }
            if (prices == null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.MRP > 0
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).OrderByDescending(p => p.Price).ToList();

                return product;
            }


            if (min == null || max == null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }
            else if (min != null || max != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.MRP >= min && i.MRP <= max
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }

            else if (brandid != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.Brand_ID == brandid
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }
            else if (cateid != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.Cat_ID == cateid
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }
            else if (jewelry != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.ID_jewelly == jewelry
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }
            else if (gold != null)
            {
                var product = (from i in db.ItemMsts
                               join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                               join m in db.Imgs on i.Img_ID equals m.ID
                               where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID && i.GoldType_ID == gold
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = p.Prod_Type,
                                   Img = m.pic_1,
                                   secondImg = m.pic_2,
                                   Path = m.path_img,
                                   Price = i.MRP
                               }).ToList();

                return product;
            }
            else if (stoneid != null)
            {
                var product = (from p in db.StoneMsts
                               join i in db.ItemMsts on p.Style_Code equals i.Style_Code
                               join m in db.StoneQltyMsts on p.StoneQlty_ID equals m.StoneQlty_ID
                               join g in db.Imgs on i.Img_ID equals g.ID
                               join pr in db.ProdMsts on i.Prod_ID equals pr.Prod_ID
                               where m.StoneQlty_ID == p.StoneQlty_ID && i.Style_Code == p.Style_Code
                               select new Productss()
                               {
                                   ID = i.Style_Code,
                                   Name = m.StoneQlty,
                                   Img = g.pic_1,
                                   secondImg = g.pic_2,
                                   Path = g.path_img,
                                   Price = i.MRP
                               }).ToList();
                return product;
            }
            var producttotal = (from i in db.ItemMsts
                                join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                                join m in db.Imgs on i.Img_ID equals m.ID
                                where i.Prod_ID == p.Prod_ID && i.Img_ID == m.ID
                                select new Productss()
                                {
                                    ID = i.Style_Code,
                                    Name = p.Prod_Type,
                                    Img = m.pic_1,
                                    secondImg = m.pic_2,
                                    Path = m.path_img,
                                    Price = i.MRP
                                }).ToList();

            return producttotal;
        }
    }
}







