using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NelQuiz.Data;
using NelQuiz.Interfaces;
using NelQuiz.Models;

namespace NelQuiz.Controllers
{
    public class AnsweroptionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly GeneralInterface generalInterface_;

        public AnsweroptionsController(ApplicationDbContext context, GeneralInterface interfacenew)
        {
            _context = context;
            generalInterface_ = interfacenew;
        }

        // GET: Answeroptions
        public async Task<IActionResult> Index(int? questionid)
        {
            var applicationDbContext = _context.Answeroptions.Include(a => a.Question);
            if (questionid != null)
            {
                applicationDbContext = _context.Answeroptions
                    .Where(u => u.QuestionId == questionid)
                    .Include(a => a.Question);
                var myans =await generalInterface_.GetQuestionById(questionid);
                ViewBag.myquizes = myans.Name;
                ViewBag.quizid = questionid;

            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Answeroptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answeroptions = await _context.Answeroptions
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answeroptions == null)
            {
                return NotFound();
            }

            return View(answeroptions);
        }

        // GET: Answeroptions/Create
        public async Task<IActionResult> Create(int? quizid)
        {
            Answeroptions questions = new Answeroptions();
            if (quizid != null)
            {
                questions.QuestionId = quizid;
                var myans = await generalInterface_.GetQuestionById(quizid);
                ViewBag.myquizes = myans.Name;
                ViewBag.myquizid =quizid;
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id");
            return View(questions);
        }

        // POST: Answeroptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,QuestionId,IsCorrectAnswer,CreatedDate,CreatedTime")] Answeroptions answeroptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answeroptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { questionid = answeroptions.QuestionId });
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answeroptions.QuestionId);
            return View(answeroptions);
        }

        public bool InsertUserAssessment(Answeroptions answeroptions)
        {


            return true;
        }

        // GET: Answeroptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answeroptions = await _context.Answeroptions.FindAsync(id);
            if (answeroptions == null)
            {
                return NotFound();
            }
            ViewBag.quizid = answeroptions.QuestionId;
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answeroptions.QuestionId);
            return View(answeroptions);
        }

        // POST: Answeroptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,QuestionId,IsCorrectAnswer,CreatedDate,CreatedTime")] Answeroptions answeroptions)
        {
            if (id != answeroptions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answeroptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnsweroptionsExists(answeroptions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Id", answeroptions.QuestionId);
            return View(answeroptions);
        }

        // GET: Answeroptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answeroptions = await _context.Answeroptions
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answeroptions == null)
            {
                return NotFound();
            }

            return View(answeroptions);
        }

        // POST: Answeroptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answeroptions = await _context.Answeroptions.FindAsync(id);
            _context.Answeroptions.Remove(answeroptions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnsweroptionsExists(int id)
        {
            return _context.Answeroptions.Any(e => e.Id == id);
        }
    }
}
