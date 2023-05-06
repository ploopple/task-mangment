using src.Models.UserModel;
using src.Services.UserServices;
using FakeItEasy;
using src.Models.ResModel;

namespace tests.UserControllerTests;

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
        Res<string> res = new() { Data = "Token" };
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
        Res<string> res = new() { Err = "user already exist" };
        UserDto req = new UserDto { Username = "King", Email = "king@mail.com", Password = "king123" };
        A.CallTo(() => _userService.signUp(req)).Returns(res);

        // Act
        var result = _userService.signUp(req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("user already exist", result.Err);
    }
    [Fact]
    public void Login_WithValidUser_ReturnResString()
    {
        // Arrange
        Res<string> res = new() { Data = "Token" };
        UserDto req = new UserDto {  Email = "king@mail.com", Password = "king123" };
        A.CallTo(() => _userService.login(req)).Returns(res);

        // Act
        var result = _userService.login(req);

        // Assert
        Assert.Null(result.Err);
        Assert.NotNull(result.Data);
        Assert.Equal("Token", result.Data);
    }

    [Fact]
    public void Login_WithNotValidUser_Email_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "The username and/or password you specified are not correct." };
        UserDto req = new UserDto {  Email = "kssing@mail.com", Password = "king124rfefw" };
        A.CallTo(() => _userService.login(req)).Returns(res);

        // Act
        var result = _userService.login(req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("The username and/or password you specified are not correct.", result.Err);
    }
    public void Login_WithNotValidUser_Pass_ReturnResStringErr()
    {
        // Arrange
        Res<string> res = new() { Err = "The username and/or password you specified are not correct." };
        UserDto req = new UserDto {  Email = "king@mail.com", Password = "king124rfefw" };
        A.CallTo(() => _userService.login(req)).Returns(res);

        // Act
        var result = _userService.login(req);

        // Assert
        Assert.Null(result.Data);
        Assert.NotNull(result.Err);
        Assert.Equal("The username and/or password you specified are not correct.", result.Err);
    }

}