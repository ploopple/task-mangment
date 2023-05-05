using System;
using src.Models.UserModel;

using src.Models.ResModel;
namespace src.Services.UserServices
{
	public interface IUserService
	{
		Res<string> signUp(UserDto req);
		Res<User> userInfo(int userId);
		Res<string> login(UserDto req);
	}
}
