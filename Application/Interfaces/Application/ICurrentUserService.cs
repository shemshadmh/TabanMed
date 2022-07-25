namespace Application.Interfaces.Application
{
    public interface ICurrentUserService
    {
        string? Username { get; }
        string? UserId { get; }
    }
}
