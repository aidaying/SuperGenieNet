using AutoMapper;
using Genie.Core.Entities.Identity;
using Genie.Core.Interfaces;
using GenieAPI.DTOs;
using GenieAPI.Errors;
using GenieAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GenieAPI.Controllers;

public class AccountController : BaseAPIController
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper) {
        _mapper = mapper;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser() {
      
        var user = await _userManager.FindByEmailDromClaimsPrinciple(User);
        return new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> checkEmailExistsAsync([FromQuery] string email) { 
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> getUserAddress()
    {
       
        var user = await _userManager.FindUserByClaimsPrincipleWithAddreass(User);
        return _mapper.Map<Address, AddressDto>(user.Address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> updateUserAddress(AddressDto address) {
        var user = await _userManager.FindUserByClaimsPrincipleWithAddreass(User);
        user.Address = _mapper.Map<AddressDto, Address>(address);
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        return BadRequest("Problem updating the user");
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new APIResponse(401));
        var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);
        if (!result.Succeeded) return Unauthorized(new APIResponse(401));
        return new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
        if (checkEmailExistsAsync(registerDto.Email).Result.Value) return new BadRequestObjectResult(new APIValidationErrorResponse { Errors = new[] { "Email is already in use." } });
        var user = new AppUser {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(new APIResponse(400));
        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user),
            Email = user.Email
        };
    }
    }
