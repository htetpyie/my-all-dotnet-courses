using HPPMDotNetCore.ExpenseTracker.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignUp
{
    public class SignUpService : ISignUpService
    {
        private readonly EfDbContext _dbContext;

        public SignUpService(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SignUpDataModel> GetItem(string refId)
        {
            SignUpDataModel data = await _dbContext
                .SignUp
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RefId.Equals(refId));
            return data;
        }

        public async Task<int> Save(SignUpReqModel model)
        {
            SignUpDataModel data = model.Change();
            data.CreatedDate = DateTime.Now;
            data.CreatedBy = 1;

            await _dbContext.SignUp.AddAsync(data);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Update(SignUpReqModel model)
        {
            SignUpDataModel data = await _dbContext
                .SignUp
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.RefId.Equals(model.RefId));

            if (data == null) return 0;

            data.ModifiedDate = DateTime.Now;
            data.IsConfirmed = model.IsConfirmed;
            data.ModifiedBy = 1;

            _dbContext.SignUp.Update(data);
            _dbContext.Entry(data).State = EntityState.Modified;

            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> IsValidateRefId(string refId)
        {
            var data = await GetItem(refId);
            return data != null;
        }

        public async Task<bool> IsValidateSign(SignInReqModel model)
        {
            int registered = Convert.ToInt32(EnumRegistrationType.Registered);
            try
            {
                var user = await _dbContext
               .User
               .AsNoTracking()
               .FirstOrDefaultAsync(x =>
                   x.IsDelete == false &&
                   // x.UserRegistrationStatus == registered &&
                   (x.Email.Equals(model.UserNameOrEmail) || x.UserName.Equals(model.UserNameOrEmail)) &&
                   x.Password.Equals(model.Password));
                return user != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
