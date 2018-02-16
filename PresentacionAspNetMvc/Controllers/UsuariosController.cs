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
        public ActionResult Login(Usuario usuario)
        
        {
            var ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];

            IUsuario usuarioCompleto = ln.ValidarUsuarioYDevolverUsuario(
                usuario.Nick, usuario.Password);
            if (usuario == null)
                Console.WriteLine("usuario incorrecto");
            else
                Console.WriteLine("usuario correcto");

            HttpContext.Session["usuario"] = usuarioCompleto;

            ((ICarrito)HttpContext.Session["carrito"]).Usuario = usuarioCompleto;


            return Redirect("/");
        }

    }
}