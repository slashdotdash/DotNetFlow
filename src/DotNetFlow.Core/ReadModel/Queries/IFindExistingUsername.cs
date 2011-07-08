namespace DotNetFlow.Core.ReadModel.Queries
{
    public interface IFindExistingUsername
    {
        bool Exists(string username);
    }
}