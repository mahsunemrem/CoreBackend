using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{ 
 public class AuthController : Controller
{
    private IAuthService _authService;
    private IUserService _userService;
    private IMapper _mapper;
    public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
    {
        _authService = authService;
        _userService = userService;
        _mapper = mapper;
    }
    public IActionResult Login()
    {
        return View(null);
    }
    [HttpPost]
    public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {

            return BadRequest(userToLogin.Message);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data);
        if (result.Success)
        {
            var opClaims = _userService.GetClaims(userToLogin.Data);
            var claims = new[] {
                    new Claim(ClaimTypes.Name, userToLogin.Data.UserName),
                    new Claim(ClaimTypes.Role, string.Join(',' ,opClaims.Select(c=>c.Name)))
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        return BadRequest(result.Message);
    }
    public IActionResult Register(int id)
    {
        var model = new UserForRegisterDto();
        if (id !=0)
        {
            var res = _userService.GetById(id);
            if (res.Success)
            {
                if (res.Data != null)
                    model = _mapper.Map<UserForRegisterDto>(res.Data);
            }
        }
        return View(model);
    }
    [HttpPost]
    public IActionResult Register(UserForRegisterDto userForRegisterDto)
    {
        var userExists = _authService.UserExists(userForRegisterDto.Email);
        if (!userExists.Success)
        {
            var updateUser = _authService.UpdateUser(userForRegisterDto, userForRegisterDto.Password);
            if (updateUser.Success)
            {
                return View(_mapper.Map<UserForRegisterDto>(updateUser.Data));
            }
        }

        var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
        if (registerResult.Success)
        {
            return View(_mapper.Map<UserForRegisterDto>(registerResult.Data));
        }

        return View(userForRegisterDto);
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }

}
}