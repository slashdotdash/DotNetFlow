namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IRegisteredUsernameRepository
    {
        bool Exists(string username);
    }
}