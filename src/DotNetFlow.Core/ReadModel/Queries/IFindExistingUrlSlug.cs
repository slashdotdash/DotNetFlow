namespace DotNetFlow.Core.ReadModel.Queries
{
    public interface IFindExistingUrlSlug
    {
        bool Exists(string slug);
    }
}