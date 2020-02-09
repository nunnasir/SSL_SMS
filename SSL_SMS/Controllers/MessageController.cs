using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSL_SMS.DAL;

namespace SSL_SMS.Controllers
{
    public class MessageController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();

        // GET: Message
        public ActionResult Index()
        {
            return View(db.MessageGroups.ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageGroup messageGroup = db.MessageGroups.Find(id);
            if (messageGroup == null)
            {
                return HttpNotFound();
            }
            return View(messageGroup);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            //return PartialView("~/Views/Shared/_Create.cshtml");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageGroup messageGroup)
        {
            if (ModelState.IsValid)
            {
                messageGroup.Create_User = "Nasir";
                db.MessageGroups.Add(messageGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messageGroup);
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageGroup messageGroup = db.MessageGroups.Find(id);
            if (messageGroup == null)
            {
                return HttpNotFound();
            }
            return View(messageGroup);
        }

        // POST: Message/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "GroupName,Message,Edit_User,Edit_Date")] MessageGroup messageGroup)
        public ActionResult Edit(MessageGroup messageGroup)
        {
            if (ModelState.IsValid)
            {
                messageGroup.Edit_User = "Nahid";
                db.Entry(messageGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messageGroup);
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageGroup messageGroup = db.MessageGroups.Find(id);
            if (messageGroup == null)
            {
                return HttpNotFound();
            }
            return View(messageGroup);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageGroup messageGroup = db.MessageGroups.Find(id);
            db.MessageGroups.Remove(messageGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
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
