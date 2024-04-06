using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace eUcionica.Pages.ZadaciFolder
{
    public class CreateModel : PageModel
    {
        private readonly DB_Context_Class _context;

        private IWebHostEnvironment _environment;

        public CreateModel(DB_Context_Class context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult OnGet()
        {
            ViewData["IDPredmet"] = new SelectList(_context.Predmeti, "ID", "Name");
            ViewData["IDOblast"] = new SelectList(_context.Oblasti, "ID", "Name");
            var uniqueNivoValues = _context.Zadaci.Select(z => z.Nivo).Distinct().ToList();
            ViewData["Nivo"] = new SelectList(uniqueNivoValues);
            return Page();
        }

        [BindProperty]
        public Zadaci Zadaci { get; set; } = default!;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Zadaci == null || Zadaci == null)
            {
                return Page();
            }
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\slike", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);

                Zadaci.Slika = Upload.FileName;
            }

            _context.Zadaci.Add(Zadaci);
            await _context.SaveChangesAsync();

            return RedirectToPage("./zadatak");
        }
    }
}
