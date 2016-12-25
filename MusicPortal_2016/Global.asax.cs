    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MusicPortal_2016
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //обработчик события запуска веб-приложения
        //сработает до того как будет обработан первый запрос
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //вызываем этот метод на стат-ом классе и в качестве параметра передаем RouteTable.Routes - таблицу маршрутизации
            //в ней хранятся все маршруты, необходимые для работы нашего приложения
            //RouteConfig - класс отдельно определен в App_Start ->RouteConfig.cs
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
