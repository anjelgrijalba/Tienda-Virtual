using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TiendaVirtual.LogicaNegocio;
using TiendaVirtual.Entidades;

namespace PresentacionWebForms
{
    public partial class FichaAnterior : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ln = (LogicaNegocio)Application["logicaNegocio"];

            int id = int.Parse(Request["id"]);

            //Request.QueryString["id"] //GET
            //Request.Form["id"] //POST

            IProducto p = ln.BuscarProductoPorId(id);

            lblId.Text = p.Id.ToString();
            lblNombre.Text = p.Nombre;
            lblPrecio.Text = p.Precio.ToString();

            imgProducto.ImageUrl = string.Format("~/fotos/{0}.png", id);
        }
    }
}