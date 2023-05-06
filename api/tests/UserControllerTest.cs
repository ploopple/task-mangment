using src.Controllers;
using src.Models.UserModel;
using src.Services.UserServices;
using FakeItEasy;
using src.Models.ResModel;


namespace tests.UserControllerTests
{

    public class UserControllerTest
    {
        public readonly IUserService _userService;
        public readonly UserController _controller;
        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
            _controller = new UserController(_userService);
        }


        [Fact]
        public void SignUP_WithValidUser_ReturnResString()
        {

            // Arrange
            Res<string> res = new () {Data = "Token"};
            UserDto req = new UserDto { Username = "King", Email = "king@mail.com", Password = "king123" };
            A.CallTo(() => _userService.signUp(req)).Returns(res);

            // Act
            var result = _controller.SignUp(req);

            // Assert
            Assert.Equal(res, result.Value);

        }

    }
}