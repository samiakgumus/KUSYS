using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Dtos;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Extensions;

namespace CemeteryPortal.Controllers
{

    public class AccountController : Controller
    {
        IAuthService _authService;
        IRoleService _roleService;
        IUserService _userService;
        public AccountController(IAuthService authService, IUserService userService, IRoleService roleService)
        {
            _authService = authService;
            _userService = userService;
            _roleService = roleService;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (userToLogin.Success)
            {
                ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(userToLogin.Data), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }

            return Json(userToLogin);


        }












        public async Task<IActionResult> SignOut()
        {

            await HttpContext.SignOutAsync();
            return Redirect("/");

        }



        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.UserName));

            var dataRole=_roleService.Get(user.Role);
            if (dataRole!=null)
            {
                claims.Add(new Claim(ClaimTypes.Role, dataRole.Name));
            }

            return claims;
        }
    }
}