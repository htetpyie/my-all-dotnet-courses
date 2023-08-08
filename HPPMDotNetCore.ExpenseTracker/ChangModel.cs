using HPPMDotNetCore.ExpenseTracker.Features;
using HPPMDotNetCore.ExpenseTracker.Features.Expense;
using HPPMDotNetCore.ExpenseTracker.Features.Income;
using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Features.User;
using HPPMDotNetCore.ExpenseTracker.Features.UserType;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Linq;

namespace HPPMDotNetCore.ExpenseTracker
{
    public static class ChangModel
    {

        #region Expense
        public static ExpenseDataModel Change(this ExpenseReqModel req)
        {
            if (req == null) return new ExpenseDataModel();
            return new ExpenseDataModel
            {
                ExpenseId = req.ExpenseId,
                IncomeId = req.IncomeId,
                ExpenseName = req.ExpenseName,
                ExpenseAmount = req.ExpenseAmount,
                UserId = req.UserId,
            };
        }

        public static ExpenseRespModel Change(this ExpenseDataModel data)
        {
            if (data == null) return new ExpenseRespModel();
            return new ExpenseRespModel
            {
                ExpenseId = data.ExpenseId,
                ExpenseName = data.ExpenseName,
                ExpenseAmount = data.ExpenseAmount,
                UserId = data.UserId,
                IncomeId = data.IncomeId,
            };
        }

        public static ExpenseReqModel Change(this ExpenseRespModel resp)
        {
            if (resp == null) return new ExpenseReqModel();
            return new ExpenseReqModel
            {
                ExpenseId = resp.ExpenseId,
                ExpenseName = resp.ExpenseName,
                ExpenseAmount = resp.ExpenseAmount,
                UserId = resp.UserId,
                IncomeId = resp.IncomeId,
                IncomeName = resp.IncomeName
            };
        }

        public static DataTableResponseModel<ExpenseRespModel> Change
            (this DataTableResponseModel<ExpenseDataModel> resp)
        {
            if (resp == null) return null;
            return new DataTableResponseModel<ExpenseRespModel>
            {
                draw = resp.draw,
                recordsFiltered = resp.recordsFiltered,
                recordsTotal = resp.recordsTotal,
                data = resp.data
                        .Select(x => x.Change()).ToList()
            };
        }

        #endregion

        #region Income
        public static IncomeDataModel Change(this IncomeReqModel req)
        {
            if (req == null) return null;
            return new IncomeDataModel
            {
                IncomeId = req.IncomeId,
                IncomeName = req.IncomeName,
                IncomeAmount = req.IncomeAmount,
                UserId = req.UserId,
            };
        }

        public static IncomeRespModel Change(this IncomeDataModel data)
        {
            if (data == null) return null;
            return new IncomeRespModel
            {
                IncomeId = data.IncomeId,
                IncomeName = data.IncomeName,
                IncomeAmount = data.IncomeAmount,
                UserId = data.UserId,
            };
        }

        public static IncomeReqModel Change(this IncomeRespModel resp)
        {
            if (resp == null) return new IncomeReqModel();
            return new IncomeReqModel
            {
                IncomeId = resp.IncomeId,
                IncomeName = resp.IncomeName,
                IncomeAmount = resp.IncomeAmount,
                UserId = resp.UserId,
            };
        }

        public static DataTableResponseModel<IncomeRespModel> Change
            (this DataTableResponseModel<IncomeDataModel> resp)
        {
            if (resp == null) return null;
            return new DataTableResponseModel<IncomeRespModel>
            {
                draw = resp.draw,
                recordsFiltered = resp.recordsFiltered,
                recordsTotal = resp.recordsTotal,
                data = resp.data
                        .Select(x=> x.Change()).ToList()
            };
        }
        #endregion

        #region User
        public static UserDataModel Change(this UserReqModel model)
        {
            if (model == null) return default;
            return new UserDataModel
            {
                UserId = model.UserId,
                UserTypeId = model.UserTypeId,
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                UserRegistrationStatus = Convert.ToInt32(model.UserRegistrationType)
            };
        }

        public static UserRespModel Change(this UserDataModel data)
        {
            if (data == null) return null;
            return new UserRespModel
            {
                UserId = data.UserId,
                UserTypeId = data.UserTypeId,
                UserName = data.UserName,
                FullName = data.FullName,
                Email = data.Email,
                Password = data.Password
            };
        }

        public static UserReqModel Change(this UserRespModel resp)
        {
            if (resp == null) return null;
            return new UserReqModel
            {
                UserId = resp.UserId,
                UserTypeId = resp.UserTypeId,
                UserName = resp.UserName,
                FullName = resp.FullName,
                Email = resp.Email,
                Password = resp.Password
            };
        }
        #endregion

        #region UserType
        public static UserTypeDataModel Change(this UserTypeReqModel model)
        {
            if (model == null) return null;
            return new UserTypeDataModel
            {
                UserTypeId = model.UserTypeId,
                UserTypeName = model.UserTypeName,
                UserTypeOrder = model.UserTypeOrder,
            };
        }
        public static UserTypeRespModel Change(this UserTypeDataModel data)
        {
            if (data == null) return null;
            return new UserTypeRespModel
            {
                UserTypeId = data.UserTypeId,
                UserTypeName = data.UserTypeName,
                UserTypeOrder = data.UserTypeOrder,
            };
        }
        public static UserTypeReqModel Change(this UserTypeRespModel resp)
        {
            if (resp == null) return null;
            return new UserTypeReqModel
            {
                UserTypeId = resp.UserTypeId,
                UserTypeName = resp.UserTypeName,
                UserTypeOrder = resp.UserTypeOrder,
            };
        }
		#endregion

		#region SignUp
        public static SignUpDataModel Change(this SignUpReqModel model)
        {
            if(model == null) return default;
            return new SignUpDataModel
            {
                UserId = model.UserId,
                RefId = model.RefId,
                IsConfirmed = model.IsConfirmed,
            };
        }
		#endregion
	}
}
