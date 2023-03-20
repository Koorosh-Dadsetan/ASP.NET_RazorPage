using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASP.NET_RazorPage.Models;

namespace ASP.NET_RazorPage.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TestDbContext _context;

        public CreateModel(TestDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee? Employee { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./EFCore");
        }
    }
}
