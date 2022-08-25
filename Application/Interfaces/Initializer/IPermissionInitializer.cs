namespace Application.Interfaces.Initializer;

public interface IPermissionInitializer
{
    Task Initialize(System.Type baseControllerType);
}