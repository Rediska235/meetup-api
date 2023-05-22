using AutoMapper;
using MeetupAPI.Application.DTOs;
using MeetupAPI.Application.Repositories;
using MeetupAPI.Application.Services.Interfaces;
using MeetupAPI.Domain;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace MeetupAPI.Application.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository repository, 
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public OrganizerDto Register(UserEntryDto userEntryDto)
    {
        var user = _repository.GetUserByUsername(userEntryDto.Username);
        if (user != null)
        {
            throw Exceptions.UsernameIsTaken;
        }

        user = new User()
        {
            Username = userEntryDto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userEntryDto.Password)
        };

        _repository.InsertUser(user);
        _repository.Save();

        return _mapper.Map<OrganizerDto>(user);
    }

    public string Login(UserEntryDto userEntryDto, string secretKey)
    {
        var user = _repository.GetUserByUsername(userEntryDto.Username);
        if (user == null)
        {
            throw Exceptions.InvalidCredential;
        }

        var isValidPassword = BCrypt.Net.BCrypt.Verify(userEntryDto.Password, user.PasswordHash);
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
