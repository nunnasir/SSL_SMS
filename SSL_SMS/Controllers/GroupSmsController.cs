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
    public class GroupSmsController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();
        SendSmsBLL _smsbll = new SendSmsBLL();

        // GET: GroupSms
        public ActionResult Index()
        {
            var contactGroup = db.Groups.ToList()
                .Select(Mapper.Map<Group, GroupDto>);

            var viewModel = new SmsSendVm
            {
                SmsLogs = new SmsLog(),
                ContactGroups = contactGroup
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SmsSendVm smsVm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new SmsSendVm
                {
                    ContactGroups = db.Groups.ToList()
                        .Select(Mapper.Map<Group, GroupDto>)
                    
                };
                return View("Index", viewModel);
            }

            var contacts = smsVm.Contacts;
            var messages = smsVm.Messages;
            string result = _smsbll.SendSms(contacts, messages);
            bool status = result.ToLower().Contains("error") ? false : true;

            SmsLog sendSms = new SmsLog();
            //1 = groupwise, 2 = bulk
            sendSms.SmsType = 1;
            sendSms.Status = status ? Convert.ToInt16(1) : Convert.ToInt16(0);
            sendSms.SendUser = "Nasir";
            sendSms.SendDate = DateTime.Now;
            sendSms.MessageStatus = result;

            db.SmsLogs.Add(sendSms);
            db.SaveChanges();

            TempData["message"] = status ? "success" : "failed";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LoadContactList(int contactGroupId)
        {
            var groups = (from g in db.Groups
                          join c in db.Contacts on g.Id equals c.GroupId
                          where c.GroupId == contactGroupId
                          select new { c, g });

            var contactList = new List<string>();

            foreach (var item in groups)
            {
                contactList.Add(item.c.ContactList);
            }

            return Json(string.Join(",", contactList.ToArray()));
        }

    }
}