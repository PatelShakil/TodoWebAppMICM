namespace TodoWebApp.Services
{
    // Services/Interfaces/IStorageStrategy.cs
    public interface IStorageStrategy
    {
        ITodoService GetService(string storageType);
    }

}
