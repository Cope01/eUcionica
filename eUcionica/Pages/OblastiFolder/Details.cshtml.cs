using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.OblastiFolder
{
    public class DetailsModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public DetailsModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

      public Oblasti Oblasti { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Oblasti == null)
            {
                return NotFound();
            }

            var oblasti = await _context.Oblasti.FirstOrDefaultAsync(m => m.ID == id);
            if (oblasti == null)
            {
                return NotFound();
            }
            else 
            {
                Oblasti = oblasti;
            }
            return Page();
        }
    }
}
