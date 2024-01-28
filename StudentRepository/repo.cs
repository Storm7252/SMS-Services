using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using SMS.DatabaseContext;
using SMS.Models;

namespace SMS.StudentRepository
{
    public class repo:Irepo
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly StudentContext _context;

        public repo(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, StudentContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }

        public async Task<IdentityResult> SignUp(SignUp FormData)
        {
            var User = new IdentityUser()
            {
               UserName=FormData.Email,
               Email=FormData.Email
            };
            var res=await _userManager.CreateAsync(User,FormData.Password);
            if(!string.IsNullOrEmpty(FormData.UserRole.ToString()))
            {
                await _userManager.AddToRoleAsync(User, FormData.UserRole.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(User, UserRoles.Student.ToString());
            }
            return res;
        }


        public async Task<SignInResult> Login(Login Data)
        {
            var res=await _signInManager.PasswordSignInAsync(Data.Email, Data.Password, Data.RememberMe,false);
            return res;
        }



        public  async void Logout()
        {
            await _signInManager.SignOutAsync();
        }



        //------------------------------------------------------------------student form-----------------

        public void RegisterDetails(StudentDetails studentDetails, string userdata)
        {
            var user = new StudentDetails()
            {
                StudentId = studentDetails.StudentId,
                Address = studentDetails.Address,
                FathersName = studentDetails.FathersName,
                PhoneNumber = studentDetails.PhoneNumber,
                UserId = userdata
            };
            
            var res = _context.StudentDetails.Add(user);
            _context.SaveChanges();

        }
    }
}
