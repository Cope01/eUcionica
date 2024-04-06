using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.ZadaciFolder
{
    public class DetailsModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public DetailsModel(DB_Context_Class context)
        {
            _context = context;
        }

        public Zadaci Zadaci { get; set; } = default!;

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
            else
            {
                Zadaci = zadaci;
            }
            return Page();
        }


    }
}
