using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Models
{
    public static class DatabaseManagement
    {
        public static List<Employee> Employees = new List<Employee>();
        public static List<List<Employee>> EmployeeSearchResults { get; set; } = new List<List<Employee>>();
        public static Employee SelectedEmployee = new();


        public static List<Company> Companies = new List<Company>();
        public static List<List<Company>> CompanySearchResults { get; set; } = new List<List<Company>>();
        public static Company SelectedCompany = new();

        
        public static void AddEmployeeToDb(Employee employee)
        {
            using (Context context = new Context())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        public static void AddCompanyToDb(Company company, CompanyLogo logo = null)
        {
            using (Context context = new Context())
            {
                context.Companies.Add(company);
                //if (logo != null)
                //{
                //    context.CompanyLogos.Add(logo);
                //}

                context.SaveChanges();
            }
        }

        public static void DeleteEmployeeFromDb(Employee employee)
        {
            using (Context context = new Context())
            {
                Employee employeeToRemove = context.Employees.First(n => n.Id == employee.Id);
                context.Employees.Remove(employeeToRemove);
                context.SaveChanges();
            }
        }

        public static void DeleteCompanyFromDb(Company company)
        {
            using (Context context = new Context())
            {
                Company companyToRemove = context.Companies.First(p => p.Id == company.Id);

                List<Employee> employeesToRemove = context.Employees.Where(p => p.PlaceOfWork.Id == company.Id).ToList();

                if (employeesToRemove.Count() > 0)
                {
                    foreach (Employee employeeToRemove in employeesToRemove)
                    {
                        context.Employees.Remove(employeeToRemove);
                        context.Entry(employeeToRemove).State = EntityState.Deleted;
                        context.SaveChanges();

                    }

                }

                //if (company.HasLogo)
                //{
                //    CompanyLogo logoToRemove = context.CompanyLogos.First(p => p.CompanyAssociated.Id == company.Id);
                //    context.CompanyLogos.Remove(logoToRemove);
                //    context.SaveChanges();
                //}



                context.Companies.Remove(companyToRemove);
                context.Entry(companyToRemove).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public static void UpdateAnEmployee(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
            using (Context context = new Context())
            {
                Employee employeeToUpdate = context.Employees.Find(SelectedEmployee.Id);
                if (firstName != "")
                {
                    employeeToUpdate.FirstName = firstName;
                }
                if (lastName != "")
                {
                    employeeToUpdate.LastName = lastName;
                }
                if (placeOfWork != "none")
                {
                    foreach (Company company in context.Companies)
                    {
                        if (company.Id == Int32.Parse(placeOfWork))
                        {
                            employeeToUpdate.PlaceOfWork = company;
                            break;
                        }
                    }
                }

                if (email != "")
                {
                    employeeToUpdate.Email = email;
                }
                if (phoneNumber != "")
                {
                    employeeToUpdate.PhoneNumber = phoneNumber;
                }

                context.Entry(employeeToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void UpdateACompany(string name, string website, string email, object? logo)
        {
            using (Context context = new Context())
            {
                Company companyToUpdate = context.Companies.Find(SelectedCompany.Id);

                if (name != "")
                {
                    companyToUpdate.Name = name;
                }
                if (website != "")
                {
                    companyToUpdate.Website = website;
                }
                if (email != "")
                {
                    companyToUpdate.Email = email;

                }

                //if (logo != null)
                //{
                //    context.CompanyLogos.Find(oldCompany).Logo = logo;
                //}
                context.Entry(companyToUpdate).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public static void GetRefinedEmployeeSearchResults(string firstName, string lastName, string placeOfWork, string email, string phoneNumber)
        {
            EmployeeSearchResults = new List<List<Employee>>();
            EmployeeSearchResults.Add(new List<Employee>());
            EmployeeSearchResults.Add(new List<Employee>());
            EmployeeSearchResults.Add(new List<Employee>());
            EmployeeSearchResults.Add(new List<Employee>());
            EmployeeSearchResults.Add(new List<Employee>());
            EmployeeSearchResults.Add(new List<Employee>());

            using (Context context = new Context())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (Employee employee in context.Employees)
                {
                    if (firstName != "" && employee.FirstName.ToLower().Contains(firstName.ToLower()))
                    {
                        EmployeeSearchResults[0].Add(employee);
                    }

                    if (lastName != "" && employee.LastName.ToLower().Contains(lastName.ToLower()))
                    {
                        EmployeeSearchResults[1].Add(employee);
                    }

                    if (placeOfWork != "" && employee.PlaceOfWork != null)
                    {
                        if (employee.PlaceOfWork.Name.ToLower().Contains(placeOfWork.ToLower()))
                            EmployeeSearchResults[2].Add(employee);
                    }

                    if (email != "" && employee.Email.ToLower().Contains(email.ToLower()))
                    {
                        EmployeeSearchResults[3].Add(employee);
                    }

                    if (phoneNumber != "" && employee.PhoneNumber.ToLower().Contains(phoneNumber.ToLower()))
                    {
                        EmployeeSearchResults[4].Add(employee);
                    }
                }
            }
            if (firstName != "")
            {
                EmployeeSearchResults[5] = EmployeeSearchResults[0];
            }
            else if (lastName != "")
            {
                EmployeeSearchResults[5] = EmployeeSearchResults[1];
            }
            else if (placeOfWork != "")
            {
                EmployeeSearchResults[5] = EmployeeSearchResults[2];
            }
            else if (email != "")
            {
                EmployeeSearchResults[5] = EmployeeSearchResults[3];
            }
            else if (phoneNumber != "")
            {
                EmployeeSearchResults[5] = EmployeeSearchResults[4];
            }

            for (int i = 1; i < 5; i++)
            {
                if (EmployeeSearchResults[i].Count > 0)
                    EmployeeSearchResults[5] = EmployeeSearchResults[5].Intersect(EmployeeSearchResults[i]).ToList();
            }
        }


        public static void GetRefinedCompanySearchResults(string name, string website, string email)
        {
            CompanySearchResults = new List<List<Company>>();
            CompanySearchResults.Add(new List<Company>());
            CompanySearchResults.Add(new List<Company>());
            CompanySearchResults.Add(new List<Company>());
            CompanySearchResults.Add(new List<Company>());

            using (Context context = new Context())
            {
                foreach (Company company in context.Companies)
                {
                    if (name != "" && company.Name.ToLower().Contains(name.ToLower()))
                    {
                        CompanySearchResults[0].Add(company);
                    }

                    if (website != "" && company.Website.ToLower().Contains(website.ToLower()))
                    {
                        CompanySearchResults[1].Add(company);
                    }

                    if (email != "" && company.Email.ToLower().Contains(email.ToLower()))
                    {
                        CompanySearchResults[2].Add(company);
                    }
                }
            }

            if (name != "")
            {
                CompanySearchResults[3] = CompanySearchResults[0];
            }
            else if (website != "")
            {
                CompanySearchResults[3] = CompanySearchResults[1];
            }
            else if (email != "")
            {
                CompanySearchResults[3] = CompanySearchResults[2];
            }

            for (int i = 1; i < 3; i++)
            {
                if (CompanySearchResults[i].Count > 0)
                    CompanySearchResults[3] = CompanySearchResults[3].Intersect(CompanySearchResults[i]).ToList();
            }
        }
    }
}
