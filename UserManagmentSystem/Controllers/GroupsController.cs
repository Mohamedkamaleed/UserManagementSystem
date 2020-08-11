
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using UserManagmentSystem.Models;

namespace UserManagmentSystem.Controllers
{
    [Authorize]

    public class GroupsController : Controller
    {
        private ModelEntities db = new ModelEntities();

        // GET: Groups
        public ActionResult Index()
        {
            return View(db.tblGroups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGroups tblGroups = db.tblGroups.Find(id);
            if (tblGroups == null)
            {
                return HttpNotFound();
            }

            GroupsViewModel groupsViewModel = new GroupsViewModel();
            ImplementedMethods implementedMethods = new ImplementedMethods();
            groupsViewModel.Name = tblGroups.Name;
            groupsViewModel.PK_Id = tblGroups.PK_Id;
            groupsViewModel.Actions = tblGroups.tblGroupActions.Select(s => new ActionsViewModel
            {
                PK_Id = s.PK_Id,
                Name = s.ActionName
            }).ToList();
            
            ViewBag.AvailableActions = implementedMethods.ActiveMethods.Where(t => !db.tblGroupActions.Any(k => k.FK_Group == id && k.ActionName == t)).Select(s => new SelectListItem { Value = s, Text = s }).ToList();


            return View(groupsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RevokeAction(int GroupId, string ActionName)
        {
            tblGroupActions tblGroupActions = db.tblGroupActions.FirstOrDefault(s => s.FK_Group == GroupId && s.ActionName == ActionName);
            if (tblGroupActions != null)
            {
                db.tblGroupActions.Remove(tblGroupActions);
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = GroupId });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAction(int GroupId, string ActionName)
        {
            tblGroupActions tblGroupActions = new tblGroupActions() { ActionName = ActionName, FK_Group = GroupId };
            if (tblGroupActions != null)
            {
                db.tblGroupActions.Add(tblGroupActions);
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = GroupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RevokeUser(int GroupId, string UserId)
        {
            tblUserGroup tblUserGroups = db.tblUserGroup.FirstOrDefault(s => s.FK_Group == GroupId && s.FK_User == UserId);
            if (tblUserGroups != null)
            {
                db.tblUserGroup.Remove(tblUserGroups);
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = GroupId });
        }
        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,Name")] tblGroups tblGroups)
        {
            if (ModelState.IsValid)
            {
                db.tblGroups.Add(tblGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblGroups);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGroups tblGroups = db.tblGroups.Find(id);
            if (tblGroups == null)
            {
                return HttpNotFound();
            }
            return View(tblGroups);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Name")] tblGroups tblGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGroups);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGroups tblGroups = db.tblGroups.Find(id);
            if (tblGroups == null)
            {
                return HttpNotFound();
            }
            return View(tblGroups);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGroups tblGroups = db.tblGroups.Find(id);
            db.tblGroups.Remove(tblGroups);
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
