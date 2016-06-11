using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Util;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private MyDbContext db = new MyDbContext();
        private UserManager<ContractUser> manager;

        //NoName Update Praesentation
        private string continueBtn = "Weiter";

        public ContractController()
        {
            manager = new UserManager<ContractUser>(new UserStore<ContractUser>(db));
        }
        // GET: Contract
        public ActionResult Index()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            //return View(db.Contracts.ToList());
            return View(db.Contracts.Where(c => c.ContractOwner.Id == currentUser.Id).ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

       

        // GET: Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", contract.OwnerId);
            ViewBag.CoordinatorId = new SelectList(db.Users, "Id", "Email", contract.CoordinatorId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", contract.DepartmentId);
            ViewBag.DispatcherId = new SelectList(db.Users, "Id", "Email", contract.DispatcherId);
            ViewBag.UDepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", contract.UDepartmentId);
            return View(contract);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IntContractNum,ExtContractNum,ContractBegin,ContractEnd,Description,OwnerId,DispatcherId,CoordinatorId,DepartmentId,UDepartmentId,ContractTypeId,ContractKindId,ContractStatusId")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "Email", contract.OwnerId);
            ViewBag.CoordinatorId = new SelectList(db.Users, "Id", "Email", contract.CoordinatorId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", contract.DepartmentId);
            ViewBag.DispatcherId = new SelectList(db.Users, "Id", "Email", contract.DispatcherId);
            ViewBag.UDepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", contract.UDepartmentId);
            return View(contract);
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
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


        //David: multiLevel Contract creation

        // GET: Contract/Create/CreateInit
        public ActionResult CreateInit()
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            string currentName = currentUser.UserName.ToString();
            ViewBag.OwnerName = currentName;

            string currentDepartmentName = UtilAugust.GetDepartmentsOfUser(currentName, db).Select(d => d.DepartmentName).FirstOrDefault();
            ViewBag.ClientSelect = new SelectList(db.Mandants, "Id", "MandantName");

            if (currentDepartmentName != null)
            {

                IQueryable<Mandant> currentClient = UtilAugust.GetClientOfDepartment(currentDepartmentName, db);
                string currentClientName = currentClient.Select(c => c.MandantName).FirstOrDefault();

                if (currentClientName != null)
                {

                    ViewBag.CoordinatorId = new SelectList(UtilAugust.GetCoordinatorsFromClient(currentClientName, db), "Id", "UserName");
                }
            }
            else
            {

                ViewBag.CoordinatorId = new SelectList(new[] { "No Coordinator" });
            }
            ViewBag.ClientId = new SelectList(db.Mandants, "Id", "MandantName");
            
            return View();
        }

        // POST: Contract/Create/CreateInit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInit([Bind(Include = "Id,Description,OwnerId,CoordinatorId")]Contract contract, string submit)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());

            //set the user in the contracts table
            contract.OwnerId = currentUser.Id;


            if (ModelState.IsValid)
            {
                contract.ContractStatusId = 1; //1==incomplete
                db.Contracts.Add(contract);
                db.SaveChanges();

                //Decide which button was pressed...then redirect
                if (submit == continueBtn)
                {
                    return RedirectToAction("CreateGeneral", new { id = contract.Id });
                }

                else

                {
                    return RedirectToAction("Index");
                }

            }

            //David: Let Author explain the following code. NO COMMENTS!!
            // ????????????????????????????????????????????????????????????????????????????????

            string currentName = currentUser.UserName.ToString();

            ViewBag.OwnerName = currentName;

            string currentDepartmentName = UtilAugust.GetDepartmentsOfUser(currentName, db).Select(d => d.DepartmentName).FirstOrDefault();
            ViewBag.ClientSelect = new SelectList(db.Mandants, "Id", "MandantName");

            if (currentDepartmentName != null)
            {

                IQueryable<Mandant> currentClient = UtilAugust.GetClientOfDepartment(currentDepartmentName, db);
                string currentClientName = currentClient.Select(c => c.MandantName).FirstOrDefault();

                if (currentClientName != null)
                {

                    ViewBag.CoordinatorId = new SelectList(UtilAugust.GetCoordinatorsFromClient(currentClientName, db), "Id", "UserName");
                }
            }
            else
            {

                ViewBag.CoordinatorId = new SelectList(new[] { "No Coordinator" });
            }
            ViewBag.ClientId = new SelectList(db.Mandants, "Id", "MandantName");

            // ????????????????????????????????????????????????????????????????????????????????
            return RedirectToAction("CreateInit");

            //Make the model available in other controllers

        }

        // GET: Contract/Create/CreateGeneral
        public ActionResult CreateGeneral(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                //Contract contract = (Contract) TempData["TempContract"];
                return HttpNotFound();
            }
            ViewBag.ContractKindId = new SelectList(db.ContractKinds, "Id", "Description"); //David: ContractKind Dropdown
            ViewBag.ContractTypeId = new SelectList(db.ContractTypes, "Id", "Description"); //David: ContractType Dropdown
            
            return View(contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGeneral([Bind(Include = "Id, Description, OwnerId, CoordinatorId, ContractTypeId, ContractKindId")] Contract contract, string submit)
        {
            if (ModelState.IsValid)
            {
                contract.ContractStatusId = 1; //1==incomplete
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                
                if (submit == continueBtn)
                {
                    return RedirectToAction("CreateDates", new { id = contract.Id });
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

            ViewBag.ContractKindId = new SelectList(db.ContractKinds, "Id", "Description", contract.ContractKindId); //Chr: ContractKind Dropdown
            ViewBag.ContractTypeId = new SelectList(db.ContractTypes, "Id", "Description", contract.ContractTypeId); //CHr: ContractType Dropdown
            
            return View(contract);
        }

        // GET: Contract/Create/CreateDates
        public ActionResult CreateDates(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Create/CreateDates
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDates([Bind(Include = "Id, Description, OwnerId, CoordinatorId, ContractTypeId, ContractKindId, ContractBegin, ContractEnd ")] Contract contract, string submit)
        {
            if (ModelState.IsValid)
            {
                contract.ContractStatusId = 1; //1==incomplete
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                
                if (submit == continueBtn)
                {
                    return RedirectToAction("CreateFiles", new { id = contract.Id });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(contract);
        }

        // GET: Contract/Create/CreateFiles
        public ActionResult CreateFiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            
            return View(contract);
        }

        // POST: Contract/Create/CreateFiles
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateFiles()
        {
            return RedirectToAction("Index");
        }

        //==============================================================================
        [HttpPost]
        public ActionResult GetJsonCoordinatorsFromClient(string client)
        {
            var data = new SelectList(UtilAugust.GetCoordinatorsFromClient(client, db), "Id", "UserName").ToList();
            return Json(data);
        }

        //Json Variant
        [HttpPost]
        public ActionResult GetJsonDispatchersFromDepartment(string department)
        {
            var data = new SelectList(UtilAugust.GetDispatchersFromDepartment(department, db), "Id", "UserName").ToList();
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetJsonDepartmentsFromClient(string client)
        {
            var data = new SelectList(UtilAugust.GetDepartmentsFromClient(client, db), "Id", "DepartmentName").ToList();
            return Json(data);
        }

    }
}
