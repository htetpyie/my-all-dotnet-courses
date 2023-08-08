using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignIn
{
    public class SignInController : Controller
    {
        private readonly ISignUpService _signUpService;
        private readonly ILogger<SignInController> _logger;

        public SignInController(ISignUpService signUpService,
            ILogger<SignInController> logger)
        {
            _signUpService = signUpService;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(SignInReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                bool signValidate = await _signUpService.IsValidateSign(model);
                if (!signValidate)
                {
                    response = Base.GetError("Username or password is incorrect.");
                }
                else
                {
                    HttpContext.Session.SetString("Id", "Validated");
                    string url = "/Dashboard/Index";
                    response = Base.GetSuccess("Login Success.", url);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Login Error" + ex.Message);
                response = Base.GetError("Something went wrong. Please try again later.");
            }
            return Json(response);
        }
    }
}
