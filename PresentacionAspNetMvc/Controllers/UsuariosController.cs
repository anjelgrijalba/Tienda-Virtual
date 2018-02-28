using PresentacionAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionAspNetMvc.Controllers
{
    public class UsuariosController : Controller
    {


        // GET:Usuarios/Login
        public ActionResult Login()
        {
           return View();
        }

        // POST: GET:Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Id,Nick,Password")] Usuario usuario)
        {
            ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
            try
            {
                if (!ln.ExisteNick(usuario.Nick))
                {
                    ViewBag.ErrorNick = "El Nick no está registrado";
                    return View(usuario);
                }
                else if (!ln.PasswordCorrecto(usuario.Nick, usuario.Password))
                {
                    ViewBag.ErrorPass = "La contraseña no es correcta";
                    return View(usuario);
                }
                else
                {
                    //string cadena = HttpContext.Session["origenYdestino"].ToString();

                    IUsuario usuarioCompleto = ln.ValidarUsuarioYDevolverUsuario(usuario.Nick, usuario.Password);

                    HttpContext.Session["usuario"] = usuarioCompleto;

                    ((ICarrito)HttpContext.Session["carrito"]).Usuario = usuarioCompleto;
                   
                    ViewBag.ErrorNick("Bienvenid@", usuarioCompleto.Nick);

                   

                    return RedirectToAction("Index","Productos");
                }
            }
            catch
            {
                return View(usuario);
            }
        }


        // GET: Usuarios/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Backend/UsuariosBack/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Nick,Password,Id")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    if (ln.ExisteNick(usuario.Nick))
                    {
                        ViewBag.Error = "Ya existe un usuario con ese nick";
                        return View(usuario);
                    }
                    else
                    {
                        ln.AltaUsuario(usuario);
                        HttpContext.Session["usuario"] = usuario;

                        ((ICarrito)HttpContext.Session["carrito"]).Usuario = usuario;
                        //Session["cantidadCarrito"] = 0;
                        HttpContext.Session["mostrarModal"] = true;
                        ViewBag.Error = "Usuario Creado Correctamente";
                        //return Redirect("/");
                        HttpContext.Session["carrito"] = new Carrito(null);
                        Session["cantidadCarrito"] = 0;

                        return View(usuario);
                    }
                }
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }


        // GET: Usuarios/Register
        public ActionResult Logout()
        {
            return View();
        }
        // POST: Usuarios/Logout
        
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public ActionResult LogoutConfirmed()
        {
            try
            {
                
                HttpContext.Session["usuario"] = null;

                HttpContext.Session["carrito"] = new Carrito(null);
                Session["cantidadCarrito"] = 0;

                return Redirect("/");

            }
            catch
            {
                return View();
            }
        }






    }
}




