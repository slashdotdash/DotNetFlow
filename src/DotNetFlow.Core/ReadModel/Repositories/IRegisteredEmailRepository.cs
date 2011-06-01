namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IRegisteredEmailRepository
    {
        bool Exists(string email);
    }
}