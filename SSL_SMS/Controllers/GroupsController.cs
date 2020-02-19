using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using OfficeOpenXml;
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

        public void DownlloadContactList(int id)
        {
            var groups = (from g in db.Groups
                          join c in db.Contacts on g.Id equals c.GroupId
                          where c.GroupId == id
                          select new { c, g });

            var groupName = groups.ToList().Select(g => g.g.Name).FirstOrDefault();
            

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(groupName + " Groups Contacts");

            Sheet.Cells["A1"].Value = "Group Name";
            Sheet.Cells["B1"].Value = "Contact Number";
            Sheet.Cells["C1"].Value = "Create Date";
            int row = 2;
            foreach (var item in groups)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.g.Name;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.c.ContactList;
                Sheet.Cells[string.Format("C{0}", row)].Value = String.Format("{0:MM/dd/yyyy}", item.c.CreateDate);

                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();

            string handle = Guid.NewGuid().ToString();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Ep.SaveAs(memoryStream);
                memoryStream.Position = 0;
                TempData[handle] = memoryStream.ToArray();
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader(
                "content-disposition",
                string.Format("attachment;  filename={0}", groupName + " group contact List " + String.Format("{0:MM/dd/yyyy}", DateTime.Now) + ".xlsx"));

            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
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
