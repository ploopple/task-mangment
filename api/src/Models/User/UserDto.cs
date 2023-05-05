using System.ComponentModel.DataAnnotations;
using src.Models.ProjectModel;

namespace src.Models.UserModel;

public class UserDto {
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}