using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string cadenaConexion =
              System.Configuration.ConfigurationManager.
               ConnectionStrings["TiendaVirtual"].
               ConnectionString;
            //string cadenaConexion =
            //  System.Configuration.ConfigurationManager.
            //   ConnectionStrings["UsuarioDbContext"].
            //   ConnectionString;

            string tipo = System.Configuration.ConfigurationManager.AppSettings["motorDao"];

            Application["logicaNegocio"] = new LogicaNegocio(tipo, cadenaConexion);
        }

        protected void Session_Start()
        {
            Session["carrito"] = new Carrito(null);
            Session["mostrarModal"] = false;
            
        }
    }
}
