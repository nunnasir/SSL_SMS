using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SSL_SMS.DAL;
using SSL_SMS.Models;

namespace SSL_SMS.Controllers
{
    public class MessageController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();

        // GET: Message
        public ActionResult Index()
        {
            var messageGroupQuery = db.MessageGroups.ToList();

            var messageGroupDto = messageGroupQuery
                .ToList()
                .Select(Mapper.Map<MessageGroup, MessageGroupDto>);

            return View(messageGroupDto);
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var messageGroup = db.MessageGroups.Find(id);

            if (messageGroup == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<MessageGroup, MessageGroupDto>(messageGroup));
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            //return PartialView("~/Views/Shared/_Create.cshtml");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageGroupDto messageGroupDto)
        {
            string filter = messageGroupDto.GroupName.Trim();
            var existMessagetGroup = db.MessageGroups.FirstOrDefault(a => a.GroupName.Contains(filter));

            if(existMessagetGroup == null && ModelState.IsValid)
            {
                messageGroupDto.Create_User = "Nasir";
                messageGroupDto.Create_Date = DateTime.Now;

                var messageGroup = Mapper.Map<MessageGroupDto, MessageGroup>(messageGroupDto);

                db.MessageGroups.Add(messageGroup);
                db.SaveChanges();

                TempData["message"] = "Added";

                return RedirectToAction("Index");
            }

            return View(messageGroupDto);
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var messageGroup = db.MessageGroups.Find(id);

            if (messageGroup == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<MessageGroup, MessageGroupDto>(messageGroup));
        }

        // POST: Message/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "GroupName,Message,Edit_User,Edit_Date")] MessageGroup messageGroup)
        public ActionResult Edit(MessageGroupDto messageGroupDto)
        {
            if (ModelState.IsValid)
            {
                messageGroupDto.Edit_User = "Nahid";
                messageGroupDto.Edit_Date = DateTime.Now;

                var messageGroup = Mapper.Map<MessageGroupDto, MessageGroup>(messageGroupDto);

                db.Entry(messageGroup).State = EntityState.Modified;
                db.SaveChanges();

                TempData["message"] = "Updated";

                return RedirectToAction("Index");
            }
            return View(messageGroupDto);
        }

        // GET: Message/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var messageGroup = db.MessageGroups.Find(id);
        //    if (messageGroup == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.MessageGroups.Remove(messageGroup);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var messageGroup = db.MessageGroups.FirstOrDefault(m => m.ID == id);
            db.MessageGroups.Remove(messageGroup);

            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
