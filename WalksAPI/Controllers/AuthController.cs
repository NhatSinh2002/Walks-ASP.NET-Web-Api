﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WalksAPI.Models.DTO;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email=registerRequestDto.Username
            };
           var identityResult= await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                //add role to this user
                if (registerRequestDto.Roles != null&& registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered!");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }
    }
}
