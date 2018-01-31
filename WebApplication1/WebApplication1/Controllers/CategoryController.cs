using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();

        public ActionResult Index()
        {
            ViewBag.AllCategories = db.Categories.ToList();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                ViewBag.activeCategory = db.Categories.FirstOrDefault(ct => ct.id == id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int? id, string CategoryName)
        {
            if (CategoryName != null)
            {
                Category selectedCategory = db.Categories.FirstOrDefault(ct => ct.id == id);
                if(selectedCategory != null)
                {
                    Category exist = db.Categories.FirstOrDefault(ct => ct.Name == CategoryName);
                    if(exist == null)
                    {
                        selectedCategory.Name = CategoryName;
                        db.SaveChanges();
                        return RedirectToAction("Index", new { id = id });
                    }
                    else
                    {
                        ViewBag.error = "bele categoriya var.";

                    }
                }
                else
                {
                    ViewBag.error = "There is no category with this Id.";
                }

            }
            else
            {
                ViewBag.error = "please fill all the fields";
            }

            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCategory(int? id, string CategoryName)
        {
            if(CategoryName != string.Empty)
            {
                Category exist = db.Categories.FirstOrDefault(ct => ct.Name == CategoryName);
                if (exist == null)
                {
                    Category ct = new Category();
                    ct.Name = CategoryName;

                    db.Categories.Add(ct);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "bele bir cat var.";

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
                Category selectedct = db.Categories.FirstOrDefault(ct => ct.id == id);
                if(selectedct != null)
                {
                    db.Categories.Remove(selectedct);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", new { id = id });
        }
    }
}