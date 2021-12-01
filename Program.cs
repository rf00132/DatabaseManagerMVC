using DatabaseManager;
using DatabaseManager.Models;

class Program
{
    static void Main(string[] args)
   {

        //DatabaseSeeder();
      
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=DatabaseManager}/{action=Index}/{id?}");

        app.Run();  
    }

    static void DatabaseSeeder()
    {
        using (var context = new Context())
        {
            Company mocrisaft = new Company()
            {
                Name = "Mocrisaft",
                Email = "Mocrisaft@Mocrisaft.com",
                Website = "Mocrisaft.com"
            };

            Company peebler = new Company()
            {
                Name = "Peebler",
                Email = "Peebler@Peebler.com",
                Website = "Peebler.com"
            };

            Company fld = new Company()
            {
                Name = "F.L.D. Inc.",
                Email = "FLD@FLD.com",
                Website = "FLD.com"
            };

            context.Companies.Add(mocrisaft);
            context.Companies.Add(peebler);
            context.Companies.Add(fld);

            //CompanyLogo mocLogo = new CompanyLogo()
            //{
            //    Logo = "yes",
            //    CompanyAssociated = mocrisaft
            //};

            //CompanyLogo peebLogo = new CompanyLogo()
            //{
            //    Logo = "no",
            //    CompanyAssociated = peebler
            //};

            //CompanyLogo fldLogo = new CompanyLogo()
            //{
            //    Logo = "maybe",
            //    CompanyAssociated = fld
            //};

            //context.CompanyLogos.Add(mocLogo);
            //context.CompanyLogos.Add(peebLogo);
            //context.CompanyLogos.Add(fldLogo);

            context.Employees.Add(new Employee()
            {
                FirstName = "John",
                LastName = "Smith",
                PlaceOfWork = mocrisaft,
                Email = "JSmith@Mocrisaft.com",
                PhoneNumber = "01234543405"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Barry",
                LastName = "Blogs",
                PlaceOfWork = mocrisaft,
                Email = "BBlogs@Mocrisaft.com",
                PhoneNumber = "01234543406"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Greg",
                LastName = "Grunder",
                PlaceOfWork = mocrisaft,
                Email = "GGrunder@Mocrisaft.com",
                PhoneNumber = "01234543407"
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Harry",
                LastName = "Huntington",
                PlaceOfWork = peebler,
                Email = "HHunt@Peebler.com",
                PhoneNumber = "01234543408"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Joe",
                LastName = "Jender",
                PlaceOfWork = peebler,
                Email = "JJender@Peebler.com",
                PhoneNumber = "01234543409"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Sam",
                LastName = "Sand",
                PlaceOfWork = peebler,
                Email = "SSand@Peebler.com",
                PhoneNumber = "01234543410"
            });

            context.Employees.Add(new Employee()
            {
                FirstName = "Hugh",
                LastName = "Henry",
                PlaceOfWork = fld,
                Email = "HHenry@fld.com",
                PhoneNumber = "01234543411"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Kate",
                LastName = "Kimble",
                PlaceOfWork = fld,
                Email = "KKimble@fld.com",
                PhoneNumber = "01234543412"
            });
            context.Employees.Add(new Employee()
            {
                FirstName = "Dom",
                LastName = "Dunder",
                PlaceOfWork = fld,
                Email = "DDunder@fld.com",
                PhoneNumber = "01234543413"
            });
            context.SaveChanges();

        }
    }
}