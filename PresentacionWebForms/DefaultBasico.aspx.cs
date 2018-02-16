using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms
{
    public partial class DefaultBasico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ln = (LogicaNegocio)Application["logicaNegocio"];
            //string usuario = (string)Session["usuario"];
            //
            //if (usuario == null)
            //{
            //    Response.Redirect("Login.aspx");
            //    return;
            //}
            //
            //lblUsuario.Text = usuario;

            gvProductos.DataSource = ln.ListadoProductos();
            gvProductos.DataBind();

            gvProductos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}