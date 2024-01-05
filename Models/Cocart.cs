using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Jewelly.Models
{


    public class Cocart
    {
        JwelleyEntities db = new JwelleyEntities();

        public void Add(int itemid, int userID, string Img, string Path, string Name, decimal? Price, string Prod, int quantity = 1)
        {
            var exc = db.ShoppingCarts.FirstOrDefault(c => c.User_id == userID && c.item_id == itemid);
            if (exc != null)
            {
                exc.Quantity += quantity;
                exc.Total = exc.Quantity * exc.Price;
                db.SaveChanges();
            }
            else
            {
                var cart = new ShoppingCart
                {
                    User_id = userID,
                    item_id = itemid,
                    Quantity = quantity
                };
                var item = db.ItemMsts.Where(s => s.Style_Code == itemid).FirstOrDefault();
                if (item != null)
                {
                    cart.Product_name = item.ProdMst.Prod_Type;
                    cart.Price = item.MRP;
                    cart.Img = item.Img.pic_1;
                    cart.Total = item.MRP * quantity;
                    cart.ImgPath = item.Img.path_img;

                    db.ShoppingCarts.Add(cart);
                    db.SaveChanges();
                }
                else
                {

                }
            };
        }

        public void Update_Quantity_Shopping(int id, int quantity, int userId, int itemid)
        {
            var exc = db.ShoppingCarts.Where(c => c.User_id == userId && c.item_id == itemid).FirstOrDefault();
            if (exc != null)
            {
                exc.Quantity += quantity;
                db.SaveChanges();
            }
        }


        public List<ShoppingCart> select_cart()
        {
            var model = db.ShoppingCarts.ToList();
            return model;
        }

        public decimal? Total_money()
        {
            var money = db.ShoppingCarts.Sum(s => s.Quantity * s.Price);
            return (decimal)money;
        }






    }





}