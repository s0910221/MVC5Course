﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : BaseController
    {
        ClientRepository repo;
        OccupationRepository occuRepo;

        public ClientsController()
        {
            repo = RepositoryHelper.GetClientRepository();
            occuRepo = RepositoryHelper.GetOccupationRepository(repo.UnitOfWork);
        }

        [Route("")]
        public ActionResult Index()
        {
            var client = repo.All();
            return View(client.Take(10));
        }

        [HttpPost]
        [Route("BatchUpdate")]
        public ActionResult BatchUpdate(IList<ClientBatchViewModel> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var client = db.Client.Find(item.ClientId);
                    client.FirstName = item.FirstName;
                    client.MiddleName = item.MiddleName;
                    client.LastName = item.LastName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData.Model = repo.All().Take(10);
            return View("Index");
        }

        [Route("search")]
        public ActionResult Search(string keyword)
        {
            var client = repo.搜尋名稱(keyword);

            return View("Index", client);
        }

        [Route("{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Route("{*name}")]
        public ActionResult Details2(string name)
        {
            string[] names = name.Split('/');
            string FirstName = names[0];
            string MiddleName = names[1];
            string LastName = names[2];

            Client client = repo.All().FirstOrDefault(p => p.FirstName == FirstName && p.MiddleName == MiddleName && p.LastName == LastName);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View("Details", client);
        }

        [Route("create")]
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IdNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IdNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                var db = repo.UnitOfWork.Context;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = repo.Find(id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
