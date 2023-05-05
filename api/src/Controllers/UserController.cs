using Microsoft.AspNetCore.Mvc;
using src.Models.UserModel;
using Microsoft.AspNetCore.Authorization;
using src.Services.UserServices;
using src.Models.ResModel;

namespace src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }
    
    [HttpGet(), Authorize()]
    public ActionResult<Res<User>> GetInfo() {
        int userId = int.Parse(User.Identity?.Name!);
        Res<User> response = _userService.userInfo(userId);

        return response.Err == null ? response : BadRequest(response); 
    }
    [HttpPost]
    public ActionResult<Res<string>> Post(UserDto req)
    {
        Res<string> response = _userService.signUp(req);
        return response.Err == null ? response : BadRequest(response); 
    }

    [HttpPost("login")]
    public ActionResult<Res<string>> post(UserDto req) {
        Res<string> response = _userService.login(req);
        return response.Err == null ? response : BadRequest(response); 
    }

}