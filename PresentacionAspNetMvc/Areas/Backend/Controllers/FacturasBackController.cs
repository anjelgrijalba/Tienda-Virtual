using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Areas.Backend.Controllers
{
    public class FacturasBackController : Controller
    {
        // GET: Backend/FacturasBack
        public ActionResult Index()
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IEnumerable<IFactura> listadoFacturas = ln.ListarFacturas();

            return View(listadoFacturas);
        }

        // GET: Backend/FacturasBack/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Backend/FacturasBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backend/FacturasBack/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Backend/FacturasBack/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Backend/FacturasBack/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Backend/FacturasBack/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Backend/FacturasBack/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
