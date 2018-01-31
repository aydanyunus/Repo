using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();

        public ActionResult Index()
        {
            ViewBag.AllProducts = db.Products.ToList();
            return View();
        }
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                ViewBag.Products = db.Products.FirstOrDefault(pro => pro.id == id);
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.AllCategories = db.Categories.ToList();
            if(id != null)
            {
                ViewBag.ActiveProduct = db.Products.FirstOrDefault(pro => pro.id == id);
            }
            return View();
        }

        [HttpPost]

        public ActionResult Edit(int? id, string Name, string Price, int Category_Id)
        {
            decimal pricedecimal;
            if (Name != string.Empty && Price != null)
            {
                if(decimal.TryParse(Price, out pricedecimal))
                {
                    Product existpro = db.Products.FirstOrDefault(pro => pro.Name == Name);
                    if(existpro == null)
                    {
                        Product selectedpro = db.Products.FirstOrDefault(pro => pro.id == id);
                        if (selectedpro != null)
                        {
                            selectedpro.Name = Name;
                            selectedpro.Price = pricedecimal;
                            selectedpro.Category_id = Category_Id;

                            db.SaveChanges();
                            return RedirectToAction("Index", new { id = id });
                        }
                        else
                        {
                            ViewBag.error = "There is no product with this Id.";
                        }
                    }
                    else
                    {
                        ViewBag.error = "bele bir product adi var.";

                    }

                }
                else
                {
                    ViewBag.error = "price yalniz reqem ola biler";
                }
               
            }
            else
            {
                ViewBag.error = "please fill all the fields";
            }
            if (id != null)
            {
                ViewBag.AllCategories = db.Categories.ToList();
                ViewBag.ActiveProduct = db.Products.FirstOrDefault(pro => pro.id == id);
            }
            return View();

        }


       
        public ActionResult AddProduct()
        {
            ViewBag.AllCategories = db.Categories.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(string Name, string Price, int Category_Id)
        {
            decimal pricedecimal;

            ViewBag.AllCategories = db.Categories.ToList();
            if(Name != string.Empty && Price != string.Empty)
            {
                if(decimal.TryParse(Price, out pricedecimal))
                {
                    Product existpro = db.Products.FirstOrDefault(pro => pro.Name == Name);
                    if (existpro == null)
                    {
                        Product pro = new Product();
                        pro.Name = Name;
                        pro.Price = pricedecimal;
                        pro.Category_id = Category_Id;

                        db.Products.Add(pro);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "bele bir product artiq var.";

                    }
                }
                else
                {
                    ViewBag.error = "price yalniz reqem olmalidir.";

                }
            }
            else
            {
                ViewBag.error = "please fill all the fields.";
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                Product selectedpro = db.Products.FirstOrDefault(pro => pro.id == id);
                if(selectedpro != null)
                {
                    db.Products.Remove(selectedpro);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { id =id});

        }
    }
}