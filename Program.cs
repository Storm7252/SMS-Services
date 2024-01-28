using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SMS.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMS.StudentRepository;
using SMS.SmsServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<StudentContext>();
builder.Services.AddScoped<Irepo, repo>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.AccessDeniedPath = "/Home/SignIn";
    opt.LoginPath = "/Home/SignIn";
});



//-----resgister services for sms services--------------
builder.Services.AddSingleton<ISmsService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var accountSid = configuration["Twilio:AccountSid"];
    var authToken = configuration["Twilio:AuthToken"];
    var phoneNumber = configuration["Twilio:PhoneNumber"];

    return new TwilioSmsService(accountSid, authToken, phoneNumber);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
