using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.UserType
{
    public interface IUserTypeService
    {
        Task<int> Delete(long id);
        Task<UserTypeRespModel> GetUserType(long id);
        Task<UserTypeListRespModel> GetUserTypeList(int pageNo, int pageSize);
        Task<int> Save(UserTypeReqModel model);
        Task<int> Update(long id, UserTypeReqModel model);
    }
}