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
    public partial class Ficha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ln = (LogicaNegocio)Application["logicaNegocio"];

            int id = int.Parse(Request["id"]);

            //Request.QueryString["id"] //GET
            //Request.Form["id"] //POST

            IProducto p = ln.BuscarProductoPorId(id);

            //lblId.Text = p.Id.ToString();
            lblNombre.Text = p.Nombre;
            lblPrecio.Text = string.Format("{0:c}", p.Precio);
            lblPrecio.Attributes["data-importe"] = p.Precio.ToString();
            lblTotal.Text = lblPrecio.Text;
            lblTotal.Attributes["data-importe"] = p.Precio.ToString();

            imgProducto.ImageUrl = string.Format("~/fotos/{0}.png", id);

            btnCarrito.CommandArgument = p.Id.ToString();
        }

        protected void btnCarrito_Command(object sender, CommandEventArgs e)
        {
            int cantidad, id;

            id =int.Parse(e.CommandArgument.ToString());
            cantidad = int.Parse(this.cantidad.Text);

            Response.Redirect("Carrito.aspx?id=" + id + "&cantidad=" + cantidad);
        }

    }
}