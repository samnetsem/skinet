using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Extensions;

namespace API.Controllers 
{
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
     
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
        ITokenService tokenService,IMapper mapper)
        {
             _mapper = mapper;
           
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService=tokenService;
             
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            

            var user= await _userManager.FindByEmailFromClamisPrinciple(HttpContext.User);

             return new UserDto
             {
                Email=user.Email,
                Token=_tokenService.CreateToken(user),
                DisplayName=user.DisplayName
             };
            
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) !=null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            

             var user= await _userManager.FindByUserByClamisPrincipleWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address,AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
         public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
         {
            var user= await _userManager.FindByUserByClamisPrincipleWithAddressAsync(HttpContext.User);
            user.Address=_mapper.Map<AddressDto,Address>(address);

            var result= await _userManager.UpdateAsync(user);
            if(result.Succeeded)
              return Ok(_mapper.Map<Address,AddressDto>(user.Address));

             else
                 return BadRequest("Problem Updating the user");
           
         }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _userManager.FindByEmailAsync(loginDto.Email);

            if(user ==null) return Unauthorized(new ApiResponse(401));


            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
             if(!result.Succeeded) return Unauthorized(new ApiResponse(401));

             return new UserDto
             {
                Email=user.Email,
                Token=_tokenService.CreateToken(user),
                DisplayName=user.DisplayName
             };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors=new[]{"Email address is in use"}});
            }
            var user =new AppUser
            {
                DisplayName=registerDto.DisplayName,
                Email=registerDto.Email,
                UserName=registerDto.Email
            };
            var result = await _userManager.CreateAsync(user,registerDto.Password);
            
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName=user.DisplayName,
                Token=_tokenService.CreateToken(user),
                Email=user.Email
            };
        }
    }
}