using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace HPPMDotNetCore.ExpenseTracker.Features.Email
{
	public class EmailService
	{
		private readonly EmailSetting _mailSetting;
		private readonly ILogger<EmailService> _logger;

		public EmailService(IOptions<EmailSetting> mailSetting,
			ILogger<EmailService> logger)
		{
			_mailSetting = mailSetting.Value;
			_logger = logger;
		}

		public void SendEmail(EmailDTO emailDTO)
		{
			try
			{
				MailMessage mail = new MailMessage();
				mail.BodyEncoding = System.Text.Encoding.UTF8;
				mail.HeadersEncoding = System.Text.Encoding.UTF8;
				mail.SubjectEncoding = System.Text.Encoding.UTF8;
				mail.Priority = System.Net.Mail.MailPriority.High;
				mail.IsBodyHtml = true;

				mail.From = new MailAddress(_mailSetting.Mail);
				mail.To.Add(new MailAddress(emailDTO.ToMail));
				mail.Subject = emailDTO.Subject;
				mail.Body = emailDTO.Body;

				SmtpClient smtp = new SmtpClient();
				NetworkCredential smtpusercredential = new NetworkCredential(_mailSetting.Mail, _mailSetting.Password);
				smtp.UseDefaultCredentials = false;
				smtp.EnableSsl = true;
				smtp.Host = _mailSetting.Host;
				smtp.Credentials = smtpusercredential;
				smtp.Port = _mailSetting.Port;
				smtp.Send(mail);
				_logger.LogInformation("Mail Send Success.");
			}
			catch (Exception ex)
			{
				_logger.LogError("Mail Send Error"+ex.Message);
			}
			
		}
	}
}
