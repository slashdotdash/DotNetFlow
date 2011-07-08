namespace DotNetFlow.Core.ReadModel.Queries
{
    public interface IFindExistingEmailAddress
    {
        bool Exists(string email);
    }
}