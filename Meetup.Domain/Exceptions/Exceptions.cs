namespace MeetupAPI.Domain.Exceptions;

public class Exceptions
{
    public static NotFoundException MeetupNotFound = new("Meetup not found.");

    public static AlreadyExistsException MeetupAlreadyExists = new("Meetup with this name already exists.");
}
