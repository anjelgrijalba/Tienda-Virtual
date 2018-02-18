using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult Index()
        {
            ICarrito carrito = (ICarrito)HttpContext.Session["carrito"];
            //return View(carrito);
            return View(carrito);
        }


        public ActionResult AgregarProducto(int id, int cantidad)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            ICarrito carrito = (ICarrito)HttpContext.Session["carrito"];
            
            IProducto producto = ln.BuscarProductoPorId(id);

            ln.AgregarProductoACarrito(producto, cantidad, carrito);
            
            HttpContext.Session["cantidadCarrito"] = (int)HttpContext.Session["cantidadCarrito"] + cantidad;
            //return View("Index", carrito);
            //return RedirectToAction ("~/Carrito");
            return RedirectToAction("Index");
        }

        public ActionResult GenerarFactura()
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            ICarrito carrito = (ICarrito)HttpContext.Session["carrito"];
            IFactura factura = ln.FacturarCarrito(carrito);
            HttpContext.Session["factura"] = factura;
            return View("Factura", factura);

        }
    }
}