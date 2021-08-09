using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using NelQuiz.Data;
using NelQuiz.Interfaces;
using NelQuiz.Models;
using NelQuiz.Viewmodels;

namespace NelQuiz.Controllers
{
    public class TopicsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly GeneralInterface generalInterface;
        private readonly IFileProvider fileProvider;

        public TopicsController(ApplicationDbContext context, GeneralInterface general,IFileProvider provider_)
        {
            _context = context;
            generalInterface = general;
            fileProvider = provider_;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Topics.ToListAsync());
        }
         // GET: Topics
        public async Task<IActionResult> ResourcesIndex(int topicindex,string topicname)
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(topicname))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath,FName= topicname });
            }
            
            ViewBag.resourceindex = topicindex;
            return View(model);
        }
         // GET: Topics
        public async Task<IActionResult> AddResource(int topicindex)
        {
            ViewBag.resourceindex = topicindex;
            var topicname = _context.Topics
                .FirstOrDefault(U => U.Id == topicindex);
            ViewBag.topicname = topicname.Name;
            ViewBag.topicid = topicname.Id;
            return View();
        }

        public double GetPercentage(int? current, int? maximum)
        {
            return (current.Value / maximum.Value) * 100;
        }

        public int GettotalQuizes(int? assessmentid, int topicid)
        {
            var quizz_ = _context.Questions
               .Where(e => e.AssessmentId == assessmentid
               && e.TopicId==topicid)
               .Count();
            return quizz_;
        }
         public int GetcorrectQuizes(int topicid)
        {
            int myval = 0;
            var user = generalInterface.GetLoggedinUser().Result;
            var mydata_ = _context.UserQuestionAnswers
               .Include(u => u.Answer)
               .Include(u => u.Question)
               .ThenInclude(u => u.Topic)
               .Include(u => u.TimeToAnswer)
               .Where(q => q.UserId == user.Id && q.IsCorrectAnswer == true && q.Question.TopicId == topicid).ToList();
            if(mydata_!=null)
            {
                myval = mydata_.Count();
            }
               
            return myval;
        }

           // GET: Topics
        public async Task<IActionResult> GetTopics(int? assessmentid,DateTime assessmentDate)
        {
            var tps = _context.Topics.AsEnumerable();
            var user =await generalInterface.GetLoggedinUser();
            var ass = _context.Assessments
                .Include(r => r.Questions)
                .ThenInclude(r => r.Topic)
                .Where(w=>w.Id==assessmentid&& w.CreatedDate==assessmentDate)
                .FirstOrDefault();
            List<TopicAnswers> listtopicAnswers = new List<TopicAnswers>();

            foreach (var a in tps)
            {
                var myid = GettotalQuizes(assessmentid, a.Id);
                var newid = GetcorrectQuizes(a.Id);
                TopicAnswers topicAnswers = new TopicAnswers();
                topicAnswers.TopicName = a.Name;
                topicAnswers.TotalQuizes = myid;
                topicAnswers.CorrectAnswers = newid;
                if (topicAnswers.TotalQuizes != 0)
                {
                    topicAnswers.Percentage = GetPercentage(topicAnswers.CorrectAnswers, topicAnswers.TotalQuizes);
                }
                else
                {
                    topicAnswers.Percentage = 0;
                }
                listtopicAnswers.Add(topicAnswers);
            }          
           
            return Ok(listtopicAnswers);
        }

        public async Task<IActionResult> Download(string filename,string topicname)
        {
            if (filename == null)
                return Content("filename not present");

            var path = GetPath(filename, topicname);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public string GetPath(string filename_,string topicname)
        {
            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/"+topicname, filename_);

            if (System.IO.File.Exists(path))
            {
                return path;                
            }
            return string.Empty;
        }

        public IActionResult DeleteFile(string filename, string topicname)
        {
            if (filename == null)
                return Content("filename not present");

            var path = GetPath(filename,topicname);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return RedirectToAction("ResourcesIndex", new { topicname = topicname });

            }
            else
            {
                ModelState.AddModelError(null, "The path to file does not exist");
                return RedirectToAction("Files");
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddResource(MyFileModel fileModel)
        {
            if (fileModel.files == null || fileModel.files.Count == 0)
                return Content("files not selected");

            foreach (var file in fileModel.files)
            {
                var fname_ = file.FileName;
               
                    var path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot/"+fileModel.TopicName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(
                           Directory.GetCurrentDirectory(), "wwwroot/" + fileModel.TopicName,fname_);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }

            return RedirectToAction("ResourcesIndex",new {topicname =fileModel.TopicName });
        }



        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topics == null)
            {
                return NotFound();
            }

            return View(topics);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Topics topics)
        {
            if (ModelState.IsValid)
            {
                topics.CreatedDate = DateTime.Now;
                topics.CreatedTime = DateTime.Now;
                _context.Add(topics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topics);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics.FindAsync(id);
            if (topics == null)
            {
                return NotFound();
            }
            return View(topics);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedDate,CreatedTime")] Topics topics)
        {
            if (id != topics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicsExists(topics.Id))
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
            return View(topics);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topics = await _context.Topics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topics == null)
            {
                return NotFound();
            }

            return View(topics);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topics = await _context.Topics.FindAsync(id);
            _context.Topics.Remove(topics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicsExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
