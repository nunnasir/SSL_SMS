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
    public class ContactGrpController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();

        // GET: Contact
        public ActionResult Index()
        {
            var contactGroupQuery = db.ContactGroups.ToList();

            var contactGroupDto = contactGroupQuery
                .ToList()
                .Select(Mapper.Map<ContactGroup, ContactGroupDto>);

            return View(contactGroupDto);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactGroup = db.ContactGroups.Find(id);

            if (contactGroup == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map < ContactGroup, ContactGroupDto > (contactGroup));
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactGroupDto contactGroupDto)
        {
            string filter = contactGroupDto.GroupName.Trim();
            
            var existContactGroup = db.ContactGroups.FirstOrDefault(a => a.GroupName.Contains(filter));

            if (existContactGroup == null && ModelState.IsValid)
            {
                contactGroupDto.Create_User = "Nasir";
                contactGroupDto.Create_Date = DateTime.Now;

                var contactGroup = Mapper.Map<ContactGroupDto, ContactGroup>(contactGroupDto);
                db.ContactGroups.Add(contactGroup);
                db.SaveChanges();

                TempData["message"] = "Added";

                return RedirectToAction("Index");
            }

            return View(contactGroupDto);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var contactGroup = db.ContactGroups.Find(id);

            if (contactGroup == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<ContactGroup, ContactGroupDto>(contactGroup));
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactGroupDto contactGroupDto)
        {
            if (ModelState.IsValid)
            {
                contactGroupDto.Edit_User = "Nahid";
                contactGroupDto.Edit_Date = DateTime.Now;

                var contactGroup = Mapper.Map<ContactGroupDto, ContactGroup>(contactGroupDto);

                db.Entry(contactGroup).State = EntityState.Modified;
                db.SaveChanges();

                TempData["message"] = "Updated";

                return RedirectToAction("Index");
            }
            return View(contactGroupDto);
        }

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var contactGroup = db.ContactGroups.Find(id);

        //    if (contactGroup == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    db.ContactGroups.Remove(contactGroup);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //    //return View(contactGroup);
        //}

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var contactGroups = db.ContactGroups.FirstOrDefault(m => m.ID == id);
            db.ContactGroups.Remove(contactGroups);
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
