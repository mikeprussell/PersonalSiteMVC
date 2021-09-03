using PersonalSiteMVC.UI.Models; //Added for acess to ContactViewModel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail; //Added for access to MailMessage
using System.Configuration; //Added for acess for the ConfigurationManager
using System.Net; //Added for access to NetworkCredential

namespace PersonalSiteMVC.UI.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //if(ModelState.IsValid == false)
            if (!ModelState.IsValid)
            {
                return View(cvm);

            } // END if

            string message = $"You have received an email from {cvm.Name} with a subject: " +
                $"{cvm.Subject}. Please respond to {cvm.Email} with your response to the " +
                $"following message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage(

                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                ConfigurationManager.AppSettings["EmailTo"].ToString(),
                cvm.Subject,
                message); 

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString());

            try
            {
                client.Send(mm);

            }

            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time." +
                    $"Please try again later. Error Message: <br/> {ex.StackTrace}";

                return View(cvm);

            } //END catch

            return View("EmailConfirmation", cvm);

        }

    }

}