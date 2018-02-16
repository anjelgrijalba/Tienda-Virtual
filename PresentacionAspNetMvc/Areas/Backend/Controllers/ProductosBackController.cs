using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Areas.Backend.Controllers
{
    public class ProductosBackController : Controller
    {
        // GET: Backend/ProductosBack
        public ActionResult Index()
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IEnumerable<IProducto> listadoProductos = ln.ListadoProductos();
           
            return View(listadoProductos);
            
        }

        // GET: Backend/ProductosBack/Details/5
        public ActionResult Details(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IProducto producto = ln.BuscarProductoPorId(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Backend/ProductosBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backend/ProductosBack/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Precio")] Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    ln.AltaProducto(producto);
                    return RedirectToAction("Index");
                }
                return View(producto);
            }
            catch
            {
                return View();
            }
        }

        // GET: Backend/ProductosBack/Edit/5
        public ActionResult Edit(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IProducto producto = ln.BuscarProductoPorId(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Backend/ProductosBack/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio")] Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    ln.ModificarProducto(producto);
                    return RedirectToAction("Index");
                }

                return View(producto);

            }
            catch
            {
                return View();
            }
        }

        // GET: Backend/ProductosBack/Delete/5
        public ActionResult Delete(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IProducto producto = ln.BuscarProductoPorId(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Backend/ProductosBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IProducto producto = ln.BuscarProductoPorId(id);
            try
            {
                // TODO: Add delete logic here

                ln.BajaProducto(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(producto);
            }
        }

        public ActionResult Inicio()
        {
            return View();
        }
    }
}
