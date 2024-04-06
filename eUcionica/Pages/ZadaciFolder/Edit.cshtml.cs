using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;

namespace eUcionica.Pages.ZadaciFolder
{
    public class EditModel : PageModel
    {
        private readonly DB_Context_Class _context;
        private IWebHostEnvironment _environment;
        public EditModel(DB_Context_Class context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [BindProperty]
        public Zadaci Zadaci { get; set; } = default!;
        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Zadaci == null)
            {
                return NotFound();
            }
            var zadaci = await _context.Zadaci.FirstOrDefaultAsync(m => m.ID == id);
            if (zadaci == null)
            {
                return NotFound();
            }
            Zadaci = zadaci;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (_context.Zadaci == null || Zadaci == null)
                {
                    return Page();
                }
                if (Upload != null && Upload.Length > 0)
                {
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\slike", Upload.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);

                        Zadaci.Slika = Upload.FileName;
                    }
                }
                _context.Update(Zadaci);
                await _context.SaveChangesAsync();

                return RedirectToPage("./zadatak");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom čuvanja podataka: {ex.Message}");
                return NotFound();
            }
        }
        private bool ZadaciExists(int id)
        {
            return (_context.Zadaci?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
