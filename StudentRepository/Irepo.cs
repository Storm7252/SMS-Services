using Microsoft.AspNetCore.Identity;
using SMS.Models;

namespace SMS.StudentRepository
{
    public interface Irepo
    {
        Task<IdentityResult> SignUp(SignUp FormData);
        Task<SignInResult> Login(Login Data);
        void Logout();
        //------------------------student details--------------

        void RegisterDetails(StudentDetails studentDetails, string userdata);
    }
}
