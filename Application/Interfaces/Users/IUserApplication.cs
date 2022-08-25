namespace Application.Interfaces.Users;

public interface IUserApplication
{
    Task<string?> GetUserSecurityStamp(string userId,CancellationToken cancellationToken=default);
}