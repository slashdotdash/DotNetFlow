using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IUserRepository : IRepository<UserAccountModel>
    {
        UserAccountModel FindByEmail(string email);
    }
}