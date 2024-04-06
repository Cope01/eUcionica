using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.PredmetiFolder
{
    public class DeleteModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DeleteModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
      public Predmeti Predmeti { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Predmeti == null)
            {
                return NotFound();
            }

            var predmeti = await _context.Predmeti.FirstOrDefaultAsync(m => m.ID == id);

            if (predmeti == null)
            {
                return NotFound();
            }
            else 
            {
                Predmeti = predmeti;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Predmeti == null)
            {
                return NotFound();
            }
            var predmeti = await _context.Predmeti.FindAsync(id);

            if (predmeti != null)
            {
                Predmeti = predmeti;
                _context.Predmeti.Remove(Predmeti);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Predmeti");
        }
    }
}
