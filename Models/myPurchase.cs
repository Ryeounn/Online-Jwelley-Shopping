using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jewelly.Models
{
    public class MyPurchase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
    }

    public class JoinMyPurchase
    {
        JwelleyEntities db = new JwelleyEntities();
        public List<MyPurchase> SelectPurchase(int id)
        {
            var purChase = (from i in db.CartLists
                            join o in db.Orderdetails on i.ID equals o.ID
                            join c in db.ItemMsts on o.Style_Code equals c.Style_Code
                            join g in db.Imgs on c.Img_ID equals g.ID
                            where i.ID == o.ID && o.Style_Code == c.Style_Code && c.Img_ID == g.ID && i.userID == id
                            select new MyPurchase()
                            {
                                ID = i.ID,
                                Name = i.Product_Name,
                                Price = i.MRP,
                                Image = g.pic_1,
                                Path = g.path_img,
                                Status = i.Status
                            }).ToList();
            return purChase;
        }
    }
}