using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.User
{
    public interface IUserService
    {
        Task<int> Delete(long id);
        Task<UserRespModel> GetUser(long id);
        Task<UserListRespModel> GetUserList(int pageNo, int pageSize);
        Task<long> Save(UserReqModel model);
        Task<int> Update(long id, UserReqModel model);
        Task<int> UpdateRegisterStatus(long id, EnumRegistrationType registerType);
        Task<bool> IsDuplicate(UserReqModel model);
    }
}