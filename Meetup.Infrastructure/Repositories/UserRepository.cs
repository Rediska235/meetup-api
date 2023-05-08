using MeetupAPI.Application.Repositories;
using MeetupAPI.Domain.Entities;
using MeetupAPI.Infrastructure.Data;

namespace MeetupAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<User> GetUsers()
    {
        return _db.Users;
    }

    public User GetUserByUsername(string username)
    {
        return _db.Users.FirstOrDefault(u => u.Username == username);
    }

    public User GetUserByRefreshToken(string refreshToken)
    {
        return _db.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
    }

    public void InsertUser(User user)
    {
        _db.Add(user);
    }

    public void UpdateUser(User user)
    {
        _db.Update(user);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
