using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms
{
    public partial class TiendaVirtual : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            // Application = diccionario global para toda la aplicación
            // Creado dentro de Global.asax en Application_Start,
            // donde en ese diccionario hemos creado la clave
            // logicaNegocio

            // Application["logicaNegocio"] = contiene la lógica de
            // negocio instanciada. La pega es que se recibe como tipo
            // object.

            // (ILogicaNegocio)Application["logicaNegocio"] = cambiamos
            // el objeto que en un principio sale como object al tipo
            // ILogicaNegocio

            // Lo guardamos en ln

            var ln = (ILogicaNegocio)Application["logicaNegocio"];

            if (Page.IsValid)
            {
                IUsuario usuario = ln.ValidarUsuarioYDevolverUsuario(
                    txtUsuario.Text, txtPassword.Text);

                if (usuario == null)
                    lblResultado.Text = "Usuario o contraseña incorrectos";
                else
                {
                    Session["usuario"] = usuario.Nick;
                    ICarrito carrito = new global::TiendaVirtual.Entidades.Carrito(usuario);
                    Session["carrito"] = carrito;

                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}