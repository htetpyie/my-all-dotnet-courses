using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using HPPMDotNetCore.ExpenseTracker.Services;
using System.Linq;

namespace HPPMDotNetCore.ExpenseTracker.Features.UserType
{
    public class UserTypeService : IUserTypeService
    {
        private readonly EfDbContext _context;

        public UserTypeService(EfDbContext context)
        {
            _context = context;
        }

        public async Task<UserTypeRespModel> GetUserType(long id)
        {
            var data = await _context
                .UserType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserTypeId == id);

            var model = data.Change();

            return model;
        }

        public async Task<UserTypeListRespModel> GetUserTypeList(int pageNo, int pageSize)
        {
            var dataList = await _context
                .UserType
                .AsNoTracking()
                .Pagination(pageNo, pageSize)
                .ToListAsync();
            var modelList = dataList.Select(x => x.Change()).ToList();

            return new UserTypeListRespModel
            {
                UserTypeList = modelList
            };
        }

        public async Task<int> Save(UserTypeReqModel model)
        {
            UserTypeDataModel data = model.Change();
            data.CreatedDate = DateTime.Now;

            await _context.UserType.AddAsync(data);
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Update(long id, UserTypeReqModel model)
        {
            var data = await _context
                .UserType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserTypeId == id);
            if (data == null) return 0;

            data.UserTypeName = model.UserTypeName;
            data.UserTypeOrder = model.UserTypeOrder;
            data.ModifiedDate = DateTime.Now;

            _context.UserType.Update(data);
            _context.Entry(data).State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(long id)
        {
            var data = await _context
                .UserType
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserTypeId == id);
            if (data == null) return 0;

            data.ModifiedDate = DateTime.Now;
            data.IsDelete = true;

            _context.UserType.Update(data);
            _context.Entry(data).State = EntityState.Deleted;
            int result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
