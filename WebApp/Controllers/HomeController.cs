using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        #region Contact Us
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendAnEmailToUs(string name, string phone, string email, string msg)
        {
            try
            {
                string host = string.Empty, adminEmailId = string.Empty, adminPwd = string.Empty, shreyaasysEmailId = string.Empty;
                int portNo = int.Parse(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                host = ConfigurationManager.AppSettings["SmtpHost"].ToString();
                shreyaasysEmailId = ConfigurationManager.AppSettings["ShreyaasysEmalId"].ToString();
                adminEmailId = ConfigurationManager.AppSettings["EmailId"].ToString();
                adminPwd = ConfigurationManager.AppSettings["Password"].ToString();
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(adminEmailId, adminPwd);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.Port = portNo;
                //smtp.EnableSsl = true;
                smtp.Credentials = credentials;

                MailMessage toCustomerMessage = new MailMessage();
                toCustomerMessage.From = new MailAddress(adminEmailId);
                toCustomerMessage.Subject = "Thank You - shreyaaSys Computers";
                toCustomerMessage.IsBodyHtml = true;
                toCustomerMessage.To.Add(new MailAddress(email));
                toCustomerMessage.Body = "<style>.box .box-primary {border-top-color: #12afd6;border-width: 3px;} .box {position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px;width: 100%;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);}</style><div class='box box-primary vertical-space' id=''>Dear " + name + ",<br/><br/><div class='box-body with-border'>We have received your enquiry and will contact you within 1 business day.<br />You can also reach us by <b>+(91) 8495-253776</b><br/><br/>Regards,<br/>shreyaaSys Computers,<br/> Rayadurg</div><!-- /.box-body --></div>";
                smtp.Send(toCustomerMessage);

                MailMessage toAdminMessage = new MailMessage();
                toAdminMessage.From = new MailAddress(adminEmailId);
                toAdminMessage.Subject = "New Enquiry - shreyaaSys Computers";
                toAdminMessage.IsBodyHtml = true;
                toAdminMessage.To.Add(new MailAddress(shreyaasysEmailId));
                toAdminMessage.Body = "<style>.box .box-primary {border-top-color: #12afd6;border-width: 3px;} .box {position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px;width: 100%;box-shadow: 0 1px 1px rgba(0, 0, 0, 0.1);}</style><div class='box box-primary vertical-space' id=''>Dear Sreenivas,<br/><br/><div class='box-body with-border'>You have received an enquiry from <a href='http://shreyaasys.com' target='_blank'>www.shreyaasys.com</a><br /><br/><p><strong>Name&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</strong>: " + name + "<br /><strong>Email</strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;: " + email + "<br /><strong>Phone No</strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; : " + phone + "<br /><strong>Enquiry Details</strong>&nbsp; &nbsp;&nbsp;&nbsp;: " + msg + "</p></div><!-- /.box-body --></div>";
                smtp.Send(toAdminMessage);
                return Json(true);
            }
            catch (Exception ex)
            {
                WriteLogs.Write(ex);
                return Json(false);
            }

        } 
        #endregion
    }
}