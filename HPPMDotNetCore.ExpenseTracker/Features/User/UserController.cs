// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
//
// namespace HPPMDotNetCore.ExpenseTracker.Features.User
// {
//     public class UserController : Controller
//     {
//         private readonly IUserService _iUserService;
//
//         public UserController(IUserService iUserService)
//         {
//             _iUserService = iUserService;
//         }
//
//         public IActionResult Index()
//         {
//             return View();
//         }
//
//         public async Task<IActionResult> GetUserList(int pageNo, int pageSize)
//         {
//             UserListRespModel respList = new UserListRespModel();
//             respList = await _iUserService.GetUserList(pageNo, pageSize);
//
//             return Json(respList);
//         }
//
//         public IActionResult CreateUser(UserReqModel model = null)
//         {
//             return View(model);
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> SaveUser(UserReqModel model)
//         {
//             long result = await _iUserService.Save(model);
//             if (result > 0)
//             {
//                 TempData["Message"] = "User Creation success!";
//                 return RedirectToAction(nameof(Index));
//             }
//
//             TempData["Message"] = "Creation Error";
//             return RedirectToAction(nameof(CreateUser), model);
//         }
//
//         public async Task<IActionResult> EditUser(string id)
//         {
//             bool isInt = int.TryParse(id, out int UserId);
//             UserRespModel resp = await _iUserService.GetUser(UserId);
//
//             if (resp == null)
//             {
//                 TempData["Message"] = "User Not Found";
//                 return RedirectToAction(nameof(Index));
//             }
//
//             UserReqModel model = resp.Change();
//             return View();
//         }
//
//         [HttpPost]
//         public async Task<IActionResult> UpdateUser(string id, UserReqModel model)
//         {
//             bool isInt = int.TryParse(id, out int UserId);
//
//             int result = await _iUserService.Update(UserId, model);
//
//             TempData["Message"] = result > 0 ?
//                 "User Update Success" : "User Update Error";
//
//             return RedirectToAction(nameof(Index));
//         }
//
//         public async Task<IActionResult> DeleteUser(string id)
//         {
//             bool isInt = int.TryParse(id, out int UserId);
//
//             int result = await _iUserService.Delete(UserId);
//
//             TempData["Message"] = result > 0 ?
//                 "User Delete Success" : "User Delete Error";
//
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
