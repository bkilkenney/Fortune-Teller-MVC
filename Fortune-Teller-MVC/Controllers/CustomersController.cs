using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fortune_Teller_MVC.Models;

namespace Fortune_Teller_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //Add fortune teller logic below:

            //1st if

            if (customer.Age % 2 == 0)
            {
                Console.WriteLine(fullName + ", you will retire in 63 years");
            }

            else if (customer.Age % 2 != 0)
            {
                Console.WriteLine(fullName + ", you will retire in 3 years");
            }
            else
            {
                Console.WriteLine("Invalid Answer");
            }

//2nd if

            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                Console.WriteLine("with $200,000 in the bank");
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                Console.WriteLine("with $6M in the bank");
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                Console.WriteLine("with $35.78 in the bank");
            }
            else if (customer.BirthMonth < 1 || customer.BirthMonth > 12)
            {
                Console.WriteLine(", and there won't be any money, but when you die, on your deathbed,\n you will receive total consciousness.  Now go");
            }
            else
            {
                Console.WriteLine("Invalid Answer");
            }


      //3rd if


            if ( customer.NumberOfSiblings == 0)
            {
                Console.WriteLine("to your vacation home in an igloo");
            }
            else if (customer.NumberOfSiblings == 1)
            {
                Console.WriteLine("to your vacation home in Schenectady");
            }
            else if (customer.NumberOfSiblings == 2)
            {
                Console.WriteLine("to your vacation home in Cleveland Hopkins International Airport Terminal C");
            }
            else if (customer.NumberOfSiblings == 3)
            {
                Console.WriteLine("to your vacation home in Walla Walla Washington");
            }
            else if (customer.NumberOfSiblings <= 0 || customer.NumberOfSiblings >= 4)
            {
                Console.WriteLine("to your vacation home at the BMV");
            }


            //Last of the ifs. It is taking the char answer and parsing it to a int behind the scenes so ==, > operators will work.  But what about the help option?  


            if (customer.FavoriteColor == "Red")
            {
                Console.WriteLine("flying a dirigible.");
            }
            else if (customer.FavoriteColor == "Orange")
            {
                Console.WriteLine("captaining the black pearl.");
            }
            else if (customer.FavoriteColor == "Yellow")
            {
                Console.WriteLine("cruisin' on a vespa.");
            }
            else if (customer.FavoriteColor == "Green")
            {
                Console.WriteLine("inside a trojan horse.");
            }
            else if (customer.FavoriteColor == "Blue")
            {
                Console.WriteLine("driving the batmobile, the crappy original one from the 70s,\nnot the Christian Bale Dark Knight batmobile.");
            }
            else if (customer.FavoriteColor == "Indigo")
            {
                Console.WriteLine("riding a goat.");
            }
            else if (customer.FavoriteColor == "Violet")
            {
                Console.WriteLine("driving a power wheel.");
            }
           
        }

            return View(customer);
        }



        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,customer.Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);                 //Add customer to the db
                db.SaveChanges();                           //Save new altered db
                Fortune fortune = new Fortune();            //Create an object of a Fortune
                fortune.CustomerID = customer.CustomerID;   //Link a customer to a fortune
                return RedirectToAction("Index");           //Return to home
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
