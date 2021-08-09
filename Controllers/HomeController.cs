using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NelQuiz.Data;
using NelQuiz.Interfaces;
using NelQuiz.Models;
using NelQuiz.Viewmodels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NelQuiz.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public readonly GeneralInterface generalInterface_;
        public Dictionary<string, string> DataItems_ = new Dictionary<string, string>();




        public string ReturnUrl { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public HomeController(SignInManager<ApplicationUser> signInManager, GeneralInterface generalinter_,
            ILogger<HomeController> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            generalInterface_ = generalinter_;
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            returnUrl ??= Url.Content("~/");

            ReturnUrl = returnUrl;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                bool isexists = generalInterface_.Userexists(viewModel.Email);
                if (isexists == false)
                {
                    ModelState.AddModelError(string.Empty, "Login failed. There is no account matching the entered Email.");
                    return View(viewModel);
                }
                try
                {
                    //bool myblocked = user_.Blocked;
                    //if (myblocked == true)
                    //{
                    //    ModelState.AddModelError(string.Empty, "Login failed. Account is blocked. Please contact Admin.");
                    //    return View(viewModel);
                    //}
                    var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);
                    //var result = await _signInManager.CheckPasswordSignInAsync(user_, viewModel.Password, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //if (myuser != null)
                        //{
                        //    if (myuser.Selected == true)
                        //    {
                        //        var user = _context.Users.Where(i => i.Email == viewModel.Email).FirstOrDefault();
                        //        user.Issubmitted = true;

                        //        var aicapplicant = generalInterface_.ApplicantByEmail(viewModel.Email);
                        //        aicapplicant.IsSelected = true;

                        //        _context.Users.Update(user);
                        //        _context.AICApplicant.Update(aicapplicant);
                        //        _context.SaveChanges();
                        //    }
                        //}

                        var newuser = _context.Users.Where(i => i.Email == viewModel.Email).FirstOrDefault();

                        _logger.LogInformation("User logged in.");
                        if (returnUrl != "/")
                        {
                            return LocalRedirect(returnUrl);
                        }
                        
                            return RedirectToAction("Index", "Home");
                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Login failed.Please check your Password/Email");
                        return View(viewModel);

                    }

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    var inn = ex.InnerException;
                    ModelState.AddModelError(string.Empty, msg);
                    return View(viewModel);
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Login failed.Please check your email/password");
                return View(viewModel);
            }



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupViewModel viewModel)
        {

            ViewData["IsDone"] = "False";
            ViewData["Err"] = "";
            int age = 0;

            if (ModelState.IsValid)
            {
               
                var s = new Guid();
                var Confirmtoken = s.ToString();

                var user = new ApplicationUser
                {
                    Deleted = false,
                    Blocked = false,
                    UserName = viewModel.Email,
                    CreatedDate = DateTime.Now,
                    CreatedTime = DateTime.UtcNow,
                    Email = viewModel.Email,                   
                    PhoneNumber = viewModel.PhoneNumber,
                    Gender = viewModel.Gender,
                    RoleId = 2,
                    ConfirmationToken = Confirmtoken,
                    DateofBirth = viewModel.DateofBirth,
                    PhoneNo = viewModel.PhoneNumber,
                    Name = viewModel.Name
                };

                var newusa = _context.Users.Where(p => p.Email == viewModel.Email).FirstOrDefault();
                if (newusa != null)
                {
                    ModelState.AddModelError(string.Empty, "A user with the entered Email already exists, please Login or change the email.");
                    //ViewData["MyGender"] = new SelectList(FillGender(), "Select");
                    //ViewData["MyIncubationCenter"] = new SelectList(FillIncubations(), "Select Incubation Center");
                    //ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
                    return View();
                }

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {                   
                    return RedirectToAction("Login", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewData["Err"] = "Please check if you entered the correct details";
            return View();

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateTask()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //var usa = _context.Users.Where(p => p.Email == viewModel.Email).FirstOrDefault();

        //_logger.LogInformation("User created a new account with password.");

        //            var st = Guid.NewGuid();
        //var code = st.ToString();
        //usa.ConfirmationToken = code;
        //            _context.Update(usa);
        //            _context.SaveChanges();
        //            string emailFor = "VerifyAccount";

    }
}
