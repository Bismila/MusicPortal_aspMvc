using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPortal_2016.Models;

namespace MusicPortal_2016.Controllers
{
    public class GenreForMenuController : Controller
    {
        private Context db = new Context();

        // GET: GenreForMenu
        public ActionResult Index(string genre)
        {
            IQueryable<Songs> songes = db.SongesObjDbSet.Include(s => s.Genre);
            ViewBag.currentGenre = genre;
            if (!String.IsNullOrEmpty(genre))
            {
                songes = songes.Where(g => g.Genre.Name == genre);
            }
          
            return View(songes.ToList());
        }



        ////public ActionResult SortSongsOnGenre()
        ////{
        ////    var songesObjDbSet = db.SongesObjDbSet.Include(s => s.Genre);
        ////    return View(songesObjDbSet.ToList());
        ////}

        //// GET: GenreForMenu/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Songs songs = db.SongesObjDbSet.Find(id);
        //    if (songs == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(songs);
        //}

        //// GET: GenreForMenu/Create
        //public ActionResult Create()
        //{
        //    ViewBag.GenreId = new SelectList(db.GenresObjDbSet, "Id", "Name");
        //    return View();
        //}

        //// POST: GenreForMenu/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,FileNameAndType,UrlRelativeAdress,UrlAbsoluteAdress,GenreId")] Songs songs)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SongesObjDbSet.Add(songs);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.GenreId = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.GenreId);
        //    return View(songs);
        //}

        //// GET: GenreForMenu/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Songs songs = db.SongesObjDbSet.Find(id);
        //    if (songs == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.GenreId = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.GenreId);
        //    return View(songs);
        //}

        //// POST: GenreForMenu/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,FileNameAndType,UrlRelativeAdress,UrlAbsoluteAdress,GenreId")] Songs songs)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(songs).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.GenreId = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.GenreId);
        //    return View(songs);
        //}

        //// GET: GenreForMenu/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Songs songs = db.SongesObjDbSet.Find(id);
        //    if (songs == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(songs);
        //}

        //// POST: GenreForMenu/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Songs songs = db.SongesObjDbSet.Find(id);
        //    db.SongesObjDbSet.Remove(songs);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
