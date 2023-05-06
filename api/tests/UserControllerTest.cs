using src.Controllers;
using src.Models.UserModel;
using src.Services.UserServices;
using FakeItEasy;
using src.Models.ResModel;
using Microsoft.AspNetCore.Mvc;

namespace tests.UserControllerTests
{

    public class UserControllerTest
    {
        public readonly IUserService _userService;
        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
        }


        [Fact]
        public void SignUP_WithValidUser_ReturnResString()
        {

            // Arrange
            Res<string> res = new () {Data = "Token"};
            UserDto req = new UserDto { Username = "King", Email = "king@mail.com", Password = "king123" };
            A.CallTo(() => _userService.signUp(req)).Returns(res);

            // Act
            var result = _userService.signUp(req);

            // Assert
            Assert.Null(result.Err);
            Assert.NotNull(result.Data);
            Assert.Equal("Token", result.Data);


        }

        [Fact]
        public void SignUP_WithNotValidUser_ReturnResStringErr()
        {

            // Arrange
            Res<string> res = new() { Err = "Email already exist" };
            UserDto req = new UserDto { Username = "King", Email = "king@mail.com", Password = "king123" };
            A.CallTo(() => _userService.signUp(req)).Returns(res);

            // Act
            var result = _userService.signUp(req);

            // Assert
            Assert.Null(result.Data);
            Assert.NotNull(result.Err);
            Assert.Equal( "Email already exist", result.Err);

        }

    }
}