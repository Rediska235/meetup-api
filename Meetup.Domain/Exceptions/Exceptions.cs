using System.Security.Authentication;

namespace MeetupAPI.Domain.Exceptions;

public class Exceptions
{
    public static NotFoundException MeetupNotFound = new("Meetup not found.");

    public static AlreadyExistsException MeetupAlreadyExists = new("Meetup with this name already exists.");
    public static AlreadyExistsException usernameIsTaken = new("Username is taken.");

    public static InvalidCredentialException invalidCredential = new("Wrong username or password");
    public static InvalidCredentialException invalidRefreshToken = new("Invalid refresh token");
}
