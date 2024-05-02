using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using dotnet_web_api.Data;
using dotnet_web_api.Dtos.Token;
using dotnet_web_api.Dtos.Users;
using dotnet_web_api.Interfaces;
using dotnet_web_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        
        
        private readonly ITokenService _tokenService;
        public UserController(ApplicationDBContext context,ITokenService tokenService)
        {
            _context=context;
            _tokenService=tokenService;
            
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Users users=new Users(){
                Fullname=userRegister.Fullname,
                Email=userRegister.Email,
                Username=userRegister.Username,
                Password=userRegister.Password
            };
            await _context.users.AddAsync(users);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var CheckUser=_context.users.Any(u=>u.Username==userLogin.Username && u.Password==userLogin.Password);
            if(!CheckUser)
            {
                return BadRequest("Invalid user");
            }


            var user=_context.users.Where(u=>u.Username==userLogin.Username).First();

            TokenModel tokenModel=new TokenModel();
            tokenModel.AcccessToken=_tokenService.CreateToken(user);
            tokenModel.RefreshToken=_tokenService.CreateRefreshToken();
            // Refresh Token
            RefreshToken refreshToken=new RefreshToken();
            refreshToken.UserId=user.Id;
            refreshToken.Token=tokenModel.RefreshToken;
            // 
            if(_context.refreshTokens.Any(r=>r.UserId==user.Id)==false)
            {
                await _context.refreshTokens.AddAsync(refreshToken);
            }
            else{
                var refesh=_context.refreshTokens.Where(r=>r.UserId==user.Id).First();
                _context.Entry(refesh).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                refesh.Token=tokenModel.RefreshToken;
            }
            await _context.SaveChangesAsync();
            return Ok(
                new {
                    Username=userLogin.Username,
                    tokenModel
                }
            );
        }
        
        [HttpPost("RenewTokenLogin")]
        public IActionResult RenewTokenLogin(TokenModel model)
        {
            var token=_tokenService.RenewToken(model);
            if(token=="error")
            {
                return BadRequest("Invalid Token");
            }
            return Ok(token);


        }
        
    }
    
}