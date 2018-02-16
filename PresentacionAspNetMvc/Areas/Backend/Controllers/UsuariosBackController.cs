using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Areas.Backend.Controllers
{
    public class UsuariosBackController : Controller
    {
        // GET: Backend/UsuariosBack
        public ActionResult Index()
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IEnumerable<IUsuario> listadoUsuarios = ln.BuscarTodosUsuarios();

            return View(listadoUsuarios);
        }

        // GET: Backend/UsuariosBack/Details/5
        public ActionResult Details(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IUsuario usuario = ln.BuscarUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Backend/UsuariosBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backend/UsuariosBack/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nick,Password")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    ln.AltaUsuario(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario);

            }
            catch
            {
                return View();
            }
        }

        // GET: Backend/UsuariosBack/Edit/5
        public ActionResult Edit(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IUsuario usuario = ln.BuscarUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Backend/UsuariosBack/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, FormCollection collection)
        public ActionResult Edit([Bind(Include = "Id,Nick,Password")] Usuario usuario)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    
            try
            {
                ln.ModificarUsuario(usuario);
              
                return RedirectToAction("Index");
            }
            catch
            {
                return View(usuario);
            }
        }

        // GET: Backend/UsuariosBack/Delete/5
        public ActionResult Delete(int id)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IUsuario usuario = ln.BuscarUsuarioPorId(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Backend/UsuariosBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IUsuario usuario = ln.BuscarUsuarioPorId(id);
            try
            {
                // TODO: Add delete logic here
               
                ln.BajaUsuario(usuario);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(usuario);
            }
        }
    }
}
