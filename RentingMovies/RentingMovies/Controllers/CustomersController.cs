using System;
using System.Collections.Generic;
using System.Data.Entity; //to include Include(c => c.MembershipType)
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentingMovies.Models;
using RentingMovies.ViewModels;

namespace RentingMovies.Controllers
{
    public class CustomersController : Controller
        //First, we need DbContext to access our db.
        //So declare private field
    {
        private ApplicationDbContext _context; // From Indentity models, defined ApplicationDbContext
                                               //its a private field.

        public CustomersController() //Initialize _context in constructor
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
            //we can pass membershipTypes in return but in future, we CANNOT edit customer details so 
            //we should use ViewModel concept
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {

            if(customer.Id == 0)


            _context.Customers.Add(customer); //It will just be in Memory not in Database.
                                              //For that call SaveChanges

            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.DateOfBirth = customer.DateOfBirth;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges(); //At this point dbcontext will go to all modify objects and based on this it will generate the SQL statements and 
                                    //do the require modifications in DB.
            return RedirectToAction("Index", "Customers");
        }

        protected override void Dispose(bool disposing) // dbcontext is disposable context
        {
            _context.Dispose();
        }


        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); //Customers come from DSset:  public DbSet<Customer> Customers { get; set; }
                                                        //Get all customers from Db.
                                                        //ToList() will immediately led to execute SQL query.
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);

        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel); //In this case, we render the view i.e. New.cshtml and
                                //It has to be done in "". Otherwise it will look for the view called EDIT
        }

        //here Customer is name of class in Model
       // private IEnumerable<Customer> GetCustomers() 
        //{
          //  return new List<Customer>
            //{
              //  new Customer {Id = 1, Name = "John"},
                //new Customer {Id = 2, Name = "Marg"}
            //};
        //}
    }
}