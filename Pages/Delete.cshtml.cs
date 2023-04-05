using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_RazorPage.Models;

namespace ASP.NET_RazorPage.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly TestDbContext _context;

        public DeleteModel(TestDbContext context)
        {
            _context = context;
        }

        public Employee? Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                Employee = employee;
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/EFCore");
        }
    }
}
