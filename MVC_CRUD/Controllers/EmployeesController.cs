using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using MVC_CRUD.Models.Domain;

namespace MVC_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDBContext MvcDemoDbContext;

        public EmployeesController(MVCDemoDBContext mvcDemoDbContext)
        {
            MvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee=await MvcDemoDbContext.Employees.ToListAsync();
            return View(employee);
        }

        [HttpGet] 

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            if(!ModelState.IsValid)
            {
                return View( addEmployeeRequest);
            }
            var employee = new Employee()
            {
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };
            await MvcDemoDbContext.Employees.AddAsync(employee);
            await MvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id) 
        { 
            var employee=await MvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee!=null)
            {
                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                };
                return View("View", viewmodel);
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await MvcDemoDbContext.Employees.FindAsync(model.Id);
                if (employee != null)
                {
                    employee.Name = model.Name;
                    employee.Email = model.Email;
                    employee.DateOfBirth = model.DateOfBirth;
                    employee.Salary = model.Salary;
                    employee.Department = model.Department;
                    await MvcDemoDbContext.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                
                return RedirectToAction("Index"); 
            }
            else 
            {                return View("View",model); 
 }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await MvcDemoDbContext.Employees.FindAsync(model.Id);  
            if (employee!=null)
            {
                MvcDemoDbContext.Employees.Remove(employee);
                await MvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
