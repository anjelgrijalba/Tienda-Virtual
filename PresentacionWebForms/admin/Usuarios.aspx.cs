using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms.admin
{
    public partial class Usuarios : System.Web.UI.Page
    {
        ILogicaNegocio ln;

        protected void Page_Load(object sender, EventArgs e)
        {
            ln = (ILogicaNegocio)Application["logicaNegocio"];

            rUsuarios.DataSource = ln.BuscarTodosUsuarios();
            rUsuarios.DataBind();
        }
    }
}