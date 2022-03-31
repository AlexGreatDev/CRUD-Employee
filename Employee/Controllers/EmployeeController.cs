using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Models;
using static Employee.Helper.Razor;
using static Employee.Helper.Access;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;
        // constructor
        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }
        
        // GET: Employee
        public async Task<IActionResult> Index()
        {
            return View( await GetList());
        }
        public IActionResult SearchPage()
        {
            return View();
        }


        // GET: Employee/AddOrEdit(Insert)
        // GET: Employee/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmployeeModel());

            var EmployeeModel = await _context.Employee.FindAsync(id);
            if (EmployeeModel == null)
            {
                return NotFound();
            }
            return View(EmployeeModel);

        }
        // Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchView(EmployeeSearchViewModel searchmodel)
        {
            if (searchmodel == null)
                return Json(new { isValid = false, html = RenderRazorViewToString(this, "SearchView", new EmployeeModel() { }) });

            var EmployeeModel = await _context.Employee.Where(p => p.EmployeeLastName == searchmodel.EmployeeLastName
                                                              || p.EmployeePhone == searchmodel.EmployeePhone).FirstOrDefaultAsync();
            if (EmployeeModel == null)
            {
                return Json(new { isValid = false, html = RenderRazorViewToString(this, "SearchView", new EmployeeModel() { }) });
            }

            return Json(new { isValid = true, html = RenderRazorViewToString(this, "SearchView", EmployeeModel) });


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("EmployeeID,EmployeeFirstName,EmployeeLastName,EmployeePhone,EmployeeZip,HireDate")] EmployeeModel EmployeeModel)
        {
            if (!ModelState.IsValid)
                return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", EmployeeModel) });

            //Insert
            if (id == 0)
            {
                EmployeeModel.Date = DateTime.Now;
                _context.Add(EmployeeModel);
                await _context.SaveChangesAsync();

            }
            //Update
            else
            {
                try
                {
                    _context.Update(EmployeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(EmployeeModel.EmployeeID))
                    { return NotFound(); }
                    else
                    { throw; }
                }
            }
            return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", await GetList()) });


        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EmployeeModel = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (EmployeeModel == null)
            {
                return NotFound();
            }

            return View(EmployeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var EmployeeModel = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(EmployeeModel);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", await GetList()) });
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
        private async Task<List<EmployeeModel>> GetList()
        {
           return  await _context.Employee.OrderBy(p => p.Date).ToListAsync();
        }
    } 
    }

