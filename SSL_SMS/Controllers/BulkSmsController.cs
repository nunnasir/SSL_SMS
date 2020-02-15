using AutoMapper;
using SSL_SMS.BLL;
using SSL_SMS.DAL;
using SSL_SMS.Models;
using SSL_SMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSL_SMS.Controllers
{
    public class BulkSmsController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();
        SendSmsBLL _smsbll = new SendSmsBLL();

        // GET: BulkSms
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SmsSendVm smsVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var contacts = smsVm.Contacts;
            var messages = smsVm.Messages;
            string result = _smsbll.SendSms(contacts, messages);
            bool status = result.ToLower().Contains("error") ? false : true;

            SmsLog sendSms = new SmsLog();
            //1 = groupwise, 2 = bulk
            sendSms.SmsType = 2;
            sendSms.Status = status ? Convert.ToInt16(1) : Convert.ToInt16(0);
            sendSms.SendUser = "Nasir";
            sendSms.SendDate = DateTime.Now;
            sendSms.MessageStatus = result;

            db.SmsLogs.Add(sendSms);
            db.SaveChanges();

            TempData["message"] = status ? "success" : "failed";

            return RedirectToAction("Index");
        }
    }
}