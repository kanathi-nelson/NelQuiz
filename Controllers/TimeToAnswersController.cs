using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NelQuiz.Data;
using NelQuiz.Models;

namespace NelQuiz.Controllers
{
    public class TimeToAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeToAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeToAnswers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeToAnswer.ToListAsync());
        }

        // GET: TimeToAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeToAnswer = await _context.TimeToAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeToAnswer == null)
            {
                return NotFound();
            }

            return View(timeToAnswer);
        }

        // GET: TimeToAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeToAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,Meaning,Description,CreatedDate,CreatedTime")] TimeToAnswer timeToAnswer)
        {
            if (ModelState.IsValid)
            {
                timeToAnswer.CreatedDate = DateTime.Now;
                timeToAnswer.CreatedTime = DateTime.Now;
                
                _context.Add(timeToAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeToAnswer);
        }


        // GET: TimeToAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeToAnswer = await _context.TimeToAnswer.FindAsync(id);
            if (timeToAnswer == null)
            {
                return NotFound();
            }
            return View(timeToAnswer);
        }

        // POST: TimeToAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,Meaning,Description,CreatedDate,CreatedTime")] TimeToAnswer timeToAnswer)
        {
            if (id != timeToAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeToAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeToAnswerExists(timeToAnswer.Id))
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
            return View(timeToAnswer);
        }

        // GET: TimeToAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeToAnswer = await _context.TimeToAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeToAnswer == null)
            {
                return NotFound();
            }

            return View(timeToAnswer);
        }

        // POST: TimeToAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeToAnswer = await _context.TimeToAnswer.FindAsync(id);
            _context.TimeToAnswer.Remove(timeToAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeToAnswerExists(int id)
        {
            return _context.TimeToAnswer.Any(e => e.Id == id);
        }
    }
}
