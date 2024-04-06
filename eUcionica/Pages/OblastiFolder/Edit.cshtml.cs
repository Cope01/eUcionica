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

namespace eUcionica.Pages.OblastiFolder
{
    public class EditModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public EditModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        [BindProperty]
        public Oblasti Oblasti { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Oblasti == null)
            {
                return NotFound();
            }

            var oblasti =  await _context.Oblasti.FirstOrDefaultAsync(m => m.ID == id);
            if (oblasti == null)
            {
                return NotFound();
            }
            Oblasti = oblasti;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Oblasti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OblastiExists(Oblasti.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Oblasti");
        }

        private bool OblastiExists(int id)
        {
          return (_context.Oblasti?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
