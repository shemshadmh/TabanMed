namespace Application.Interfaces.Application
{
    public interface ICurrentServices
    {
        string? Username { get; }
        string? LanguageName { get; }
        int? LanguageId { get; }
    }
}
