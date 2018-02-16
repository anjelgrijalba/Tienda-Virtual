using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.Entidades;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms.admin
{
    public partial class Usuario : System.Web.UI.Page
    {
        ILogicaNegocio ln;
        string op;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            ln = (ILogicaNegocio)Application["logicaNegocio"];

            op = Request["op"];

            if (Request["id"] != null)
                id = int.Parse(Request["id"]);

            if (!IsPostBack && op != null)
            {
                if (op == "editar" || op == "borrar")
                {
                    IUsuario usuario = ln.BuscarUsuarioPorId(id);

                    txtUsuario.Text = usuario.Nick;
                    txtPassword.Text = usuario.Password;
                }

                switch (op)
                {
                    case "editar":
                        txtUsuario.Enabled = true;
                        txtPassword.Enabled = true;

                        break;
                    case "borrar":
                        txtUsuario.Enabled = false;
                        txtPassword.Enabled = false;

                        btn.Text = "Borrar";
                        btn.CssClass = btn.CssClass + " btn-danger";

                        break;
                    case "alta":
                        break;
                    default:
                        break;
                }

            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            switch (op)
            {
                case "alta":
                    ln.AltaUsuario(new global::TiendaVirtual.Entidades.Usuario(id, txtUsuario.Text, txtPassword.Text));
                    break;
                case "editar":
                    ln.ModificarUsuario(new global::TiendaVirtual.Entidades.Usuario(id, txtUsuario.Text, txtPassword.Text));
                    break;
                case "borrar":
                    ln.BajaUsuario(id);
                    break;
                default:
                    break;
            }

            Response.Redirect("Usuarios.aspx");
        }
    }
}