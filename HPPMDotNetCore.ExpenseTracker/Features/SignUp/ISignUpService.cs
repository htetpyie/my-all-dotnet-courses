using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignUp
{
	public interface ISignUpService
	{
		Task<SignUpDataModel> GetItem(string id);
		Task<int> Save(SignUpReqModel model);
		Task<int> Update(SignUpReqModel model);
		Task<bool> IsValidateRefId(string refId);
		Task<bool> IsValidateSign(SignInReqModel model);
    }
}