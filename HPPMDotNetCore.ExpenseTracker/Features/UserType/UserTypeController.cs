// using HPPMDotNetCore.ExpenseTracker.Features.Income;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
//
// namespace HPPMDotNetCore.ExpenseTracker.Features.UserType
// {
//     public class UserTypeController : Controller
//     {
//         private readonly IUserTypeService _iUserTypeService;
//
//         public UserTypeController(IUserTypeService iUserTypeService)
//         {
//             _iUserTypeService = iUserTypeService;
//         }
//
//         public IActionResult Index()
//         {
//             return View();
//         }
//
//         public async Task<IActionResult> GetUserTypeList(int pageNo, int pageSize)
//         {
//             UserTypeListRespModel respList = new UserTypeListRespModel();
//             respList = await _iUserTypeService.GetUserTypeList(pageNo, pageSize);
//
//             return Json(respList);
//         }
//
//         public IActionResult CreateUserType(UserTypeReqModel model = null)
//         {
//             return View(model);
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> SaveUserType(UserTypeReqModel model)
//         {
//             int result = await _iUserTypeService.Save(model);
//             if (result > 0)
//             {
//                 TempData["Message"] = "UserType Creation success!";
//                 return RedirectToAction(nameof(Index));
//             }
//
//             TempData["Message"] = "Creation Error";
//             return RedirectToAction(nameof(CreateUserType), model);
//         }
//
//         public async Task<IActionResult> EditUserType(string id)
//         {
//             bool isInt = int.TryParse(id, out int UserTypeId);
//             UserTypeRespModel resp = await _iUserTypeService.GetUserType(UserTypeId);
//
//             if (resp == null)
//             {
//                 TempData["Message"] = "UserType Not Found";
//                 return RedirectToAction(nameof(Index));
//             }
//
//             UserTypeReqModel model = resp.Change();
//             return View();
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> UpdateUserType(string id, UserTypeReqModel model)
//         {
//             bool isInt = int.TryParse(id, out int UserTypeId);
//
//             int result = await _iUserTypeService.Update(UserTypeId, model);
//
//             TempData["Message"] = result > 0 ?
//                 "UserType Update Success" : "UserType Update Error";
//
//             return RedirectToAction(nameof(Index));
//         }
//
//         public async Task<IActionResult> DeleteUserType(string id)
//         {
//             bool isInt = int.TryParse(id, out int UserTypeId);
//
//             int result = await _iUserTypeService.Delete(UserTypeId);
//
//             TempData["Message"] = result > 0 ?
//                 "UserType Delete Success" : "UserType Delete Error";
//
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
