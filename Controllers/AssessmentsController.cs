using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class AssessmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GeneralInterface generalInterface;
        private readonly IFileProvider fileProvider;

        public AssessmentsController(ApplicationDbContext context,GeneralInterface general,IFileProvider provider)
        {
            _context = context;
            generalInterface = general;
            fileProvider = provider;
        }

        // GET: Assessments
        public async Task<IActionResult> Index()
        {
           // var usa =await generalInterface.GetLoggedinUser();
            //var usaasses = _context.UserAssessment.Where(y => y.UserId == usa.Id);
            return View(await _context.Assessments.ToListAsync());
        }

        // GET: Assessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessments = await _context.Assessments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessments == null)
            {
                return NotFound();
            }

            return View(assessments);
        }
        // GET: Assessments/Details/5
        public async Task<IActionResult> AssessmentPerfomance(int? userid)
        {
            if (userid == null)
            {
                return NotFound();
            }
            var assessments = await _context.Assessments
                .FirstOrDefaultAsync(m => m.Id == userid);
            if (assessments == null)
            {
                return NotFound();
            }

            return View(assessments);
        }


        // GET: Assessments/Details/5
        [HttpGet]
        public async Task<IActionResult> AssessmentAnswers(int? assessmentid)
        {
            
            var quizes = _context.Questions
                    .Include(i => i.Assessments)
                    .Include(i => i.Answeroptions)
                    .Include(i => i.Topic)
                  .Where(m => m.AssessmentId == assessmentid)
                  .ToList();
            List<AssessmentViewmodel> assessments = new List<AssessmentViewmodel>();

            foreach (var a in quizes)
            {
                var myquiz = _context.UserQuestionAnswers
                    .Include(y=>y.Answer)
                .Where(u => u.QuestionId == a.Id).FirstOrDefault();
                AssessmentViewmodel viewmodel = new AssessmentViewmodel();
                viewmodel.QuestionName = a.Name;
                if (myquiz != null)
                {
                    viewmodel.ChosenAnswerId = myquiz.Answer.Id;
                }
                viewmodel.AnswerOptions = new List<AnswerOption>();
                foreach (var i in a.Answeroptions)
                {
                    AnswerOption answeroptions = new AnswerOption()
                    {
                        IsCorrectAnswer = i.IsCorrectAnswer,
                        Id = i.Id,
                        Name =i.Name                        
                    };
                    viewmodel.AnswerOptions.Add(answeroptions);
                }
                assessments.Add(viewmodel);
            }
            return View(assessments);
        }

        [HttpGet]
        public async Task<IActionResult> QuizResultsAsync(DateTime dateTime)
        {
            var usa = generalInterface.GetLoggedinUser().Result;
            var userassessment = _context.UserAssessment
                .Include(r=>r.User)
                .Include(r=>r.Assessments)
                .Where(y => y.CreatedDate == dateTime
                && y.UserId== usa.Id).FirstOrDefault();
                var mytopics_ =await new TopicsController(_context, generalInterface,fileProvider).GetTopics(userassessment.AssessmentsId,dateTime);
            var myval = mytopics_ as ObjectResult;
            if(myval!=null)
            {
                var mydata = myval.Value as List<TopicAnswers>;
                ViewBag.results = mydata;
            }
            if (userassessment != null)
            {
                string paf;
                var perc_ = GetPercentage(userassessment.CorrectQuizes, userassessment.TotalMarks);
                if(perc_>=50)
                {
                    paf = "PASS";
                }
                else
                {
                    paf = "FAIL";
                }
                ViewBag.score = perc_;
                ViewBag.Perfomance = paf;
                return View(userassessment);
            }
            else
            {
                UserAssessment newuserAssessment = new UserAssessment();
                return View(newuserAssessment);
            }
        }
        public double GetPercentage(int? current, int? maximum)
        {
            return (current.Value / maximum.Value) * 100;
        }

            [HttpGet]
        public async Task<IActionResult> TakeAssessment(int? assessmentid)
        {
            if (assessmentid == null)
            {
                return NotFound();
            }
            var requiredtime = generalInterface.GetTimeAssessmentById(assessmentid);
            ViewBag.questiontime = requiredtime;

            var item_ =await GetAssessmentQuestionsAsync(assessmentid);
            var rst = item_ as ObjectResult;
            if (rst.StatusCode == StatusCodes.Status202Accepted)
            {
                return RedirectToAction("AssessmentDone", new { assid = "assessment" });
            }
            else if(rst.StatusCode == StatusCodes.Status201Created)
            {
                var model = rst.Value as UserAssessment;
                return RedirectToAction("QuizResults", new { dateTime = model.CreatedDate });
            }
           
            else
            {
                var model = rst.Value as AssessmentViewmodel;

                return View(model);
            }
        }

        public async Task<IActionResult> GetAssessmentQuestionsAsync(int? assessmentid)
        {
            var usaid_ =await generalInterface.GetLoggedinUser();
            var quizes = GetQuizesDone();
            List<AnswerOption> QuizAnswers = new List<AnswerOption>();
            var assessments = new List<Questions>();
           
                assessments = _context.Questions
                    .Include(i => i.Assessments)
                    .Include(i => i.Answeroptions)
                    .Include(i => i.Topic)
                  .Where(m => m.AssessmentId == assessmentid)
                  .ToList();
            foreach (var a in quizes)
            {
                var quiz_ =await generalInterface.GetQuestionById(a);
                if(quiz_!=null)
                {
                    assessments.Remove(quiz_);
                }
            }
            UserAssessment newuserAssessment = new UserAssessment();

            if (assessments.Count() !< 1)
            {

                var getcorrect_ = getcorrectanswers(assessmentid);
                var gettotal = gettotaltime(assessmentid);
                var getallquiz = getallquestions(assessmentid);
                var exist_ = _context.UserAssessment.Where(tr => tr.CreatedDate == DateTime.Today).FirstOrDefault();
            if (exist_ == null)
            {
                newuserAssessment = new UserAssessment()
                {
                    AssessmentsId = assessmentid,
                    CreatedDate = DateTime.Today,
                    CreatedTime = DateTime.Now,
                    UserId = usaid_.Id,
                    TimeToComplete = gettotal,
                    CorrectQuizes = getcorrect_,
                    TotalMarks = getallquiz
                };
                _context.Add(newuserAssessment);
                _context.SaveChanges();
            }
                if (assessments.Count() < 1)
                {
                    return Created("", newuserAssessment);
                }
                else
                {
                    return Accepted();
                }
            }
            else
            {
                if (assessments == null)
                {
                    return NotFound();
                }
                assessments = GeneralExtension.Randomize(assessments);
                var myitem = assessments.FirstOrDefault();
                foreach (var a in myitem.Answeroptions)
                {
                    AnswerOption answerOption = new AnswerOption();
                    answerOption.Id = a.Id;
                    answerOption.Name = a.Name;
                    QuizAnswers.Add(answerOption);
                }
                AssessmentViewmodel assessmentViewmodel = new AssessmentViewmodel()
                {
                    QuestionId = myitem.Id,
                    QuestionName = myitem.Name,
                    AnswerOptions = QuizAnswers,
                    Assessmentid = assessmentid
                };
                return Ok(assessmentViewmodel);
            }
        }

        public List<int?> GetQuizesDone()
        {
            var usaid_ = generalInterface.GetLoggedinUser().Result;
            var assessments = _context.UserQuestionAnswers
               .Include(i => i.Answer)
               .Include(i => i.TimeToAnswer)
               .Include(i => i.Question)
               .Include(i => i.User)
             .Where(m => m.UserId == usaid_.Id);
             
            if(assessments!=null)
            {
               return assessments.Select(u => u.QuestionId)
             .ToList();
            }
            return new List<int?>();
        }

        [HttpPost]
        public async Task<IActionResult> TakeAssessment(AssessmentViewmodel assessmentViewmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var requiredtime = generalInterface.GetTimeAssessmentById(assessmentViewmodel.Assessmentid);
                    int? timetocompletequiz = 0;
                    timetocompletequiz = requiredtime - assessmentViewmodel.Timetoanswer;

                    var userid = await generalInterface.GetLoggedinUser();
                    int timetakenid_ = 0;
                    TimeToAnswer timeToAnswer = GetTimeToAnswer(timetocompletequiz);
                    if (timeToAnswer != null)
                    {
                        timetakenid_ = timeToAnswer.Id;
                    }
                    var iscorrectans_ = IsCorrectAnswer(assessmentViewmodel.ChosenAnswerId);
                    UserQuestionAnswers userAssessment = new UserQuestionAnswers()
                    {
                        TimeToAnswerId = timetakenid_,
                        TimeToComplete = timetocompletequiz,
                        UserId = userid.Id,
                        QuestionId = assessmentViewmodel.QuestionId,
                        AnswerId = assessmentViewmodel.ChosenAnswerId,
                        CreatedDate = DateTime.Today,
                        CreatedTime = DateTime.Now,
                        IsCorrectAnswer = iscorrectans_

                    };
                    _context.Add(userAssessment);
                    _context.SaveChanges();
                    int avgtime = averagetime(assessmentViewmodel.Assessmentid);
                    int tot = gettotaltime(assessmentViewmodel.Assessmentid);
                    var myid = _context.UserAnswers.Where(t => t.AssessmentsId == assessmentViewmodel.Assessmentid && t.CreatedDate == DateTime.Today && t.UserId == userid.Id).FirstOrDefault();
                    if (myid != null)
                    {
                        myid.AssessmentsId = assessmentViewmodel.Assessmentid;
                        myid.CreatedDate = DateTime.Today;
                        myid.CreatedTime = DateTime.Now;
                        myid.UserId = userid.Id   ;
                        myid.AverageTimeToAnswer = avgtime;
                        myid.TotallTimeToAnswer = tot;
                        
                        _context.Update(myid);
                        _context.SaveChanges();
                    }
                    else
                    {
                        UserAnswers itemid = new UserAnswers();
                        itemid.AssessmentsId = assessmentViewmodel.Assessmentid;
                        itemid.CreatedDate = DateTime.Today;
                        itemid.CreatedTime = DateTime.Now;
                        itemid.UserId = userid.Id;
                        itemid.AverageTimeToAnswer = avgtime;
                        itemid.TotallTimeToAnswer = tot;
                        _context.Add(itemid);
                        _context.SaveChanges();
                    }

                
                    return RedirectToAction("TakeAssessment", new { assessmentid = assessmentViewmodel.Assessmentid });
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ann error occured when updating the answer.");
                    return View(assessmentViewmodel);

                }
            }
            else
            {
                return View(assessmentViewmodel);

            }
        }
        public bool IsCorrectAnswer(int? ansid)
        {
            var myans = _context.Answeroptions
                .Where(u => u.Id == ansid)
                .FirstOrDefault();
            if (myans != null)
            {
                return myans.IsCorrectAnswer;
            }
            else
            {
                return false;
            }
        }
        public int averagetime(int? Assessmentid)
        {
            var user_ = generalInterface.GetLoggedinUser().Result;
            int avgtime = 0;
            var asst = _context.UserQuestionAnswers
                .Include(u => u.Answer)
                .Include(u => u.Question)
                .Include(u => u.TimeToAnswer)
                .Include(u => u.User)
                .Where(u => u.Question.AssessmentId == Assessmentid
                && u.CreatedDate == DateTime.Today
                && u.UserId == user_.Id).ToList();
            if (asst != null)
            {
                var mytime = asst.Select(y => y.TimeToComplete).ToList();
                var mavg = mytime.Average(item => item);
                avgtime = Convert.ToInt32(mavg);
                return avgtime;
            }
            return avgtime;
        }
        public int gettotaltime(int? Assessmentid)
        {
            var user_ = generalInterface.GetLoggedinUser().Result;
            int avgtime = 0;
            var asst = _context.UserQuestionAnswers
                .Include(u => u.Answer)
                .Include(u => u.Question)
                .Include(u => u.TimeToAnswer)
                .Include(u => u.User)
                .Where(u => u.Question.AssessmentId == Assessmentid
                && u.CreatedDate == DateTime.Today
                && u.UserId == user_.Id).ToList();
            if(asst!=null)
            {
                var mytime =asst.Select(y => y.TimeToComplete).ToList();
                var mysum =  mytime.Sum(item => item);
                avgtime = Convert.ToInt32(mysum);
                return avgtime;
            }
            return avgtime;
        }

          public int getcorrectanswers(int? Assessmentid)
        {
            var user_ = generalInterface.GetLoggedinUser().Result;
            int avgtime = 0;
            var asst = _context.UserQuestionAnswers
                .Include(u => u.Answer)
                .Include(u => u.Question)
                .Include(u => u.TimeToAnswer)
                .Include(u => u.User)
                .Where(u => u.Question.AssessmentId == Assessmentid
                && u.CreatedDate == DateTime.Today
                && u.UserId == user_.Id
                && u.IsCorrectAnswer==true).Count();
           
            return asst;
        }
          public int getallquestions(int? Assessmentid)
        {
            var user_ = generalInterface.GetLoggedinUser().Result;
            int avgtime = 0;
            var asst = _context.UserQuestionAnswers
                .Include(u => u.Answer)
                .Include(u => u.Question)
                .Include(u => u.TimeToAnswer)
                .Include(u => u.User)
                .Where(u => u.Question.AssessmentId == Assessmentid
                && u.CreatedDate == DateTime.Today
                && u.UserId == user_.Id)
                .Select(i=>i.QuestionId).Count();
           
            return asst;
        }


        public TimeToAnswer GetTimeToAnswer(int? timetoanswer)
        {
            var timetaken_ = _context.TimeToAnswer
                .Where(u => u.StartTime <= timetoanswer && u.EndTime > timetoanswer)
                .FirstOrDefault();
            return timetaken_;
        }

            // GET: Assessments/Create
            public IActionResult Create()
        {
            return View();
        }

        // POST: Assessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,QuestionPeriodInSeconds")] Assessments assessments)
        {
            if (ModelState.IsValid)
            {
                assessments.CreatedDate = DateTime.Today;
                assessments.CreatedTime = DateTime.Now;
                _context.Add(assessments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assessments);
        }

        // GET: Assessments/Edit/5
        public IActionResult AssessmentDone(string assid)
        {           
            ViewBag.message = "You have already taken the assessment today, please try again later";            
            return View();
        }
        // GET: Assessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessments = await _context.Assessments.FindAsync(id);
            if (assessments == null)
            {
                return NotFound();
            }
            return View(assessments);
        }

        // POST: Assessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,QuestionPeriodInSeconds")] Assessments assessments)
        {
            if (id != assessments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    assessments.CreatedDate = DateTime.Today;
                    assessments.CreatedTime = DateTime.Now;
                    _context.Update(assessments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssessmentsExists(assessments.Id))
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
            return View(assessments);
        }

        // GET: Assessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assessments = await _context.Assessments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assessments == null)
            {
                return NotFound();
            }

            return View(assessments);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assessments = await _context.Assessments.FindAsync(id);
            _context.Assessments.Remove(assessments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssessmentsExists(int id)
        {
            return _context.Assessments.Any(e => e.Id == id);
        }
    }
}
