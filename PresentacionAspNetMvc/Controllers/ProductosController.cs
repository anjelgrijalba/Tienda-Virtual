using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            Session["Controlador"] = "Productos";
            Session["Vista"] = "Index";
            var ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
            return View(ln.ListadoProductos());
        }

        [HttpGet]
        public ActionResult Ficha(int id)
        {
            Session["Controlador"] = "Productos";
            Session["Vista"] = "Index";
            var ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
            return View(ln.BuscarProductoPorId(id));
        }

        [HttpPost]
        public string Ficha(int id, int cantidad)
        {
            return $"Los datos recibidos son {id} {cantidad}";
        }
    }
}