using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicPortal_2016
{
    public class RouteConfig
    {
        //Этот м-д запускаем в Global.asax
        //Тут мы производим настройку маршрутов
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Регистрация маршрута {controller}/{action}/{id} (добавляем в таблицу маршрутизации новый маршрут)
            //После запуска приложения сделайте запрос по адресу
            routes.MapRoute(
                name: "Default",//имя маршрута
                url: "{controller}/{action}/{id}",//переменные сегментов
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }//значения по умолчанию
            );
            routes.MapRoute(
              name: "CreateTrack",//имя маршрута
              url: "{controller}/{action}/{id}",//переменные сегментов
              defaults: new { controller = "Songs", action = "Create", id = UrlParameter.Optional }//значения по умолчанию
          );
        }
    }
}
