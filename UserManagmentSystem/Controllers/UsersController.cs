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
    public class UsersController : Controller
    {
        private ModelEntities db = new ModelEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }

            UsersViewModel usersViewModel = new UsersViewModel();
            usersViewModel.Id = aspNetUsers.Id;
            usersViewModel.LockoutEnabled = aspNetUsers.LockoutEnabled;
            usersViewModel.LockoutEndDateUtc = aspNetUsers.LockoutEndDateUtc;
            usersViewModel.Name = aspNetUsers.UserName;
            usersViewModel.PhoneNumber = aspNetUsers.PhoneNumber;
            usersViewModel.PhoneNumberConfirmed = aspNetUsers.PhoneNumberConfirmed;
            usersViewModel.TwoFactorEnabled = aspNetUsers.TwoFactorEnabled;
            usersViewModel.UserName = aspNetUsers.UserName;
            usersViewModel.Groups = db.tblUserGroup
                .Where(s => s.FK_User == aspNetUsers.Id)
                .Select(s => new GroupsViewModel()
                {
                    PK_Id = s.FK_Group,
                    Name = s.tblGroups.Name
                }).ToList();

            ViewBag.AvailableGroups = db.tblGroups.Where(t => !db.tblUserGroup.Any(k => k.FK_User == id && k.FK_Group == t.PK_Id))
                .Select(s => new SelectListItem { Value = s.PK_Id.ToString(), Text = s.Name }).ToList();

            return View(usersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RevokeGroup(int GroupId, string UserId)
        {
            tblUserGroup tblUserGroups = db.tblUserGroup.FirstOrDefault(s => s.FK_Group == GroupId && s.FK_User == UserId);
            if (tblUserGroups != null)
            {
                db.tblUserGroup.Remove(tblUserGroups);
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = UserId });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGroup(int GroupId, string UserId)
        {
            tblUserGroup tblUserGroups = new tblUserGroup()
            {
                FK_Group = GroupId,
                FK_User = UserId
            };
            if (tblUserGroups != null)
            {
                db.tblUserGroup.Add(tblUserGroups);
                db.SaveChanges();
            }


            return RedirectToAction("Details", new { id = UserId });
        }


        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
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
