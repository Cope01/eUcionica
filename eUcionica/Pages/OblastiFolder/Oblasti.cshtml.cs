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
    public class IndexModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public IndexModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Oblasti> Oblasti { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Oblasti != null)
            {
                Oblasti = await _context.Oblasti.ToListAsync();
            }
        }
    }
}
