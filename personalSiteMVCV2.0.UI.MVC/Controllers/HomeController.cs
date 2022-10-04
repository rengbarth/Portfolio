using personalSiteMVCV2._0.UI.MVC.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace personalSiteMVCV2._0.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            //Create body for our email message
            string body = string.Format(
                "Name: {0}<br/>Email: {1}<br/>Subject: {2}<br/>Message:<br/>{3}",
                contact.Name,
                contact.Email,
                contact.Subject,
                contact.Message
                );

            //Create and configure the Mail Message
            MailMessage msg = new MailMessage(
                "no-reply@devsofraph.com",
                "raphael.engbarth@gmail.com",
                contact.Subject,
                body
                );

            //Additional configs for Mail message obj
            msg.IsBodyHtml = true;
            msg.CC.Add("sample@email.com"); //Adds a CC address
            msg.Priority = MailPriority.High;

            //Create and configure the SMTP client
            SmtpClient client = new SmtpClient("208.118.63.250");
            client.Credentials = new NetworkCredential("no-reply@devsofraph.com", "!Password");

            if (ModelState.IsValid)
            {
                using (client)
                {
                    //(Using)open a connection between application and the mail server
                    try
                    {
                        client.Send(msg);
                    }
                    catch
                    {
                        ViewBag.ErrorMessage = "There was an error sending your mail message. Please try again.";
                        return View();
                    }
                }
                return View("ContactConfirmation", contact);
            }

            return View();
        }

    }
}
