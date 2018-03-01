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
            IUsuario usuario = (IUsuario)HttpContext.Session["usuario"];

            if (usuario == null)
            {
                Session["Controlador"] = "Carrito";
                Session["Vista"] = "GenerarFactura";
                return RedirectToAction("Login", "Usuarios");
            }
            else
            {
                ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

                ICarrito carrito = (ICarrito)HttpContext.Session["carrito"];

                string numeroFactura = ln.GenerarNumero();

                IFactura factura = ln.FacturarCarrito(carrito, numeroFactura);

                ln.AltaFactura(factura);

                int idFactura = ln.GetIdFactura(numeroFactura);

                ln.AltaLineas(factura, idFactura);

                HttpContext.Session["factura"] = factura;

                return View("Factura", factura);
            }
        }

        public ActionResult Vaciar()
        {
            Session["cantidadCarrito"] = 0;
            Session["carrito"] = new Carrito(null);
            return RedirectToAction("Index", "Productos");
        }        
    }
}