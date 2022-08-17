namespace Application.Interfaces.Application
{
    public interface ICurrentServices
    {
        string? Username { get; }
        string LanguageDisplayName { get; }
        int LanguageId { get; }
    }
}
