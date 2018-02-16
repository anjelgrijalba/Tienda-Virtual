using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.LogicaNegocio;

namespace PresentacionWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ln = (ILogicaNegocio)Application["logicaNegocio"];

            rProductos.DataSource = ln.ListadoProductos();

            rProductos.DataBind();
        }
    }
}