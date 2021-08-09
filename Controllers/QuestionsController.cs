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
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly GeneralInterface generalInterface_;

        public QuestionsController(ApplicationDbContext context, GeneralInterface interfacenew)
        {
            _context = context;
            generalInterface_ = interfacenew;
        }

        // GET: Questions
        public async Task<IActionResult> Index(int? assessmentid)
        {
            var questionslist = await _context.Questions.Include(o=>o.Topic)
                .Include(o=>o.Answeroptions)
                .ToListAsync();
            if (assessmentid != null)
            {
                questionslist = await _context.Questions.Include(o=>o.Topic)
                    .Include(o => o.Answeroptions)                    
                    .Where(iu=>iu.AssessmentId==assessmentid)
                    .ToListAsync();
                var myass_ =await generalInterface_.GetAssessmentById(assessmentid);
                ViewBag.newassessment = myass_.Name;
            }
            return View(questionslist);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.Include(o=>o.Topic)
                .Include(o => o.Answeroptions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public IActionResult Create(int? assessmentid)
        {
            Questions questions = new Questions();
            if(assessmentid!=null)
            {
                questions.AssessmentId = assessmentid;
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name");
            return View(questions);
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AssessmentId,AnswerId,TopicId")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                questions.CreatedDate = DateTime.Now;
                questions.CreatedTime = DateTime.Now;
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { assessmentid = questions.AssessmentId });
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", questions.TopicId);
            return View(questions);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", questions.TopicId);
            return View(questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,AnswerId,AssessmentId,TopicId,CreatedDate,CreatedTime")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Name", questions.TopicId);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.Include(o=>o.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Include(o=>o.Topic).Any(e => e.Id == id);
        }
    }
}
