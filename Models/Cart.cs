using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Jewelly.Models
{
    public class cart_item
    {
        public ItemMst ItemMst { get; set; }
        public decimal? quantity { get; set; }
    }

    public class Cart
    {
        List<cart_item> items = new List<cart_item>();
        public IEnumerable<cart_item> Items
        {
            get { return items; }
        }

        public void Add(ItemMst itemMst, int ql = 1)
        {
            var item = items.FirstOrDefault(x => x.ItemMst.Style_Code == itemMst.Style_Code && x.ItemMst.Quantity >= ql);
            if (item == null)
            {
                items.Add(item = new cart_item
                {
                    ItemMst = itemMst,
                    quantity = ql,
                });
            }
            else
            {
                item.quantity += ql;
            }
        }

        public void Update_Quantity_Shopping(int id, int quantity)
        {
            var item = items.Find(s => s.ItemMst.Style_Code == id);
            if (item != null)
            {
                item.quantity = quantity;
            }
        }

        public List<ItemMst> select_cart()
        {
            var model = db.ItemMsts.ToList();
            //var name= model.FirstOrDefault().Name;
            //var price = db.Ticket.FirstOrDefault(s => s.Name == name);
            //if (price != null)
            //{
            // price.Price = model.FirstOrDefault().Price;
            //}
            return model;
        }

        public List<ItemMst> select_price(int id, decimal Price)
        {
            var price = db.ItemMsts.Where(x => x.Style_Code == id).FirstOrDefault();
            if (price != null)
            {
                price.MRP = Price;
            }
            return select_price(id, Price);
        }
        JwelleyEntities db = new JwelleyEntities();
        public decimal? Total_money()
        {
            var money = items.Sum(s => s.quantity * s.ItemMst.MRP);
            return (decimal)money;
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s.ItemMst.Style_Code == id);
        }

        public int Total_Quantity_in_Cart()
        {
            return (int)items.Sum(s => s.quantity);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}