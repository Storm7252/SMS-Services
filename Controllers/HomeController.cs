using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.DatabaseContext;
using SMS.Models;
using SMS.SmsServices;
using SMS.StudentRepository;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISmsService _smsService;
        private readonly Irepo _sturepo;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly StudentContext _stucon;

        public HomeController(ISmsService smsService,Irepo sturepo,RoleManager<IdentityRole> roleManager,StudentContext stucon)
        {
            this._smsService = smsService;
            _sturepo = sturepo;
            this.roleManager = roleManager;
            this._stucon = stucon;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Login Data)
        {
            if (ModelState.IsValid)
            {
                var res =await  _sturepo.Login(Data);
                if (res.Succeeded)
                {
                    
                    if (User.IsInRole(UserRoles.Admin))
                    {
                        return RedirectToAction("AdminDashboard");
                    }
                    else if (User.IsInRole(UserRoles.Student))
                    {
                        var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        var studId = _stucon.StudentDetails.Where(opt => opt.UserId == userID).Select(item=>item.UserId).FirstOrDefault();
                        var newstuId = Convert.ToString(studId);
                        var result = string.Equals(userID, newstuId);
                        if (result == true)
                        {
                            return RedirectToAction("StudentDashBoard");
                        }
                        else
                        {
                            return RedirectToAction("StudentForm");
                        }
                        
                    }
                    return RedirectToAction("Privacy");
                }
                else
                {
                    ModelState.AddModelError("",$"Please Enter Valid Credentials");
                    return View(Data);
                }
               
            }
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> SignUp(SignUp FormData)
        {
            if(ModelState.IsValid)
            {
                var res=await _sturepo.SignUp(FormData);
                if (!res.Succeeded)
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(FormData);
                }
                ModelState.Clear();
                try
                {
                    _smsService.SendSms("+918899733348",$"{FormData.UserName}  has Created account in softStacks Pvt Ltd");
                }catch(Exception ex)
                {
                    return Content(ex.Message);
                }
               
                return RedirectToAction("SignIn");
            }

            return View(); 
        }

        public IActionResult Logout()
        {
            _sturepo.Logout();
            return RedirectToAction("SignIn");
        }



        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
           
            return View();
        }



        [Authorize(Roles = "Student")]
        public IActionResult StudentDashBoard()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studId = _stucon.StudentDetails.Where(opt => opt.UserId == userID).Include(item=>item.IdentityUser).FirstOrDefault();
            return View(studId);
        }

        public IActionResult StudentForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentFormdetails(StudentDetails form)
        {
            if(User != null)
            {
                var userdata = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                _sturepo.RegisterDetails(form, userdata);
                return RedirectToAction("StudentDashBoard");
            }
            else
            {
                return Content("User not found");
            }
            
            
        }





        /*public string setRoles()
        {
            roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(UserRoles.Student)).GetAwaiter().GetResult();
            return "Roles has been Added";
        }
*/





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
