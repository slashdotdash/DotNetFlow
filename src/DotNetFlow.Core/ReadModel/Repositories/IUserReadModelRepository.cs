using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IUserReadModelRepository : IReadModelRepository<UserAccountModel>
    {
        UserAccountModel FindByUsernameOrEmail(string email);
    }
}