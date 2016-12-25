using MusicPortal_2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace MusicPortal_2016.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        // создаем контекст данных
        Context db = new Context();

        public ActionResult Index()
        {
            ViewBag.genreName = db.GenresObjDbSet.ToList();
            var g = db.SongesObjDbSet.ToList();
            return View(g);
        }
        public ActionResult IndexAdmin()//переходит на страницу админа
        {
            using (Context dbContext = new Context())
            {
                return View(dbContext.UsersObjDbSet.ToList());
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Users accountUsers)
        {
            if (ModelState.IsValid)
            {
                using (Context dbContext = new Context())
                {
                    //хэшируем пароль + добавляем соль
                    byte[] saltbuf = new byte[16];
                    // Реализует криптографический генератор случайных чисел, используя реализацию, предоставляемую поставщиком служб шифрования (CSP). 
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetBytes(saltbuf);

                    StringBuilder sb = new StringBuilder(16);
                    for (int i = 0; i < 16; i++)
                        sb.Append(string.Format("{0:X2}", saltbuf[i]));
                    string salt = sb.ToString();

                    // Формирует хэшированный пароль, подходящий для хранения в файле конфигурации, в зависимости от указанного пароля и алгоритма хэширования.
                    string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(
                                        salt + accountUsers.Password /* Пароль для хэширования */,
                                        "MD5" /* Используемый хэш - алгоритм */);
                    // accountUsers.IsApproved = false;
                    accountUsers.Password = hash;
                    accountUsers.Solt = salt;
                    accountUsers.ConfirmPassword = hash;
                    //записываем в БД
                    dbContext.UsersObjDbSet.Add(accountUsers);
                    dbContext.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = accountUsers.FirstName + " " + accountUsers.LastName +
                                  " успешно зарегистрирован. Ожидайте подтверждения от администратора.";
            }
            return View();
        }
        public ActionResult UserList()//у админа выдает список всех пользователей
        {
            var users = db.UsersObjDbSet.ToList();
            return View(users);
        }

        public ActionResult EditOneUser(int? id)
        {
            return View();
        }
        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            using (Context dbContext = new Context())
            {
                var ListAllUsers = dbContext.UsersObjDbSet.ToList();
                foreach (var p in ListAllUsers)
                {
                    if (user.Login == p.Login)
                    {
                        string salt = p.Solt;
                        // Формирует хэшированный пароль, подходящий для хранения в файле конфигурации, в зависимости от указанного пароля и алгоритма хэширования.
                        /* Пароль для хэширования */ /*Используемый хэш - алгоритм */
                        string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(salt + user.Password, "MD5");
                        string pass = p.Password;
                        if (pass != hash)
                        {
                            ModelState.AddModelError("", "Логин или пароль введены не верно!");
                            break;

                        }
                        if (user.Login.ToString() == "admin" )
                        {
                            Session["Id"] = p.Id.ToString();
                            Session["Login"] = p.Login.ToString();

                            return RedirectToAction("IndexAdmin");
                        }
                       else if(user.Login.ToString() != "admin")
                        {
                            Session["Id"] = p.Id.ToString();
                            Session["Login"] = p.Login.ToString();
                            return RedirectToAction("LoggedIn");
                        }
                    }
                }

                }

                return View();
            }

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
        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                ViewBag.genreName = db.GenresObjDbSet.ToList();
                var g = db.SongesObjDbSet.ToList();
                ViewBag.thisSessionLogin = Session["Login"];
                //var g = db.SongesObjDbSet.Include(s => s.Genre).ToList();
                return View(g);
                //return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

       
     
    }
}