namespace Application.Interfaces.Initializer;

public interface IDbInitializer
{
    Task Initialize(Type baseControllerType, bool isDevelopment = false);
}