using DataBaseContext;
using DatabaseEntityLib;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eUcionica.Pages.Testovi
{
    public class testiranjeModel : PageModel
    {
        private readonly DB_Context_Class _context;
        private readonly int testSize = 3;

        public testiranjeModel(DB_Context_Class context)
        {
            _context = context;
        }

        public List<Predmeti> Predmeti { get; set; } = new List<Predmeti>();
        public List<Oblasti> Oblasti { get; set; } = new List<Oblasti>();
        public List<Zadaci> SelectedQuestions { get; set; } = new List<Zadaci>();
        public List<string> UserAnswers { get; set; } = new List<string>();

        [BindProperty]
        public int SelectedPredmetId { get; set; }

        [BindProperty]
        public int SelectedOblastId { get; set; }

        public int CorrectAnswers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Predmeti = await _context.Predmeti.ToListAsync();
            Oblasti = await _context.Oblasti.ToListAsync();

            SelectedQuestions = JsonConvert.DeserializeObject<List<Zadaci>>(HttpContext.Session.GetString("SelectedQuestions") ?? "[]");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var questions = _context.Zadaci.AsQueryable();

            if (SelectedPredmetId != 0)
            {
                questions = questions.Where(x => x.IDPredmet == SelectedPredmetId);
            }
            else if (SelectedOblastId != 0)
            {
                questions = questions.Where(x => x.IDOblast == SelectedOblastId);
            }

            var questionsList = await questions.ToListAsync();
            var random = new Random();
            SelectedQuestions = questionsList.OrderBy(x => random.Next()).Take(testSize).ToList();

            HttpContext.Session.SetString("SelectedQuestions", JsonConvert.SerializeObject(SelectedQuestions));

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAnswersAsync(List<string> userAnswers)
        {
            UserAnswers = userAnswers;
            HttpContext.Session.SetString("UserAnswers", JsonConvert.SerializeObject(UserAnswers));
            SelectedPredmetId = 0;
            SelectedOblastId = 0;

            return RedirectToPage(new { handler = "CheckAnswers" });
        }
        public async Task<IActionResult> OnPostCheckAnswersAsync()
        {
            SelectedQuestions = JsonConvert.DeserializeObject<List<Zadaci>>(HttpContext.Session.GetString("SelectedQuestions"));
            UserAnswers = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("UserAnswers"));

            CorrectAnswers = 0;

            if (SelectedQuestions != null && UserAnswers != null)
            {
                for (int i = 0; i < SelectedQuestions.Count; i++)
                {
                    var questionFromDb = await _context.Zadaci.FindAsync(SelectedQuestions[i].ID);
                    if (questionFromDb != null && i < UserAnswers.Count)
                    {
                        var correctAnswer = questionFromDb.Odgovor.Trim().ToLower();
                        var userAnswer = UserAnswers[i].Trim().ToLower();
                        if (correctAnswer == userAnswer)
                        {
                            CorrectAnswers++;
                        }
                    }
                }
            }

            return Page();
        }
    }
}