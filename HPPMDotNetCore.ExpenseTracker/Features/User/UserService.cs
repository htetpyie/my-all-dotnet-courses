using HPPMDotNetCore.ExpenseTracker.Features.Income;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using HPPMDotNetCore.ExpenseTracker.Services;
using System.Linq;

namespace HPPMDotNetCore.ExpenseTracker.Features.User
{
    public class UserService : IUserService
    {
        private readonly EfDbContext _context;

        public UserService(EfDbContext context)
        {
            _context = context;
        }

        public async Task<UserRespModel> GetUser(long id)
        {
            var data = await _context
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsDelete == false && x.UserId == id);

            var model = data.Change();

            return model;
        }

        public async Task<UserListRespModel> GetUserList(int pageNo, int pageSize)
        {
            var dataList = await _context
                .User
                .AsNoTracking()
                .Pagination(pageNo, pageSize)
                .ToListAsync();
            var modelList = dataList.Select(x => x.Change()).ToList();

            return new UserListRespModel
            {
                UserList = modelList
            };
        }

        public async Task<bool> IsDuplicate(UserReqModel model)
        {
            UserDataModel data = model.Change();
            bool userExist = await _context
                .User
                .AsNoTracking()
                .AnyAsync(x => (x.UserName == data.UserName || x.Email == data.Email));
            return userExist;
        }

        public async Task<long> Save(UserReqModel model)
        {
            UserDataModel data = model.Change();
            data.CreatedDate = DateTime.Now;
            await _context.User.AddAsync(data);
            await _context.SaveChangesAsync();

            return data.UserId;
        }

        public async Task<int> Update(long id, UserReqModel model)
        {
            var data = await _context
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == id);
            if (data == null) return 0;

            data.UserTypeId = model.UserTypeId;
            data.UserName = model.UserName;
            data.FullName = model.FullName;
            data.Email = model.Email;
            data.Password = model.Password;
            data.ModifiedDate = DateTime.Now;

            _context.User.Update(data);
            _context.Entry(data).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(long id)
        {
            var data = await _context
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == id);
            if (data == null) return 0;

            data.ModifiedDate = DateTime.Now;
            data.IsDelete = true;

            _context.User.Update(data);
            _context.Entry(data).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> UpdateRegisterStatus(long id,
            EnumRegistrationType registerType)
        {
            var user = await _context
                .User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsDelete == false && x.UserId == id);

            user.UserRegistrationStatus = Convert.ToInt32(registerType);
            user.ModifiedDate = DateTime.Now;

            _context.User.Update(user);
            _context.Entry(user).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
