using System.Net;
using System.Net.Mail;
using System.Configuration;
using System;
using System.Collections.Specialized;

namespace LightSwitchApplication
{
    public class MailHelper
    {
        private string _SMTPSendingEmailAddress { get; set; }
        private string _SMTPServer { get; set; }
        private string _SMTPUserId { get; set; }
        private string _SMTPPassword { get; set; }
        private int _SMTPPort { get; set; }
        private bool _SMTPSSL { get; set; }

        private string _MailFromName { get; set; }
        private string _MailToEmail { get; set; }
        private string _MailToName { get; set; }
        private string _MailSubject { get; set; }
        private string _MailBody { get; set; }
        private Boolean _Failed { get; set; }

        public MailHelper(
            string SendFromName, string SendToEmail,
            string SendToName, string Subject,
            string Body, UserPrefs prefs)
        {
            //_MailFromName = SendFromName;
            _MailFromName = prefs.DisaplayName;
            _MailToEmail = SendToEmail;
            _MailToName = SendToName;
            _MailSubject = Subject;
            _MailBody = Body;
            _Failed = false;

            try
            {
                _SMTPSendingEmailAddress = prefs.MailAddress;
                _SMTPServer = prefs.Server;
                _SMTPUserId = prefs.MailAddress.Split('@')[0];
                _SMTPPassword = prefs.Password;
                _SMTPPort = Convert.ToInt32(prefs.Port);
                _SMTPSSL = Convert.ToBoolean(prefs.SSL);
            }
            catch (Exception exp)
            {
                _Failed = true;
            }
            
           
            //_SMTPSendingEmailAddress = Convert.ToString(ConfigurationSettings.AppSettings["SMTPSendingEmailAddress"]);
            //_SMTPServer = Convert.ToString(ConfigurationSettings.AppSettings["SMTPServer"]);
            //_SMTPUserId = Convert.ToString(ConfigurationSettings.AppSettings["SMTPUserID"]);
            //_SMTPPassword = Convert.ToString(ConfigurationSettings.AppSettings["SMTPPassword"]);
            //_SMTPPort = Convert.ToInt32(ConfigurationSettings.AppSettings["SMTPPort"]);
            //_SMTPSSL = Convert.ToBoolean(ConfigurationSettings.AppSettings["SMTPSSL"]);



        }

        public void SendMail()
        {
            if (_Failed)
                return;

            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;

            System.Net.Mail.MailAddress mailFrom =
                new System.Net.Mail.MailAddress(_SMTPSendingEmailAddress, _MailFromName);

         
            var _with1 = mail;
            _with1.From = mailFrom;
            _with1.To.Add(_MailToEmail);
            _with1.Subject = _MailSubject;
            _with1.Body = _MailBody;

            SmtpClient smtp = new SmtpClient(_SMTPServer, _SMTPPort);
            smtp.EnableSsl = _SMTPSSL;

            smtp.Credentials =
                new NetworkCredential(_SMTPUserId, _SMTPPassword);

            smtp.Send(mail);
        }
    }
}