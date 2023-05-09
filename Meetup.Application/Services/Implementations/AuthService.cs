using MeetupAPI.Application.Repositories;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Domain.Exceptions;
using MeetupAPI.Domain;
using Microsoft.AspNetCore.Http;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Services.Interfaces;

namespace MeetupAPI.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public User Register(AuthUserDto request)
    {
        var user = _repository.GetUserByUsername(request.Username);
        if (user != null)
        {
            throw Exceptions.UsernameIsTaken;
        }

        user = new User()
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _repository.InsertUser(user);
        _repository.Save();

        return user;
    }

    public string Login(AuthUserDto request, string secretKey)
    {
        var user = _repository.GetUserByUsername(request.Username);
        if (user == null)
        {
            throw Exceptions.InvalidCredential;
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (isValidPassword == false)
        {
            throw Exceptions.InvalidCredential;
        }

        string token = JwtManager.CreateToken(user, secretKey);

        RefreshToken refreshToken = JwtManager.GenerateRefreshToken();
        JwtManager.SetRefreshToken(refreshToken, _httpContextAccessor.HttpContext, user);
        _repository.UpdateUser(user);
        _repository.Save();

        return token;
    }

    public string RefreshToken(string username, string secretKey)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        var user = _repository.GetUserByRefreshToken(refreshToken);
        if (user == null || user.Username != username)
        {
            throw Exceptions.InvalidRefreshToken;
        }

        string token = JwtManager.CreateToken(user, secretKey);

        var newRefreshToken = JwtManager.GenerateRefreshToken();
        JwtManager.SetRefreshToken(newRefreshToken, _httpContextAccessor.HttpContext, user);
        _repository.UpdateUser(user);
        _repository.Save();

        return token;
    }
}
