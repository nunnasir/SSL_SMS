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
    public class GroupsController : Controller
    {
        private SSL_SMSEntities db = new SSL_SMSEntities();

        // GET: Groups
        public ActionResult Index()
        {
            var groupQuery = db.Groups.ToList();

            var groupDto = groupQuery
                .ToList()
                .Select(Mapper.Map<Group, GroupDto>);

            return View(groupDto);
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.FirstOrDefault(g => g.Id == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Group, GroupDto>(group));
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupDto groupDto)
        {
            if (ModelState.IsValid)
            {
                string filter = groupDto.Name.Trim();
                var existGroup = db.Groups.FirstOrDefault(a => a.Name.Contains(filter));

                if(existGroup != null)
                {
                    TempData["message"] = "Existed";
                    return View(groupDto);
                }

                groupDto.CreateUser = "Nasir";
                groupDto.CreateDate = DateTime.Now;
                var group = Mapper.Map<GroupDto, Group>(groupDto);
                db.Groups.Add(group);
                db.SaveChanges();
                TempData["message"] = "Added";
                return RedirectToAction("Index");
            }
            return View(groupDto);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.FirstOrDefault(g => g.Id == id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<Group, GroupDto>(group));
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupDto groupDto)
        {
            if (ModelState.IsValid)
            {
                groupDto.UpdateUser = "Nahid";
                groupDto.UpdateDate = DateTime.Now;

                var group = Mapper.Map<GroupDto, Group>(groupDto);

                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();

                TempData["message"] = "Updated";

                return RedirectToAction("Index");
            }
            return View(groupDto);
        }

        //Groups/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var group = db.Groups.FirstOrDefault(m => m.Id == id);
            db.Groups.Remove(group);
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
