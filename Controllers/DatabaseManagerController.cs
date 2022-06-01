using DatabaseManager;
using DatabaseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSharpReflection3.Controllers
{
    public class DatabaseManagerController : Controller
    {
        private readonly ILogger<DatabaseManagerController> _logger;
        public static Employee employeeToDelete = new Employee();
        public static Company companyToDelete = new Company();
        public DatabaseManagerController(ILogger<DatabaseManagerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Database Manager";
            return View();
        }

        public IActionResult Company()
        {
            ViewBag.Title = "Company Management";
            return View();
        }

        public IActionResult CompanyAdd()
        {
            ViewBag.Title = "Add Company";
            return View();
        }

        [ActionName("CompanyAdd"), HttpPost]
        public IActionResult CompanyAdd(string name, string email, string website, object? logo)
        {
            Company companyToAdd = new Company()
            {
                Name = ModelState["Name"].AttemptedValue,
                Website = ModelState["Website"].AttemptedValue,
                Email = ModelState["Email"].AttemptedValue
            };

            DatabaseManagement.AddCompanyToDb(companyToAdd);
            return RedirectToAction("Company");
        }

        public IActionResult CompanySearch()
        {
            ViewBag.Title = "Search for Company";
            return View();
        }

        [ActionName("CompanySearch"), HttpPost]
        public IActionResult CompanySearch(string name, string website, string email) //, object? logo logo search broken 
        {
            DatabaseManagement.GetRefinedCompanySearchResults(
                ModelState["Name"].AttemptedValue,
                ModelState["Website"].AttemptedValue,
                ModelState["Email"].AttemptedValue
                );

            return RedirectToAction("CompanySearchResults");
        }

        public IActionResult CompanySearchResults()
        {
            ViewBag.Title = "Company Search Results";
            return View();
        }

        [ActionName("CompanySearchResults"), HttpPost]
        public IActionResult CompanySearchResults(Company company)
        {
            ViewBag.CompanyToView = company;
            return RedirectToAction("CompanyUpdate");
        }

        public IActionResult CompanyUpdate()
        {
            ViewBag.Title = "Update Company";
            return View();
        }

        public IActionResult CompanyView()
        {
            ViewBag.Title = "View Company";
            return View();
        }

        public IActionResult CompanyDelete()
        {
            ViewBag.Title = "Delete Company";
            return View();
        }

        public ActionResult DeleteCompany(string deleteCompany)
        {
            using (Context context = new Context())
            {
                DatabaseManagement.SelectedCompany = context.Companies.Find(Int32.Parse(deleteCompany));
            }
            return RedirectToAction("CompanyDelete");

        }

        public ActionResult ConfirmDeleteCompany()
        {
            using (Context context = new Context())
            {
                DatabaseManagement.DeleteCompanyFromDb(DatabaseManagement.SelectedCompany);
            }
            return RedirectToAction("Company");
        }
        public ActionResult SelectCompany(string updateCompany)
        {
            using (Context context = new Context())
            {
                foreach (Company company in context.Companies)
                {
                    if (company.Id == Int32.Parse(updateCompany))
                    {
                        DatabaseManagement.SelectedCompany = company;
                        break;
                    }
                }
            }
            return RedirectToAction("CompanyView");
        }

        [ActionName("CompanyUpdate"), HttpPost]
        public IActionResult CompanyUpdate(string name, string website, string email)
        {         
            string newName = ModelState["Name"].AttemptedValue;
            string newEmail = ModelState["Email"].AttemptedValue;
            string newWebsite = ModelState["Website"].AttemptedValue;

            DatabaseManagement.UpdateACompany(
                newName,
                newWebsite,
                newEmail,
                null
                );
           
            return RedirectToAction("CompanyView");
        }

        public IActionResult Employee()
        {
            ViewBag.Title = "Employee Management";

            return View();
        }

        public IActionResult EmployeeAdd()
        {
            ViewBag.Title = "Add Employee";

            return View();
        }

        [ActionName("EmployeeAdd"), HttpPost]
        public IActionResult EmployeeAdd(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
            Employee employeeToAdd = new Employee()
            {
                FirstName = ModelState["FirstName"].AttemptedValue,
                LastName = ModelState["LastName"].AttemptedValue,
                PhoneNumber = ModelState["PhoneNumber"].AttemptedValue,
                Email = ModelState["Email"].AttemptedValue
            };

            if (ModelState["PlaceOfWork"].AttemptedValue != "none")
            {
                using(Context context = new Context())
                {
                    employeeToAdd.PlaceOfWork = context.Companies.Find(int.Parse(ModelState["PlaceOfWork"].AttemptedValue));
                }
            }
            
            DatabaseManagement.AddEmployeeToDb(employeeToAdd);
            return RedirectToAction("Employee");
        }

        public IActionResult EmployeeSearch()
        {
            ViewBag.Title = "Search for Employee";
            return View();
        }

        [ActionName("EmployeeSearch"), HttpPost]
        public IActionResult EmployeeSearch(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
           
            DatabaseManagement.GetRefinedEmployeeSearchResults(
                ModelState["FirstName"].AttemptedValue, 
                ModelState["LastName"].AttemptedValue,  
                ModelState["PlaceOfWork"].AttemptedValue,
                ModelState["PhoneNumber"].AttemptedValue, 
                ModelState["Email"].AttemptedValue
            );

            return RedirectToAction("EmployeeSearchResults");
        }

        public IActionResult EmployeeSearchResults()
        {
            ViewBag.Title = "Employee Search Results";
            return View();
        }

        [ActionName("EmployeeSearchResults"), HttpPost]
        public IActionResult EmployeeSearchResults(Employee employee)
        {
            ViewBag.EmployeeToView = employee;
            return RedirectToAction("EmployeeUpdate");
        }

        public IActionResult EmployeeUpdate()
        {
            ViewBag.Title = "Update Employee";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult EmployeeView()
        {
            ViewBag.Title = "View Employee";
            return View();
        }

        public IActionResult EmployeeDelete()
        {
            ViewBag.Title = "Delete Employee";
            return View();
        }

        public ActionResult DeleteEmployee(string deleteEmployee)
        {
            using (Context context = new Context())
            {
                DatabaseManagement.SelectedEmployee = context.Employees.Find(Int32.Parse(deleteEmployee));
            }
            return RedirectToAction("EmployeeDelete");
        }

        public ActionResult ConfirmDeleteEmployee()
        {
            using (Context context = new Context())
            {
                DatabaseManagement.DeleteEmployeeFromDb(DatabaseManagement.SelectedEmployee);
            }
            return RedirectToAction("Employee");
        }

        public ActionResult SelectEmployee(string updateEmployee)
        {
            using (Context context = new Context())
            {
                foreach(Employee employee in context.Employees)
                {
                    if(employee.Id == Int32.Parse(updateEmployee))
                    {
                        DatabaseManagement.SelectedEmployee = employee;
                        break;
                    }
                }
                if (DatabaseManagement.SelectedEmployee.PlaceOfWork != null)
                {
                    DatabaseManagement.SelectedCompany = DatabaseManagement.SelectedEmployee.PlaceOfWork;
                }
            }
            
            return RedirectToAction("EmployeeView");
        }

        [ActionName("EmployeeUpdate"), HttpPost]
        public IActionResult EmployeeUpdate(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
            string newFname = ModelState["FirstName"].AttemptedValue;
            string newLname = ModelState["LastName"].AttemptedValue;
            string newPow = ModelState["PlaceOfWork"].AttemptedValue;
            string newNum = ModelState["PhoneNumber"].AttemptedValue;
            string newEmail = ModelState["Email"].AttemptedValue;

            DatabaseManagement.UpdateAnEmployee(
                newFname,
                newLname,
                newPow,
                newEmail,
                newNum
                );

            return RedirectToAction("EmployeeView");
        }
    }
}