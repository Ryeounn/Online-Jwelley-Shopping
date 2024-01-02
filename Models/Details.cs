using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jewelly.Models
{
    public class Details
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }
        public string Img2 { get; set; }

        public string Img3 { get; set; }

        public string Img4 { get; set; }

        public string Img5 { get; set; }


        public string Path { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

        public string DimCrt { get; set; }

        public string DimTPye { get; set; }

        public string Cat { get; set; }

        public string GoldType { get; set; }

        public string Jewelype { get; set; }

        public string Certify { get; set; }

        public string Prod { get; set; }

        public decimal Stone { get; set; }

        public decimal Gold_Wt { get; set; }

        public decimal Net_Gold { get; set; }

        public decimal Gold_Rate { get; set; }

        public decimal Gold_Amt { get; set; }

        public decimal Product_Weight { get; set; }



    }
    public class JoinDetails
    {
        public JwelleyEntities db = new JwelleyEntities();

        public List<Details> SelectDetails(int id)
        {
            var details = (from i in db.ItemMsts
                           let p = i.ProdMst
                           let m = i.BrandMst
                           let t = i.CatMst
                           let r = i.GoldKrtMst
                           let d = i.DimInfoMst
                           let j = i.JewelTypeMst
                           let y = i.Img
                           let c = i.CertifyMst
                           let s = i.Stone_Wt
                           where i.Style_Code == id
                           select new Details()

                           //join p in db.ProdMsts on i.Prod_ID equals p.Prod_ID
                           //join m in db.BrandMsts on i.Brand_ID equals m.Brand_ID
                           //join t in db.CatMsts on i.Cat_ID equals t.Cat_ID
                           //join r in db.GoldKrtMsts on i.GoldType_ID equals r.GoldType_ID
                           //join d in db.DimInfoMsts on i.DimID equals d.DimID
                           //join j in db.JewelTypeMsts on i.ID_jewelly equals j.ID
                           //join y in db.Imgs on i.Img_ID equals y.ID
                           //join c in db.CertifyMsts on i.Certify_ID equals c.Certify_ID
                           //where i.Prod_ID == p.Prod_ID && i.Brand_ID == m.Brand_ID && i.Cat_ID == t.Cat_ID && i.GoldType_ID == r.GoldType_ID && d.DimID == i.DimID && i.ID_jewelly == j.ID && y.ID == i.Img_ID && c.Certify_ID == i.Certify_ID && i.Style_Code == id
                           //select new Details()
                           {
                               Name = p.Prod_Type,
                               Brand = m.Brand_Type,
                               Cat = t.Cat_Name,
                               GoldType = r.Gold_Crt,
                               DimCrt = d.DimCrt,
                               DimTPye = d.DimType,
                               Jewelype = j.Jewellery_Type,
                               Certify = c.Certify_Type,
                               Img = y.pic_1,
                               Img2 = y.pic_2,
                               Img3 = y.pic_3,
                               Img4 = y.pic_4,
                               Img5 = y.pic_5,
                               Path = y.path_img,
                               Prod = i.Prod_Quality,
                               Price = i.MRP,
                               Stone = i.Stone_Wt,
                               Gold_Wt = i.Gold_Wt,
                               Net_Gold = i.Net_Gold,
                               Gold_Rate = i.Gold_Rate,
                               Gold_Amt = i.Gold_Amt,
                               Id = i.Style_Code,
                               Product_Weight = i.Tot_Gross_Wt,

                           }).ToList();
            return details;
        }
    }
}