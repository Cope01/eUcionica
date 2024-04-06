using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataBaseContext;
using DatabaseEntityLib;

namespace eUcionica.Pages.ZadaciFolder
{
    public class ZadaciModel : PageModel
    {
        private readonly DB_Context_Class _context;

        public ZadaciModel(DB_Context_Class context)
        {
            _context = context;
        }

        public IList<Zadaci> Zadaci { get; set; } = default!;

        [BindProperty]
        public string SearchText { get; set; }

        public void OnPost()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Zadaci = _context.Zadaci
                    .Include(p => p.Predmet)
                    .Include(p => p.Oblast)
                    .ToList();
            }
            else
            {
                Zadaci = _context.Zadaci
                    .Where(x => x.Predmet.Name.Contains(SearchText) || x.Oblast.Name.Contains(SearchText))
                    .Include(p => p.Predmet)
                    .Include(p => p.Oblast)
                    .ToList();
            }
        }

        public void OnGet()
        {
            Zadaci = _context.Zadaci
                .Include(p => p.Predmet)
                .Include(p => p.Oblast)
                .ToList();
        }
    }
}