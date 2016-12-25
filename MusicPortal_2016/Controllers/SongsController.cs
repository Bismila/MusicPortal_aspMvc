using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using MusicPortal_2016.Models;

namespace MusicPortal_2016.Controllers
{
    public class SongsController : Controller
    {
        private Context db = new Context();

        // GET: Songs
        public ActionResult Index()
        {
            ViewBag.genreName = db.GenresObjDbSet.ToList();
             var g = db.SongesObjDbSet.Include(s => s.Genre).ToList();
            return View(g);

        }
        [HttpGet]
        public FileResult Download(int? id)
        {

            if (id == null)
            {
                ViewBag.Error = "Файл не найден!";
            }

            var item = db.SongesObjDbSet.Find(id);
            var pathRel = item.UrlAbsoluteAdress;
            var fileName = item.FileNameAndType;

            byte[] fileBytes = System.IO.File.ReadAllBytes(pathRel);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Songs songs = db.SongesObjDbSet.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            return View(songs);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            // Формируем список жанров для передачи в представление
            ViewBag.ListGenre = new SelectList(db.GenresObjDbSet, "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        /* [ValidateAntiForgeryToken]*/ //[Bind(Include = "Id,Name,UrlAdress,GenderId")]
        public ActionResult Create(Songs songs, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    //Загрузка файла в проект
                    string filename = Path.GetFileName(file.FileName);
                    string tempfolder = Server.MapPath("~/UploadSongs");
                    string pathAbsol = Path.Combine(Server.MapPath("~/UploadSongs"), filename);
                    string pathRel = "/UploadSongs/" + filename;
                    if (filename != null)
                    {
                        file.SaveAs(Path.Combine(tempfolder, filename));
                    }
                    songs.UrlRelativeAdress = pathRel; //записали относительный адрес трэка
                    songs.UrlAbsoluteAdress = pathAbsol;
                    songs.FileNameAndType = filename;
                    db.SongesObjDbSet.Add(songs);//добавили трэк в БД
                    db.SaveChanges();
                    if (Session["Login"].ToString() == "admin")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("LoggedIn", "Users");
                    }
                }
                else
                {
                    ViewBag.Message = "Вы не указали файл";
                }

            }
            //else
            //{
            //    ViewBag.Message = "Заполните все поля";
            //    return View();
            //}
            ViewBag.ListGenre = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.Genre.Id);
            return View(songs);

        }


        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Songs songs = db.SongesObjDbSet.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListGenre = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.GenreId);
            return View(songs);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Songs songs)//[Bind(Include = "Id,Name,UrlAdress,IdGender")]
        {
            if (ModelState.IsValid)
            {
                db.Entry(songs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListGenre = new SelectList(db.GenresObjDbSet, "Id", "Name", songs.GenreId);
            return View(songs);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Songs songs = db.SongesObjDbSet.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            return View(songs);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Songs songs = db.SongesObjDbSet.Find(id);
            db.SongesObjDbSet.Remove(songs);
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
