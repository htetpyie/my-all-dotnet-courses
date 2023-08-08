using HPPMDotNetCore.ExpenseTracker.Features.SignUp;
using HPPMDotNetCore.ExpenseTracker.Features.Email;
using HPPMDotNetCore.ExpenseTracker.Features.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignUp
{
    public class SignUpController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISignUpService _signUpService;
        private readonly EmailService _emailService;
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(IUserService userService,
            ISignUpService signUpService,
            EmailService emailService,
            IOptions<AppSetting> appSetting,
            ILogger<SignUpController> logger)
        {
            _userService = userService;
            _signUpService = signUpService;
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserReqModel model)
        {
            MessageResponseModel response = new MessageResponseModel();
            try
            {
                Guid guid = Guid.NewGuid();

                bool duplicate = await _userService.IsDuplicate(model);
                if (duplicate)
                {
                    response = Base.GetError("User name or email already exists.");
                    return Json(response);
                }

                //save user
                long userId = await _userService.Save(model);

                //Save to Signup
                SignUpReqModel signUpReqModel = new SignUpReqModel
                {
                    UserId = userId,
                    RefId = guid.ToString(),
                };
                await _signUpService.Save(signUpReqModel);

                model.RefId = guid.ToString();
                SendSignUpMail(model);

                _logger.LogInformation("Email has been successfully send.");
                response = Base.GetSuccess("Confirmation link is send to your email.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                response = Base.GetError("Something went wrong. Please try again later.");
            }

            return Json(response);
        }

        [NonAction]
        private void SendSignUpMail(UserReqModel user)
        {
            try
            {
                String subjectName = "Please confirm this email:";
                //string htmlForm = @$"<a href='https://localhost:43301/Signup/RecieveMail/{user.RefId}'>Confirm</a>";
                string htmlString = @$"                                                     
								<div style=""font-size:20px"">Hi {user.FullName},</div>

								<div style=""margin:15px 0"">
									Thanks for creating a Expense Tracker Id. Please verify your email address by clicking the button below.
								</div>

								<div style=""padding:10px 0;text-align:left;vertical-align:top"">
                                    <a href=""https://localhost:43301/Signup/RecieveMail?refId={user.RefId}""
                                            style=""box-sizing:border-box;
                                                    text-decoration:none;
                                                    background-color:#007bff;
                                                    border:solid 1px #007bff;
                                                    border-radius:4px;
                                                    color:#ffffff;
                                                    cursor: pointer;
                                                    font-size:16px;font-weight:bold;margin:0;
                                                    padding:9px 25px;display:inline-block;
                                                    letter-spacing:1px"" target=""_blank"">
										Verify email address
									</a>
								</div> ";

                EmailDTO email = new EmailDTO
                {
                    ToMail = user.Email,
                };
                email.ToMail = user.Email;
                email.Subject = subjectName;
                email.Body = htmlString;

                _emailService.SendEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> RecieveMail(string refId)
        {
            bool isValidate = await _signUpService.IsValidateRefId(refId);

            //Update Signup Status
            if (isValidate)
            {
                var signUp = await _signUpService.GetItem(refId);
                var user = await _userService.GetUser(signUp.UserId);

                SignUpReqModel request = new SignUpReqModel
                {
                    RefId = refId,
                    IsConfirmed = true
                };

                await _signUpService.Update(request);
                await _userService.UpdateRegisterStatus(
                    user.UserId, EnumRegistrationType.Registered);
            }

            TempData["Message"] = "Your Registration Success! Please Login";
            return Redirect("/SignIn/Login");
        }
    }
}