﻿using PresentacionAspNetMvc.Models;
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
                IUsuario usuarioCompleto = ln.ValidarUsuarioYDevolverUsuario(usuario.Nick, usuario.Password);

                HttpContext.Session["usuario"] = usuarioCompleto;

                ((ICarrito)HttpContext.Session["carrito"]).Usuario = usuarioCompleto;

                return Redirect("/");
                //return RedirectToAction("Index");
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
        public ActionResult Register([Bind(Include = "Id,Nick,Password")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ILogicaNegocio ln = (ILogicaNegocio)HttpContext.Application["logicaNegocio"];
                    ln.AltaUsuario(usuario);
                    HttpContext.Session["usuario"] = usuario;

                    ((ICarrito)HttpContext.Session["carrito"]).Usuario = usuario;
                    //HttpContext.Session["mostrarModal"] = true;

                    return Redirect("/");
                    //return View(usuario);

                }
                return View(usuario);
            }
            catch
            {
                return View();
            }
        }



        
        

    }
}




