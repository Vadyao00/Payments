using Paymnets.Orders.Application.Models.Authentication;

namespace Paymnets.Orders.Application.Abstractions;

public interface IAuthService
{
    Task<UserResponse> Register(UserRegisterDto userRegisterModel);
    Task<UserResponse> Login(UserLoginDto userRegisterDto);
}