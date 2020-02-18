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
    public class ContactsController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();

        // GET: Contacts
        public ActionResult Index()
        {
            var contactQuery = db.Contacts.Include(c => c.Group)
                .ToList()
                .Select(Mapper.Map<Contact, ContactDto>);
            return View(contactQuery);
        }



        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Contact, ContactDto>(contact));
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ContactDto model = new ContactDto();

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            return View(model);
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                int groupId = (int)contactDto.GroupId;
                var existContact = db.Contacts.FirstOrDefault(g => g.GroupId == groupId);

                //if (existContact != null)
                //{
                //    TempData["message"] = "Existed";
                //    ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", contactDto.GroupId);
                //    return View(contactDto);
                //}

                var contacts = contactDto.ContactList.Split(',');

                var duplicate = new List<string>();
                var invalid = new List<string>();
                int valid = 0;

                foreach (var item in contacts)
                {
                    var existNumber = db.Contacts.FirstOrDefault(g => g.GroupId == groupId && g.ContactList == item);

                    if (!checkValidation(item))
                    {
                        invalid.Add(item);
                    }
                    else if(existNumber != null)
                    {
                        duplicate.Add(item);
                    }
                    else
                    {
                        Contact model = new Contact();
                        model.ContactList = item;
                        model.GroupId = groupId;
                        model.CreateUser = "Nasir";
                        model.CreateDate = DateTime.Now;
                        db.Contacts.Add(model);
                        db.SaveChanges();

                        valid++;
                    }
                    
                }

                //contactDto.CreateUser = "Nasir";
                //contactDto.CreateDate = DateTime.Now;

                //var contact = Mapper.Map<ContactDto, Contact>(contactDto);

                //db.Contacts.Add(contact);
                //db.SaveChanges();

                //TempData["validNumber"] = valid;
                TempData["message"] = "Added";
                TempData["validNumber"] = valid;
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", contactDto.GroupId);
            return View(contactDto);
        }

        private bool checkValidation(string item)
        {
            var returnValue = true;

            if (item.Length > 13 || item.Length < 10)
            {
                returnValue = false;
            }
            else if (item.Length == 13 && item.Substring(item.Length - 10) != "880")
            {
                returnValue = false;
            }
            else if (item.Length == 11 && item[0] != '0')
            {
                returnValue = false;
            }

            return returnValue;
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(g => g.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", contact.GroupId);

            return View(Mapper.Map<Contact, ContactDto>(contact));
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                contactDto.UpdateUser = "Nahid";
                contactDto.UpdateDate = DateTime.Now;

                var contact = Mapper.Map<ContactDto, Contact>(contactDto);

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Updated";
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", contactDto.GroupId);
            return View(contactDto);
        }

        //Groups/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var contact = db.Contacts.FirstOrDefault(m => m.Id == id);
            db.Contacts.Remove(contact);
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
