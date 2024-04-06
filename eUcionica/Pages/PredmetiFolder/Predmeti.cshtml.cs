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
    public class IndexModel : PageModel
    {
        private readonly DataBaseContext.DB_Context_Class _context;

        public IndexModel(DataBaseContext.DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Predmeti> Predmeti { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Predmeti != null)
            {
                Predmeti = await _context.Predmeti.ToListAsync();
            }
        }
    }
}
