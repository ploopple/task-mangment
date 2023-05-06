using System.Security.Claims;
using System.Text;
using src.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using src.Models.UserModel;
using src.Models.ResModel;


namespace src.Services.UserServices;

public class UserService: IUserService
{
    private readonly DataContext _context;

    private readonly IConfiguration _config;

    public UserService(IConfiguration config, DataContext context)
    {
        _context = context;
        _config = config;
    }

    private string CreateToken(string id)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, id)
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:Token")!));
        SigningCredentials cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public Res<User> userInfo(int userId) {
        Res<User> response = new ();
        // int userId = int.Parse(User.Identity?.Name!);
        // Console.WriteLine()
        User? user = _context.Users?.Find(userId);
        if(user == null) {
            response.Err = "user does not exsist";
            return  response;
        }
        response.Data = user;
        return response; 
    }
    public Res<string> signUp(UserDto req) {
        Res<string> response = new Res<string>();
        if(_context.Users?.FirstOrDefault(user => user.Email == req.Email) == null) {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);
            User newUser = new User { Username = req.Username, Email = req.Email, Password = hashPassword };
            _context.Users?.Add(newUser);
            _context.SaveChanges();
            response.Data = CreateToken(newUser.Id+"");
            return response;
        }

        response.Err = "user already exist.";
        return response;
    }

    public Res<string> login(UserDto req) {
        Res<string> response = new Res<string>();
        User user = _context.Users?.FirstOrDefault(user => user.Email == req.Email)!;
        if(user == null) {
            response.Err = "The username and/or password you specified are not correct.";
            return response;
        }
        if(!BCrypt.Net.BCrypt.Verify(req.Password, user.Password)){
            response.Err = "The username and/or password you specified are not correct.";
            return response;
        }
            response.Data = CreateToken(user.Id+"");
            return response;
    }


}
