using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Agro360.Areas.Home.Controllers
{
    [Area("Home")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpClientFactory _clientFactory;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IHttpClientFactory clientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInModel model)
        {

            if (ModelState.IsValid)
            {

                ApplicationUser loggingUser=new ApplicationUser();
                if (model.Email.Contains("@"))
                {
                    loggingUser = await _userManager.FindByEmailAsync(model.Email);
                }
                else
                {
                    loggingUser = await _userManager.FindByNameAsync(model.Email);
                }

                if (loggingUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(loggingUser.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roleName = await _userManager.GetRolesAsync(user);
                    var role = roleName.FirstOrDefault();

                    if (role == Roles.AdminRole)
                    {
                        return LocalRedirect("/Admin/Home/Index");
                    }

                    if (role == Roles.DOARole)
                    {
                        return LocalRedirect("/DOA/Home/Index");
                    }
                    if (role == Roles.KeellsRole)
                    {
                        return LocalRedirect("/Keells/Home/Index");
                    }
                    if (role == Roles.FarmerRole)
                    {
                        return LocalRedirect("/Farmer/Home/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                ZipCodeAddress latLongData = await GetLatLong(11000);
                var user = new ApplicationUser
                {
                    Email = model.mail,
                    UserName = model.uname,
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    NIC=model.NIC,
                    Address=model.Address,
                    ZipCode=model.ZipCode,
                    PhoneNumber=model.ContactNo,
                    Latitude= latLongData.location[0].latitude,
                    Longitude= latLongData.location[0].longitude
                }; 
                var result = await _userManager.CreateAsync(user, model.Psswd);         

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.FarmerRole);
                    return LocalRedirect("/Home/Account/Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToRoute(new { action = "Login", Controller = "Account", area = "Home" });
        }

        private async Task<ZipCodeAddress> GetLatLong(int zipcode)
        {
            ZipCodeAddress data=new ZipCodeAddress();

            var  reqUrl = $"https://thezipcodes.com/api/v1/search?zipCode={zipcode}&countryCode=LK&apiKey=92987ed2d4e21eb30e056d55c37345e1";
            var request = new HttpRequestMessage(HttpMethod.Get, reqUrl);

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadFromJsonAsync<ZipCodeAddress>();
            }
            return data;
        }
    }
}
