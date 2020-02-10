using SSL_SMS.DAL;
using SSL_SMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSL_SMS.Controllers
{
    public class SMSController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();
        // GET: SMS
        public ActionResult Index()
        {
            var smsGroup = db.MessageGroups.ToList();

            var viewModel = new MessageGroupVm
            {
                SendSmsStatu = new SendSmsStatu(),
                MessageGroups = smsGroup
            };

            return View("Index", viewModel);
        }
    }
}