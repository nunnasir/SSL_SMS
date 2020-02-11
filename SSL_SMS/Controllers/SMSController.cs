using AutoMapper;
using SSL_SMS.DAL;
using SSL_SMS.Models;
using SSL_SMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.IO;

namespace SSL_SMS.Controllers
{
    public class SMSController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();
        // GET: SMS
        public ActionResult Index()
        {
            var smsGroup = db.MessageGroups.ToList()
                .Select(Mapper.Map<MessageGroup, MessageGroupDto>);

            var contactGroup = db.ContactGroups.ToList()
                .Select(Mapper.Map<ContactGroup, ContactGroupDto>);

            var viewModel = new MessageGroupVm
            {
                SendSmsStatu = new SendSmsStatu(),
                MessageGroups = smsGroup,
                ContactGroups = contactGroup
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MessageGroupVm messageGroupVm)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MessageGroupVm
                {
                    MessageGroups = db.MessageGroups.ToList()
                        .Select(Mapper.Map<MessageGroup, MessageGroupDto>),
                    ContactGroups = db.ContactGroups.ToList()
                        .Select(Mapper.Map<ContactGroup, ContactGroupDto>)

                };
                return View("Index", viewModel);
            }

            var messages = messageGroupVm.Messages;
            var contacts = messageGroupVm.Contacts;
            var messageGroupId = messageGroupVm.SendSmsStatu.MessageGroupId;
            var contactGroupId = messageGroupVm.SendSmsStatu.ContactGroupId;

            bool status = SendSms(contacts, messages);

            SendSmsStatu send = new SendSmsStatu();
            send.ContactGroupId = contactGroupId;
            send.MessageGroupId = messageGroupId;
            send.Status = 0;
            send.SendUser = "Nasir Uddin";
            send.SendDate = DateTime.Now;

            db.SendSmsStatus.Add(send);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LoadContactList(int contactGroupId)
        {
            var contactGroup = db.ContactGroups.Find(contactGroupId);
            return Json(contactGroup.ContactList);
        }

        [HttpPost]
        public ActionResult LoadMessage(int messageGroupId)
        {
            var contactGroup = db.MessageGroups.Find(messageGroupId);
            return Json(contactGroup.Message);
        }

        private bool SendSms(string contacts, string messages)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                String to = contacts;
                String token = "a614b41374ce79aaefdd8c08585306ec";
                String message = System.Uri.EscapeUriString(messages);
                String url = "http://api.greenweb.com.bd/api.php?token=" + token + "&to=" + to + "&message=" + message;
                request = WebRequest.Create(url);

                // Send the 'HttpWebRequest' and wait for response.
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new
                System.IO.StreamReader(stream, ec);
                result = reader.ReadToEnd();
                Console.WriteLine(result);
                reader.Close();
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }

            if ((result.ToLower()).Contains("error"))
                return false;
            return true;
        }
    }
}