using System.Security.Authentication;

namespace MeetupAPI.Domain.Exceptions;

public class Exceptions
{
    public static NotFoundException MeetupNotFound = new("Meetup not found.");

    public static AlreadyExistsException MeetupAlreadyExists = new("Meetup with this name already exists.");
    public static AlreadyExistsException UsernameIsTaken = new("Username is taken.");

    public static InvalidCredentialException InvalidCredential = new("Wrong username or password");
    public static InvalidCredentialException InvalidRefreshToken = new("Invalid refresh token");

    public static NotAllowedException NotYourMeetup = new("You cannot interact with this meetup");
}
