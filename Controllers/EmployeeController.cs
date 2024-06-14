using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechNeuronstask.ApplicationContext;
using TechNeuronstask.Models;

namespace TechNeuronstask.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly ApplicationDbContext _Context;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            _Context = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var emp= await _Context.Employees.ToListAsync();
                    return View(emp);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //To Add Employee details 
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRequest request) 
        {
            try
            {
                if (_Context.Employees.Any(e => e.Id == request.Id))
                {
                    ModelState.AddModelError("Id", "This ID is already in use. Please choose another.");
                    return View(request);
                }
                var employee = new Employee()
                {
                    Name = request.Name,
                    Id = request.Id,
                    Description = request.Description
                };
                _Context.Employees.Add(employee);
                await _Context.SaveChangesAsync();
                //return View("Index");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        //to redirect employee based on id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var emp=await _Context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if(emp==null)
                {
                    return NotFound();
                }
                var employe = new Employee
                {
                    Name = emp.Name,
                    Id = emp.Id,
                    Description = emp.Description
                };
                return await Task.Run(() => View("Edit",employe));
                return View(employe);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        //to edit employee details
        [HttpPost]
        public async Task<IActionResult> Edit(Employee request)
        {
            try
            {
                var emp=await _Context.Employees.FirstOrDefaultAsync(x=>x.Id==request.Id);
                if(emp!=null)
                {
                    emp.Name = request.Name;
                    emp.Id = request.Id;
                    emp.Description = request.Description;

                    await _Context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
                //return View("Index");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        //To Delete the employee details
        [HttpPost]
        public async Task<IActionResult> Delete(Employee model)
        {
            var emp = await _Context.Employees.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (emp != null)
            {
                _Context.Employees.Remove(emp);
                await _Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
