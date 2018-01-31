using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {

        OnlineShopEntities db = new OnlineShopEntities();
        public ActionResult Index()
        {
            ViewBag.AllCustomers = db.Customers.ToList();
            return View();
        }

        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                ViewBag.ActiveCustomer = db.Customers.FirstOrDefault(cus => cus.id == id);
                ViewBag.Orders = db.Orders.Where(or => or.Customer_id == id).ToList();
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {
            if(id != null)
            {
                ViewBag.ActiveCustomer = db.Customers.FirstOrDefault(cus => cus.id == id);
            }
            return View();
        }

        [HttpPost]

        public ActionResult Edit(int? id, string Firstname, string Lastname, string Email, string Password, string RepeatPassword)
        {
            if(id != null && Firstname != string.Empty && Lastname != string.Empty && Email != string.Empty && Password != string.Empty)
            {
                if(RepeatPassword == Password)
                {
                    Customer existCus = db.Customers.FirstOrDefault(cus => cus.Email == Email);
                    if (existCus == null)
                    {
                        Customer customer = db.Customers.FirstOrDefault(cus => cus.id == id);
                        if (customer != null)
                        {

                            customer.Firstname = Firstname;
                            customer.Lastname = Lastname;
                            customer.Email = Email;
                            customer.Password = Password;

                            db.SaveChanges();

                            return RedirectToAction("Index", new { id = id });
                        }
                        else
                        {
                            ViewBag.Error = "bele bir customer yoxdur.";
                        }
                    }
                    else
                    {
                        ViewBag.Error = "bele bir email movcuddur.";
                    }

                }
                else
                {
                    ViewBag.Error = "Password and Repeat Password doesn't match.";
                }
            
            }else
            {
                ViewBag.Error = "please fill all the fields.";

            }
            return View();
        }

        public ActionResult AddCustomer()
        {
            return View();
        }


        [HttpPost]

        public ActionResult AddCustomer(string Firstname, string Lastname, string Email, string Password, string RepeatPassword, string Image)
        {
            if(Firstname != string.Empty && Lastname != string.Empty && Email != string.Empty 
                && Password != string.Empty && Image != string.Empty && RepeatPassword != string.Empty)
            {
                if(Password == RepeatPassword)
                {
                    Customer existCus = db.Customers.FirstOrDefault(cus => cus.Email == Email);
                    if (existCus == null)
                    {
                        Customer customer = new Customer();
                        customer.Firstname = Firstname;
                        customer.Lastname = Lastname;
                        customer.Email = Email;
                        customer.Password = Password;
                        customer.Photo = Image;

                        db.Customers.Add(customer);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Bele bir Customer artiq movcuddur.";
                    }
                }else
                {
                    ViewBag.Error = "password ve repeat password uygun deyil.";

                }
            }
            else
            {
                ViewBag.Error = "please fill all the fields.";
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                Customer selectedCus = db.Customers.FirstOrDefault(cus => cus.id == id);
                if(selectedCus != null)
                {
                    db.Customers.Remove(selectedCus);
                    db.SaveChanges();


                }

            }
            return RedirectToAction("Index");


        }
    }
}