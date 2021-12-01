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
            ViewBag.DatabaseType = "Company";
            return View();
        }

        public IActionResult CompanyAdd()
        {
            ViewBag.Title = "Add Company";
            ViewBag.DatabaseType = "Company";
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
            //if (logo != null)
            //{
            //    companyToAdd.HasLogo = true;
            //    CompanyLogo logoToAdd = new CompanyLogo()
            //    {
            //        Logo = logo,
            //        CompanyAssociated = companyToAdd
            //    };
                
            //    DatabaseManagement.AddCompanyToDb(companyToAdd, logoToAdd);
            //} 
            //else 
            //{
            //    DatabaseManagement.AddCompanyToDb(companyToAdd);
            //}
            DatabaseManagement.AddCompanyToDb(companyToAdd);
            return RedirectToAction("Company");
        }

        public IActionResult CompanySearch()
        {
            ViewBag.Title = "Search for Company";
            ViewBag.DatabaseType = "Company";
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
            //using (Context context = new Context())
            //{
            //    context.Companies.Add(new Company()
            //    {
            //        Name = ModelState["Name"].AttemptedValue,
            //        Website = ModelState["Website"].AttemptedValue,
            //        Email = ModelState["Email"].AttemptedValue
            //    });
            //    context.SaveChanges();
            //}

            return RedirectToAction("CompanySearchResults");
        }

        public IActionResult CompanySearchResults()
        {
            ViewBag.Title = "Company Search Results";
            ViewBag.DatabaseType = "Company";
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
            ViewBag.DatabaseType = "Company";
            return View();
        }

        [ActionName("CompanyUpdate"), HttpPost]
        public IActionResult CompanyUpdate(string name, string website, string email, object? logo)
        {
            DatabaseManagement.UpdateACompany(
                ModelState["Name"].AttemptedValue,
                ModelState["Website"].AttemptedValue,
                ModelState["Email"].AttemptedValue,
                null
                );
            return RedirectToAction("Company");
        }
        public IActionResult Employee()
        {
            ViewBag.Title = "Employee Management";
            ViewBag.DatabaseType = "Employee";

            return View();
        }

        
        public IActionResult EmployeeAdd()
        {
            ViewBag.Title = "Add Employee";
            ViewBag.DatabaseType = "Employee";

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

            if (ModelState["PlaceOfWork"].AttemptedValue != "")
            {
                using(Context context = new Context())
                {
                    if(context.Companies.Find(ModelState["PlaceOfWork"].AttemptedValue) != null)
                        employeeToAdd.PlaceOfWork = context.Companies.Find(ModelState["PlaceOfWork"].AttemptedValue);
                }
                
            }
            else
            {
                employeeToAdd.PlaceOfWork = null;
            }

            DatabaseManagement.AddEmployeeToDb(employeeToAdd);
            return RedirectToAction("Employee");
        }

        public IActionResult EmployeeSearch()
        {
            ViewBag.Title = "Search for Employee";
            ViewBag.DatabaseType = "Employee";
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
            ViewBag.DatabaseType = "Employee";
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
            ViewBag.DatabaseType = "Employee";
            return View();
        }

        [ActionName("EmployeeUpdate"), HttpPost]
        public IActionResult EmployeeUpdate(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
            DatabaseManagement.UpdateAnEmployee(
                ModelState["FirstName"].AttemptedValue,
                ModelState["LastName"].AttemptedValue,
                ModelState["PlaceOfWork"].AttemptedValue,
                ModelState["PhoneNumber"].AttemptedValue,
                ModelState["Email"].AttemptedValue
                );
            return RedirectToAction("Employee");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult DeleteEmployee(string deleteEmployee)
        {
            DatabaseManagement.DeleteEmployeeFromDb(DatabaseManagement.EmployeeSearchResults[5][Int32.Parse(deleteEmployee)]);
            return RedirectToAction("Employee");
        }

        public ActionResult DeleteCompany(string deleteCompany)
        {
            DatabaseManagement.DeleteCompanyFromDb(DatabaseManagement.CompanySearchResults[3][Int32.Parse(deleteCompany)]);
            return RedirectToAction("Company");
        }

        public ActionResult UpdateCompany(string updateCompany)
        {
            DatabaseManagement.SelectedCompany = DatabaseManagement.CompanySearchResults[3][Int32.Parse(updateCompany)];
            return RedirectToAction("CompanyUpdate");
        }

        public ActionResult UpdateEmployee(string updateEmployee)
        {
            DatabaseManagement.SelectedEmployee = DatabaseManagement.EmployeeSearchResults[5][Int32.Parse(updateEmployee)];
            if(DatabaseManagement.SelectedEmployee.PlaceOfWork != null)
                DatabaseManagement.SelectedCompany = DatabaseManagement.EmployeeSearchResults[5][Int32.Parse(updateEmployee)].PlaceOfWork;
            return RedirectToAction("EmployeeUpdate");
        }
    }
}